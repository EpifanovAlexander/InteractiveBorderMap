using System;
using System.Drawing;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace InteractiveBorderMapApp
{
    internal class ReportCreator
    {

        //https://csharp.webdelphi.ru/c-net-core-i-excel-nachalo-raboty/


        public void CreateBuildReport(List<Building> buildings )
        {
            string nowTime = Convert.ToString(Convert.ToString(DateTime.Now.Ticks)); // номер отчета
            string pathFromExeFile = (string)AppDomain.CurrentDomain.BaseDirectory;  // путь до exe файла

            // путь к документу
            string pathDocument = pathFromExeFile + "/reports/" + nowTime + "_BuildReport.docx";
            // создаём документ
            DocX document = DocX.Create(pathDocument);

            //================================================== Шапка документа ==================================================//
            document.InsertParagraph("Департамент городского имущества Москвы").
                   // устанавливаем шрифт
                   Font("Times New Roman").
                   // устанавливаем размер шрифта
                   FontSize(20).
                   // делаем текст жирным
                   Bold().
                   // выравниваем текст по центру
                   Alignment = Alignment.center;

            document.InsertParagraph("Отчет об анализе индустриального квартала").
                   // устанавливаем шрифт
                   Font("Times New Roman").
                   // устанавливаем размер шрифта
                   FontSize(14).
                   // выравниваем текст по центру
                   Alignment = Alignment.center;
            //=====================================================================================================================//

            for (int i = 0; i < buildings.Count; i++)
            {
                // создаём таблицу
                Table table = document.AddTable(6, 4);
                for (int numOfRows = 0; numOfRows < table.RowCount; numOfRows++)
                {
                    table.Rows[numOfRows].MergeCells(1, 3); //мержим ячейки с 1 по 3 чтоб было больше места под данные
                }
                // располагаем таблицу по центру
                table.Alignment = Alignment.center;
                // меняем стандартный дизайн таблицы
                table.Design = TableDesign.TableGrid; //TableDesign.None - без границ

                //0    1        2     3       4     5      6     7
                //num, address, area, living, year, emerg, type, material
                //public string Number;
                //public string Address;
                //public double Area;
                //public bool IsLiving;
                //public string Year;
                //public bool IsEmergency;
                //public bool IsType;
                //public string Material;
                //================================================= Шаблон отчета =================================================//
                table.Rows[0].Cells[0].Paragraphs[0].Append("Адрес").Bold(true).FontSize(14).Font("Times New Roman");
                table.Rows[1].Cells[0].Paragraphs[0].Append("Жилое").Bold(true).FontSize(14).Font("Times New Roman");
                table.Rows[2].Cells[0].Paragraphs[0].Append("Год постройки").Bold(true).FontSize(14).Font("Times New Roman");
                table.Rows[3].Cells[0].Paragraphs[0].Append("Материал").Bold(true).FontSize(14).Font("Times New Roman");
                //=================================================================================================================//

                //======================================================= Данные с report ======================================================//
                table.Rows[0].Cells[1].Paragraphs[0].Append(reports[i].addr).FontSize(14).Font(buildings[i].Address);
                table.Rows[1].Cells[1].Paragraphs[0].Append(reports[i].residential).FontSize(14).Font(buildings[i].IsLiving ? "Да" : "Нет");
                table.Rows[2].Cells[1].Paragraphs[0].Append(reports[i].numOfWorkplaces.ToString()).FontSize(14).Font(buildings[i].Year);
                table.Rows[3].Cells[1].Paragraphs[0].Append(reports[i].rights).FontSize(14).Font((buildings[i].Material);
                //==============================================================================================================================//

                // создаём параграф и вставляем таблицу
                document.InsertParagraph().InsertTableAfterSelf(table);
            }
            // сохраняем документ
            document.Save();
        }

        public void CreateAreaReport(List<Area> areas)
        {
            string nowTime = Convert.ToString(Convert.ToString(DateTime.Now.Ticks)); // номер отчета
            string pathFromExeFile = (string)AppDomain.CurrentDomain.BaseDirectory;  // путь до exe файла

            // путь к документу
            string pathDocument = pathFromExeFile + "/reports/" + nowTime + "_BuildReport.docx";
            // создаём документ
            DocX document = DocX.Create(pathDocument);

            //================================================== Шапка документа ==================================================//
            document.InsertParagraph("Департамент городского имущества Москвы").
                   // устанавливаем шрифт
                   Font("Times New Roman").
                   // устанавливаем размер шрифта
                   FontSize(20).
                   // делаем текст жирным
                   Bold().
                   // выравниваем текст по центру
                   Alignment = Alignment.center;

            document.InsertParagraph("Отчет об анализе индустриального квартала").
                   // устанавливаем шрифт
                   Font("Times New Roman").
                   // устанавливаем размер шрифта
                   FontSize(14).
                   // выравниваем текст по центру
                   Alignment = Alignment.center;
            //=====================================================================================================================//

            for (int i = 0; i < areas.Count; i++)
            {
                // создаём таблицу
                Table table = document.AddTable(6, 4);
                for (int numOfRows = 0; numOfRows < table.RowCount; numOfRows++)
                {
                    table.Rows[numOfRows].MergeCells(1, 3); //мержим ячейки с 1 по 3 чтоб было больше места под данные
                }
                // располагаем таблицу по центру
                table.Alignment = Alignment.center;
                // меняем стандартный дизайн таблицы
                table.Design = TableDesign.TableGrid; //TableDesign.None - без границ

                //    public string District;
                //    public string Number;
                //    public bool IsCustom;
                //    public bool IsVRI;
                //    public bool IsEmergency;

                //================================================= Шаблон отчета =================================================//
                table.Rows[0].Cells[0].Paragraphs[0].Append("Район").Bold(true).FontSize(14).Font("Times New Roman");
                table.Rows[1].Cells[0].Paragraphs[0].Append("Номер").Bold(true).FontSize(14).Font("Times New Roman");
                table.Rows[2].Cells[0].Paragraphs[0].Append("аварийное").Bold(true).FontSize(14).Font("Times New Roman");
                //=================================================================================================================//

                //======================================================= Данные с report ======================================================//
                table.Rows[0].Cells[1].Paragraphs[0].Append(reports[i].addr).FontSize(14).Font(areas[i].District);
                table.Rows[1].Cells[1].Paragraphs[0].Append(reports[i].residential).FontSize(14).Font(areas[i].Number);
                table.Rows[2].Cells[1].Paragraphs[0].Append(reports[i].numOfWorkplaces.ToString()).FontSize(14).Font(areas[i].IsEmergency);
                //==============================================================================================================================//

                // создаём параграф и вставляем таблицу
                document.InsertParagraph().InsertTableAfterSelf(table);
            }
            // сохраняем документ
            document.Save();
        }


    }
}
