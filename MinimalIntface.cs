using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace orGenta_NNv
{
    public class MinimalIntface : System.Windows.Forms.Form
	{
		public System.Windows.Forms.RichTextBox txtDataEntered;
		private System.Windows.Forms.Button btnEnter;
		private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRestore;
        public Label lblKBname;
        private Button btnMInote;
        private System.ComponentModel.IContainer components;
        private NoteForm myNoteForm;
        public string wordToCheck = "";
        private string stopChars = " .,;'\"";
        public string NewNoteText = "";
        public string AllCatList = "";
        private bool OptCreateCategories = true;
        private bool OptHighlightCats = true;
        private frmMain myParent;
        private string saveOneChar;
        public bool userClickedControl = false;
        private ContextMenuStrip ctxMenuTextClick;
        private ToolStripMenuItem menucreateCat;
        private ToolStripMenuItem menuDontLink;
        public ArrayList newPotCats = new ArrayList();
        public ArrayList existingCats = new ArrayList();
        private ArrayList holdPotStopWords = new ArrayList();
        private char priorKeyChar = ' ';
        private List<List<string>> myCatSupp;

        public MinimalIntface(frmMain parent)
		{
			InitializeComponent();
            myParent = parent;
            this.txtDataEntered.SelectAll();
            myCatSupp = myParent.TempCatSuppress;
            OptCreateCategories = myParent.optCreateCategories;
            OptHighlightCats = myParent.optHighlightCats;

            // Create the ToolTip and associate with the Restore button.
            using (ToolTip tTipRestore = new ToolTip())
            {
                tTipRestore.AutoPopDelay = 5000;
                tTipRestore.InitialDelay = 1000;
                tTipRestore.ReshowDelay = 500;
                tTipRestore.ShowAlways = true;
                tTipRestore.SetToolTip(this.btnRestore, "Restore");
            }
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinimalIntface));
            this.txtDataEntered = new System.Windows.Forms.RichTextBox();
            this.ctxMenuTextClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menucreateCat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDontLink = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.lblKBname = new System.Windows.Forms.Label();
            this.btnMInote = new System.Windows.Forms.Button();
            this.ctxMenuTextClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDataEntered
            // 
            this.txtDataEntered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataEntered.ContextMenuStrip = this.ctxMenuTextClick;
            this.txtDataEntered.Location = new System.Drawing.Point(150, 0);
            this.txtDataEntered.Name = "txtDataEntered";
            this.txtDataEntered.Size = new System.Drawing.Size(934, 26);
            this.txtDataEntered.TabIndex = 0;
            this.txtDataEntered.Text = "--> Enter a new Item, or a single word to Search";
            this.txtDataEntered.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDataEntered_MouseClick);
            this.txtDataEntered.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDataEntered_KeyPress);
            // 
            // ctxMenuTextClick
            // 
            this.ctxMenuTextClick.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxMenuTextClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menucreateCat,
            this.menuDontLink});
            this.ctxMenuTextClick.Name = "ctxMenuTextClick";
            this.ctxMenuTextClick.Size = new System.Drawing.Size(167, 68);
            this.ctxMenuTextClick.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMenuTextClick_ItemClicked);
            // 
            // menucreateCat
            // 
            this.menucreateCat.Name = "menucreateCat";
            this.menucreateCat.Size = new System.Drawing.Size(166, 32);
            this.menucreateCat.Text = "Create Category";
            // 
            // menuDontLink
            // 
            this.menuDontLink.Name = "menuDontLink";
            this.menuDontLink.Size = new System.Drawing.Size(166, 32);
            this.menuDontLink.Text = "Don\'t Link";
            // 
            // btnEnter
            // 
            this.btnEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnEnter.Location = new System.Drawing.Point(13, 0);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(64, 16);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "Enter";
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(166, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 16);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
            this.btnRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRestore.Location = new System.Drawing.Point(814, 0);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(0);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(39, 26);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // lblKBname
            // 
            this.lblKBname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblKBname.Location = new System.Drawing.Point(0, 0);
            this.lblKBname.Name = "lblKBname";
            this.lblKBname.Size = new System.Drawing.Size(150, 26);
            this.lblKBname.TabIndex = 5;
            this.lblKBname.Text = "<KBname>";
            // 
            // btnMInote
            // 
            this.btnMInote.Location = new System.Drawing.Point(746, 0);
            this.btnMInote.Name = "btnMInote";
            this.btnMInote.Size = new System.Drawing.Size(69, 26);
            this.btnMInote.TabIndex = 1;
            this.btnMInote.Text = "+Note";
            this.btnMInote.UseVisualStyleBackColor = true;
            this.btnMInote.Click += new System.EventHandler(this.btnMInote_Click);
            // 
            // MinimalIntface
            // 
            this.AcceptButton = this.btnEnter;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(851, 20);
            this.ControlBox = false;
            this.Controls.Add(this.btnMInote);
            this.Controls.Add(this.lblKBname);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.txtDataEntered);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEnter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MinimalIntface";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.MinimalIntface_Activated);
            this.ctxMenuTextClick.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnExit_Click(object sender, System.EventArgs e)
		{
            userClickedControl = false;
            wordToCheck = "";
            this.Visible = false;
		}

		private void btnRestore_Click(object sender, System.EventArgs e)
		{
			this.txtDataEntered.Focus();
            userClickedControl = false;
            wordToCheck = "";
            this.Visible = false;
			this.txtDataEntered.Text = "";
		}

        private void btnMInote_Click(object sender, EventArgs e)
        {
            myNoteForm = new NoteForm(this);
            myNoteForm.MdiParent = null;
            Rectangle sSize = Screen.PrimaryScreen.WorkingArea;
            myNoteForm.Top = (sSize.Height / 2) - 200;
            myNoteForm.Left = (sSize.Width / 2) - 300;
            myNoteForm.NoteIsOnNewItem = true;
            string itemText = txtDataEntered.Text;
            string itemSamp = itemText + "...";
            if (itemText.Length > 20) { itemSamp = itemText.Substring(0, 20) + "..."; }
            myNoteForm.Text = "Note For: \"" + itemSamp + "\"";
            NewNoteText = "";
            myNoteForm.ShowDialog(this);
            txtDataEntered.Focus();
            SendKeys.Send("{Enter}");
        }

        private void txtDataEntered_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (userClickedControl) { return; }

            if (e.KeyChar == '\b')
            {
                // Handle backspace over a stop character
                int chkStop = stopChars.IndexOf(priorKeyChar.ToString());
                if (chkStop >= 0)
                {
                    if (sender.ToString() == "") { return; }
                    string testChar = txtDataEntered.Text.Substring(txtDataEntered.Text.Length - 1);
                    priorKeyChar = testChar.ToCharArray()[0];
                    if (stopChars.IndexOf(testChar.ToString()) > -1) { return; }
                    wordToCheck = GetLastWordFrom(sender.ToString());
                    newPotCats.Remove(wordToCheck);
                    existingCats.Remove(wordToCheck);
                    return;
                }

                // Remove one character
                try { wordToCheck = wordToCheck.Substring(0, wordToCheck.Length - 1); }
                catch { }
                priorKeyChar = e.KeyChar;
                return;
            }

            priorKeyChar = e.KeyChar;
            if (txtDataEntered.SelectionLength == 0) 
            {
                txtDataEntered.SelectionStart = txtDataEntered.TextLength;
                ResetEntryColor(); 
            }

            if (stopChars.IndexOf(e.KeyChar.ToString()) > -1)
            {
                if (wordToCheck.Length > 3)
                {
                    if (myParent.orgStopWords.IndexOf(wordToCheck) < 0)
                    { 
                        saveOneChar = e.KeyChar.ToString();
                        e.Handled = true;
                        checkWordForHighlighting(wordToCheck);
                        SendKeys.Send(saveOneChar);
                    }
                }
                
                wordToCheck = "";
                return;
            }
      
            wordToCheck += e.KeyChar.ToString();
      
        }

        private string GetLastWordFrom(string DataSoFar)
        {
            int findSpace = DataSoFar.Length - 1;
            try { while (DataSoFar.Substring(findSpace, 1) != " ") { findSpace--; } }
            catch { }
            return DataSoFar.Substring(findSpace + 1);
        }

        public void ResetEntryColor()
        {
            txtDataEntered.SelectionLength = 1;
            txtDataEntered.SelectionBackColor = SystemColors.Window;
            txtDataEntered.SelectionColor = SystemColors.WindowText;
            txtDataEntered.SelectionStart = txtDataEntered.TextLength + 1;
            txtDataEntered.SelectionLength = 0;
        }

        private void checkWordForHighlighting(string wordToCheck)
        {
            Console.WriteLine("Checking: " + wordToCheck);

            // see if it matches existing category
            int findStart = AllCatList.IndexOf(wordToCheck.ToLower());
            int needAspace = findStart + wordToCheck.Length;
            if ((findStart > -1) && (AllCatList.Substring(needAspace, 1) == " "))
            {
                // highlight matching category
                if (!OptHighlightCats) { return; }
                HighLightWord(wordToCheck, Color.Bisque, Color.Black);
                existingCats.Add(wordToCheck);
                SendKeys.Send("{End}");
                return;
            }

            if (!OptCreateCategories) { return; }

            // highlight possible new category
            HighLightWord(wordToCheck, Color.SlateGray, Color.White);
            newPotCats.Add(wordToCheck);
            SendKeys.Send("{End}");            
        }

        private void HighLightWord(string wordToCheck, Color Backgr, Color Foreg)
        {
            txtDataEntered.SelectionStart = txtDataEntered.TextLength - wordToCheck.Length;
            txtDataEntered.SelectionLength = wordToCheck.Length;
            txtDataEntered.SelectionBackColor = Backgr;
            txtDataEntered.SelectionColor = Foreg;
        }

        private void txtDataEntered_MouseClick(object sender, MouseEventArgs e)
        {
            userClickedControl = true;
        }

        private void ctxMenuTextClick_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            CatSupressForm myNewCatListForm = new CatSupressForm();
            CheckedListBox myNewCatList = myNewCatListForm.ckListCategories;
            myNewCatListForm.Left = this.Left + 45;

            if (e.ClickedItem == menucreateCat)
            {
                // opt-in newPotCats
                foreach (string oneCat in newPotCats)
                    { myNewCatList.Items.Add(oneCat, true); }

                myNewCatListForm.Top = this.Top - (newPotCats.Count * 16) - 50;
                myNewCatListForm.Height = (newPotCats.Count * 16) + 32;

                myNewCatListForm.ShowDialog();

                StreamWriter writeStops = new StreamWriter("orgStopWords.txt", true);

                // Add unchecked items to stopwords, remove from newPotCats

                holdPotStopWords.Clear();
                holdPotStopWords = (ArrayList)newPotCats.Clone();
                foreach (string oneCat in myNewCatList.CheckedItems)
                    { holdPotStopWords.Remove(oneCat);  }
                foreach (string stopWord in holdPotStopWords)
                {
                    writeStops.WriteLine(stopWord);
                    myParent.orgStopWords += " " + stopWord;
                    newPotCats.Remove(stopWord);
                }

                writeStops.Close();
                writeStops.Dispose();
                return;
            }

            if (e.ClickedItem == menuDontLink)
            {
                myNewCatListForm.lblListHeader.Text = "Don't link these:";

                foreach (string oneCat in newPotCats)
                    { myNewCatList.Items.Add(oneCat, false); }
                foreach (string oneCat in existingCats)
                    { myNewCatList.Items.Add(oneCat, false); }

                myNewCatListForm.Top = this.Top - (myNewCatList.Items.Count * 16) - 50;
                myNewCatListForm.Height = (myNewCatList.Items.Count * 16) + 32;

                myNewCatListForm.ShowDialog();

                foreach (string oneCat in myNewCatList.CheckedItems)
                {
                    List<string> oneSup = new List<string> { oneCat, this.txtDataEntered.Text };
                    myCatSupp.Add(oneSup);
                }

                return;
            }

        }

        private void MinimalIntface_Activated(object sender, EventArgs e)
        {
            OptCreateCategories = myParent.optCreateCategories;
            OptHighlightCats = myParent.optHighlightCats;
            menucreateCat.Visible = false;
            if (OptCreateCategories) { menucreateCat.Visible = true; }
            menuDontLink.Visible = false;
            if (OptHighlightCats) { menuDontLink.Visible = true; }
        }
    }
}
