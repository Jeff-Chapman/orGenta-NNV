namespace orGenta_NNv
{
    partial class ItemsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
  
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsForm));
            this.ItemGrid = new System.Windows.Forms.DataGridView();
            this.hasNoteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.itemDescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateCreatedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuItemsContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vwGetItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orGentaDBDataSet = new orGenta_NNv.orGentaDBDataSet();
            this.tbNewItem = new System.Windows.Forms.TextBox();
            this.btnNote = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splGridSplitter = new System.Windows.Forms.SplitContainer();
            this.btnChildItems = new System.Windows.Forms.Button();
            this.CachedGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cacheID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localCacheTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.localCacheDataSet = new orGenta_NNv.localCacheDataSet();
            this.label2 = new System.Windows.Forms.Label();
            this.pb2arrow = new System.Windows.Forms.PictureBox();
            this.tmrSoftAssign = new System.Windows.Forms.Timer(this.components);
            this.vw_Get_ItemsTableAdapter = new orGenta_NNv.orGentaDBDataSetTableAdapters.vw_Get_ItemsTableAdapter();
            this.localCacheTableTableAdapter = new orGenta_NNv.localCacheDataSetTableAdapters.localCacheTableTableAdapter();
            this.tableAdapterManager = new orGenta_NNv.localCacheDataSetTableAdapters.TableAdapterManager();
            this.ttChildItems = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ItemGrid)).BeginInit();
            this.menuItemsContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vwGetItemsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orGentaDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splGridSplitter)).BeginInit();
            this.splGridSplitter.Panel1.SuspendLayout();
            this.splGridSplitter.Panel2.SuspendLayout();
            this.splGridSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CachedGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localCacheTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localCacheDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2arrow)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemGrid
            // 
            this.ItemGrid.AllowUserToAddRows = false;
            this.ItemGrid.AutoGenerateColumns = false;
            this.ItemGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.ItemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hasNoteDataGridViewCheckBoxColumn,
            this.itemDescDataGridViewTextBoxColumn,
            this.dateCreatedDataGridViewTextBoxColumn,
            this.itemIDDataGridViewTextBoxColumn});
            this.ItemGrid.ContextMenuStrip = this.menuItemsContext;
            this.ItemGrid.DataSource = this.vwGetItemsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.ItemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ItemGrid.Location = new System.Drawing.Point(0, 0);
            this.ItemGrid.Name = "ItemGrid";
            this.ItemGrid.RowHeadersWidth = 40;
            this.ItemGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemGrid.RowTemplate.Height = 28;
            this.ItemGrid.Size = new System.Drawing.Size(900, 424);
            this.ItemGrid.TabIndex = 0;
            this.ItemGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemGrid_CellClick);
            this.ItemGrid.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.ItemGrid_CellContextMenuStripNeeded);
            this.ItemGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemGrid_CellEndEdit);
            this.ItemGrid.SelectionChanged += new System.EventHandler(this.ItemGrid_SelectionChanged);
            this.ItemGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.ItemGrid_UserDeletingRow);
            this.ItemGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ItemGrid_MouseDown);
            this.ItemGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ItemGrid_MouseUp);
            // 
            // hasNoteDataGridViewCheckBoxColumn
            // 
            this.hasNoteDataGridViewCheckBoxColumn.DataPropertyName = "hasNote";
            this.hasNoteDataGridViewCheckBoxColumn.HeaderText = "Note";
            this.hasNoteDataGridViewCheckBoxColumn.MinimumWidth = 8;
            this.hasNoteDataGridViewCheckBoxColumn.Name = "hasNoteDataGridViewCheckBoxColumn";
            this.hasNoteDataGridViewCheckBoxColumn.Width = 40;
            // 
            // itemDescDataGridViewTextBoxColumn
            // 
            this.itemDescDataGridViewTextBoxColumn.DataPropertyName = "ItemDesc";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.itemDescDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.itemDescDataGridViewTextBoxColumn.HeaderText = "Item Description";
            this.itemDescDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.itemDescDataGridViewTextBoxColumn.Name = "itemDescDataGridViewTextBoxColumn";
            this.itemDescDataGridViewTextBoxColumn.Width = 490;
            // 
            // dateCreatedDataGridViewTextBoxColumn
            // 
            this.dateCreatedDataGridViewTextBoxColumn.DataPropertyName = "DateCreated";
            this.dateCreatedDataGridViewTextBoxColumn.HeaderText = "Created";
            this.dateCreatedDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.dateCreatedDataGridViewTextBoxColumn.Name = "dateCreatedDataGridViewTextBoxColumn";
            this.dateCreatedDataGridViewTextBoxColumn.Width = 150;
            // 
            // itemIDDataGridViewTextBoxColumn
            // 
            this.itemIDDataGridViewTextBoxColumn.DataPropertyName = "ItemID";
            this.itemIDDataGridViewTextBoxColumn.HeaderText = "ItemID";
            this.itemIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.itemIDDataGridViewTextBoxColumn.Name = "itemIDDataGridViewTextBoxColumn";
            this.itemIDDataGridViewTextBoxColumn.Visible = false;
            this.itemIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // menuItemsContext
            // 
            this.menuItemsContext.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuItemsContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem,
            this.assignToToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem});
            this.menuItemsContext.Name = "menuItemsContext";
            this.menuItemsContext.Size = new System.Drawing.Size(173, 106);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(172, 32);
            this.zoomToolStripMenuItem.Text = "Zoom";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem_Click);
            // 
            // assignToToolStripMenuItem
            // 
            this.assignToToolStripMenuItem.Name = "assignToToolStripMenuItem";
            this.assignToToolStripMenuItem.Size = new System.Drawing.Size(172, 32);
            this.assignToToolStripMenuItem.Text = "Assign To...";
            this.assignToToolStripMenuItem.Click += new System.EventHandler(this.assignToToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(172, 32);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // vwGetItemsBindingSource
            // 
            this.vwGetItemsBindingSource.DataMember = "vw_Get_Items";
            this.vwGetItemsBindingSource.DataSource = this.orGentaDBDataSet;
            this.vwGetItemsBindingSource.Sort = "DateCreated DESC";
            // 
            // orGentaDBDataSet
            // 
            this.orGentaDBDataSet.DataSetName = "orGentaDBDataSet";
            this.orGentaDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tbNewItem
            // 
            this.tbNewItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNewItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNewItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNewItem.Location = new System.Drawing.Point(0, 0);
            this.tbNewItem.Name = "tbNewItem";
            this.tbNewItem.Size = new System.Drawing.Size(778, 30);
            this.tbNewItem.TabIndex = 0;
            this.tbNewItem.Click += new System.EventHandler(this.tbNewItem_Click);
            this.tbNewItem.TextChanged += new System.EventHandler(this.tbNewItem_TextChanged);
            // 
            // btnNote
            // 
            this.btnNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNote.Enabled = false;
            this.btnNote.Location = new System.Drawing.Point(778, 0);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(71, 31);
            this.btnNote.TabIndex = 2;
            this.btnNote.Text = "+Note";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(848, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(52, 31);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Type new items here...";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // splGridSplitter
            // 
            this.splGridSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splGridSplitter.Location = new System.Drawing.Point(0, 29);
            this.splGridSplitter.Name = "splGridSplitter";
            this.splGridSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splGridSplitter.Panel1
            // 
            this.splGridSplitter.Panel1.Controls.Add(this.btnChildItems);
            this.splGridSplitter.Panel1.Controls.Add(this.ItemGrid);
            // 
            // splGridSplitter.Panel2
            // 
            this.splGridSplitter.Panel2.AutoScroll = true;
            this.splGridSplitter.Panel2.Controls.Add(this.CachedGrid);
            this.splGridSplitter.Panel2.Controls.Add(this.label2);
            this.splGridSplitter.Panel2MinSize = 24;
            this.splGridSplitter.Size = new System.Drawing.Size(900, 523);
            this.splGridSplitter.SplitterDistance = 424;
            this.splGridSplitter.TabIndex = 7;
            this.splGridSplitter.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.splGridSplitter_SplitterMoving);
            this.splGridSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splGridSplitter_SplitterMoved);
            // 
            // btnChildItems
            // 
            this.btnChildItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChildItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChildItems.Location = new System.Drawing.Point(854, 2);
            this.btnChildItems.Margin = new System.Windows.Forms.Padding(0);
            this.btnChildItems.Name = "btnChildItems";
            this.btnChildItems.Size = new System.Drawing.Size(46, 26);
            this.btnChildItems.TabIndex = 1;
            this.btnChildItems.Text = "+ +";
            this.btnChildItems.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ttChildItems.SetToolTip(this.btnChildItems, "Include Child Items");
            this.btnChildItems.UseVisualStyleBackColor = true;
            this.btnChildItems.Click += new System.EventHandler(this.btnChildItems_Click);
            // 
            // CachedGrid
            // 
            this.CachedGrid.AllowUserToAddRows = false;
            this.CachedGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CachedGrid.AutoGenerateColumns = false;
            this.CachedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CachedGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.cacheID});
            this.CachedGrid.DataSource = this.localCacheTableBindingSource;
            this.CachedGrid.Location = new System.Drawing.Point(0, 25);
            this.CachedGrid.Name = "CachedGrid";
            this.CachedGrid.RowHeadersWidth = 62;
            this.CachedGrid.RowTemplate.Height = 28;
            this.CachedGrid.Size = new System.Drawing.Size(900, 70);
            this.CachedGrid.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Category";
            this.dataGridViewTextBoxColumn1.HeaderText = "Category";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ParentID";
            this.dataGridViewTextBoxColumn2.HeaderText = "ParentID";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ItemDesc";
            this.dataGridViewTextBoxColumn3.HeaderText = "ItemDesc";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DateCreated";
            this.dataGridViewTextBoxColumn4.HeaderText = "DateCreated";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "NoteValue";
            this.dataGridViewTextBoxColumn5.HeaderText = "NoteValue";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "KBname";
            this.dataGridViewTextBoxColumn6.HeaderText = "KBname";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // cacheID
            // 
            this.cacheID.DataPropertyName = "cacheID";
            this.cacheID.HeaderText = "cacheID";
            this.cacheID.MinimumWidth = 8;
            this.cacheID.Name = "cacheID";
            this.cacheID.Visible = false;
            this.cacheID.Width = 150;
            // 
            // localCacheTableBindingSource
            // 
            this.localCacheTableBindingSource.DataMember = "localCacheTable";
            this.localCacheTableBindingSource.DataSource = this.localCacheDataSet;
            // 
            // localCacheDataSet
            // 
            this.localCacheDataSet.DataSetName = "localCacheDataSet";
            this.localCacheDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(422, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Cached Items (not yet replicated to DB) are shown below...";
            // 
            // pb2arrow
            // 
            this.pb2arrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pb2arrow.Image = ((System.Drawing.Image)(resources.GetObject("pb2arrow.Image")));
            this.pb2arrow.Location = new System.Drawing.Point(840, 378);
            this.pb2arrow.Name = "pb2arrow";
            this.pb2arrow.Size = new System.Drawing.Size(30, 71);
            this.pb2arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb2arrow.TabIndex = 1;
            this.pb2arrow.TabStop = false;
            // 
            // tmrSoftAssign
            // 
            this.tmrSoftAssign.Interval = 250;
            this.tmrSoftAssign.Tick += new System.EventHandler(this.tmrSoftAssign_Tick);
            // 
            // vw_Get_ItemsTableAdapter
            // 
            this.vw_Get_ItemsTableAdapter.ClearBeforeFill = true;
            // 
            // localCacheTableTableAdapter
            // 
            this.localCacheTableTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.localCacheTableTableAdapter = this.localCacheTableTableAdapter;
            this.tableAdapterManager.UpdateOrder = orGenta_NNv.localCacheDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ItemsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 545);
            this.Controls.Add(this.pb2arrow);
            this.Controls.Add(this.splGridSplitter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnNote);
            this.Controls.Add(this.tbNewItem);
            this.Name = "ItemsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "dbName :: Category";
            this.Activated += new System.EventHandler(this.ItemsForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemsForm_FormClosing);
            this.Load += new System.EventHandler(this.ItemsForm_Load);
            this.ResizeBegin += new System.EventHandler(this.ItemsForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.ItemsForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.ItemGrid)).EndInit();
            this.menuItemsContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vwGetItemsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orGentaDBDataSet)).EndInit();
            this.splGridSplitter.Panel1.ResumeLayout(false);
            this.splGridSplitter.Panel2.ResumeLayout(false);
            this.splGridSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splGridSplitter)).EndInit();
            this.splGridSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CachedGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localCacheTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localCacheDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2arrow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNote;
        private System.Windows.Forms.Label label1;
        public orGentaDBDataSet orGentaDBDataSet;
        private System.Windows.Forms.BindingSource vwGetItemsBindingSource;
        private orGentaDBDataSetTableAdapters.vw_Get_ItemsTableAdapter vw_Get_ItemsTableAdapter;

        private System.Windows.Forms.SplitContainer splGridSplitter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pb2arrow;
        private System.Windows.Forms.Timer tmrSoftAssign;
        public System.Windows.Forms.TextBox tbNewItem;
        public System.Windows.Forms.DataGridView ItemGrid;
        private System.Windows.Forms.ContextMenuStrip menuItemsContext;
        private System.Windows.Forms.ToolStripMenuItem assignToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private localCacheDataSet localCacheDataSet;
        private System.Windows.Forms.BindingSource localCacheTableBindingSource;
        private localCacheDataSetTableAdapters.localCacheTableTableAdapter localCacheTableTableAdapter;
        private localCacheDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView CachedGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn cacheID;
        private System.Windows.Forms.Button btnChildItems;
        private System.Windows.Forms.ToolTip ttChildItems;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasNoteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemDescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateCreatedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemIDDataGridViewTextBoxColumn;
        public System.Windows.Forms.Button btnOK;
    }
}