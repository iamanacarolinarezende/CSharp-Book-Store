namespace Final_Project.GUI
{
    partial class AddJobTitle
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
            this.textBoxAddNewRole = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonAddNewRole = new System.Windows.Forms.Button();
            this.buttonExitE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxAddNewRole
            // 
            this.textBoxAddNewRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAddNewRole.Location = new System.Drawing.Point(93, 74);
            this.textBoxAddNewRole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAddNewRole.Name = "textBoxAddNewRole";
            this.textBoxAddNewRole.Size = new System.Drawing.Size(193, 21);
            this.textBoxAddNewRole.TabIndex = 56;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(90, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(154, 17);
            this.label10.TabIndex = 57;
            this.label10.Text = "Add below the new role";
            // 
            // buttonAddNewRole
            // 
            this.buttonAddNewRole.BackColor = System.Drawing.Color.LightGray;
            this.buttonAddNewRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddNewRole.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonAddNewRole.Location = new System.Drawing.Point(209, 133);
            this.buttonAddNewRole.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddNewRole.Name = "buttonAddNewRole";
            this.buttonAddNewRole.Size = new System.Drawing.Size(137, 37);
            this.buttonAddNewRole.TabIndex = 66;
            this.buttonAddNewRole.Text = "&Add";
            this.buttonAddNewRole.UseVisualStyleBackColor = false;
            this.buttonAddNewRole.Click += new System.EventHandler(this.buttonAddNewRole_Click);
            // 
            // buttonExitE
            // 
            this.buttonExitE.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExitE.Location = new System.Drawing.Point(33, 135);
            this.buttonExitE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExitE.Name = "buttonExitE";
            this.buttonExitE.Size = new System.Drawing.Size(139, 37);
            this.buttonExitE.TabIndex = 65;
            this.buttonExitE.Text = "E&xit";
            this.buttonExitE.UseVisualStyleBackColor = true;
            this.buttonExitE.Click += new System.EventHandler(this.buttonExitE_Click);
            // 
            // AddJobTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 215);
            this.Controls.Add(this.buttonAddNewRole);
            this.Controls.Add(this.buttonExitE);
            this.Controls.Add(this.textBoxAddNewRole);
            this.Controls.Add(this.label10);
            this.Name = "AddJobTitle";
            this.Text = "AddJobTitle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAddNewRole;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonAddNewRole;
        private System.Windows.Forms.Button buttonExitE;
    }
}