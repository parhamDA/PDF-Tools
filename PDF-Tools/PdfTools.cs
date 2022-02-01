using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace PDF_Tools
{
    public class PdfTools
    {
        /// <summary>
        /// Delegate for updating each jobs progress. 
        /// </summary>
        /// <param name="progressAmount">Amount of progress</param>
        public delegate void UpdateProgress(int progressAmount);

        /// <summary>
        /// The event which is connected to the UpdateProgress delegate.
        /// </summary>
        public event UpdateProgress OnUpdateProgress;

        /// <summary>
        /// Merge source files together and save them into destination file.
        /// </summary>
        /// <param name="sourceFiles">The list of source files</param>
        /// <param name="destinationFile">Destination file to save merged files into it</param>
        public void Merge(string[] sourceFiles, string destinationFile)
        {
            PdfDocument pdfDestination = null;
            PdfMerger pdfMerger = null;

            try
            {
                // Step 1: Temporary file to prevent rewrite on the original file.
                //         Destination file will be changed with the original in the last step.
                //         In case if the destination file in an existing file
                pdfDestination = new PdfDocument(new PdfWriter(destinationFile + "temp"));
                pdfMerger = new PdfMerger(pdfDestination);

                for (var i = 0; i < sourceFiles.Length; i++)
                    using (var pdfSource = new PdfDocument(new PdfReader(sourceFiles[i])))
                    {
                        pdfMerger.Merge(pdfSource, 1, pdfSource.GetNumberOfPages());

                        // Update merge job progress.
                        OnUpdateProgress?.Invoke(i);
                    }

                pdfDestination.Close();
                pdfMerger.Close();

                // Step 2: Replace the destination file with the temp one.
                File.Delete(destinationFile);
                File.Move(destinationFile + "temp", destinationFile);
            }
            catch (Exception e)
            {
                if (pdfDestination != null && !pdfDestination.IsClosed())
                {
                    pdfDestination.AddNewPage();
                    pdfDestination.Close();
                    pdfMerger?.Close();
                }

                File.Delete(destinationFile + "temp");
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Append source files to the end of original file.
        /// </summary>
        /// <param name="sourceFiles">The list of source files</param>
        /// <param name="originalFile">Original file to append source files to the end of it</param>
        public void Append(string[] sourceFiles, string originalFile)
        {
            PdfDocument pdfOriginal = null;
            PdfDocument pdfTemp = null;
            PdfMerger pdfMerger = null;

            try
            {
                pdfOriginal = new PdfDocument(new PdfReader(originalFile));

                // Step 1: Temporary file to prevent rewrite on the original file.
                //         Destination file will be changed with the original in the last step.
                //         In case if the destination file in an existing file
                pdfTemp = new PdfDocument(new PdfWriter(originalFile + "temp"));
                pdfMerger = new PdfMerger(pdfTemp);

                // Step 2: First add the original file content. 
                pdfMerger.Merge(pdfOriginal, 1, pdfOriginal.GetNumberOfPages());

                // Step 3: Then add the other source file content
                for (var i = 0; i < sourceFiles.Length; i++)
                    using (var sourceFile = new PdfDocument(new PdfReader(sourceFiles[i])))
                    {
                        pdfMerger.Merge(sourceFile, 1, sourceFile.GetNumberOfPages());

                        // Update merge job progress.
                        OnUpdateProgress?.Invoke(i);
                    }

                pdfTemp.Close();
                pdfOriginal.Close();
                pdfMerger.Close();

                // Step 4: Replace the original file with the temp one.
                File.Delete(originalFile);
                File.Move(originalFile + "temp", originalFile);
            }
            catch (Exception e)
            {
                if (pdfTemp != null && !pdfTemp.IsClosed())
                {
                    pdfTemp.Close();
                    pdfOriginal.Close();
                    pdfMerger?.Close();
                }

                File.Delete(originalFile + "temp");
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Append source files after a specific page number to the end of original file.
        /// </summary>
        /// <param name="sourceFiles">The list of source files</param>
        /// <param name="originalFile">Original file to append source files to the end of it</param>
        /// <param name="pageNumber">Specific page number to append source files after it</param>
        public void AppendPage(string[] sourceFiles, string originalFile, int pageNumber)
        {
            PdfDocument pdfOriginal = null;
            PdfDocument pdfTemp = null;
            PdfMerger pdfMerger = null;
            try
            {
                pdfOriginal = new PdfDocument(new PdfReader(originalFile));

                // Step 1: Temporary file to prevent rewrite on the original file.
                //         Destination file will be changed with the original in the last step.
                //         In case if the destination file in an existing file
                pdfTemp = new PdfDocument(new PdfWriter(originalFile + "temp"));

                pdfMerger = new PdfMerger(pdfTemp);

                // Step 2: First add the original file content from first page until pageNumber input parameter. 
                pdfMerger.Merge(pdfOriginal, 1, pageNumber);

                // Step 3: Then add the other source file content
                for (var i = 0; i < sourceFiles.Length; i++)
                    using (var pdfSourceFile = new PdfDocument(new PdfReader(sourceFiles[i])))
                    {
                        pdfMerger.Merge(pdfSourceFile, 1, pdfSourceFile.GetNumberOfPages());

                        // Update merge job progress.
                        OnUpdateProgress?.Invoke(i);
                    }

                // Step 4: Finally append the rest content from Original file,
                // From the pageNumber to end of the file, if the pageNumber is not the end page.
                if (pageNumber < pdfOriginal.GetNumberOfPages())
                    pdfMerger.Merge(pdfOriginal, pageNumber + 1, pdfOriginal.GetNumberOfPages());

                pdfTemp.Close();
                pdfOriginal.Close();
                pdfMerger.Close();

                // Step 5: Replace the original file with the temp one.
                File.Delete(originalFile);
                File.Move(originalFile + "temp", originalFile);
            }
            catch (Exception e)
            {
                if (pdfTemp != null && !pdfTemp.IsClosed())
                {
                    pdfTemp.AddNewPage();
                    pdfTemp.Close();

                    pdfOriginal.Close();
                    pdfMerger?.Close();
                }

                File.Delete(originalFile + "temp");
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Split specific pages from the source file and save them in the destination file.
        /// </summary>
        /// <param name="sourceFile">The source file to split pages from it</param>
        /// <param name="destinationFile">The destination file to save splitted pages into it</param>
        /// <param name="pageRange">Specific pages to split them</param>
        public void Split(string sourceFile, string destinationFile, int[] pageRange)
        {
            PdfDocument pdfDestination = null;
            PdfDocument pdfSource = null;
            PdfMerger pdfMerger = null;

            try
            {
                // Step 1: Temporary file to prevent rewrite on the original file.
                //         Destination file will be changed with the original in the last step.
                //         In case if the destination file in an existing file
                pdfDestination = new PdfDocument(new PdfWriter(destinationFile + "temp"));
                pdfMerger = new PdfMerger(pdfDestination);

                pdfSource = new PdfDocument(new PdfReader(sourceFile));

                // Extract and merge each page from the source page.
                for (var i = 0; i < pageRange.Length; i++)
                {
                    pdfMerger.Merge(pdfSource, pageRange[i], pageRange[i]);

                    OnUpdateProgress?.Invoke(i);
                }

                pdfDestination.Close();
                pdfMerger.Close();
                pdfSource.Close();

                // Step 5: Replace the original file with the temp one.
                File.Delete(destinationFile);
                File.Move(destinationFile + "temp", destinationFile);
            }
            catch (Exception e)
            {
                if (pdfDestination != null && !pdfDestination.IsClosed())
                {
                    pdfDestination.AddNewPage();
                    pdfDestination.Close();

                    pdfMerger?.Close();
                    pdfSource?.Close();
                }

                File.Delete(destinationFile + "temp");
                throw new Exception(e.Message);
            }
        }
    }
}
