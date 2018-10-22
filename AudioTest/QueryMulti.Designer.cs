namespace AudioTest
{
    partial class QueryMulti
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
            this.One = new System.Windows.Forms.Button();
            this.Two = new System.Windows.Forms.Button();
            this.Three = new System.Windows.Forms.Button();
            this.Four = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // One
            // 
            this.One.Location = new System.Drawing.Point(0, 0);
            this.One.Name = "One";
            this.One.Size = new System.Drawing.Size(110, 58);
            this.One.TabIndex = 0;
            this.One.Text = "One";
            this.One.UseVisualStyleBackColor = true;
            this.One.Click += new System.EventHandler(this.button1_Click);
            // 
            // Two
            // 
            this.Two.Location = new System.Drawing.Point(116, 0);
            this.Two.Name = "Two";
            this.Two.Size = new System.Drawing.Size(110, 58);
            this.Two.TabIndex = 1;
            this.Two.Text = "Two";
            this.Two.UseVisualStyleBackColor = true;
            this.Two.Click += new System.EventHandler(this.Two_Click);
            // 
            // Three
            // 
            this.Three.Location = new System.Drawing.Point(0, 64);
            this.Three.Name = "Three";
            this.Three.Size = new System.Drawing.Size(110, 58);
            this.Three.TabIndex = 2;
            this.Three.Text = "Three";
            this.Three.UseVisualStyleBackColor = true;
            this.Three.Click += new System.EventHandler(this.Three_Click);
            // 
            // Four
            // 
            this.Four.Location = new System.Drawing.Point(116, 64);
            this.Four.Name = "Four";
            this.Four.Size = new System.Drawing.Size(110, 58);
            this.Four.TabIndex = 3;
            this.Four.Text = "Four";
            this.Four.UseVisualStyleBackColor = true;
            this.Four.Click += new System.EventHandler(this.Four_Click);
            // 
            // QueryMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 120);
            this.Controls.Add(this.Four);
            this.Controls.Add(this.Three);
            this.Controls.Add(this.Two);
            this.Controls.Add(this.One);
            this.Name = "QueryMulti";
            this.Text = "QueryMulti";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button One;
        public System.Windows.Forms.Button Two;
        public System.Windows.Forms.Button Three;
        public System.Windows.Forms.Button Four;
    }
}