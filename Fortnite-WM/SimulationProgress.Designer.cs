namespace Fortnite_WM
{
    partial class SimulationProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationProgress));
            this.lb_Progress = new System.Windows.Forms.Label();
            this.pb_SimulationProgress = new System.Windows.Forms.ProgressBar();
            this.btn_Close_Progress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_Progress
            // 
            this.lb_Progress.AutoSize = true;
            this.lb_Progress.Location = new System.Drawing.Point(12, 49);
            this.lb_Progress.Name = "lb_Progress";
            this.lb_Progress.Size = new System.Drawing.Size(35, 13);
            this.lb_Progress.TabIndex = 0;
            this.lb_Progress.Text = "label1";
            // 
            // pb_SimulationProgress
            // 
            this.pb_SimulationProgress.Location = new System.Drawing.Point(12, 12);
            this.pb_SimulationProgress.Name = "pb_SimulationProgress";
            this.pb_SimulationProgress.Size = new System.Drawing.Size(279, 23);
            this.pb_SimulationProgress.Step = 1;
            this.pb_SimulationProgress.TabIndex = 1;
            // 
            // btn_Close_Progress
            // 
            this.btn_Close_Progress.Location = new System.Drawing.Point(216, 44);
            this.btn_Close_Progress.Name = "btn_Close_Progress";
            this.btn_Close_Progress.Size = new System.Drawing.Size(75, 23);
            this.btn_Close_Progress.TabIndex = 2;
            this.btn_Close_Progress.Text = "Schließen";
            this.btn_Close_Progress.UseVisualStyleBackColor = true;
            this.btn_Close_Progress.Click += new System.EventHandler(this.Btn_Close_Progress_Click);
            // 
            // SimulationProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(303, 73);
            this.Controls.Add(this.btn_Close_Progress);
            this.Controls.Add(this.pb_SimulationProgress);
            this.Controls.Add(this.lb_Progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimulationProgress";
            this.Text = "Runden";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_Progress;
        private System.Windows.Forms.ProgressBar pb_SimulationProgress;
        private System.Windows.Forms.Button btn_Close_Progress;
    }
}