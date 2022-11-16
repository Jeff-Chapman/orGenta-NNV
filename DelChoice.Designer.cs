namespace orGenta_NNv
{
    partial class DelChoice
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
            this.tbQuestion = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbChoicebox = new System.Windows.Forms.GroupBox();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.rbMove = new System.Windows.Forms.RadioButton();
            this.rbLeave = new System.Windows.Forms.RadioButton();
            this.gbChoicebox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbQuestion
            // 
            this.tbQuestion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQuestion.Location = new System.Drawing.Point(12, 12);
            this.tbQuestion.Multiline = true;
            this.tbQuestion.Name = "tbQuestion";
            this.tbQuestion.ReadOnly = true;
            this.tbQuestion.Size = new System.Drawing.Size(438, 79);
            this.tbQuestion.TabIndex = 0;
            this.tbQuestion.Text = "Category [category] has children... what would you like to do with them?";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(215, 274);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 44);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(335, 274);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(103, 44);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbChoicebox
            // 
            this.gbChoicebox.Controls.Add(this.rbDelete);
            this.gbChoicebox.Controls.Add(this.rbMove);
            this.gbChoicebox.Controls.Add(this.rbLeave);
            this.gbChoicebox.Location = new System.Drawing.Point(15, 84);
            this.gbChoicebox.Name = "gbChoicebox";
            this.gbChoicebox.Size = new System.Drawing.Size(434, 172);
            this.gbChoicebox.TabIndex = 3;
            this.gbChoicebox.TabStop = false;
            // 
            // rbDelete
            // 
            this.rbDelete.AutoSize = true;
            this.rbDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDelete.Location = new System.Drawing.Point(41, 121);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(198, 29);
            this.rbDelete.TabIndex = 2;
            this.rbDelete.TabStop = true;
            this.rbDelete.Text = "Delete the children";
            this.rbDelete.UseVisualStyleBackColor = true;
            this.rbDelete.Click += new System.EventHandler(this.delOption_Click);
            // 
            // rbMove
            // 
            this.rbMove.AutoSize = true;
            this.rbMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMove.Location = new System.Drawing.Point(41, 75);
            this.rbMove.Name = "rbMove";
            this.rbMove.Size = new System.Drawing.Size(286, 29);
            this.rbMove.TabIndex = 1;
            this.rbMove.TabStop = true;
            this.rbMove.Text = "Move children under [parent]";
            this.rbMove.UseVisualStyleBackColor = true;
            this.rbMove.Click += new System.EventHandler(this.delOption_Click);
            // 
            // rbLeave
            // 
            this.rbLeave.AutoSize = true;
            this.rbLeave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLeave.Location = new System.Drawing.Point(41, 29);
            this.rbLeave.Name = "rbLeave";
            this.rbLeave.Size = new System.Drawing.Size(311, 29);
            this.rbLeave.TabIndex = 0;
            this.rbLeave.TabStop = true;
            this.rbLeave.Text = "Leave children under [category]";
            this.rbLeave.UseVisualStyleBackColor = true;
            this.rbLeave.Click += new System.EventHandler(this.delOption_Click);
            // 
            // DelChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 375);
            this.ControlBox = false;
            this.Controls.Add(this.gbChoicebox);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DelChoice";
            this.ShowInTaskbar = false;
            this.Text = "Choose Action";
            this.TopMost = true;
            this.gbChoicebox.ResumeLayout(false);
            this.gbChoicebox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbQuestion;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbChoicebox;
        public System.Windows.Forms.RadioButton rbDelete;
        public System.Windows.Forms.RadioButton rbMove;
        public System.Windows.Forms.RadioButton rbLeave;
        public System.Windows.Forms.Button btnCancel;

    }
}