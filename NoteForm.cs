using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace orGenta_NNv
{
    public partial class NoteForm : Form
    {
        public bool NoteIsOnNewItem;
        public string parentItemID;
        public string originalNoteText;
        public bool noteWasBlank = true;
        public int parentClickedRow;
        private ItemsForm myItemsForm;
        private MinimalIntface myMIform;
        private SharedRoutines myItemCleaner;
        private SharedRoutines myDBupdater = new SharedRoutines();
        private string EmptyNoteText = "Enter your note info here...";
        private bool EscKeyPressed = false;
        private bool OkayBtnPressed = false;

        public NoteForm(ItemsForm parent)
        {
            InitializeComponent();
            myItemsForm = parent;
            myItemCleaner = new SharedRoutines();
        }

        public NoteForm(MinimalIntface parent)
        {
            InitializeComponent();
            myMIform = parent;
            myItemCleaner = new SharedRoutines();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OkayBtnPressed = true;
            if (tbNoteText.Text == "")
            { 
                this.Close();
                return;
            }

            if (tbNoteText.Text == EmptyNoteText)
            {
                this.Close();
                myItemsForm.ItemGrid.Rows[parentClickedRow].Cells[0].Value = 0;
                return;
            }

            if (NoteIsOnNewItem)
            {
                // saving handled in parent "item" form

                try
                {                
                    myItemsForm.NewNoteText = tbNoteText.Text;
                    Point mIfLoc = myItemsForm.Location;
                    Point mainLoc = myItemsForm.MdiParent.Location;
                    Point btOKloc = myItemsForm.btnOK.Location;
                    int TotX = mIfLoc.X + mainLoc.X + btOKloc.X + 30;
                    int TotY = mIfLoc.Y + mainLoc.Y + btOKloc.Y + 120;
                    Point newCursLoc = new Point(TotX, TotY);
                    Cursor.Position = newCursLoc;
                }
                catch { myMIform.NewNoteText = tbNoteText.Text; }
                this.Close();
                return;
            }

            // insert new note for an existing item

            string holdNote;
            bool longErrs = myItemsForm.myParentForm.myParentForm.optLongErrMessages;
            if (noteWasBlank)
            {
                holdNote = myItemCleaner.CleanTheItem(tbNoteText.Text);
                string insNoteCmd = "INSERT INTO [Notes] ([ItemID],[NoteValue]) VALUES (";
                insNoteCmd += parentItemID + ",'" + holdNote + "')";
                int rowsIns = myDBupdater.DBinsert(longErrs, "NoteForm:btnOK_Click", myItemsForm.myDBconx, insNoteCmd);

                // update hasNote flag in the Item
                string updItemNoteCmd = "UPDATE Items SET hasNote = 1 WHERE ItemID = " + parentItemID;
                int rowsUpdI = myDBupdater.DBupdate(longErrs, "NoteForm:btnOK_Click", myItemsForm.myDBconx, updItemNoteCmd);

                this.Close();
                return;
            }
            
            // update the note on an existing item

            holdNote = myItemCleaner.CleanTheItem(tbNoteText.Text);
            string updNoteCmd = "UPDATE [Notes] SET [NoteValue] =  '" + holdNote;
            updNoteCmd += "' WHERE ItemID = " + parentItemID;

            int rowsUpd = myDBupdater.DBupdate(longErrs, "NoteForm:btnOK_Click", myItemsForm.myDBconx, updNoteCmd);
            this.Close();
        }

        private void NoteForm_Activated(object sender, EventArgs e)
        {
            tbNoteText.Focus();
        }

        void tbNoteText_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);
            string linkToDoc = docPath[0];
            Application.DoEvents();
            SendKeys.Send("{BackSpace}");
            tbNoteText.Text += " <file://" + linkToDoc + ">";
        }

        private void tbNoteText_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string UrlToVisit = e.LinkText;
            try { System.Diagnostics.Process.Start(UrlToVisit); }
            catch { MessageBox.Show("Unable to Navigate to Link", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EscKeyPressed = true;
            if (NoteHadChanges()) { return; }
            this.Close();
        }

        private bool NoteHadChanges()
        {
            if (tbNoteText.Text == originalNoteText) { return false; }
            DialogResult userChoice = MessageBox.Show("Choose Cancel to Discard Changes...", "Cancel Entry?", MessageBoxButtons.RetryCancel);
            if (userChoice == DialogResult.Cancel)
            {
                if (tbNoteText.Text == EmptyNoteText)
                    { myItemsForm.ItemGrid.Rows[parentClickedRow].Cells[0].Value = 0; }
                return false;
            }
            return true;
        }

        private void NoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((EscKeyPressed) || (OkayBtnPressed)) { return; }
            if (NoteHadChanges()) { e.Cancel = true; }
        }
    }
}
