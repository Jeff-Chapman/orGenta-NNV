namespace orGenta_NNv
{
    partial class ImportStuff
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
            this.pnlImportOptions = new System.Windows.Forms.Panel();
            this.pnlEveryOptions = new System.Windows.Forms.Panel();
            this.lblImportCats = new System.Windows.Forms.Label();
            this.lblImportUnassigned = new System.Windows.Forms.Label();
            this.rbImportCats = new System.Windows.Forms.RadioButton();
            this.rbImportUnassigned = new System.Windows.Forms.RadioButton();
            this.rbOPML = new System.Windows.Forms.RadioButton();
            this.rbHalna = new System.Windows.Forms.RadioButton();
            this.rbEverything = new System.Windows.Forms.RadioButton();
            this.rbItemsDates = new System.Windows.Forms.RadioButton();
            this.rbItems = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.ImportItemsDialog = new System.Windows.Forms.OpenFileDialog();
            this.pbImportProgress = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlImportOptions.SuspendLayout();
            this.pnlEveryOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImportOptions
            // 
            this.pnlImportOptions.Controls.Add(this.pnlEveryOptions);
            this.pnlImportOptions.Controls.Add(this.rbOPML);
            this.pnlImportOptions.Controls.Add(this.rbHalna);
            this.pnlImportOptions.Controls.Add(this.rbEverything);
            this.pnlImportOptions.Controls.Add(this.rbItemsDates);
            this.pnlImportOptions.Controls.Add(this.rbItems);
            this.pnlImportOptions.Location = new System.Drawing.Point(12, 12);
            this.pnlImportOptions.Name = "pnlImportOptions";
            this.pnlImportOptions.Size = new System.Drawing.Size(403, 236);
            this.pnlImportOptions.TabIndex = 0;
            // 
            // pnlEveryOptions
            // 
            this.pnlEveryOptions.Controls.Add(this.lblImportCats);
            this.pnlEveryOptions.Controls.Add(this.lblImportUnassigned);
            this.pnlEveryOptions.Controls.Add(this.rbImportCats);
            this.pnlEveryOptions.Controls.Add(this.rbImportUnassigned);
            this.pnlEveryOptions.Location = new System.Drawing.Point(3, 3);
            this.pnlEveryOptions.Name = "pnlEveryOptions";
            this.pnlEveryOptions.Size = new System.Drawing.Size(398, 230);
            this.pnlEveryOptions.TabIndex = 4;
            this.pnlEveryOptions.Visible = false;
            // 
            // lblImportCats
            // 
            this.lblImportCats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportCats.Location = new System.Drawing.Point(33, 127);
            this.lblImportCats.Name = "lblImportCats";
            this.lblImportCats.Size = new System.Drawing.Size(328, 57);
            this.lblImportCats.TabIndex = 3;
            this.lblImportCats.Text = "Import into pre-existing Categories (create as neeeded)";
            this.lblImportCats.Click += new System.EventHandler(this.lblImportCats_Click);
            // 
            // lblImportUnassigned
            // 
            this.lblImportUnassigned.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportUnassigned.Location = new System.Drawing.Point(33, 48);
            this.lblImportUnassigned.Name = "lblImportUnassigned";
            this.lblImportUnassigned.Size = new System.Drawing.Size(328, 79);
            this.lblImportUnassigned.TabIndex = 2;
            this.lblImportUnassigned.Text = "Import to [xxx] and suppress Auto-Assign";
            this.lblImportUnassigned.Click += new System.EventHandler(this.lblImportUnassigned_Click);
            // 
            // rbImportCats
            // 
            this.rbImportCats.AutoSize = true;
            this.rbImportCats.Location = new System.Drawing.Point(6, 129);
            this.rbImportCats.Name = "rbImportCats";
            this.rbImportCats.Size = new System.Drawing.Size(21, 20);
            this.rbImportCats.TabIndex = 1;
            this.rbImportCats.TabStop = true;
            this.rbImportCats.UseVisualStyleBackColor = true;
            // 
            // rbImportUnassigned
            // 
            this.rbImportUnassigned.AutoSize = true;
            this.rbImportUnassigned.Checked = true;
            this.rbImportUnassigned.Location = new System.Drawing.Point(7, 52);
            this.rbImportUnassigned.Name = "rbImportUnassigned";
            this.rbImportUnassigned.Size = new System.Drawing.Size(21, 20);
            this.rbImportUnassigned.TabIndex = 0;
            this.rbImportUnassigned.TabStop = true;
            this.rbImportUnassigned.UseVisualStyleBackColor = true;
            // 
            // rbOPML
            // 
            this.rbOPML.AutoSize = true;
            this.rbOPML.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOPML.Location = new System.Drawing.Point(3, 190);
            this.rbOPML.Name = "rbOPML";
            this.rbOPML.Size = new System.Drawing.Size(264, 29);
            this.rbOPML.TabIndex = 4;
            this.rbOPML.TabStop = true;
            this.rbOPML.Text = "Everything (OPML format)";
            this.rbOPML.UseVisualStyleBackColor = true;
            // 
            // rbHalna
            // 
            this.rbHalna.AutoSize = true;
            this.rbHalna.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHalna.Location = new System.Drawing.Point(3, 147);
            this.rbHalna.Name = "rbHalna";
            this.rbHalna.Size = new System.Drawing.Size(323, 29);
            this.rbHalna.TabIndex = 3;
            this.rbHalna.TabStop = true;
            this.rbHalna.Text = "Everything (Halna tabbed format)";
            this.rbHalna.UseVisualStyleBackColor = true;
            // 
            // rbEverything
            // 
            this.rbEverything.AutoSize = true;
            this.rbEverything.Enabled = false;
            this.rbEverything.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEverything.Location = new System.Drawing.Point(3, 104);
            this.rbEverything.Name = "rbEverything";
            this.rbEverything.Size = new System.Drawing.Size(312, 29);
            this.rbEverything.TabIndex = 2;
            this.rbEverything.TabStop = true;
            this.rbEverything.Text = "Everything (orGenta csv format)";
            this.rbEverything.UseVisualStyleBackColor = true;
            // 
            // rbItemsDates
            // 
            this.rbItemsDates.AutoSize = true;
            this.rbItemsDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbItemsDates.Location = new System.Drawing.Point(3, 61);
            this.rbItemsDates.Name = "rbItemsDates";
            this.rbItemsDates.Size = new System.Drawing.Size(178, 29);
            this.rbItemsDates.TabIndex = 1;
            this.rbItemsDates.TabStop = true;
            this.rbItemsDates.Text = "Items and Dates";
            this.rbItemsDates.UseVisualStyleBackColor = true;
            // 
            // rbItems
            // 
            this.rbItems.AutoSize = true;
            this.rbItems.Checked = true;
            this.rbItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbItems.Location = new System.Drawing.Point(3, 18);
            this.rbItems.Name = "rbItems";
            this.rbItems.Size = new System.Drawing.Size(84, 29);
            this.rbItems.TabIndex = 0;
            this.rbItems.TabStop = true;
            this.rbItems.Text = "Items";
            this.rbItems.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(312, 263);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 44);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ImportItemsDialog
            // 
            this.ImportItemsDialog.FileName = "NewItems.txt";
            this.ImportItemsDialog.Filter = "Text files|*.txt; *.csv|XML files|*.xml; *.opml|All files|*.*";
            this.ImportItemsDialog.ReadOnlyChecked = true;
            this.ImportItemsDialog.Title = "Select Data to Import";
            // 
            // pbImportProgress
            // 
            this.pbImportProgress.Location = new System.Drawing.Point(12, 278);
            this.pbImportProgress.Name = "pbImportProgress";
            this.pbImportProgress.Size = new System.Drawing.Size(290, 28);
            this.pbImportProgress.TabIndex = 2;
            this.pbImportProgress.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 44);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ImportStuff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 325);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbImportProgress);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlImportOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportStuff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Data";
            this.pnlImportOptions.ResumeLayout(false);
            this.pnlImportOptions.PerformLayout();
            this.pnlEveryOptions.ResumeLayout(false);
            this.pnlEveryOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImportOptions;
        private System.Windows.Forms.RadioButton rbEverything;
        private System.Windows.Forms.RadioButton rbItemsDates;
        private System.Windows.Forms.RadioButton rbItems;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog ImportItemsDialog;
        private System.Windows.Forms.ProgressBar pbImportProgress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlEveryOptions;
        private System.Windows.Forms.Label lblImportCats;
        private System.Windows.Forms.Label lblImportUnassigned;
        private System.Windows.Forms.RadioButton rbImportCats;
        private System.Windows.Forms.RadioButton rbImportUnassigned;
        private System.Windows.Forms.RadioButton rbHalna;
        private System.Windows.Forms.RadioButton rbOPML;
    }
}