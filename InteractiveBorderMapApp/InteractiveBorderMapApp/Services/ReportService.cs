using InteractiveBorderMapApp.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Words;
using Aspose.Words.Tables;

namespace InteractiveBorderMapApp.Services
{
    public class ReportService
    {
        private string webRootPath = HomeController.WebRootPath;
        public FileStream GetReport(string reportId)
        {
            var path = Path.Combine(webRootPath, @$"reports/{reportId}");
            return File.OpenRead(path);
        }

        public string CreateBuildReport(List<Building> buildings)
        {
            string nowTime = Convert.ToString(DateTime.Now.Ticks); // номер отчета

            // путь к документу
            string reportName = nowTime + "_BuildReport.docx";
            string pathDocument = Path.Combine(Path.Combine(webRootPath, "reports"), reportName);

            // Document Builder object to create Table in Word document
            DocumentBuilder toCreateTableInWord = new DocumentBuilder();

            for (int i = 0; i < buildings.Count; i++)
            {
                // Mark the start of Word Table 
                Table tableInWord = toCreateTableInWord.StartTable();

                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("№");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write(buildings[i].Number);

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Адрес");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write(buildings[i].Address);

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Район");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write(buildings[i].Area.ToString());

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Жилое");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write((buildings[i].IsLiving) ? "Да" : "Нет");

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Год");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write(buildings[i].Year);

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Аварийное");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write((buildings[i].IsEmergency) ? "Да" : "Нет");

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Материал");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write(buildings[i].Material);

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------

                toCreateTableInWord.EndTable();

                toCreateTableInWord.Writeln(); //для разделения таблиц
            }
            // Save Word DOCX document with Table on disk
            toCreateTableInWord.Document.Save(pathDocument);
            return reportName;
        }

        public string CreateAreaReport(List<Area> areas)
        {
            string nowTime = Convert.ToString(DateTime.Now.Ticks); // номер отчета
            // путь к документу
            string reportName = nowTime + "_AreaReport.docx";
            string pathDocument = Path.Combine(Path.Combine(webRootPath, "reports"), reportName);

            // Document Builder object to create Table in Word document
            DocumentBuilder toCreateTableInWord = new DocumentBuilder();

            for (int i = 0; i < areas.Count; i++)
            {
                // Mark the start of Word Table 
                Table tableInWord = toCreateTableInWord.StartTable();

                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("№");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write(areas[i].Number);

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Район");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write(areas[i].District);

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Самовольная постройка");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write((areas[i].IsCustom) ? "Да" : "Нет");

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Вид разрешенного использования");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write((areas[i].IsVRI) ? "Да" : "Нет");

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write("Аварийное");

                toCreateTableInWord.InsertCell();
                toCreateTableInWord.Write((areas[i].IsEmergency) ? "Да" : "Нет");

                toCreateTableInWord.EndRow();
                //--------------------------------------------------------
                toCreateTableInWord.EndTable();

                toCreateTableInWord.Writeln(); //для разделения таблиц
            }
            // Save Word DOCX document with Table on disk
            toCreateTableInWord.Document.Save(pathDocument);
            return reportName;
        }

    }
}
