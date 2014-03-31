namespace TheGame
{
    partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.timerFirstStart = new System.Windows.Forms.Timer(this.components);
			this.timerTact = new System.Windows.Forms.Timer(this.components);
			this.timerRestart = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timerFirstStart
			// 
			this.timerFirstStart.Enabled = true;
			this.timerFirstStart.Interval = 20;
			this.timerFirstStart.Tick += new System.EventHandler(this.timerFirstStart_Tick);
			// 
			// timerTact
			// 
			this.timerTact.Interval = 15;
			this.timerTact.Tick += new System.EventHandler(this.timerTact_Tick);
			// 
			// timerRestart
			// 
			this.timerRestart.Interval = 1000;
			this.timerRestart.Tick += new System.EventHandler(this.timerRestart_Tick);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 575);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Eternal Black Space";
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerFirstStart;
        private System.Windows.Forms.Timer timerTact;
        private System.Windows.Forms.Timer timerRestart;

    }
}

