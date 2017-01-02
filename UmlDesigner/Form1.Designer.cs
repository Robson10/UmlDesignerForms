namespace UmlDesigner
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
            this.button1 = new System.Windows.Forms.Button();
            this.end1 = new UmlDesigner.Components.End();
            this.start1 = new UmlDesigner.Components.Start();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(374, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // end1
            // 
            this.end1.BackColor = System.Drawing.Color.Transparent;
            this.end1.IsSelected = false;
            this.end1.Location = new System.Drawing.Point(59, 145);
            this.end1.Name = "end1";
            this.end1.Size = new System.Drawing.Size(100, 50);
            this.end1.TabIndex = 3;
            // 
            // start1
            // 
            this.start1.BackColor = System.Drawing.Color.Transparent;
            this.start1.IsSelected = false;
            this.start1.Location = new System.Drawing.Point(84, 63);
            this.start1.Name = "start1";
            this.start1.Size = new System.Drawing.Size(100, 50);
            this.start1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 267);
            this.Controls.Add(this.start1);
            this.Controls.Add(this.end1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private Components.End end1;
        private Components.Start start1;
    }
}

