namespace GpxFromStrava
{
    partial class Form1
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
            this.btnGetGpx = new System.Windows.Forms.Button();
            this.txtActivityId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGetGpx
            // 
            this.btnGetGpx.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGetGpx.Location = new System.Drawing.Point(148, 51);
            this.btnGetGpx.Name = "btnGetGpx";
            this.btnGetGpx.Size = new System.Drawing.Size(75, 23);
            this.btnGetGpx.TabIndex = 0;
            this.btnGetGpx.Text = "Get .gpx";
            this.btnGetGpx.UseVisualStyleBackColor = true;
            this.btnGetGpx.Click += new System.EventHandler(this.btnGetGpx_Click);
            // 
            // txtActivityId
            // 
            this.txtActivityId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActivityId.Location = new System.Drawing.Point(110, 10);
            this.txtActivityId.Name = "txtActivityId";
            this.txtActivityId.Size = new System.Drawing.Size(248, 20);
            this.txtActivityId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Strava activity ID:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 101);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtActivityId);
            this.Controls.Add(this.btnGetGpx);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get .gpx from Strava Activity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetGpx;
        private System.Windows.Forms.TextBox txtActivityId;
        private System.Windows.Forms.Label label1;
    }
}

