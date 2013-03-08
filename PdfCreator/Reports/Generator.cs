/* -----------------------------------------------------------------------
    // Copyright (C) 2012  See The Link Limited

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>

    Orginal Source and contact details can be found:
    https://github.com/davehawes/RDLC-Pdf-Generator
----------------------------------------------------------------------- */

namespace SeeTheLink.PdfCreator.Reports
{
    using System.Data;

    using Microsoft.Reporting.WebForms;

    /// <summary>
    /// This class is responsible for getting the correct rdlc file and merging it with the correct dataset to create the report which can then be saved as a pdf.
    /// </summary>
    public class Generator
    {
        public enum FileTypes { PDF, WORD, EXCEL, IMAGE }

        public static byte[] CreateFile(ReportFromStreamDefinitionFacade report, FileTypes ouputType = FileTypes.PDF)
        {
            var localReport = GetNewLocalReport(report.ReportDataSet);
            localReport.LoadReportDefinition(report.Definition);

            foreach (var subReport in report.SubReports)
            {
                localReport.LoadSubreportDefinition(subReport.Name, subReport.Definition);
            }

            return localReport.Render(ouputType.ToString());
        }
        
        public static byte[] CreateFile(ReportFromFileDefinitionFacade report, FileTypes outputType = FileTypes.PDF)
        {
            var localReport = GetNewLocalReport(report.ReportDataSet);

            localReport.ReportPath = report.DefinitionFilePath;

            return localReport.Render(outputType.ToString());
        }

        private static LocalReport GetNewLocalReport(DataSet dataSet)
        {
            var reportViewer = new ReportViewer();
            LocalReport localReport = reportViewer.LocalReport;

            localReport.SubreportProcessing += LocalReport_SubreportProcessing;
            localReport.EnableExternalImages = true;
            localReport.EnableHyperlinks = true;

            localReport.DataSources.Clear();

            foreach (DataTable datatable in dataSet.Tables)
            {
                localReport.DataSources.Add(new ReportDataSource(datatable.TableName, datatable));
            }

            return localReport;
        }

        private static void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var masterReport = sender as LocalReport;
            e.DataSources.Add(masterReport.DataSources[e.ReportPath]);
        }
    }
}
