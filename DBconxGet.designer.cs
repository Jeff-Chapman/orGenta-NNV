namespace orGenta_NNv
{
    partial class DBconxGet
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
            this.lblServerOrPath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.cbUseAsDefault = new System.Windows.Forms.CheckBox();
            this.btnSelectDB = new System.Windows.Forms.Button();
            this.openDBdialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblServerOrPath
            // 
            this.lblServerOrPath.AutoSize = true;
            this.lblServerOrPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerOrPath.Location = new System.Drawing.Point(60, 26);
            this.lblServerOrPath.Name = "lblServerOrPath";
            this.lblServerOrPath.Size = new System.Drawing.Size(58, 25);
            this.lblServerOrPath.TabIndex = 0;
            this.lblServerOrPath.Text = "Path:";
            this.lblServerOrPath.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(340, 127);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(109, 49);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Next >>";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbServer
            // 
            this.tbServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServer.Location = new System.Drawing.Point(138, 25);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(311, 30);
            this.tbServer.TabIndex = 5;
            this.tbServer.Click += new System.EventHandler(this.tbServer_Click);
            // 
            // tbDatabase
            // 
            this.tbDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDatabase.Location = new System.Drawing.Point(138, 75);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(280, 30);
            this.tbDatabase.TabIndex = 6;
            this.tbDatabase.Click += new System.EventHandler(this.tbDatabase_Click);
            // 
            // cbUseAsDefault
            // 
            this.cbUseAsDefault.AutoSize = true;
            this.cbUseAsDefault.Location = new System.Drawing.Point(138, 141);
            this.cbUseAsDefault.Name = "cbUseAsDefault";
            this.cbUseAsDefault.Size = new System.Drawing.Size(143, 24);
            this.cbUseAsDefault.TabIndex = 12;
            this.cbUseAsDefault.Text = "Use As Default";
            this.cbUseAsDefault.UseVisualStyleBackColor = true;
            // 
            // btnSelectDB
            // 
            this.btnSelectDB.Location = new System.Drawing.Point(418, 77);
            this.btnSelectDB.Name = "btnSelectDB";
            this.btnSelectDB.Size = new System.Drawing.Size(36, 34);
            this.btnSelectDB.TabIndex = 14;
            this.btnSelectDB.Text = "...";
            this.btnSelectDB.UseVisualStyleBackColor = true;
            this.btnSelectDB.Click += new System.EventHandler(this.btnSelectDB_Click);
            // 
            // openDBdialog
            // 
            this.openDBdialog.FileName = "orGenta.mdb";
            // 
            // DBconxGet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 197);
            this.Controls.Add(this.btnSelectDB);
            this.Controls.Add(this.cbUseAsDefault);
            this.Controls.Add(this.tbDatabase);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblServerOrPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DBconxGet";
            this.Text = "Connect to a Database";
            this.Activated += new System.EventHandler(this.DBconxGet_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServerOrPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox tbServer;
        public System.Windows.Forms.TextBox tbDatabase;
        public System.Windows.Forms.CheckBox cbUseAsDefault;
        private System.Windows.Forms.Button btnSelectDB;
        public System.Windows.Forms.OpenFileDialog openDBdialog;
    }
}