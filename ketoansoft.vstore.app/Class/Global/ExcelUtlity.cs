﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace ketoansoft.app.Class.Global
{
    public class ExcelUtlity
    {
        public bool WriteDataTableToExcel(System.Data.DataTable dataTable)
        {
            string saveAsLocation = "";
            string worksheetName ="";
            string path = Application.StartupPath + @"\ExcelTemplate\SOCTCN11.xls";
            string cell1 = "";
            string cell2 = "";
            Microsoft.Office.Interop.Excel.Application excelApplication = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

            excelApplication = new Microsoft.Office.Interop.Excel.Application();
            excelApplication.Visible = false;
            excelApplication.DisplayAlerts = false;
            try
            {
                workbook = excelApplication.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                string cellValue = "";
                object cellObject = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[22, 1];
                cellObject = range.get_Value(null);
                cellValue = (cellObject == null ? "" : cellObject.ToString().Trim());
                cell1 = cellValue;

                cellValue = "";
                cellObject = null;
                range = null;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[22, 2];
                cellObject = range.get_Value(null);
                cellValue = (cellObject == null ? "" : cellObject.ToString().Trim());
                cell2 = cellValue;


                excelApplication.Quit();
                excelApplication = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;

                //gắn header
                excelSheet.Cells[1, 1] = cell1;
                excelSheet.Cells[1, 2] = cell2;

                // loop through each row and add values to our sheet
                int rowcount = 2;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                        }

                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                        //for alternate rows
                        if (rowcount > 3)
                        {
                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {
                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }

                }
                //gắn footer
                excelSheet.Cells[rowcount + 1, 1] = cell1;
                excelSheet.Cells[rowcount + 1, 2] = cell2;

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;


                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);


                //now save the workbook and exit Excel


                excelworkBook.SaveAs(saveAsLocation); ;
                excelworkBook.Close();
                excel.Quit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }

        }
        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }
        public bool WriteDataTableToExcel_SOCTCN11_Mauchuan(System.Data.DataTable dataTable, string taikhoan, string madtpn, DateTime from, DateTime to, DateTime print)
        {
            string saveAsLocation = Application.StartupPath + @"\Excel\SOCTCN11.xls";  
            string worksheetName = "SHVND";

            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;

                #region General Cells
                //Kiểu chữ
                excelSheet.Range["A1", "G" + (dataTable.Rows.Count + 20)].Font.Name = "Times New Roman";
                excelSheet.Range["A4", "G11"].Font.Name = "Tahoma";
                //Font chữ
                excelSheet.Range["A1","A3"].Font.Size = 10;
                excelSheet.Range["A4", "A7"].Font.Size = 12;
                excelSheet.Range["A9", "G10"].Font.Size = 10;
                excelSheet.Range["A11", "G" + (dataTable.Rows.Count + 20)].Font.Size = 9;
                excelSheet.Range["A4", "A6"].Font.FontStyle = "Bold";
                excelSheet.Range["A9", "G11"].Font.FontStyle = "Bold";
                //Canh giữa chữ
                excelSheet.Range["A4", "G7"].HorizontalAlignment = -4108;
                excelSheet.Range["A12", "C" + (dataTable.Rows.Count + 12)].HorizontalAlignment = -4108;
                excelSheet.Range["E12", "E" + (dataTable.Rows.Count + 12)].HorizontalAlignment = -4108;
                excelSheet.Range["A9", "G11"].HorizontalAlignment = -4108;
                excelSheet.Range["D9"].VerticalAlignment = -4108;
                //Merge
                excelSheet.Range["A4", "G4"].MergeCells = true;
                excelSheet.Range["A5", "G5"].MergeCells = true;
                excelSheet.Range["A6", "G6"].MergeCells = true;
                excelSheet.Range["A7", "G7"].MergeCells = true;
                excelSheet.Range["D9", "D10"].MergeCells = true;
                excelSheet.Range["A9", "C9"].MergeCells = true;
                excelSheet.Range["F9", "G9"].MergeCells = true;
                //Kích thước Cột và Dòng
                excelSheet.Range["A1", "A" + (dataTable.Rows.Count + 20)].RowHeight = "16.5";
                excelSheet.Range["A1"].ColumnWidth = 6;
                excelSheet.Range["B1"].ColumnWidth = 8;
                excelSheet.Range["C1"].ColumnWidth = 12;
                excelSheet.Range["D1"].ColumnWidth = 46;
                excelSheet.Range["E1"].ColumnWidth = 7;
                excelSheet.Range["F1","G1"].ColumnWidth = 12;
                //Border
                excelSheet.Range["A9", "G10"].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                excelSheet.Range["A9", "G10"].Borders.Weight = 2d;
                excelSheet.Range["A11", "G" + (dataTable.Rows.Count + 11)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;
                excelSheet.Range["A11", "G" + (dataTable.Rows.Count + 11)].Borders.Weight = 2d;
                excelSheet.Range["A" + (dataTable.Rows.Count + 12), "G" + (dataTable.Rows.Count + 13)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                excelSheet.Range["A" + (dataTable.Rows.Count + 12), "G" + (dataTable.Rows.Count + 13)].Borders.Weight = 2d;
                //Công thức
                excelSheet.Range["F" + (dataTable.Rows.Count + 12), "F" + (dataTable.Rows.Count + 12)].Formula = String.Format("=sum(F12:F{0})", dataTable.Rows.Count + 11);
                excelSheet.Range["G" + (dataTable.Rows.Count + 12), "G" + (dataTable.Rows.Count + 12)].Formula = String.Format("=sum(G12:G{0})", dataTable.Rows.Count + 11);
                #endregion

                //gắn header
                excelSheet.Cells[1, 1] = "CÔNG TY ABCDab";
                excelSheet.Cells[2, 1] = "M17 LÊ HOÀNG PHÁI,GÒ VẤP,TP.HCM";
                excelSheet.Cells[3, 1] = "Mã số thuế : 0300688235";
                excelSheet.Cells[4, 1] = "SỔ CHI TIẾT TÀI KHOẢN THEO ĐỐI TƯỢNG";
                excelSheet.Cells[5, 1] = "Tài khoản : " + taikhoan;
                excelSheet.Cells[6, 1] = "Mã ĐTPN : " + madtpn;
                excelSheet.Cells[7, 1] = "Từ ngày " + from.ToString("dd/MM/yyyy") + " đến ngày " + to.ToString("dd/MM/yyyy");
                excelSheet.Cells[9, 1] = "Chứng từ";
                excelSheet.Cells[9, 4] = "Diễn giải";
                excelSheet.Cells[9, 5] = "TK";
                excelSheet.Cells[9, 6] = "VND";
                excelSheet.Cells[10, 1] = "Loại";
                excelSheet.Cells[10, 2] = "Số";
                excelSheet.Cells[10, 3] = "Ngày";
                excelSheet.Cells[10, 5] = "DU";
                excelSheet.Cells[10, 6] = "Nợ";
                excelSheet.Cells[10, 7] = "Có";
                excelSheet.Cells[11, 4] = "SỐ DƯ ĐẦU KỲ";
                excelSheet.Cells[dataTable.Rows.Count + 12, 4] = "CỘNG PHÁT SINH";
                excelSheet.Cells[dataTable.Rows.Count + 13, 4] = "SỐ DƯ CUỐI KỲ";
                excelSheet.Cells[dataTable.Rows.Count + 14, 6] = "Ngày " + print.Day + " tháng " + print.Month + " năm " + print.Year;
                excelSheet.Cells[dataTable.Rows.Count + 15, 1] = "Người lập";
                excelSheet.Cells[dataTable.Rows.Count + 15, 4] = "Kế toán trưởng";
                excelSheet.Cells[dataTable.Rows.Count + 15, 6] = "Giám đốc";
                // loop through each row and add values to our sheet
                int rowcount = 11;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    excelSheet.Cells[rowcount, 1] = datarow["LOAI"].ToString();
                    excelSheet.Cells[rowcount, 2] = datarow["SO"].ToString();
                    excelSheet.Cells[rowcount, 3] = datarow["NGAY"].ToString();
                    excelSheet.Cells[rowcount, 4] = datarow["DIENGIAI"].ToString();
                    excelSheet.Cells[rowcount, 5] = datarow["TKDU"].ToString();
                    excelSheet.Cells[rowcount, 6] = datarow["NO"].ToString();
                    excelSheet.Cells[rowcount, 7] = datarow["CO"].ToString();
                }
                //gắn footer
                //excelSheet.Cells[rowcount + 1, 1] = cell1;
                //excelSheet.Cells[rowcount + 1, 2] = cell2;

                //now save the workbook and exit Excel
                excelworkBook.SaveAs(saveAsLocation);
                excelworkBook.Close();
                excel.Quit();
                Process.Start(saveAsLocation);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }
        }
        public bool WriteDataTableToExcel_SOCTCN30_InChiTietQuyCach(System.Data.DataTable dataTable, string taikhoan, string madtpn, DateTime from, DateTime to, DateTime print)
        {
            string saveAsLocation = Application.StartupPath + @"\Excel\SOCTCN30.xls";
            string worksheetName = "SHVND";

            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;

                #region General Cells
                //Kiểu chữ
                excelSheet.Range["A1", "N" + (dataTable.Rows.Count + 20)].Font.Name = "Times New Roman";
                excelSheet.Range["A4", "N10"].Font.Name = "Tahoma";
                //Font chữ
                excelSheet.Range["A1", "A3"].Font.Size = 10;
                excelSheet.Range["A4", "A7"].Font.Size = 12;
                excelSheet.Range["A9", "N10"].Font.Size = 10;
                excelSheet.Range["A11", "G" + (dataTable.Rows.Count + 20)].Font.Size = 9;
                excelSheet.Range["A4", "A6"].Font.FontStyle = "Bold";
                excelSheet.Range["A9", "N11"].Font.FontStyle = "Bold";
                excelSheet.Range["E" + (dataTable.Rows.Count + 12), "E" + (dataTable.Rows.Count + 13)].Font.FontStyle = "Bold"; 
                //Canh giữa chữ
                excelSheet.Range["A4", "N7"].HorizontalAlignment = -4108;
                excelSheet.Range["A9", "N11"].HorizontalAlignment = -4108;
                excelSheet.Range["A12", "D" + (dataTable.Rows.Count + 12)].HorizontalAlignment = -4108;
                excelSheet.Range["F12", "J" + (dataTable.Rows.Count + 12)].HorizontalAlignment = -4108;                
                excelSheet.Range["E9"].VerticalAlignment = -4108;
                excelSheet.Range["E" + (dataTable.Rows.Count + 12), "E" + (dataTable.Rows.Count + 13)].HorizontalAlignment = -4108;                              
                //Merge
                excelSheet.Range["A4", "N4"].MergeCells = true;
                excelSheet.Range["A5", "N5"].MergeCells = true;
                excelSheet.Range["A6", "N6"].MergeCells = true;
                excelSheet.Range["A7", "N7"].MergeCells = true;
                excelSheet.Range["A9", "C9"].MergeCells = true;
                excelSheet.Range["M9", "N9"].MergeCells = true;
                //Kích thước Cột và Dòng
                excelSheet.Range["A1", "A" + (dataTable.Rows.Count + 20)].RowHeight = "16.5";
                excelSheet.Range["A1"].ColumnWidth = 6;
                excelSheet.Range["B1"].ColumnWidth = 8;
                excelSheet.Range["C1"].ColumnWidth = 12;
                excelSheet.Range["D1"].ColumnWidth = 10;
                excelSheet.Range["E1"].ColumnWidth = 45;
                excelSheet.Range["F1", "L1"].ColumnWidth = 10;
                excelSheet.Range["M1", "N1"].ColumnWidth = 12;
                //Border
                excelSheet.Range["A9", "N10"].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                excelSheet.Range["A9", "N10"].Borders.Weight = 2d;
                excelSheet.Range["A11", "N" + (dataTable.Rows.Count + 11)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;
                excelSheet.Range["A11", "N" + (dataTable.Rows.Count + 11)].Borders.Weight = 2d;
                excelSheet.Range["A" + (dataTable.Rows.Count + 12), "N" + (dataTable.Rows.Count + 13)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                excelSheet.Range["A" + (dataTable.Rows.Count + 12), "N" + (dataTable.Rows.Count + 13)].Borders.Weight = 2d;
                //Công thức
                excelSheet.Range["M" + (dataTable.Rows.Count + 12), "M" + (dataTable.Rows.Count + 12)].Formula = String.Format("=sum(M12:M{0})", dataTable.Rows.Count + 11);
                excelSheet.Range["N" + (dataTable.Rows.Count + 12), "N" + (dataTable.Rows.Count + 12)].Formula = String.Format("=sum(N12:N{0})", dataTable.Rows.Count + 11);
                #endregion

                //gắn header & footer
                excelSheet.Range["A1"].Value2 = "CÔNG TY ABCDab";
                excelSheet.Range["A2"].Value2 = "M17 LÊ HOÀNG PHÁI,GÒ VẤP,TP.HCM";
                excelSheet.Range["A3"].Value2 = "Mã số thuế : 0300688235";
                excelSheet.Range["A4"].Value2 = "SỔ CHI TIẾT TÀI KHOẢN THEO ĐỐI TƯỢNG";
                excelSheet.Range["A5"].Value2 = "Tài khoản : " + taikhoan;
                excelSheet.Range["A6"].Value2 = "Mã ĐTPN : " + madtpn;
                excelSheet.Range["A7"].Value2 = "Từ ngày " + from.ToString("dd/MM/yyyy") + " đến ngày " + to.ToString("dd/MM/yyyy");
                excelSheet.Range["A9"].Value2 = "Chứng từ";
                excelSheet.Range["D9"].Value2 = "Mã";
                excelSheet.Range["E9"].Value2 = "Diễn giải";
                excelSheet.Range["F9"].Value2 = "Đơn";
                excelSheet.Range["G9"].Value2 = "Số";
                excelSheet.Range["H9"].Value2 = "Dài";
                excelSheet.Range["I9"].Value2 = "Rộng";
                excelSheet.Range["J9"].Value2 = "Số";
                excelSheet.Range["K9"].Value2 = "Đơn";
                excelSheet.Range["L9"].Value2 = "TK";
                excelSheet.Range["M9"].Value2 = "VND";
                excelSheet.Range["A10"].Value2 = "Loại";
                excelSheet.Range["B10"].Value2 = "Số";
                excelSheet.Range["C10"].Value2 = "Ngày";
                excelSheet.Range["D10"].Value2 = "dm";
                excelSheet.Range["F10"].Value2 = "vị";
                excelSheet.Range["G10"].Value2 = "lượng";
                excelSheet.Range["J10"].Value2 = "M2";
                excelSheet.Range["K10"].Value2 = "giá";
                excelSheet.Range["L10"].Value2 = "DU";
                excelSheet.Range["M10"].Value2 = "Nợ";
                excelSheet.Range["N10"].Value2 = "Có";
                excelSheet.Range["E11"].Value2 = "SỐ DƯ ĐẦU KỲ";
                excelSheet.Range["E" + (dataTable.Rows.Count + 12)].Value2 = "CỘNG PHÁT SINH";
                excelSheet.Range["E" + (dataTable.Rows.Count + 13)].Value2 = "SỐ DƯ CUỐI KỲ";
                excelSheet.Range["A" + (dataTable.Rows.Count + 15)].Value2 = "Người lập";
                excelSheet.Range["G" + (dataTable.Rows.Count + 15)].Value2 = "Kế toán trưởng";
                excelSheet.Range["M" + (dataTable.Rows.Count + 15)].Value2 = "Giám đốc";
                excelSheet.Range["M" + (dataTable.Rows.Count + 14)].Value2 = "Ngày " + print.Day + " tháng " + print.Month + " năm " + print.Year;
                // loop through each row and add values to our sheet
                int rowcount = 11;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    excelSheet.Range["A" + rowcount].Value2 = datarow["LOAI"].ToString();
                    excelSheet.Range["B" + rowcount].Value2 = datarow["SO"].ToString();
                    excelSheet.Range["C" + rowcount].Value2 = datarow["NGAY"].ToString();
                    excelSheet.Range["D" + rowcount].Value2 = datarow["MADM"].ToString();
                    excelSheet.Range["E" + rowcount].Value2 = datarow["DIENGIAI"].ToString();
                    excelSheet.Range["F" + rowcount].Value2 = datarow["DONVI"].ToString();
                    excelSheet.Range["G" + rowcount].Value2 = datarow["SOLUONG"].ToString();
                    excelSheet.Range["H" + rowcount].Value2 = datarow["DAI"].ToString();
                    excelSheet.Range["I" + rowcount].Value2 = datarow["RONG"].ToString();
                    excelSheet.Range["J" + rowcount].Value2 = datarow["SOM2"].ToString();
                    excelSheet.Range["K" + rowcount].Value2 = datarow["DONGIA"].ToString();
                    excelSheet.Range["L" + rowcount].Value2 = datarow["TKDU"].ToString();
                    excelSheet.Range["M" + rowcount].Value2 = datarow["NO"].ToString();
                    excelSheet.Range["N" + rowcount].Value2 = datarow["CO"].ToString();
                }

                //now save the workbook and exit Excel
                excelworkBook.SaveAs(saveAsLocation);
                excelworkBook.Close();
                excel.Quit();
                Process.Start(saveAsLocation);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }
        }
        public bool WriteDataTableToExcel_SOCTCN11_03_V_ViewtatcaDT(System.Data.DataTable dataTable, DateTime from, DateTime to, DateTime print, List<string> listDTPN)
        {
            string saveAsLocation = Application.StartupPath + @"\Excel\SOCTCN11_03_V.xls";
            string worksheetName = "IN";

            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;

                int rowcount = 0;
                foreach (var item in listDTPN)
                {
                    string expression;
                    expression = "DTPN = '" + item + "'";
                    var dataRow = dataTable.Select(expression);
                    #region General Cells
                    //Kiểu chữ
                    excelSheet.Range["A" + (rowcount+1), "L" + (rowcount + dataRow.Length + 20)].Font.Name = "Times New Roman";
                    excelSheet.Range["A" + (rowcount + 4), "L" + (rowcount + 4)].Font.Name = "Tahoma";
                    //Font chữ
                    excelSheet.Range["A" + (rowcount + 1), "A" + (rowcount + 3)].Font.Size = 10;
                    excelSheet.Range["A" + (rowcount + 4), "A" + (rowcount + 7)].Font.Size = 12;
                    excelSheet.Range["A" + (rowcount + 9), "L" + (rowcount + 10)].Font.Size = 10;
                    excelSheet.Range["A" + (rowcount + 11), "L" + (rowcount + dataRow.Length)].Font.Size = 9;
                    excelSheet.Range["A" + (rowcount + 4), "A" + (rowcount + 6)].Font.FontStyle = "Bold";
                    excelSheet.Range["A" + (rowcount + 9), "L" + (rowcount + 11)].Font.FontStyle = "Bold";
                    excelSheet.Range["G" + (rowcount + dataRow.Length + 11), "L" + (rowcount +dataRow.Length + 13)].Font.FontStyle = "Bold";
                    //Canh giữa chữ
                    excelSheet.Range["A" + (rowcount + 4), "L" + (rowcount + 7)].HorizontalAlignment = -4108;
                    excelSheet.Range["A" + (rowcount + 9), "L" + (rowcount + 10)].HorizontalAlignment = -4108;
                    excelSheet.Range["A" + (rowcount + 11), "E" + (rowcount + dataRow.Length + 11)].HorizontalAlignment = -4108;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 11), "F" + (rowcount + dataRow.Length + 11)].HorizontalAlignment = -4108;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 12), "F" + (rowcount + dataRow.Length + 12)].HorizontalAlignment = -4108;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 13), "F" + (rowcount + dataRow.Length + 13)].HorizontalAlignment = -4108;
                    //Merge
                    excelSheet.Range["A" + (rowcount + 4), "L" + (rowcount + 4)].MergeCells = true;
                    excelSheet.Range["A" + (rowcount + 5), "L" + (rowcount + 5)].MergeCells = true;
                    excelSheet.Range["A" + (rowcount + 6), "L" + (rowcount + 6)].MergeCells = true;
                    excelSheet.Range["A" + (rowcount + 7), "L" + (rowcount + 7)].MergeCells = true;
                    excelSheet.Range["A" + (rowcount + 9), "C" + (rowcount + 9)].MergeCells = true;
                    excelSheet.Range["D" + (rowcount + 9), "E" + (rowcount + 9)].MergeCells = true;
                    excelSheet.Range["I" + (rowcount + 9), "J" + (rowcount + 9)].MergeCells = true;
                    excelSheet.Range["K" + (rowcount + 9), "L" + (rowcount + 9)].MergeCells = true;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 11), "G" + (rowcount + dataRow.Length + 11)].MergeCells = true;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 12), "G" + (rowcount + dataRow.Length + 12)].MergeCells = true;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 13), "G" + (rowcount + dataRow.Length + 13)].MergeCells = true;
                    //Kích thước Cột và Dòng
                    excelSheet.Range["A" + (rowcount + 1), "A" + (rowcount + dataRow.Length + 20)].RowHeight = "16.5";
                    excelSheet.Range["A1"].ColumnWidth = 6;
                    excelSheet.Range["B1"].ColumnWidth = 8;
                    excelSheet.Range["C1"].ColumnWidth = 12;
                    excelSheet.Range["D1"].ColumnWidth = 8;
                    excelSheet.Range["E1"].ColumnWidth = 12;
                    excelSheet.Range["F1"].ColumnWidth = 40;
                    excelSheet.Range["G1"].ColumnWidth = 40;
                    excelSheet.Range["H1", "L1"].ColumnWidth = 10;
                    //Border
                    excelSheet.Range["A" + (rowcount + 9), "L" + (rowcount + 10)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    excelSheet.Range["A" + (rowcount + 9), "L" + (rowcount + 10)].Borders.Weight = 2d;
                    excelSheet.Range["A" + (rowcount + 11), "L" + (rowcount+ dataRow.Length + 10)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDot;
                    excelSheet.Range["A" + (rowcount + 11), "L" + (rowcount + dataRow.Length + 10)].Borders.Weight = 2d;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 11), "L" + (rowcount + dataRow.Length + 13)].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 11), "L" + (rowcount + dataRow.Length + 13)].Borders.Weight = 2d;
                    //Công thức
                    excelSheet.Range["I" + (rowcount + dataRow.Length + 11), "I" + (rowcount + dataRow.Length + 11)].Formula = String.Format("=sum(I{0}:I{1})", rowcount+11, rowcount + dataRow.Length + 10);
                    excelSheet.Range["J" + (rowcount + dataRow.Length + 11), "J" + (rowcount + dataRow.Length + 11)].Formula = String.Format("=sum(J{0}:J{1})", rowcount+11, rowcount + dataRow.Length + 10);
                    excelSheet.Range["K" + (rowcount + dataRow.Length + 11), "K" + (rowcount + dataRow.Length + 11)].Formula = String.Format("=sum(K{0}:K{1})", rowcount+11, rowcount + dataRow.Length + 10);
                    excelSheet.Range["L" + (rowcount + dataRow.Length + 11), "L" + (rowcount + dataRow.Length + 11)].Formula = String.Format("=sum(L{0}:L{1})",  rowcount+11, rowcount + dataRow.Length + 10);
                    //excelSheet.Range["I" + (rowcount + dataRow.Length + 12), "I" + (rowcount + dataRow.Length + 12)].Formula = String.Format("=sum(I{0}:I{1})", rowcount + 11, rowcount + dataRow.Length + 10);
                    //excelSheet.Range["J" + (rowcount + dataRow.Length + 12), "J" + (rowcount + dataRow.Length + 12)].Formula = String.Format("=sum(J{0}:J{1})", rowcount + 11, rowcount + dataRow.Length + 10);
                    //excelSheet.Range["K" + (rowcount + dataRow.Length + 12), "K" + (rowcount + dataRow.Length + 12)].Formula = String.Format("=sum(K{0}:K{1})", rowcount + 11, rowcount + dataRow.Length + 10);
                    //excelSheet.Range["L" + (rowcount + dataRow.Length + 12), "L" + (rowcount + dataRow.Length + 12)].Formula = String.Format("=sum(L{0}:L{1})", rowcount + 11, rowcount + dataRow.Length + 10);
                    #endregion

                    //gắn header & footer
                    excelSheet.Range["A" + (rowcount + 1)].Value2 = "CÔNG TY ABCDab";
                    excelSheet.Range["A" + (rowcount + 2)].Value2 = "M17 LÊ HOÀNG PHÁI,GÒ VẤP,TP.HCM";
                    excelSheet.Range["A" + (rowcount + 3)].Value2 = "Mã số thuế : 0300688235";
                    excelSheet.Range["A" + (rowcount + 4)].Value2 = "SỔ CHI TIẾT TÀI KHOẢN THEO ĐỐI TƯỢNG";
                    excelSheet.Range["A" + (rowcount + 5)].Value2 = "Tài khoản : " + dataRow[0]["TK"];
                    excelSheet.Range["A" + (rowcount + 6)].Value2 = "Mã ĐTPN : " + dataRow[0]["DTPN"];
                    excelSheet.Range["A" + (rowcount + 7)].Value2 = "Từ ngày " + from.ToString("dd/MM/yyyy") + " đến ngày " + to.ToString("dd/MM/yyyy");
                    excelSheet.Range["A" + (rowcount + 9)].Value2 = "Chứng từ";
                    excelSheet.Range["D" + (rowcount + 9)].Value2 = "Hóa đơn";
                    excelSheet.Range["F" + (rowcount + 9)].Value2 = "Tên khách hàng";
                    excelSheet.Range["G" + (rowcount + 9)].Value2 = "Diễn giải";
                    excelSheet.Range["H" + (rowcount + 9)].Value2 = "TK";
                    excelSheet.Range["I" + (rowcount + 9)].Value2 = "USD - Phát sinh";
                    excelSheet.Range["K" + (rowcount + 9)].Value2 = "VND - Phát sinh";
                    excelSheet.Range["A" + (rowcount + 10)].Value2 = "Loại";
                    excelSheet.Range["B" + (rowcount + 10)].Value2 = "Số";
                    excelSheet.Range["C" + (rowcount + 10)].Value2 = "Ngày";
                    excelSheet.Range["D" + (rowcount + 10)].Value2 = "Số";
                    excelSheet.Range["E" + (rowcount + 10)].Value2 = "Ngày";
                    excelSheet.Range["H" + (rowcount + 10)].Value2 = "ĐƯ";
                    excelSheet.Range["I" + (rowcount + 10)].Value2 = "Nợ";
                    excelSheet.Range["J" + (rowcount + 10)].Value2 = "Có";
                    excelSheet.Range["K" + (rowcount + 10)].Value2 = "Nợ";
                    excelSheet.Range["L" + (rowcount + 10)].Value2 = "Có";
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 11)].Value2 = "CỘNG PHÁT SINH";
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 12)].Value2 = "SỐ DƯ ĐẦU KỲ";
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 13)].Value2 = "SỐ DƯ CUỐI KỲ";
                    excelSheet.Range["J" + (rowcount + dataRow.Length + 14)].Value2 = "Ngày " + print.Day + " tháng " + print.Month + " năm " + print.Year;
                    excelSheet.Range["C" + (rowcount + dataRow.Length + 15)].Value2 = "Người lập";
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 15)].Value2 = "Kế toán trưởng";
                    excelSheet.Range["K" + (rowcount + dataRow.Length + 15)].Value2 = "Giám đốc";
                    excelSheet.Range["C" + (rowcount + dataRow.Length + 20)].Value2 = "NLS";
                    excelSheet.Range["F" + (rowcount + dataRow.Length + 20)].Value2 = "KTT";
                    excelSheet.Range["K" + (rowcount + dataRow.Length + 20)].Value2 = "HTP";    
                    // loop through each row and add values to our sheet   
                    rowcount += 10;
                    foreach (var row in dataRow)
                    {
                        rowcount += 1;
                        excelSheet.Range["A" + rowcount].Value2 = row["LOAI"].ToString();
                        excelSheet.Range["B" + rowcount].Value2 = row["SO"].ToString();
                        excelSheet.Range["C" + rowcount].Value2 = row["NGAY"].ToString();
                        //excelSheet.Range["D" + rowcount].Value2 = row["MADM"].ToString();
                        //excelSheet.Range["E" + rowcount].Value2 = row["DIENGIAI"].ToString();
                        //excelSheet.Range["F" + rowcount].Value2 = row["DONVI"].ToString();
                        //excelSheet.Range["G" + rowcount].Value2 = row["SOLUONG"].ToString();
                        //excelSheet.Range["H" + rowcount].Value2 = row["DAI"].ToString();
                        //excelSheet.Range["I" + rowcount].Value2 = row["RONG"].ToString();
                        //excelSheet.Range["J" + rowcount].Value2 = row["SOM2"].ToString();
                        //excelSheet.Range["K" + rowcount].Value2 = row["DONGIA"].ToString();
                        //excelSheet.Range["L" + rowcount].Value2 = row["TKDU"].ToString();
                        //excelSheet.Range["M" + rowcount].Value2 = row["NO"].ToString();
                        //excelSheet.Range["N" + rowcount].Value2 = row["CO"].ToString();
                    }
                    rowcount += 10;
                }                
                //now save the workbook and exit Excel
                excelworkBook.SaveAs(saveAsLocation);
                excelworkBook.Close();
                excel.Quit();
                Process.Start(saveAsLocation);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }
        }
    }
}
