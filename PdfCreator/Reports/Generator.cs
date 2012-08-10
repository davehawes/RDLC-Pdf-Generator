// -----------------------------------------------------------------------
// <copyright file="Generator.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PdfCreator.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Microsoft.Reporting.WebForms;

    /// <summary>
    /// This class is responsible for getting the correct rdlc file and merging it with the correct dataset to create the report which can then be saved as a pdf.
    /// </summary>
    public class Generator
    {
        public static byte[] CreatePdf(ReportFromStreamDefinitionFacade report)
        {
            var reportViewer = new ReportViewer();
            LocalReport localReport = reportViewer.LocalReport;

            localReport.SubreportProcessing += LocalReport_SubreportProcessing;
            localReport.EnableExternalImages = true;
            localReport.LoadReportDefinition(report.Definition);
            
                foreach (var subReport in report.SubReports)
                {
                    localReport.LoadSubreportDefinition(subReport.Name, subReport.Definition);
                }
            

            localReport.DataSources.Clear();

            foreach (DataTable datatable in report.ReportDataSet.Tables)
            {
                localReport.DataSources.Add(new ReportDataSource(datatable.TableName, datatable));
            }

            Warning[] warnings;
            string mimeType, encoding, fileNameExtension;
            string[] streams;

            byte[] bytes = localReport.Render(
                "PDF",
                null,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return bytes;
        }

        public static byte[] CreatePdf(ReportFromFileDefinitionFacade reportFacade)
        {
            var reportViewer = new ReportViewer();
            LocalReport localReport = reportViewer.LocalReport;

            localReport.SubreportProcessing += LocalReport_SubreportProcessing;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportFacade.DefinitionFilePath;

            localReport.DataSources.Clear();

            foreach (DataTable datatable in reportFacade.ReportDataSet.Tables)
            {
                localReport.DataSources.Add(new ReportDataSource(datatable.TableName, datatable));
            }

            Warning[] warnings;
            string mimeType, encoding, fileNameExtension;
            string[] streams;

            byte[] bytes = localReport.Render(
                "PDF", 
                null, 
                out mimeType, 
                out encoding, 
                out fileNameExtension,
                out streams, 
                out warnings);

            return bytes;
        }

        private static void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var masterReport = sender as LocalReport;
            e.DataSources.Add(masterReport.DataSources[e.ReportPath]);
        }
    }
}
