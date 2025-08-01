namespace HypoxicTimer
{
    partial class HypoxicTimer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HypoxicTimer));
            this.Start = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.TheTimeLeft = new System.Windows.Forms.Label();
            this.Period = new System.Windows.Forms.Label();
            this.TotalHypoxia = new System.Windows.Forms.Label();
            this.testModeBox = new System.Windows.Forms.CheckBox();
            this.FinishTime = new System.Windows.Forms.Label();
            this.finishTimer = new System.Windows.Forms.Timer(this.components);
            this.oximeterBox = new System.Windows.Forms.CheckBox();
            this.SpO2Label = new System.Windows.Forms.Label();
            this.oximeterTimer = new System.Windows.Forms.Timer(this.components);
            this.HypoxicTrainingIndex = new System.Windows.Forms.Label();
            this.Debug = new System.Windows.Forms.Button();
            this.OxPlotSurface2D = new NPlot.Windows.PlotSurface2D();
            this.pulsePlotSurface2D = new NPlot.Windows.PlotSurface2D();
            this.OverviewPlotSurface2D1 = new NPlot.Windows.PlotSurface2D();
            this.distributionPlotSurface2D1 = new NPlot.Windows.PlotSurface2D();
            this.OptionsButton = new System.Windows.Forms.Button();
            this.ReplayFileButtong = new System.Windows.Forms.Button();
            this.replayOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DiaryButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.ProfilesFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.goalPlotSurface2D1 = new NPlot.Windows.PlotSurface2D();
            this.ReadButton = new System.Windows.Forms.Button();
            this.readCsvFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.AddToDiaryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(216, 10);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(297, 10);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(75, 23);
            this.Pause.TabIndex = 2;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // mainTimer
            // 
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // TheTimeLeft
            // 
            this.TheTimeLeft.AutoSize = true;
            this.TheTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TheTimeLeft.ForeColor = System.Drawing.Color.Red;
            this.TheTimeLeft.Location = new System.Drawing.Point(4, 138);
            this.TheTimeLeft.Name = "TheTimeLeft";
            this.TheTimeLeft.Size = new System.Drawing.Size(194, 73);
            this.TheTimeLeft.TabIndex = 6;
            this.TheTimeLeft.Text = "00:00";
            // 
            // Period
            // 
            this.Period.AutoSize = true;
            this.Period.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Period.ForeColor = System.Drawing.Color.Red;
            this.Period.Location = new System.Drawing.Point(13, 211);
            this.Period.Name = "Period";
            this.Period.Size = new System.Drawing.Size(121, 33);
            this.Period.TabIndex = 7;
            this.Period.Text = "Hypoxia";
            // 
            // TotalHypoxia
            // 
            this.TotalHypoxia.AutoSize = true;
            this.TotalHypoxia.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalHypoxia.Location = new System.Drawing.Point(13, 256);
            this.TotalHypoxia.Name = "TotalHypoxia";
            this.TotalHypoxia.Size = new System.Drawing.Size(481, 33);
            this.TotalHypoxia.TabIndex = 8;
            this.TotalHypoxia.Text = "Total 00:00:00 Hypoxic 00:00:00 1/6";
            // 
            // testModeBox
            // 
            this.testModeBox.AutoSize = true;
            this.testModeBox.Location = new System.Drawing.Point(941, 10);
            this.testModeBox.Name = "testModeBox";
            this.testModeBox.Size = new System.Drawing.Size(77, 17);
            this.testModeBox.TabIndex = 9;
            this.testModeBox.Text = "Test Mode";
            this.testModeBox.UseVisualStyleBackColor = true;
            // 
            // FinishTime
            // 
            this.FinishTime.AutoSize = true;
            this.FinishTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinishTime.Location = new System.Drawing.Point(13, 303);
            this.FinishTime.Name = "FinishTime";
            this.FinishTime.Size = new System.Drawing.Size(386, 33);
            this.FinishTime.TabIndex = 12;
            this.FinishTime.Text = "End at 1:23:44 Left: 01:23:45";
            // 
            // finishTimer
            // 
            this.finishTimer.Tick += new System.EventHandler(this.finishTimer_Tick);
            // 
            // oximeterBox
            // 
            this.oximeterBox.AutoSize = true;
            this.oximeterBox.ForeColor = System.Drawing.Color.Blue;
            this.oximeterBox.Location = new System.Drawing.Point(93, 13);
            this.oximeterBox.Name = "oximeterBox";
            this.oximeterBox.Size = new System.Drawing.Size(89, 17);
            this.oximeterBox.TabIndex = 14;
            this.oximeterBox.Text = "Use Oximeter";
            this.oximeterBox.UseVisualStyleBackColor = true;
            this.oximeterBox.CheckedChanged += new System.EventHandler(this.oximeterBox_CheckedChanged);
            // 
            // SpO2Label
            // 
            this.SpO2Label.AutoSize = true;
            this.SpO2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpO2Label.ForeColor = System.Drawing.Color.Red;
            this.SpO2Label.Location = new System.Drawing.Point(6, 33);
            this.SpO2Label.Name = "SpO2Label";
            this.SpO2Label.Size = new System.Drawing.Size(250, 73);
            this.SpO2Label.TabIndex = 15;
            this.SpO2Label.Text = "00% 99";
            // 
            // oximeterTimer
            // 
            this.oximeterTimer.Interval = 10;
            this.oximeterTimer.Tick += new System.EventHandler(this.oximeterTimer_Tick);
            // 
            // HypoxicTrainingIndex
            // 
            this.HypoxicTrainingIndex.AutoSize = true;
            this.HypoxicTrainingIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HypoxicTrainingIndex.ForeColor = System.Drawing.Color.Black;
            this.HypoxicTrainingIndex.Location = new System.Drawing.Point(13, 101);
            this.HypoxicTrainingIndex.Name = "HypoxicTrainingIndex";
            this.HypoxicTrainingIndex.Size = new System.Drawing.Size(215, 33);
            this.HypoxicTrainingIndex.TabIndex = 16;
            this.HypoxicTrainingIndex.Text = "HTi 0 %/min [0]";
            // 
            // Debug
            // 
            this.Debug.Location = new System.Drawing.Point(1024, 6);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(75, 23);
            this.Debug.TabIndex = 17;
            this.Debug.Text = "Debug";
            this.Debug.UseVisualStyleBackColor = true;
            this.Debug.Click += new System.EventHandler(this.Debug_Click);
            // 
            // OxPlotSurface2D
            // 
            this.OxPlotSurface2D.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OxPlotSurface2D.AutoScaleAutoGeneratedAxes = false;
            this.OxPlotSurface2D.AutoScaleTitle = false;
            this.OxPlotSurface2D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.OxPlotSurface2D.DateTimeToolTip = false;
            this.OxPlotSurface2D.Legend = null;
            this.OxPlotSurface2D.LegendZOrder = -1;
            this.OxPlotSurface2D.Location = new System.Drawing.Point(525, 37);
            this.OxPlotSurface2D.Name = "OxPlotSurface2D";
            this.OxPlotSurface2D.RightMenu = null;
            this.OxPlotSurface2D.ShowCoordinates = false;
            this.OxPlotSurface2D.Size = new System.Drawing.Size(713, 388);
            this.OxPlotSurface2D.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.OxPlotSurface2D.TabIndex = 18;
            this.OxPlotSurface2D.Text = "plotSurface2D1";
            this.OxPlotSurface2D.Title = "";
            this.OxPlotSurface2D.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.OxPlotSurface2D.XAxis1 = null;
            this.OxPlotSurface2D.XAxis2 = null;
            this.OxPlotSurface2D.YAxis1 = null;
            this.OxPlotSurface2D.YAxis2 = null;
            // 
            // pulsePlotSurface2D
            // 
            this.pulsePlotSurface2D.AutoScaleAutoGeneratedAxes = false;
            this.pulsePlotSurface2D.AutoScaleTitle = false;
            this.pulsePlotSurface2D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pulsePlotSurface2D.DateTimeToolTip = false;
            this.pulsePlotSurface2D.Legend = null;
            this.pulsePlotSurface2D.LegendZOrder = -1;
            this.pulsePlotSurface2D.Location = new System.Drawing.Point(2, 339);
            this.pulsePlotSurface2D.Name = "pulsePlotSurface2D";
            this.pulsePlotSurface2D.RightMenu = null;
            this.pulsePlotSurface2D.ShowCoordinates = false;
            this.pulsePlotSurface2D.Size = new System.Drawing.Size(517, 86);
            this.pulsePlotSurface2D.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.pulsePlotSurface2D.TabIndex = 19;
            this.pulsePlotSurface2D.Text = "plotSurface2D1";
            this.pulsePlotSurface2D.Title = "";
            this.pulsePlotSurface2D.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.pulsePlotSurface2D.XAxis1 = null;
            this.pulsePlotSurface2D.XAxis2 = null;
            this.pulsePlotSurface2D.YAxis1 = null;
            this.pulsePlotSurface2D.YAxis2 = null;
            // 
            // OverviewPlotSurface2D1
            // 
            this.OverviewPlotSurface2D1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OverviewPlotSurface2D1.AutoScaleAutoGeneratedAxes = false;
            this.OverviewPlotSurface2D1.AutoScaleTitle = false;
            this.OverviewPlotSurface2D1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.OverviewPlotSurface2D1.DateTimeToolTip = false;
            this.OverviewPlotSurface2D1.Legend = null;
            this.OverviewPlotSurface2D1.LegendZOrder = -1;
            this.OverviewPlotSurface2D1.Location = new System.Drawing.Point(2, 431);
            this.OverviewPlotSurface2D1.Name = "OverviewPlotSurface2D1";
            this.OverviewPlotSurface2D1.RightMenu = null;
            this.OverviewPlotSurface2D1.ShowCoordinates = false;
            this.OverviewPlotSurface2D1.Size = new System.Drawing.Size(1236, 187);
            this.OverviewPlotSurface2D1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.OverviewPlotSurface2D1.TabIndex = 20;
            this.OverviewPlotSurface2D1.Text = "plotSurface2D1";
            this.OverviewPlotSurface2D1.Title = "";
            this.OverviewPlotSurface2D1.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.OverviewPlotSurface2D1.XAxis1 = null;
            this.OverviewPlotSurface2D1.XAxis2 = null;
            this.OverviewPlotSurface2D1.YAxis1 = null;
            this.OverviewPlotSurface2D1.YAxis2 = null;
            // 
            // distributionPlotSurface2D1
            // 
            this.distributionPlotSurface2D1.AutoScaleAutoGeneratedAxes = false;
            this.distributionPlotSurface2D1.AutoScaleTitle = false;
            this.distributionPlotSurface2D1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.distributionPlotSurface2D1.DateTimeToolTip = false;
            this.distributionPlotSurface2D1.Legend = null;
            this.distributionPlotSurface2D1.LegendZOrder = -1;
            this.distributionPlotSurface2D1.Location = new System.Drawing.Point(184, 138);
            this.distributionPlotSurface2D1.Name = "distributionPlotSurface2D1";
            this.distributionPlotSurface2D1.RightMenu = null;
            this.distributionPlotSurface2D1.ShowCoordinates = false;
            this.distributionPlotSurface2D1.Size = new System.Drawing.Size(228, 115);
            this.distributionPlotSurface2D1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.distributionPlotSurface2D1.TabIndex = 23;
            this.distributionPlotSurface2D1.Text = "plotSurface2D1";
            this.distributionPlotSurface2D1.Title = "";
            this.distributionPlotSurface2D1.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.distributionPlotSurface2D1.XAxis1 = null;
            this.distributionPlotSurface2D1.XAxis2 = null;
            this.distributionPlotSurface2D1.YAxis1 = null;
            this.distributionPlotSurface2D1.YAxis2 = null;
            // 
            // OptionsButton
            // 
            this.OptionsButton.Location = new System.Drawing.Point(12, 10);
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(75, 23);
            this.OptionsButton.TabIndex = 1;
            this.OptionsButton.Text = "Options...";
            this.OptionsButton.UseVisualStyleBackColor = true;
            this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // ReplayFileButtong
            // 
            this.ReplayFileButtong.Location = new System.Drawing.Point(606, 10);
            this.ReplayFileButtong.Name = "ReplayFileButtong";
            this.ReplayFileButtong.Size = new System.Drawing.Size(75, 23);
            this.ReplayFileButtong.TabIndex = 25;
            this.ReplayFileButtong.Text = "Replay...";
            this.ReplayFileButtong.UseVisualStyleBackColor = true;
            this.ReplayFileButtong.Click += new System.EventHandler(this.ReplayFileButtong_Click);
            // 
            // replayOpenFileDialog
            // 
            this.replayOpenFileDialog.DefaultExt = "xml";
            this.replayOpenFileDialog.ReadOnlyChecked = true;
            // 
            // DiaryButton
            // 
            this.DiaryButton.Location = new System.Drawing.Point(525, 10);
            this.DiaryButton.Name = "DiaryButton";
            this.DiaryButton.Size = new System.Drawing.Size(75, 23);
            this.DiaryButton.TabIndex = 26;
            this.DiaryButton.Text = "Diary";
            this.DiaryButton.UseVisualStyleBackColor = true;
            this.DiaryButton.Click += new System.EventHandler(this.DiaryButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Location = new System.Drawing.Point(378, 10);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 27;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // goalPlotSurface2D1
            // 
            this.goalPlotSurface2D1.AutoScaleAutoGeneratedAxes = false;
            this.goalPlotSurface2D1.AutoScaleTitle = false;
            this.goalPlotSurface2D1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.goalPlotSurface2D1.DateTimeToolTip = false;
            this.goalPlotSurface2D1.Legend = null;
            this.goalPlotSurface2D1.LegendZOrder = -1;
            this.goalPlotSurface2D1.Location = new System.Drawing.Point(428, 38);
            this.goalPlotSurface2D1.Name = "goalPlotSurface2D1";
            this.goalPlotSurface2D1.RightMenu = null;
            this.goalPlotSurface2D1.ShowCoordinates = false;
            this.goalPlotSurface2D1.Size = new System.Drawing.Size(91, 215);
            this.goalPlotSurface2D1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.goalPlotSurface2D1.TabIndex = 28;
            this.goalPlotSurface2D1.Text = "plotSurface2D1";
            this.goalPlotSurface2D1.Title = "";
            this.goalPlotSurface2D1.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.goalPlotSurface2D1.XAxis1 = null;
            this.goalPlotSurface2D1.XAxis2 = null;
            this.goalPlotSurface2D1.YAxis1 = null;
            this.goalPlotSurface2D1.YAxis2 = null;
            // 
            // ReadButton
            // 
            this.ReadButton.Location = new System.Drawing.Point(687, 10);
            this.ReadButton.Name = "ReadButton";
            this.ReadButton.Size = new System.Drawing.Size(75, 23);
            this.ReadButton.TabIndex = 29;
            this.ReadButton.Text = "Read...";
            this.ReadButton.UseVisualStyleBackColor = true;
            this.ReadButton.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // readCsvFileDialog
            // 
            this.readCsvFileDialog.DefaultExt = "*.csv";
            this.readCsvFileDialog.Filter = "CSV|*.csv|All Files|*.*";
            // 
            // AddToDiaryButton
            // 
            this.AddToDiaryButton.Location = new System.Drawing.Point(769, 10);
            this.AddToDiaryButton.Name = "AddToDiaryButton";
            this.AddToDiaryButton.Size = new System.Drawing.Size(93, 23);
            this.AddToDiaryButton.TabIndex = 30;
            this.AddToDiaryButton.Text = "Add To Diary";
            this.AddToDiaryButton.UseVisualStyleBackColor = true;
            this.AddToDiaryButton.Click += new System.EventHandler(this.AddToDiaryButton_Click);
            // 
            // HypoxicTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 630);
            this.Controls.Add(this.AddToDiaryButton);
            this.Controls.Add(this.ReadButton);
            this.Controls.Add(this.goalPlotSurface2D1);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.DiaryButton);
            this.Controls.Add(this.ReplayFileButtong);
            this.Controls.Add(this.OptionsButton);
            this.Controls.Add(this.distributionPlotSurface2D1);
            this.Controls.Add(this.OverviewPlotSurface2D1);
            this.Controls.Add(this.pulsePlotSurface2D);
            this.Controls.Add(this.OxPlotSurface2D);
            this.Controls.Add(this.Debug);
            this.Controls.Add(this.HypoxicTrainingIndex);
            this.Controls.Add(this.SpO2Label);
            this.Controls.Add(this.oximeterBox);
            this.Controls.Add(this.FinishTime);
            this.Controls.Add(this.testModeBox);
            this.Controls.Add(this.TotalHypoxia);
            this.Controls.Add(this.Period);
            this.Controls.Add(this.TheTimeLeft);
            this.Controls.Add(this.Pause);
            this.Controls.Add(this.Start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HypoxicTimer";
            this.Text = "Hypoxic Timer 1.4";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HypoxicTimer_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Pause;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Label TheTimeLeft;
        private System.Windows.Forms.Label Period;
        private System.Windows.Forms.Label TotalHypoxia;
        private System.Windows.Forms.CheckBox testModeBox;
        private System.Windows.Forms.Label FinishTime;
        private System.Windows.Forms.Timer finishTimer;
        private System.Windows.Forms.CheckBox oximeterBox;
        private System.Windows.Forms.Label SpO2Label;
        private System.Windows.Forms.Timer oximeterTimer;
        private System.Windows.Forms.Label HypoxicTrainingIndex;
        private System.Windows.Forms.Button Debug;
        private NPlot.Windows.PlotSurface2D OxPlotSurface2D;
        private NPlot.Windows.PlotSurface2D pulsePlotSurface2D;
        private NPlot.Windows.PlotSurface2D OverviewPlotSurface2D1;
        private NPlot.Windows.PlotSurface2D distributionPlotSurface2D1;
        private System.Windows.Forms.Button OptionsButton;
        private System.Windows.Forms.Button ReplayFileButtong;
        private System.Windows.Forms.OpenFileDialog replayOpenFileDialog;
        private System.Windows.Forms.Button DiaryButton;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.FolderBrowserDialog ProfilesFolderBrowserDialog;
        private NPlot.Windows.PlotSurface2D goalPlotSurface2D1;
        private System.Windows.Forms.Button ReadButton;
        private System.Windows.Forms.OpenFileDialog readCsvFileDialog;
        private System.Windows.Forms.Button AddToDiaryButton;
    }
}

