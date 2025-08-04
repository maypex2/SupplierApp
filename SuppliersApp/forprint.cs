using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SuppliersApp
{
    public class forprint
    {
        private DataGridView dataGridView;
        private PrintDocument printDocument;
        private PrintPreviewDialog printPreviewDialog;
        private PrintDialog printDialog;
        private PageSetupDialog pageSetupDialog;
        private int rowIndex = 0;

        public forprint(DataGridView dgv)
        {
            dataGridView = dgv;
            InitializePrinting();
        }

        private void InitializePrinting()
        {
            printDocument = new PrintDocument();
            printPreviewDialog = new PrintPreviewDialog();
            printDialog = new PrintDialog();
            pageSetupDialog = new PageSetupDialog(); // Initialize page setup dialog

            // Setup page setup dialog
            pageSetupDialog.Document = printDocument;

            // Default settings (removed landscape setting)
            printDocument.DefaultPageSettings.Margins = new Margins(10, 8, 10, 8);

            printDocument.BeginPrint += PrintDocument_BeginPrint;
            printDocument.PrintPage += PrintDocument_PrintPage;

            // Setup dialogs
            printPreviewDialog.Document = printDocument;
            printDialog.Document = printDocument;
            printPreviewDialog.UseAntiAlias = true;
            printPreviewDialog.PrintPreviewControl.Zoom = 1.0;

            // Set default paper size
            SetDefaultPaperSize("Letter");
        }

        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            rowIndex = 0;
        }

        public void PageSetup()
        {
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                // Save user settings
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
            }
        }


        public void Print()
        {
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;
            printDialog.UseEXDialog = true;
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Apply printer-specific settings if needed
                ApplyPrinterSpecificSettings();
                printDocument.Print();
            }
        }

        private void ApplyPrinterSpecificSettings()
        {
            // Get the selected printer's default settings
            PrinterSettings printerSettings = printDialog.PrinterSettings;

            // Update our document settings
            printDocument.DefaultPageSettings.Landscape = printerSettings.DefaultPageSettings.Landscape;
            printDocument.DefaultPageSettings.PaperSize = printerSettings.DefaultPageSettings.PaperSize;
        }

        private PaperSize CreatePaperSize(string paperName)
        {
            if (paperName.Equals("Letter", StringComparison.OrdinalIgnoreCase))
            {
                return new PaperSize("Letter", 850, 1100);
            }
            else if (paperName.Equals("A4", StringComparison.OrdinalIgnoreCase))
            {
                return new PaperSize("A4", 827, 1169);
            }
            return new PaperSize("Letter", 850, 1100);
        }


        public void PrintPreview()
        {
            try
            {
                if (PrinterSettings.InstalledPrinters.Count == 0)
                {
                    printDocument.DefaultPageSettings.PaperSize = CreatePaperSize("Letter");
                }
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing print preview: {ex.Message}");
            }
        }

        private void SetDefaultPaperSize(string defaultPaperName)
        {
            try
            {
                if (printDocument.PrinterSettings.PaperSizes.Count == 0)
                {
                    printDocument.DefaultPageSettings.PaperSize = CreatePaperSize(defaultPaperName);
                    return;
                }

                bool paperSet = false;
                foreach (PaperSize size in printDocument.PrinterSettings.PaperSizes)
                {
                    if (size.PaperName.Equals(defaultPaperName, StringComparison.OrdinalIgnoreCase))
                    {
                        printDocument.DefaultPageSettings.PaperSize = size;
                        paperSet = true;
                        break;
                    }
                }

                if (!paperSet && printDocument.PrinterSettings.PaperSizes.Count > 0)
                {
                    printDocument.DefaultPageSettings.PaperSize = printDocument.PrinterSettings.PaperSizes[0];
                }
            }
            catch
            {
                printDocument.DefaultPageSettings.PaperSize = CreatePaperSize("Letter");
            }
        }


        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Fonts
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font cellFont = new Font("Arial", 9);
            Font titleFont = new Font("Arial", 14, FontStyle.Bold);

            // Set up positions
            float pageWidth = e.PageBounds.Width;
            float printableWidth = e.MarginBounds.Width;
            float topMargin = 30; // Reduced top margin
            float yPos = topMargin;

            // Print title (centered)
            string title = "Suppliers List";
            SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
            e.Graphics.DrawString(title, titleFont, Brushes.Black,
                (pageWidth - titleSize.Width) / 2, yPos);

            yPos += titleSize.Height + 15; // Reduced space after title

            // Column width percentages
            float[] widthPercentages = {
                0.05f,   
                0.14f,  
                0.12f,   
                0.18f,   
                0.16f,  
                0.12f,   
                0.12f,   
                0.13f    
            };

            // Calculate actual column widths
            List<float> columnWidths = new List<float>();
            float totalTableWidth = 0;
            foreach (float percentage in widthPercentages)
            {
                float width = printableWidth * percentage;
                columnWidths.Add(width);
                totalTableWidth += width;
            }

            // Center table horizontally
            float tableStartX = (pageWidth - totalTableWidth) / 2;
            float xPos = tableStartX;

            
            float headerHeight = headerFont.Height * 1.8f;

            // Print column headers
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                string headerText = dataGridView.Columns[i].HeaderText;

                // Create rectangle for header cell
                RectangleF headerRect = new RectangleF(xPos, yPos, columnWidths[i], headerHeight);

                // Draw header background
                e.Graphics.FillRectangle(Brushes.LightGray, headerRect);

                // Format header text
                StringFormat headerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.LineLimit
                };

                // Draw header text with padding
                RectangleF textRect = new RectangleF(
                    headerRect.X + 2,
                    headerRect.Y + 2,
                    headerRect.Width - 6,
                    headerRect.Height - 4
                );

                e.Graphics.DrawString(headerText, headerFont, Brushes.Black, textRect, headerFormat);

                // Draw border around header
                e.Graphics.DrawRectangle(Pens.Black,
                    headerRect.X, headerRect.Y, headerRect.Width, headerRect.Height);

                xPos += columnWidths[i];
            }

            yPos += headerHeight + 3; // Small space after headers

            // Print rows
            while (rowIndex < dataGridView.Rows.Count)
            {
                DataGridViewRow row = dataGridView.Rows[rowIndex];

                if (row.IsNewRow)
                {
                    rowIndex++;
                    continue;
                }

                xPos = tableStartX;
                float maxCellHeight = 20; // Minimum row height

                // Calculate max height for this row
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    string text = row.Cells[i].FormattedValue?.ToString() ?? "";
                    SizeF size = e.Graphics.MeasureString(
                        text,
                        cellFont,
                        new SizeF(columnWidths[i] - 6, float.MaxValue),
                        StringFormat.GenericTypographic
                    );

                    if (size.Height > maxCellHeight)
                        maxCellHeight = size.Height;
                }

                // Add padding to row height
                maxCellHeight += 6;

                // Check for page break
                if (yPos + maxCellHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                // Draw cells
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    string text = row.Cells[i].FormattedValue?.ToString() ?? "";
                    RectangleF cellRect = new RectangleF(xPos, yPos, columnWidths[i], maxCellHeight);

                    // Alternate row colors
                    e.Graphics.FillRectangle(
                        rowIndex % 2 == 0 ? Brushes.White : Brushes.WhiteSmoke,
                        cellRect
                    );

                    // Draw cell text
                    StringFormat cellFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    };

                    // Text area with padding
                    RectangleF textRect = new RectangleF(
                        cellRect.X + 4,
                        cellRect.Y,
                        cellRect.Width - 8,
                        cellRect.Height
                    );

                    e.Graphics.DrawString(text, cellFont, Brushes.Black, textRect, cellFormat);

                    // Draw cell border
                    e.Graphics.DrawRectangle(Pens.LightGray,
                        cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);

                    xPos += columnWidths[i];
                }

                yPos += maxCellHeight;
                rowIndex++;
            }

            e.HasMorePages = false;
            rowIndex = 0;
        }
    }
}