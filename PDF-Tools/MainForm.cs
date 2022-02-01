using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PDF_Tools
{
    public partial class MainForm : Form
    {
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private string _destinationFilePath;
        private int[] _splitPageRange;

        /// <summary>
        /// Initialize background worker.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// Set tool tip for Picture Box control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            toolTip.AutoPopDelay = 10000;
            toolTip.SetToolTip(pictureBox, "Page Range: 3 or 3,5,7 or 3-7");
        }

        /// <summary>
        /// Set enable, disable appending after specific page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbAppend_CheckedChanged(object sender, EventArgs e)
        {
            lblSpecificPage.Enabled = tbAppendPage.Enabled = rbAppend.Checked;
        }

        /// <summary>
        /// Set enable, disable splitting ability
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbSplit_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox.Enabled = tbSplit.Enabled = rbSplit.Checked;
        }

        /// <summary>
        /// Prevent any char expected numbers, -, backspace and Comma. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbSplit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) &&
                e.KeyChar != '-' &&
                e.KeyChar != ',' &&
                e.KeyChar != '\b')
                e.Handled = true;
        }

        /// <summary>
        /// Prevent any char expect numbers and backspace.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbSpecificPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        /// <summary>
        /// Get the destination file from the user by open dialog box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDestinationFile_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = @"PDF File (*.pdf)|*.pdf",
            };

            // Append job won't overwrite on selected file, so doesn't need the overwrite prompt.
            if (rbAppend.Checked)
                saveFileDialog.OverwritePrompt = false;

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            foreach (var item in listBox.Items)
            {
                if (saveFileDialog.FileName != item.ToString()) continue;
                MessageBox.Show
                (saveFileDialog.FileName + @" is already exist in the selected files", @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (Path.GetExtension(saveFileDialog.FileName) != ".pdf")
            {
                MessageBox.Show
                  (saveFileDialog.FileName + @" extension is not PDF, try again", @"Error",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);

                return;
            }

            _destinationFilePath = saveFileDialog.FileName;
            lblStatus.Text = @"Status: Destination file is  " + _destinationFilePath;
        }

        /// <summary>
        /// Get PDF file(s) from user to do the tools jobs on them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectFiles_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = @"PDF Files (*.pdf)|*.pdf",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            foreach (var fileName in openFileDialog.FileNames)
            {
                if (Path.GetExtension(fileName) != ".pdf")
                    MessageBox.Show
                    (@"Selected file: " + fileName + @" is not PDF file", @"Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else if (fileName == _destinationFilePath)
                    MessageBox.Show
                    (@"Selected file: " + fileName + @" is already selected as destination file", @"Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else
                    listBox.Items.Add(fileName);
            }
        }

        /// <summary>
        ///  Move selected item up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex <= 0) return;

            listBox.Items.Insert(listBox.SelectedIndex - 1, listBox.SelectedItem);
            listBox.SelectedIndex = listBox.SelectedIndex - 2;
            listBox.Items.RemoveAt(listBox.SelectedIndex + 2);
        }

        /// <summary>
        /// Move selected item down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1 || listBox.SelectedIndex >= listBox.Items.Count - 1) return;

            listBox.Items.Insert(listBox.SelectedIndex + 2, listBox.SelectedItem);
            listBox.SelectedIndex = listBox.SelectedIndex + 2;
            listBox.Items.RemoveAt(listBox.SelectedIndex - 2);
        }

        /// <summary>
        /// Check validations and run background worker, and run the background worker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_destinationFilePath))
            {
                MessageBox.Show(@"Definition file is not set!", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listBox.Items.Count == 0)
            {
                MessageBox.Show(@"There is no PDF files in the List!", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbMerge.Checked && listBox.Items.Count < 2)
            {
                MessageBox.Show(@"For merging, select at least two PDF file!", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rbSplit.Checked)
            {
                if (listBox.Items.Count > 1)
                {
                    MessageBox.Show(@"For splitting, select just one PDF file!", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (string.IsNullOrEmpty(tbSplit.Text))
                {
                    MessageBox.Show(@"Page range for splitting is empty!", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(tbSplit.Text))
                {
                    MessageBox.Show(@"Page range is not set!", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _splitPageRange = ExtractPagesRange(tbSplit.Text);
                if (_splitPageRange == null)
                {
                    MessageBox.Show(@"Page range is not valid!", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            progressBar.Value = 0;

            // Set ProgressBar maximum base on selected job, splitting or the other jobs.
            progressBar.Maximum = _splitPageRange?.Length ?? listBox.Items.Count;
            DisableButtons();

            // If all validation is current ... 
            if (_backgroundWorker.IsBusy) return;
            _backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Delete single PDF files address from ListBox control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1) return;
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        /// <summary>
        /// Delete all PDF selected file path from ListBox control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
        }

        /// <summary>
        /// Background worker error handler and enable buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(e.Error.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            EnableButtons();
        }

        /// <summary>
        /// Update ProgressBar and percent Label after each file append, merged or each page split.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == progressBar.Maximum)
            {
                progressBar.Maximum = e.ProgressPercentage + 1;
                progressBar.Value = e.ProgressPercentage + 1;
                progressBar.Maximum = e.ProgressPercentage;
            }
            else progressBar.Value = e.ProgressPercentage + 1;

            lblPrecent.Text = (progressBar.Value * 100 / progressBar.Maximum) + @"%";
        }

        /// <summary>
        /// Run a properly method base on selected job in background. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var pdfTools = new PdfTools();

            // Get the update progress changes.
            pdfTools.OnUpdateProgress += PdfTools_OnUpdateProgress;

            try
            {
                if (rbMerge.Checked)
                    pdfTools.Merge(listBox.Items.OfType<string>().ToArray(), _destinationFilePath);
                else if (rbSplit.Checked)
                    pdfTools.Split(listBox.Items[0].ToString(), _destinationFilePath, _splitPageRange);
                else if (rbAppend.Checked && string.IsNullOrEmpty(tbAppendPage.Text))
                    pdfTools.Append(listBox.Items.OfType<string>().ToArray(), _destinationFilePath);
                else if (rbAppend.Checked && !string.IsNullOrEmpty(tbAppendPage.Text))
                    pdfTools.AppendPage(listBox.Items.OfType<string>().ToArray(), _destinationFilePath,
                        int.Parse(tbAppendPage.Text));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Update progress bar value when progress event in the PdfTools class changed.
        /// The progressBar changes in the _backgroundWorker.ReportProgress event.
        /// </summary>
        /// <param name="progressAmount"></param>
        private void PdfTools_OnUpdateProgress(int progressAmount)
        {
            _backgroundWorker.ReportProgress(progressAmount);
        }

        /// <summary>
        /// Disable buttons during background worker is working on the PDF files.
        /// </summary>
        private void DisableButtons()
        {
            btnDelete.Enabled = btnDeleteAll.Enabled = btnDestinationFile.Enabled =
                btnDown.Enabled = btnUp.Enabled = btnSelectFiles.Enabled = btnRun.Enabled = false;
        }

        /// <summary>
        /// Enable buttons after background worker is done or stop with an error.
        /// </summary>
        private void EnableButtons()
        {
            btnDelete.Enabled = btnDeleteAll.Enabled = btnDestinationFile.Enabled =
                btnDown.Enabled = btnUp.Enabled = btnSelectFiles.Enabled = btnRun.Enabled = true;
        }

        /// <summary>
        /// Extract the pages from the split TextBox.
        /// </summary>
        /// <param name="text">Range of numbers in 3 or 3,5,7 or 3-7 format </param>
        /// <returns></returns>
        private int[] ExtractPagesRange(string text)
        {
            // Step 1: Set pattern to prevent wrong chars in the range
            const string patterns = ",,|--|-,|,-|^,|^-|-$|,$|[0-9]+-[0-9]+-";
            var pageNumbers = new List<int>();

            // Step 2: The above patterns must not be see in the text.
            if (Regex.IsMatch(text, patterns))
                return null;

            // Step 3: If the range is in invalid format.
            // Split numbers in text from 2,6,7-10 to 2 6 7-10
            foreach (var item in text.Split(','))
            {
                // Extract single numbers.
                if (!item.Contains("-"))
                    pageNumbers.Add(int.Parse(item));
                else
                {
                    // Extract rage numbers like 3-5 or 9-6
                    var rangeTemp = item.Split('-');
                    var num1 = int.Parse(rangeTemp[0]);
                    var num2 = int.Parse(rangeTemp[1]);

                    // For example 3 to 5
                    if (num1 < num2)
                        for (var i = num1; i < num2 + 1; i++)
                            pageNumbers.Add(i);
                    // For example 5 - 3
                    else if (num1 > num2)
                        for (var i = num2; i < num1 - 1; i--)
                            pageNumbers.Add(i);
                    // For example 3 to 3
                    else pageNumbers.Add(num1);
                }
            }
            return pageNumbers.ToArray();
        }
    }
}
