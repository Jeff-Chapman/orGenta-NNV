using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace orGenta_NNv
{

    public class AdvancedPrint : System.Windows.Forms.Form
	{

        #region Documentation
        /* The DataGridView passed in the PrintInitialization gets assigned to PrintItems, and
         * it contains more than what has to print, as some columns
         * are supressed from visibility in the view or aren't worth printing.
         * 
         * PrintItems[colCounter, dsLineCounter] element are:
         * Column   0 = hasNote, Byte
         *          1 = itemDesc, String        <-- printable
         *          2 = dateCreated, DateTime   <-- printable
         *          3 = itemID, Int32
         *          
         * ComputedWidths[] array element 0 is the width of longest category, after that
         * each element is for a printed column 
        */
        #endregion

        #region Private Variables
        private Font printFont;
		private int ItemRowCount;
    	private int dsLineCounter;
		private int DateWidth;
		private DataGridView PrintItems;
		private ArrayList SavedCategories;
        private IDbConnection myDBconx;
		private ArrayList ItemIDs;
		private System.ComponentModel.Container components = null;
        private System.Drawing.Printing.PrintDocument prtGridPrintDoc;
        private float linesPerPage;
        private float yPos;
        private int countOfLinesPrinted;
        private StringFormat StringFormatParms;
        private float leftMargin;
        private float topMargin;
        private int[] columnWidths;
        private int[] columnsAdjusted;
        private int[] colStartLocs;
        private int PreviousStartLoc;
        private int numOfPrintCols = 2;
        #endregion
        
        #region Entry Points

        public AdvancedPrint()
        {
            InitializeComponent();
        }

        public void PrintInitialization(DataGridView iGridDataSource, ArrayList RowXrefArray, IDbConnection orgDatabase, ArrayList inpItemIDs)
		{
			//	Assign values that were passed to local variables
			PrintItems = iGridDataSource;
			ItemRowCount = RowXrefArray.Count;
			SavedCategories = RowXrefArray;
			myDBconx = orgDatabase;
			ItemIDs = inpItemIDs;

			//	Default font value to use
			printFont = new Font("Arial", 10);
		}

		#endregion

		#region Windows Form Designer generated code
        private System.Windows.Forms.Button btnFonts;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gBoxZoom;
        private System.Windows.Forms.Button btnZoomPlus;
        private System.Windows.Forms.Button btnZoomMinus;
        public PrintPreviewControl prtPreviewWindow;
        private System.Windows.Forms.FontDialog fontChooser;
        private System.Windows.Forms.PageSetupDialog orGentaPageSetup;
        private System.Windows.Forms.Button btnPrintSetup;

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedPrint));
            this.prtPreviewWindow = new System.Windows.Forms.PrintPreviewControl();
            this.prtGridPrintDoc = new System.Drawing.Printing.PrintDocument();
            this.btnFonts = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gBoxZoom = new System.Windows.Forms.GroupBox();
            this.btnZoomMinus = new System.Windows.Forms.Button();
            this.btnZoomPlus = new System.Windows.Forms.Button();
            this.fontChooser = new System.Windows.Forms.FontDialog();
            this.orGentaPageSetup = new System.Windows.Forms.PageSetupDialog();
            this.btnPrintSetup = new System.Windows.Forms.Button();
            this.gBoxZoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // prtPreviewWindow
            // 
            this.prtPreviewWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prtPreviewWindow.AutoZoom = false;
            this.prtPreviewWindow.Document = this.prtGridPrintDoc;
            this.prtPreviewWindow.Location = new System.Drawing.Point(13, 12);
            this.prtPreviewWindow.Name = "prtPreviewWindow";
            this.prtPreviewWindow.Size = new System.Drawing.Size(428, 369);
            this.prtPreviewWindow.TabIndex = 0;
            this.prtPreviewWindow.Zoom = 0.75D;
            // 
            // prtGridPrintDoc
            // 
            this.prtGridPrintDoc.DocumentName = "orGenta Tasks";
            this.prtGridPrintDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.prtGridPrintDoc_BeginPrint);
            this.prtGridPrintDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prtGridPrintDoc_PrintPage);
            // 
            // btnFonts
            // 
            this.btnFonts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFonts.Location = new System.Drawing.Point(467, 41);
            this.btnFonts.Name = "btnFonts";
            this.btnFonts.Size = new System.Drawing.Size(120, 34);
            this.btnFonts.TabIndex = 1;
            this.btnFonts.Text = "Fonts ...";
            this.btnFonts.Click += new System.EventHandler(this.btnFonts_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(467, 288);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 33);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(467, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 34);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            // 
            // gBoxZoom
            // 
            this.gBoxZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.gBoxZoom.Controls.Add(this.btnZoomMinus);
            this.gBoxZoom.Controls.Add(this.btnZoomPlus);
            this.gBoxZoom.Location = new System.Drawing.Point(461, 179);
            this.gBoxZoom.Name = "gBoxZoom";
            this.gBoxZoom.Size = new System.Drawing.Size(128, 93);
            this.gBoxZoom.TabIndex = 5;
            this.gBoxZoom.TabStop = false;
            this.gBoxZoom.Text = "Zoom";
            // 
            // btnZoomMinus
            // 
            this.btnZoomMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMinus.Location = new System.Drawing.Point(70, 35);
            this.btnZoomMinus.Name = "btnZoomMinus";
            this.btnZoomMinus.Size = new System.Drawing.Size(39, 34);
            this.btnZoomMinus.TabIndex = 1;
            this.btnZoomMinus.Text = "-";
            this.btnZoomMinus.Click += new System.EventHandler(this.btnZoomMinus_Click);
            // 
            // btnZoomPlus
            // 
            this.btnZoomPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomPlus.Location = new System.Drawing.Point(19, 35);
            this.btnZoomPlus.Name = "btnZoomPlus";
            this.btnZoomPlus.Size = new System.Drawing.Size(39, 34);
            this.btnZoomPlus.TabIndex = 0;
            this.btnZoomPlus.Text = "+";
            this.btnZoomPlus.Click += new System.EventHandler(this.btnZoomPlus_Click);
            // 
            // fontChooser
            // 
            this.fontChooser.Color = System.Drawing.Color.White;
            this.fontChooser.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontChooser.ShowEffects = false;
            // 
            // orGentaPageSetup
            // 
            this.orGentaPageSetup.Document = this.prtGridPrintDoc;
            // 
            // btnPrintSetup
            // 
            this.btnPrintSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintSetup.Location = new System.Drawing.Point(467, 104);
            this.btnPrintSetup.Name = "btnPrintSetup";
            this.btnPrintSetup.Size = new System.Drawing.Size(120, 34);
            this.btnPrintSetup.TabIndex = 6;
            this.btnPrintSetup.Text = "Setup ...";
            this.btnPrintSetup.Click += new System.EventHandler(this.btnPrintSetup_Click);
            // 
            // AdvancedPrint
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(608, 389);
            this.Controls.Add(this.btnPrintSetup);
            this.Controls.Add(this.gBoxZoom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnFonts);
            this.Controls.Add(this.prtPreviewWindow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.gBoxZoom.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Printing
		public void btnPrint_Click(object sender, System.EventArgs e)
		{
			//	Initiate actual printing
			try
			{
				this.prtGridPrintDoc.Print();
			}
			catch {
                bool debugstop = true;
                // TODO: if in testing mode write out the error exception here and its stacktrace
            }
		}

		private void prtGridPrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			dsLineCounter = 0;
		}

		private void prtGridPrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
		{
            SetupPageAndCols(ev);

			//	initialize work variables for the page
			string PrintWorkLine = "";
			string[] TestCategArray;
			string RightMostCat;
			string PrevPrintCateg = "";
			object dbItemToConvert;

			while ((countOfLinesPrinted < linesPerPage) && (dsLineCounter < ItemRowCount)) 
			{
				//	initialize work variables for the present print line
				int MostLinesThisRow = 0;
				int linesReqdToPrint;
				int charsActuallyPrinted;
				int colCounter;

				//	one column at a time
                for (colCounter = 0; colCounter <= numOfPrintCols; colCounter++)
				{
                    // TODO: add setting to print entire category path on its own printline
					if (colCounter == 0)			// category
					{
                        PrintChangedCatHeader(ref PrintWorkLine, ref PrevPrintCateg, out TestCategArray, out RightMostCat);
					}
					else
					    { dbItemToConvert = PrintColumnData(ref PrintWorkLine, colCounter); }

					//	get our present starting vertical location
					yPos = topMargin + (countOfLinesPrinted * printFont.GetHeight(ev.Graphics));

					//	set the graphical bounds for this print column
					RectangleF OutputRectangle = new RectangleF(ev.MarginBounds.Left + colStartLocs[colCounter], yPos, 
						columnsAdjusted[colCounter], ev.MarginBounds.Height - (yPos - ev.MarginBounds.Top));

					//	do the actual printing
					ev.Graphics.DrawString (PrintWorkLine, printFont, Brushes.Black, OutputRectangle, StringFormatParms);

					//	save the amount of actual lines the printing required
					ev.Graphics.MeasureString(PrintWorkLine, printFont, OutputRectangle.Size, StringFormatParms, out charsActuallyPrinted, out linesReqdToPrint);

					//	if its the maxmimum sofar for this row, then save it
					if (linesReqdToPrint > MostLinesThisRow)
						{ MostLinesThisRow = linesReqdToPrint;}
				}

				//	Notes get printed following the item
                Byte hasNote = (Byte)PrintItems[0, dsLineCounter].Value;
				if (hasNote == 1)   
				{
                    MostLinesThisRow = PrintTheNote(ev, MostLinesThisRow, out linesReqdToPrint, out charsActuallyPrinted);
				}

				//	increment dataset counter and page-line counter
				dsLineCounter++;
				countOfLinesPrinted = countOfLinesPrinted + MostLinesThisRow;
			}

			//	set flagging to show if there's still more
			if (dsLineCounter < ItemRowCount) 
			    { ev.HasMorePages = true; }
			else 
			    { ev.HasMorePages = false; }
		}

        private object PrintColumnData(ref string PrintWorkLine, int colCounter)
        {
            object dbItemToConvert = new object();

            //	see if this is one of the Date-type fields
            string ColType = PrintItems[colCounter, dsLineCounter].ValueType.Name;
            if (ColType == "DateTime")  
            {
                //	change display of field to DateTime
                try
                {
                    dbItemToConvert = PrintItems[colCounter, dsLineCounter].Value;
                }
                catch { dbItemToConvert = ""; }
                if (dbItemToConvert != DBNull.Value)
                    { PrintWorkLine = Convert.ToDateTime(dbItemToConvert).ToShortDateString(); }
                else
                    { PrintWorkLine = ""; }
            }
            else
            {
                //	not a date field, just print it as it is
                try
                {
                    PrintWorkLine = PrintItems[colCounter, dsLineCounter].Value.ToString();
                }
                catch { PrintWorkLine = ""; }
            }
            return dbItemToConvert;
        }

        private void PrintChangedCatHeader(ref string PrintWorkLine, ref string PrevPrintCateg, out string[] TestCategArray, out string RightMostCat)
        {
            //	get the last element in the category path
            TestCategArray = SavedCategories[dsLineCounter].ToString().Split(new char[] { '\\' });
            RightMostCat = TestCategArray[TestCategArray.Length - 1];
            if (PrevPrintCateg == RightMostCat)
            {
                //	supress printing of duplicates from previous line
                PrintWorkLine = "";
            }
            else
            {
                //	print the category name
                PrintWorkLine = RightMostCat;
                PrevPrintCateg = RightMostCat;
            }
        }

        private int PrintTheNote(System.Drawing.Printing.PrintPageEventArgs ev, int MostLinesThisRow, out int linesReqdToPrint, out int charsActuallyPrinted)
        {
            //	Read the note from the dB
            string NoteTextToShow = "";
            int thisItemKey = Convert.ToInt16(ItemIDs[dsLineCounter]);
            string GetItemCmd = "SELECT NoteValue FROM Notes WHERE ItemID = " + thisItemKey.ToString();
            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = GetItemCmd;

            try
            {
                NoteTextToShow = (string)cmd.ExecuteScalar();
                if (NoteTextToShow == null)
                { NoteTextToShow = ""; }
            }
            catch
            { NoteTextToShow = ""; }

            //	set current position and allowable area for printing
            yPos = topMargin + ((countOfLinesPrinted + MostLinesThisRow) * printFont.GetHeight(ev.Graphics));
            RectangleF NoteRectangle = new RectangleF(ev.MarginBounds.Left + 50, yPos,
                ev.MarginBounds.Width, ev.MarginBounds.Height - (yPos - ev.MarginBounds.Top));

            //	print the note and see how large it was
            ev.Graphics.DrawString(NoteTextToShow, printFont, Brushes.Black, NoteRectangle, StringFormatParms);
            ev.Graphics.MeasureString(NoteTextToShow, printFont, NoteRectangle.Size, StringFormatParms, out charsActuallyPrinted, out linesReqdToPrint);
            MostLinesThisRow += linesReqdToPrint;
            return MostLinesThisRow;
        }

        private void SetupPageAndCols(System.Drawing.Printing.PrintPageEventArgs ev)
        {
            //	set up initial variables for the page
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            yPos = 0;
            countOfLinesPrinted = 0;

            //	initialize printing "format" parameter
            StringFormatParms = new StringFormat();
            StringFormatParms.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.FitBlackBox;

            //	retrieve the current side-margin setttings
            leftMargin = ev.MarginBounds.Left;
            topMargin = ev.MarginBounds.Top;

            //	call the subroutines that determine the column widths
            columnWidths = ComputeColWidths(ev.Graphics);
            columnsAdjusted = AdjustColumns(columnWidths, ev.MarginBounds.Width);

            //	compute the starting horizontal location for each column
            colStartLocs = new int[columnsAdjusted.Length];
            PreviousStartLoc = 0;
            colStartLocs[0] = 0;
            for (int i = 1; i < columnsAdjusted.Length; i++)
            {
                colStartLocs[i] = PreviousStartLoc + columnsAdjusted[i - 1];
                PreviousStartLoc = colStartLocs[i];
            }
        }

		private int[] AdjustColumns(int[] columnWidths, int pageWidthAvail)
		{
			//	first count how many columns are available for shrinking
			int TotalWidth = 0;
			int targetColsToShrink = 0;
			for (int i = 0; i < columnWidths.Length; i++)
			{
				TotalWidth += columnWidths[i];
				if ((columnWidths[i] > DateWidth) && (i > 0))
				{
					targetColsToShrink++;
				}
			}

			//	do we need to shrink anyway?
			if (TotalWidth <= pageWidthAvail)
			{
				return columnWidths;
			}

			//	how much should we shrink each column
			int AmountToShrink = (int) ((TotalWidth - pageWidthAvail) / targetColsToShrink);

			//	go ahead and shrink 'em
			for (int i = 0; i < columnWidths.Length; i++)
			{
				if ((columnWidths[i] > DateWidth) && (i > 0))
				{
					columnWidths[i] = columnWidths[i] - AmountToShrink;
				}
			}
			return columnWidths;
		}
        
		private int[] ComputeColWidths(Graphics pageGraphics)
		{
            int[] ComputedWidths = new int[numOfPrintCols + 1];

            FindLongestCategory(pageGraphics, ComputedWidths);

			//	find the longest string for the description
            CalcAFieldLength(pageGraphics, ComputedWidths, 1);

            //	use a standard long date value for date-formatted fields
            DateWidth = (int)(pageGraphics.MeasureString("12/33/2888", printFont).Width + 2);

            //  second printed field is a datetime
            ComputedWidths[2] = DateWidth;
			
			return ComputedWidths;
		}

        private void CalcAFieldLength(Graphics pageGraphics, int[] ComputedWidths, int colCounter)
        {
            int maxLenField = 0;
            int rowCounter = 0;
            string longestValue = "";

            //	loop through all values for this column
            while (rowCounter < ItemRowCount)
            {                
                try
                {
                    string testString = PrintItems[colCounter, rowCounter].Value.ToString();
                    if (testString.Length > maxLenField)
                    {
                        maxLenField = testString.Length;
                        longestValue = testString;
                    }
                }
                catch { }
                rowCounter++;
            }

            //	set size for this column
            ComputedWidths[colCounter] = (int)(pageGraphics.MeasureString(longestValue, printFont).Width * 1.05);
        }

        private void FindLongestCategory(Graphics pageGraphics, int[] ComputedWidths)
        {
            //	find the longest category
            int CatCount = 0;
            int maxCatSize = 0;
            int TestCatSize = 0;
            string[] TestCategArray;
            string RightMostCat;
            while (CatCount < SavedCategories.Count)
            {
                //	get the rightmost subcategory for each node
                TestCategArray = SavedCategories[CatCount].ToString().Split(new char[] { '\\' });
                RightMostCat = TestCategArray[TestCategArray.Length - 1] + "  ";

                //	use the category name before the dash
                TestCatSize = (int)(pageGraphics.MeasureString(RightMostCat, printFont).Width * 1.1);
                if (TestCatSize > maxCatSize)
                {
                    maxCatSize = TestCatSize;
                }
             
                CatCount++;
            }

            //	save the size of the largest category
            ComputedWidths[0] = maxCatSize;
        }
		#endregion

		#region Preview and Print Option Settings

		private void btnFonts_Click(object sender, System.EventArgs e)
		{
			//	user is changing the font
			this.fontChooser.ShowDialog(this);
			printFont = this.fontChooser.Font;
			this.prtPreviewWindow.InvalidatePreview();
		}

		public void btnPrintSetup_Click(object sender, System.EventArgs e)
		{
			//	user is changing the printer setup
			this.orGentaPageSetup.ShowDialog(this);
			this.prtPreviewWindow.InvalidatePreview();
		}

 		private void btnZoomPlus_Click(object sender, System.EventArgs e)
		{
			//	user is zooming in
			this.btnZoomMinus.Enabled = true;
			this.prtPreviewWindow.Zoom = this.prtPreviewWindow.Zoom + .25;
			if (this.prtPreviewWindow.Zoom == 2)
			{
				this.btnZoomPlus.Enabled = false;
			}
		}

		private void btnZoomMinus_Click(object sender, System.EventArgs e)
		{
			//	user is zooming out
			this.btnZoomPlus.Enabled = true;
			this.prtPreviewWindow.Zoom = this.prtPreviewWindow.Zoom - .25;
			if (this.prtPreviewWindow.Zoom == .25)
			{
				this.btnZoomMinus.Enabled = false;
			}
		}

		#endregion

		#region Form Disposing
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
		#endregion


    }
}
