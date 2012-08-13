// -----------------------------------------------------------------------
// <copyright file="Generator.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PdfCreator.Reports
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
