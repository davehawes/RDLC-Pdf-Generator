// -----------------------------------------------------------------------
// <copyright file="GeneratorTestFixtures.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PdfCreator.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    using PdfCreator.Reports;
    using PdfCreator.Reports.MasterDetailSample;
    using PdfCreator.Reports.Sample1;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class GeneratorTestFixtures
    {
        [Test]
        public void CreatePdf_SimpleDataSet_ValidPdfFile()
        {
            var dataset = CreateSample1Dataset();
            var reportDefinition = new ReportFromFileDefinitionFacade(dataset, "Reports\\Sample1\\Template.rdlc");
            var pdfFile = Reports.Generator.CreatePdf(reportDefinition);

            var filestream = File.Create("C:\\test.pdf");
            filestream.Write(pdfFile, 0, pdfFile.Length);
            filestream.Flush(true);
            filestream.Close();
        }

        private static Dataset CreateSample1Dataset()
        {
            var dataset = new Reports.Sample1.Dataset();
            var masterDataRow = dataset.MasterData.NewMasterDataRow();

            masterDataRow.Id = 1;
            masterDataRow.Name = "Master Data Row 1";
            masterDataRow.Description = "Description about data row 1";

            dataset.MasterData.AddMasterDataRow(masterDataRow);
            return dataset;
        }

        [Test]
        public void CreatePdf_SampleReportViaStreamDefinition_ValidPdfFile()
        {
            var dataset = CreateSample1Dataset();
            var reportDefinition = new ReportFromStreamDefinitionFacade(
                dataset, File.ReadAllBytes("Reports\\Sample1\\Template.rdlc"));
            
            var pdfFile = Reports.Generator.CreatePdf(reportDefinition);

            var filestream = File.Create("C:\\test.pdf");
            filestream.Write(pdfFile, 0, pdfFile.Length);
            filestream.Flush(true);
            filestream.Close();
        }

        [Test]
        public void CreatePdf_MasterDetailDataSet_ValidPdfFile()
        {
            var dataset = CreateMasterDetailDataset();
            var reportDefinition = new ReportFromFileDefinitionFacade(dataset, "Reports\\MasterDetailSample\\Template.rdlc");
            var pdfFile = Reports.Generator.CreatePdf(reportDefinition);

            var filestream = File.Create("C:\\test.pdf");
            filestream.Write(pdfFile, 0, pdfFile.Length);
            filestream.Flush(true);
            filestream.Close();
        }

        [Test]
        public void CreatePdf_MasterDetailDatasetFromStream_ValidPdfFile()
        {
            var dataset = CreateMasterDetailDataset();

            var report = new ReportFromStreamDefinitionFacade(dataset, File.ReadAllBytes("Reports\\MasterDetailSample\\Template.rdlc"));

            report.SubReports.Add(new SubReportFromStreamDefinitionFacade(File.ReadAllBytes("Reports\\MasterDetailSample\\Template_Detail.rdlc"), "Template_Detail"));
            
            var pdfFile = Reports.Generator.CreatePdf(report);

            var filestream = File.Create("C:\\test.pdf");
            filestream.Write(pdfFile, 0, pdfFile.Length);
            filestream.Flush(true);
            filestream.Close();
        }

        private static DataSet CreateMasterDetailDataset()
        {
            var dataset = new Reports.MasterDetailSample.DataSet();
            var masterDataRow = dataset.Template.NewTemplateRow();

            masterDataRow.Id = 1;
            masterDataRow.Name = "Master Data Row 1";
            masterDataRow.Description = "Description about master data row 1";

            dataset.Template.AddTemplateRow(masterDataRow);

            var detailDataRow = dataset.Template_Detail.NewTemplate_DetailRow();
            detailDataRow.Description = "Description about detail data row 1";
            detailDataRow.Name = "Name of description row 1";
            detailDataRow.MasterDataId = 1;
            detailDataRow.Id = 4;

            dataset.Template_Detail.AddTemplate_DetailRow(detailDataRow);
            return dataset;
        }
    }
}
