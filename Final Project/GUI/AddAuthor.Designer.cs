namespace Final_Project.GUI
{
    partial class AddAuthor
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
            this.textBoxAuthorLastName = new System.Windows.Forms.TextBox();
            this.textBoxAuthorFirstName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxAuthorEmail = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonAddAuthor = new System.Windows.Forms.Button();
            this.buttonExitE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxAuthorLastName
            // 
            this.textBoxAuthorLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAuthorLastName.Location = new System.Drawing.Point(279, 58);
            this.textBoxAuthorLastName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAuthorLastName.Name = "textBoxAuthorLastName";
            this.textBoxAuthorLastName.Size = new System.Drawing.Size(140, 21);
            this.textBoxAuthorLastName.TabIndex = 30;
            // 
            // textBoxAuthorFirstName
            // 
            this.textBoxAuthorFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAuthorFirstName.Location = new System.Drawing.Point(87, 58);
            this.textBoxAuthorFirstName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAuthorFirstName.Name = "textBoxAuthorFirstName";
            this.textBoxAuthorFirstName.Size = new System.Drawing.Size(140, 21);
            this.textBoxAuthorFirstName.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(279, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 17);
            this.label8.TabIndex = 32;
            this.label8.Text = "Last Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(87, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "First Name";
            // 
            // textBoxAuthorEmail
            // 
            this.textBoxAuthorEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAuthorEmail.Location = new System.Drawing.Point(90, 128);
            this.textBoxAuthorEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAuthorEmail.Name = "textBoxAuthorEmail";
            this.textBoxAuthorEmail.Size = new System.Drawing.Size(263, 21);
            this.textBoxAuthorEmail.TabIndex = 106;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(90, 154);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 13);
            this.label16.TabIndex = 108;
            this.label16.Text = "abc@email.com";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(87, 96);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 17);
            this.label17.TabIndex = 107;
            this.label17.Text = "Email*";
            // 
            // buttonAddAuthor
            // 
            this.buttonAddAuthor.BackColor = System.Drawing.Color.LightGray;
            this.buttonAddAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAuthor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAddAuthor.Location = new System.Drawing.Point(273, 195);
            this.buttonAddAuthor.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddAuthor.Name = "buttonAddAuthor";
            this.buttonAddAuthor.Size = new System.Drawing.Size(137, 37);
            this.buttonAddAuthor.TabIndex = 110;
            this.buttonAddAuthor.Text = "&Add";
            this.buttonAddAuthor.UseVisualStyleBackColor = false;
            this.buttonAddAuthor.Click += new System.EventHandler(this.buttonAddAuthor_Click);
            // 
            // buttonExitE
            // 
            this.buttonExitE.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExitE.Location = new System.Drawing.Point(97, 197);
            this.buttonExitE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExitE.Name = "buttonExitE";
            this.buttonExitE.Size = new System.Drawing.Size(139, 37);
            this.buttonExitE.TabIndex = 109;
            this.buttonExitE.Text = "E&xit";
            this.buttonExitE.UseVisualStyleBackColor = true;
            this.buttonExitE.Click += new System.EventHandler(this.buttonExitE_Click);
            // 
            // AddAuthor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 259);
            this.Controls.Add(this.buttonAddAuthor);
            this.Controls.Add(this.buttonExitE);
            this.Controls.Add(this.textBoxAuthorEmail);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBoxAuthorLastName);
            this.Controls.Add(this.textBoxAuthorFirstName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Name = "AddAuthor";
            this.Text = "AdAuthor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAuthorLastName;
        private System.Windows.Forms.TextBox textBoxAuthorFirstName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxAuthorEmail;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonAddAuthor;
        private System.Windows.Forms.Button buttonExitE;
    }
}