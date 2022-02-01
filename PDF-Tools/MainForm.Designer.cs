namespace PDF_Tools
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.btnDestinationFile = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.rbMerge = new System.Windows.Forms.RadioButton();
            this.rbAppend = new System.Windows.Forms.RadioButton();
            this.lblSpecificPage = new System.Windows.Forms.Label();
            this.rbSplit = new System.Windows.Forms.RadioButton();
            this.tbAppendPage = new System.Windows.Forms.TextBox();
            this.tbSplit = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblPrecent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(181, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status: Destination FIle is not set yet!";
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Location = new System.Drawing.Point(12, 34);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(101, 23);
            this.btnSelectFiles.TabIndex = 2;
            this.btnSelectFiles.Text = "Select PDF Files";
            this.btnSelectFiles.UseVisualStyleBackColor = true;
            this.btnSelectFiles.Click += new System.EventHandler(this.BtnSelectFiles_Click);
            // 
            // btnDestinationFile
            // 
            this.btnDestinationFile.Location = new System.Drawing.Point(119, 34);
            this.btnDestinationFile.Name = "btnDestinationFile";
            this.btnDestinationFile.Size = new System.Drawing.Size(101, 23);
            this.btnDestinationFile.TabIndex = 3;
            this.btnDestinationFile.Text = "Destination File";
            this.btnDestinationFile.UseVisualStyleBackColor = true;
            this.btnDestinationFile.Click += new System.EventHandler(this.BtnDestinationFile_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(12, 63);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(638, 290);
            this.listBox.TabIndex = 4;
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(575, 34);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAll.TabIndex = 5;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(494, 34);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(656, 75);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 7;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(656, 104);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 8;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // rbMerge
            // 
            this.rbMerge.AutoSize = true;
            this.rbMerge.Checked = true;
            this.rbMerge.Location = new System.Drawing.Point(15, 359);
            this.rbMerge.Name = "rbMerge";
            this.rbMerge.Size = new System.Drawing.Size(306, 17);
            this.rbMerge.TabIndex = 9;
            this.rbMerge.TabStop = true;
            this.rbMerge.Text = "Merge files by creating a new file or overwrite an existing file";
            this.rbMerge.UseVisualStyleBackColor = true;
            // 
            // rbAppend
            // 
            this.rbAppend.AutoSize = true;
            this.rbAppend.Location = new System.Drawing.Point(15, 382);
            this.rbAppend.Name = "rbAppend";
            this.rbAppend.Size = new System.Drawing.Size(164, 17);
            this.rbAppend.TabIndex = 10;
            this.rbAppend.Text = "Append files to an existing file";
            this.rbAppend.UseVisualStyleBackColor = true;
            this.rbAppend.CheckedChanged += new System.EventHandler(this.RbAppend_CheckedChanged);
            // 
            // lblSpecificPage
            // 
            this.lblSpecificPage.AutoSize = true;
            this.lblSpecificPage.Enabled = false;
            this.lblSpecificPage.Location = new System.Drawing.Point(32, 402);
            this.lblSpecificPage.Name = "lblSpecificPage";
            this.lblSpecificPage.Size = new System.Drawing.Size(292, 13);
            this.lblSpecificPage.TabIndex = 11;
            this.lblSpecificPage.Text = "After a specific page number (Zero means at the beginning): ";
            // 
            // rbSplit
            // 
            this.rbSplit.AutoSize = true;
            this.rbSplit.Location = new System.Drawing.Point(15, 421);
            this.rbSplit.Name = "rbSplit";
            this.rbSplit.Size = new System.Drawing.Size(209, 17);
            this.rbSplit.TabIndex = 12;
            this.rbSplit.Text = "Split page(s) and save them in new file:";
            this.rbSplit.UseVisualStyleBackColor = true;
            this.rbSplit.CheckedChanged += new System.EventHandler(this.RbSplit_CheckedChanged);
            // 
            // tbAppendPage
            // 
            this.tbAppendPage.Enabled = false;
            this.tbAppendPage.Location = new System.Drawing.Point(330, 399);
            this.tbAppendPage.MaxLength = 3;
            this.tbAppendPage.Name = "tbAppendPage";
            this.tbAppendPage.Size = new System.Drawing.Size(100, 20);
            this.tbAppendPage.TabIndex = 13;
            this.tbAppendPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbAppendPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSpecificPage_KeyPress);
            // 
            // tbSplit
            // 
            this.tbSplit.Enabled = false;
            this.tbSplit.Location = new System.Drawing.Point(330, 425);
            this.tbSplit.MaxLength = 12;
            this.tbSplit.Name = "tbSplit";
            this.tbSplit.Size = new System.Drawing.Size(100, 20);
            this.tbSplit.TabIndex = 14;
            this.tbSplit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSplit_KeyPress);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(539, 388);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 17;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::PDF_Tools.Properties.Resources.info2;
            this.pictureBox.Location = new System.Drawing.Point(436, 429);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(18, 16);
            this.pictureBox.TabIndex = 19;
            this.pictureBox.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(425, 359);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(189, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 20;
            // 
            // lblPrecent
            // 
            this.lblPrecent.AutoSize = true;
            this.lblPrecent.Location = new System.Drawing.Point(618, 363);
            this.lblPrecent.Name = "lblPrecent";
            this.lblPrecent.Size = new System.Drawing.Size(21, 13);
            this.lblPrecent.TabIndex = 21;
            this.lblPrecent.Text = "0%";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 450);
            this.Controls.Add(this.lblPrecent);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.tbSplit);
            this.Controls.Add(this.tbAppendPage);
            this.Controls.Add(this.rbSplit);
            this.Controls.Add(this.lblSpecificPage);
            this.Controls.Add(this.rbAppend);
            this.Controls.Add(this.rbMerge);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnDestinationFile);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF Tools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSelectFiles;
        private System.Windows.Forms.Button btnDestinationFile;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.RadioButton rbMerge;
        private System.Windows.Forms.RadioButton rbAppend;
        private System.Windows.Forms.Label lblSpecificPage;
        private System.Windows.Forms.RadioButton rbSplit;
        private System.Windows.Forms.TextBox tbAppendPage;
        private System.Windows.Forms.TextBox tbSplit;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblPrecent;
    }
}

