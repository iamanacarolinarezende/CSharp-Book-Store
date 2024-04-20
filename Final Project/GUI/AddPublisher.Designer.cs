namespace Final_Project.GUI
{
    partial class AddPublisher
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
            this.buttonAddNewPublisher = new System.Windows.Forms.Button();
            this.buttonExitPublisher = new System.Windows.Forms.Button();
            this.textBoxAddNewPublisher = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAddNewPublisher
            // 
            this.buttonAddNewPublisher.BackColor = System.Drawing.Color.LightGray;
            this.buttonAddNewPublisher.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddNewPublisher.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAddNewPublisher.Location = new System.Drawing.Point(209, 138);
            this.buttonAddNewPublisher.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddNewPublisher.Name = "buttonAddNewPublisher";
            this.buttonAddNewPublisher.Size = new System.Drawing.Size(137, 37);
            this.buttonAddNewPublisher.TabIndex = 70;
            this.buttonAddNewPublisher.Text = "&Add";
            this.buttonAddNewPublisher.UseVisualStyleBackColor = false;
            this.buttonAddNewPublisher.Click += new System.EventHandler(this.buttonAddNewPublisher_Click);
            // 
            // buttonExitPublisher
            // 
            this.buttonExitPublisher.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExitPublisher.Location = new System.Drawing.Point(33, 140);
            this.buttonExitPublisher.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExitPublisher.Name = "buttonExitPublisher";
            this.buttonExitPublisher.Size = new System.Drawing.Size(139, 37);
            this.buttonExitPublisher.TabIndex = 69;
            this.buttonExitPublisher.Text = "E&xit";
            this.buttonExitPublisher.UseVisualStyleBackColor = true;
            this.buttonExitPublisher.Click += new System.EventHandler(this.buttonExitPublisher_Click);
            // 
            // textBoxAddNewPublisher
            // 
            this.textBoxAddNewPublisher.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAddNewPublisher.Location = new System.Drawing.Point(93, 79);
            this.textBoxAddNewPublisher.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAddNewPublisher.Name = "textBoxAddNewPublisher";
            this.textBoxAddNewPublisher.Size = new System.Drawing.Size(193, 21);
            this.textBoxAddNewPublisher.TabIndex = 67;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(90, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(227, 17);
            this.label10.TabIndex = 68;
            this.label10.Text = "Add below the new publisher name";
            // 
            // AddPublisher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 215);
            this.Controls.Add(this.buttonAddNewPublisher);
            this.Controls.Add(this.buttonExitPublisher);
            this.Controls.Add(this.textBoxAddNewPublisher);
            this.Controls.Add(this.label10);
            this.Name = "AddPublisher";
            this.Text = "AddPublisher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddNewPublisher;
        private System.Windows.Forms.Button buttonExitPublisher;
        private System.Windows.Forms.TextBox textBoxAddNewPublisher;
        private System.Windows.Forms.Label label10;
    }
}