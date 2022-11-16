
namespace orGenta_NNv
{
    partial class WarnNewKDB
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
            this.lblWarn = new System.Windows.Forms.Label();
            this.cbUnderstand = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWarn
            // 
            this.lblWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarn.Location = new System.Drawing.Point(12, 21);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(670, 108);
            this.lblWarn.TabIndex = 0;
            this.lblWarn.Text = "Generally, we discourage making new Knowledge Bases; it is usually only done for " +
    "separate knowledge domains, having distinct personnel accessibility or operation" +
    "al considerations.";
            // 
            // cbUnderstand
            // 
            this.cbUnderstand.AutoSize = true;
            this.cbUnderstand.Location = new System.Drawing.Point(105, 141);
            this.cbUnderstand.Name = "cbUnderstand";
            this.cbUnderstand.Size = new System.Drawing.Size(167, 24);
            this.cbUnderstand.TabIndex = 1;
            this.cbUnderstand.Text = "OK, I understand...";
            this.cbUnderstand.UseVisualStyleBackColor = true;
            this.cbUnderstand.Click += new System.EventHandler(this.cbUnderstand_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(435, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 45);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WarnNewKDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 199);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbUnderstand);
            this.Controls.Add(this.lblWarn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WarnNewKDB";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Knowlede DB";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.CheckBox cbUnderstand;
        private System.Windows.Forms.Button btnCancel;
    }
}