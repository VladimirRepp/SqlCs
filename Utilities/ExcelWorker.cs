using System;
using System.Linq;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

static class ExcelWorker
{
    /// <summary>
    /// Экспорт в таблицу Exel
    /// </summary>
    /// <param name="path"> - расположение файла</param>
    /// <param name="dataGridView"> - таблица экспорта</param>
    /// <param name="activeSheet"> - номер активной таблицы</param>
    /// <param name="sheetName"> - название активного листа</param>
    /// <param name="visible"> - флажок, показать документ по завершению</param>
    public static bool TryExportIntoExcel(string path, ref DataGridView dataGridView, int activeSheet = 1, string sheetName = "Лист 1", bool visible = false)
    {
        // Загрузить Excel, затем создать новую пустую рабочую книгу
        Excel.Application excelApp = new Excel.Application();
        System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();

        try
        {
            // Делаем временно неактивным документ
            excelApp.Interactive = false;
            excelApp.EnableEvents = false;
            excelApp.Visible = visible;

            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = excelApp.ActiveSheet;

            // Выбираем лист на котором будем работать
            workSheet = (Excel.Worksheet)excelApp.Sheets[activeSheet];
            // Название листа
            workSheet.Name = sheetName;

            // Установить заголовки столбцов в ячейках
            for (int i = 1; i <= dataGridView.Columns.Count; i++)
            {
                workSheet.Cells[1, i] = dataGridView.Columns[i - 1].HeaderCell.Value.ToString();
            }

            int row = 2; // так как записан заголовок
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int colmn = 1; colmn <= dataGridView.Columns.Count; colmn++)
                {
                    workSheet.Cells[row, colmn] = dataGridView.Rows[i].Cells[colmn - 1].Value.ToString();
                }

                row++;
            }

            // Придать симпатичный вид табличным данным
            workSheet.Range["A1"].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);

            // Сохранить файл, выйти из Excel
            excelApp.DisplayAlerts = false;
            workSheet.SaveAs(string.Format(path, Environment.CurrentDirectory));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            excelApp.Quit();
            excelApp = null;

            excelProc.Kill();
        }

        return true;
    }

    /// <summary>
    /// Экспорт в таблицу Exel
    /// </summary>
    /// <param name="path"> - расположение файла</param>
    /// <param name="headers"> - список заголовков</param>
    /// <param name="values"> - список параметров для экспорта</param>
    /// <param name="activeSheet"> - номер активной таблицы</param>
    /// <param name="sheetName"> - название активного листа</param>
    /// <param name="visible"> - флажок, показать документ по завершению</param>
    public static bool TryExportIntoExcel(string path, List<string> headers, List<List<string>> values, int activeSheet = 1, string sheetName = "Лист 1", bool visible = false)
    {
        // Загрузить Excel, затем создать новую пустую рабочую книгу
        Excel.Application excelApp = new Excel.Application();
        System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();

        try
        {
            // Делаем временно неактивным документ
            excelApp.Interactive = false;
            excelApp.EnableEvents = false;
            excelApp.Visible = visible;

            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = excelApp.ActiveSheet;

            // Выбираем лист на котором будем работать
            workSheet = (Excel.Worksheet)excelApp.Sheets[activeSheet];
            // Название листа
            workSheet.Name = sheetName;

            // Установить заголовки столбцов в ячейках
            for (int i = 1; i <= headers.Count; i++)
            {
                workSheet.Cells[1, i] = headers[i - 1];
            }

            int row = 2; // так как записан заголовок
            for (int i = 0; i < values.Count; i++)
            {
                for (int colmn = 1; colmn <= values[i].Count; colmn++)
                {
                    workSheet.Cells[row, colmn] = values[i][colmn - 1];
                }

                row++;
            }

            // Придать симпатичный вид табличным данным
            workSheet.Range["A1"].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);

            // Сохранить файл, выйти из Excel
            excelApp.DisplayAlerts = false;
            workSheet.SaveAs(string.Format(path, Environment.CurrentDirectory));
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            excelApp.Quit();
            excelApp = null;

            excelProc.Kill();
        }

        return true;
    }

    /// <summary>
    /// Импорт из Exel
    /// </summary>
    /// <param name="parh"> - путь до Exel файла</param>
    /// <param name="countRows"> - количество строк для чтения</param>
    /// <param name="countColumns"> - количество столбцов для чтения</param>
    /// <returns>Список строк считанных параметров</returns>
    public static bool TryImportFromExcel(string path, out List<List<string>> values, int countRows = 0, int countColumns = 0)
    {
        // Загрузить Excel, затем создать новую пустую рабочую книгу
        Excel.Application excelApp = new Excel.Application();
        System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();
        values = null;

        try
        {
            excelApp.Visible = false;
            excelApp.Workbooks.Open(string.Format(path, Environment.CurrentDirectory));

            Excel._Worksheet workSheet = excelApp.ActiveSheet;

            // Диапазон ячеек
            Excel.Range xlRange = workSheet.UsedRange;
            countRows = xlRange.Rows.Count;
            countColumns = xlRange.Columns.Count;

            values = new List<List<string>>();

            for (int row = 2; row <= countRows; row++)// row от 2 - так как не читаем заголовок
            {
                List<string> line = new List<string>();

                for (int column = 1; column <= countColumns; column++)
                {
                    line.Add(xlRange.Cells[row, column].Value2.ToString());
                }

                values.Add(line);
            }

            workSheet = null;
            xlRange = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            excelApp.Quit();
            excelApp = null;

            excelProc.Kill();
        }

        return values != null;
    }
}
