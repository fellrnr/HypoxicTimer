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
    /// A Control that can be placed on a form
    /// </summary>
    public class SparklineControl : Control
    {
        public SparklineControl()
        {
            Renderer.WidthChanged += RendererWidthChanged;
        }

        readonly SparklineRenderer _renderer = new SparklineRenderer();

        /// <summary>
        /// Set properties on this object to control the sparkline
        /// </summary>
        public SparklineRenderer Renderer
        {
            get { return _renderer; }
        }

        /// <summary>
        /// Sync our width to that calculated by the renederer (it drives the width)
        /// </summary>
        void RendererWidthChanged(object sender, EventArgs e)
        {
            Width = Renderer.Width;
        }

        /// <summary>
        /// Sync renderer height to our height (we drive the height)
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Renderer.Height = Height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Renderer.Render(e.Graphics, null);
        }
    }


}
