namespace JsonGeneratorApp.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            topPanel = new Panel();
            txtDirectory = new TextBox();
            lblFolder = new Label();
            btnSelectFolder = new Button();
            bottomPanel = new Panel();
            lblStatus = new Label();
            btnExport = new Button();
            splitContainer = new SplitContainer();
            dgvRecords = new DataGridView();
            lstLogs = new ListBox();
            lblEventLog = new Label();
            topPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecords).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.Controls.Add(txtDirectory);
            topPanel.Controls.Add(lblFolder);
            topPanel.Controls.Add(btnSelectFolder);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(12, 13);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(860, 45);
            topPanel.TabIndex = 0;
            // 
            // txtDirectory
            // 
            txtDirectory.Dock = DockStyle.Fill;
            txtDirectory.Location = new Point(70, 0);
            txtDirectory.Name = "txtDirectory";
            txtDirectory.ReadOnly = true;
            txtDirectory.Size = new Size(650, 35);
            txtDirectory.TabIndex = 2;
            // 
            // lblFolder
            // 
            lblFolder.AutoSize = true;
            lblFolder.Dock = DockStyle.Left;
            lblFolder.Location = new Point(0, 0);
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new Size(70, 30);
            lblFolder.TabIndex = 1;
            lblFolder.Text = "Folder";
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Dock = DockStyle.Right;
            btnSelectFolder.Location = new Point(720, 0);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(140, 45);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Select folder...";
            btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(lblStatus);
            bottomPanel.Controls.Add(btnExport);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(12, 503);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(860, 45);
            bottomPanel.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Left;
            lblStatus.Location = new Point(0, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(276, 30);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Select a folder to get started";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnExport
            // 
            btnExport.BackColor = SystemColors.Highlight;
            btnExport.Dock = DockStyle.Right;
            btnExport.Enabled = false;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(700, 0);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(160, 45);
            btnExport.TabIndex = 0;
            btnExport.Text = "Save report...";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(12, 58);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(dgvRecords);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(lstLogs);
            splitContainer.Panel2.Controls.Add(lblEventLog);
            splitContainer.Size = new Size(860, 445);
            splitContainer.SplitterDistance = 286;
            splitContainer.TabIndex = 2;
            // 
            // dgvRecords
            // 
            dgvRecords.AllowUserToAddRows = false;
            dgvRecords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecords.Dock = DockStyle.Fill;
            dgvRecords.Location = new Point(0, 0);
            dgvRecords.Name = "dgvRecords";
            dgvRecords.ReadOnly = true;
            dgvRecords.RowHeadersVisible = false;
            dgvRecords.RowHeadersWidth = 72;
            dgvRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecords.Size = new Size(860, 286);
            dgvRecords.TabIndex = 0;
            // 
            // lstLogs
            // 
            lstLogs.Dock = DockStyle.Fill;
            lstLogs.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lstLogs.FormattingEnabled = true;
            lstLogs.Location = new Point(0, 30);
            lstLogs.Name = "lstLogs";
            lstLogs.Size = new Size(860, 125);
            lstLogs.TabIndex = 1;
            // 
            // lblEventLog
            // 
            lblEventLog.AutoSize = true;
            lblEventLog.Dock = DockStyle.Top;
            lblEventLog.Location = new Point(0, 0);
            lblEventLog.Name = "lblEventLog";
            lblEventLog.Size = new Size(104, 30);
            lblEventLog.TabIndex = 0;
            lblEventLog.Text = "Event log:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(splitContainer);
            Controls.Add(bottomPanel);
            Controls.Add(topPanel);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 2, 4, 2);
            MinimumSize = new Size(700, 450);
            Name = "MainForm";
            Padding = new Padding(12, 13, 12, 13);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ETL File Watcher - Report Generator";
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRecords).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private TextBox txtDirectory;
        private Label lblFolder;
        private Button btnSelectFolder;
        private Panel bottomPanel;
        private Label lblStatus;
        private Button btnExport;
        private SplitContainer splitContainer;
        private DataGridView dgvRecords;
        private ListBox lstLogs;
        private Label lblEventLog;
    }
}
