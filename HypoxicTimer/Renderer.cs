// Copyright © 2007 John M Rusk (http://dotnet.agilekiwi.com)
// 
// You may use this source code ("The Software") in any manner you wish, 
// subject to the following conditions:
//
// (a) The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
// (b) THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// NOTE: ** this version has a small bug, in which the lowest point of the sparkline does not
// quite touch the bottom of the rendering area.  I.e. the scaling is slightly "off", not 
// quite scaling the sparkline far enough to touch the bottom of the rendering area. **

///<summary>
/// Basic generation of "sparklines" for .net WinForms.  C# 2.0 and later.
/// See http://en.wikipedia.org/wiki/Sparkline for background to sparklines.
///</summary>

namespace HypoxicTimer
{
    /// <summary>
    /// Renders a sparkline to a given <see cref="Graphics"/> object.  Can be used by
    /// <see cref="SparklineColumn"/> and can also be used in other contexts.
    /// For info on sparklines, see http://www.edwardtufte.com/bboard/q-and-a-fetch-msg?msg_id=0001OR 
    /// </summary>
    public class SparklineRenderer
    {
        ICollection<int?> _dataElements = new List<int?>();
        int _pixelsPerElement;
        int _height;
        Color _lineColor = Color.FromArgb(40, 40, 40);
        Color _backgroundColor = Color.White;

        public event EventHandler WidthChanged;

        public class HorizontalBandingColor
        {
            int offsetTo;
            Color lineColor;
            public HorizontalBandingColor(int offsetTo, Color lineColor)
            {
                this.offsetTo = offsetTo;
                this.lineColor = lineColor;
            }
            public int OffsetTo { get { return offsetTo; } }
            public Color LineColor { get { return lineColor; } }
        }

        HorizontalBandingColor[] horizontalBandingColors = null;
        public HorizontalBandingColor[] HorizontalBandingColors
        {
            get { return horizontalBandingColors; }
            set { horizontalBandingColors = value; }
        }

        private Color ColorForValue(int offset, int? value)
        {
            if (horizontalBandingColors != null)
            {
                foreach (HorizontalBandingColor aHorizontalBandingColor in horizontalBandingColors)
                {
                    if (offset < aHorizontalBandingColor.OffsetTo)
                        return aHorizontalBandingColor.LineColor;
                }
            }
            return BackgroundColor;
        }


        /// <summary>
        /// Number of pixels to use for each data element.  Width of control is automatically set to
        /// count of <see cref="DataElements"/> * <see cref="PixelsPerElement"/>
        /// </summary>
        public int PixelsPerElement
        {
            get { return _pixelsPerElement; }
            set
            {
                _pixelsPerElement = value;
                OnWidthChanged();
            }
        }

        /// <summary>
        /// Data displayed by the sparkline.
        /// </summary>
        public ICollection<int?> DataElements
        {
            get { return _dataElements; }
            set
            {
                _dataElements = value;
                OnWidthChanged();
            }
        }

        /// <summary>
        /// Height in pixels. Must be specified by user.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Width in pixels. Automaticall set to count of <see cref="DataElements"/> * <see cref="PixelsPerElement"/>
        /// </summary>
        public int Width
        {
            get { return DataElements != null ? DataElements.Count * PixelsPerElement : PixelsPerElement; }
        }

        void OnWidthChanged()
        {
            if (WidthChanged != null)
                WidthChanged(this, EventArgs.Empty);
        }

        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        /// Draw the sparkline
        /// </summary>
        public void Render(Graphics g, Rectangle? cellBounds)
        {
            if(cellBounds == null)
                g.Clear(BackgroundColor);

            SmoothingMode oldSmoothing = g.SmoothingMode;
            PixelOffsetMode oldOffsetMode = g.PixelOffsetMode;

            try
            {
                // these settings seem to give the best antialiasing
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.Default;

                DoRender(g, cellBounds);
            }
            finally
            {
                g.SmoothingMode = oldSmoothing;
                g.PixelOffsetMode = oldOffsetMode;
            }
        }

        void DoRender(Graphics g, Rectangle? cellBounds)
        {
            int? max;
            int? min;
            GetBounds(out max, out min);
            if (min == null || max == null)
                return; // no range of values to draw

            float range = max.Value - min.Value;
            if (range == 0f)
                range = 1f;
            float multiplier = Height / range;

            Point? last = null;
            int x = 0;
            int i = 0;
            foreach (int? element in DataElements)
            {
                Point? current;
                if (element == null)
                {
                    current = null;
                }
                else
                {
                    int y = (int)((element - min) * multiplier);
                    y = Height - y; // flip y co-ord to standard PC-style coordinates
                    int offsetx = x;
                    int offsety = y;
                    if (cellBounds != null)
                    {
                        offsetx += cellBounds.Value.X;
                        offsety += cellBounds.Value.Y;
                    }
                    current = new Point(offsetx, offsety);
                    Color aColor = ColorForValue(i, element);
                    using (Pen pen = new Pen(aColor, -1))
                    // force single pixel width (see http://www.bobpowell.net/single_pixel_lines.htm )
                    {
                        if (last == null)
                            g.DrawEllipse(pen, current.Value.X, current.Value.Y, 1, 1); // draw "one pixel"
                        else
                            g.DrawLine(pen, last.Value, current.Value);
                    }
                }

                last = current;
                x += PixelsPerElement;
                i++;
            }
        }

        void GetBounds(out int? max, out int? min)
        {
            min = null;
            max = null;
            if (DataElements == null)
                return;
            foreach (int? element in DataElements)
            {
                if (element < (min ?? int.MaxValue))
                    min = element;
                if (element > (max ?? int.MinValue))
                    max = element;
            }
            if (min > 0)
                min = 0;  // in this implementation, we are always tying our sparkline "y axis" to 0 at the bottom, if min is positive
        }
    }
}


