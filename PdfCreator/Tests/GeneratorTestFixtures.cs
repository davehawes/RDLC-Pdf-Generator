// -----------------------------------------------------------------------
// <copyright file="GeneratorTestFixtures.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PdfCreator.Tests
{
    using System.IO;
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
            var file = Reports.Generator.CreateFile(reportDefinition);

            SaveFile(file, "SimpleDataSet.pdf");
        }
        
        [Test]
        public void CreatePdf_SampleReportViaStreamDefinition_ValidPdfFile()
        {
            var dataset = CreateSample1Dataset();
            var reportDefinition = new ReportFromStreamDefinitionFacade(dataset, File.ReadAllBytes("Reports\\Sample1\\Template.rdlc"));
            var file = Reports.Generator.CreateFile(reportDefinition);

            SaveFile(file, "SimpleDataSetViaStreamDefinition.pdf");
        }

        [Test]
        public void CreatePdf_MasterDetailDataSet_ValidPdfFile()
        {
            var dataset = CreateMasterDetailDataset();
            var reportDefinition = new ReportFromFileDefinitionFacade(dataset, "Reports\\MasterDetailSample\\Template.rdlc");
            var file = Reports.Generator.CreateFile(reportDefinition);

            SaveFile(file, "MasterDetailDataSet.pdf");
        }

        [Test]
        public void CreatePdf_MasterDetailDataSet_ValidWordFile()
        {
            var dataset = CreateMasterDetailDataset();
            var reportDefinition = new ReportFromFileDefinitionFacade(dataset, "Reports\\MasterDetailSample\\Template.rdlc");
            var file = Reports.Generator.CreateFile(reportDefinition, Generator.FileTypes.WORD);

            SaveFile(file, "MasterDetailDataSet.doc");
        }

        [Test]
        public void CreatePdf_MasterDetailDataSet_ValidExcelFile()
        {
            var dataset = CreateMasterDetailDataset();
            var reportDefinition = new ReportFromFileDefinitionFacade(dataset, "Reports\\MasterDetailSample\\Template.rdlc");
            var file = Reports.Generator.CreateFile(reportDefinition, Generator.FileTypes.EXCEL);

            SaveFile(file, "MasterDetailDataSet.xls");
        }

        [Test]
        public void CreatePdf_MasterDetailDataSet_ValidImageFile()
        {
            var dataset = CreateMasterDetailDataset();
            var reportDefinition = new ReportFromFileDefinitionFacade(dataset, "Reports\\MasterDetailSample\\Template.rdlc");
            var file = Reports.Generator.CreateFile(reportDefinition, Generator.FileTypes.IMAGE);

            SaveFile(file, "MasterDetailDataSet.jpg");
        }

        [Test]
        public void CreatePdf_MasterDetailDatasetFromStream_ValidPdfFile()
        {
            var dataset = CreateMasterDetailDataset();
            var report = new ReportFromStreamDefinitionFacade(dataset, File.ReadAllBytes("Reports\\MasterDetailSample\\Template.rdlc"));

            report.SubReports.Add(new SubReportFromStreamDefinitionFacade(File.ReadAllBytes("Reports\\MasterDetailSample\\Template_Detail.rdlc"), "Template_Detail"));            
            var file = Reports.Generator.CreateFile(report);

            SaveFile(file, "MasterDetailDatasetFromStream.pdf");
        }

        private static void SaveFile(byte[] pdfFile, string fileName)
        {
            var filestream = File.Create(fileName);
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

        private static DataSet CreateMasterDetailDataset()
        {
            var dataset = new Reports.MasterDetailSample.DataSet();
            var masterDataRow = dataset.Template.NewTemplateRow();

            masterDataRow.Id = 1;
            masterDataRow.Name = "Master Data Row 1";
            masterDataRow.Description = "Description about master data row 1";

            dataset.Template.AddTemplateRow(masterDataRow);

            masterDataRow = dataset.Template.NewTemplateRow();
            masterDataRow.Id = 3;
            masterDataRow.Name = "Master Data Row 3";
            masterDataRow.Description = "Description about master data row 3";

            dataset.Template.AddTemplateRow(masterDataRow);

            var detailDataRow = dataset.Template_Detail.NewTemplate_DetailRow();
            detailDataRow.Description = "Master 1, detail data row 1";
            detailDataRow.Name = "Name of description row 1";
            detailDataRow.MasterDataId = 1;
            detailDataRow.Id = 4;

            dataset.Template_Detail.AddTemplate_DetailRow(detailDataRow);

            detailDataRow = dataset.Template_Detail.NewTemplate_DetailRow();
            detailDataRow.Description = "Master 1, about detail data row 2";
            detailDataRow.Name = "Name of description row 2";
            detailDataRow.MasterDataId = 1;
            detailDataRow.Id = 7;

            dataset.Template_Detail.AddTemplate_DetailRow(detailDataRow);


            detailDataRow = dataset.Template_Detail.NewTemplate_DetailRow();
            detailDataRow.Description = "Master 3, Detail data row 1";
            detailDataRow.Name = "Name of description row 1";
            detailDataRow.MasterDataId = 3;
            detailDataRow.Id = 10;

            dataset.Template_Detail.AddTemplate_DetailRow(detailDataRow);

            detailDataRow = dataset.Template_Detail.NewTemplate_DetailRow();
            detailDataRow.Description = "Master 3, detail data row 2";
            detailDataRow.Name = "Name of description row 2";
            detailDataRow.MasterDataId = 3;
            detailDataRow.Id = 11;

            dataset.Template_Detail.AddTemplate_DetailRow(detailDataRow);

            return dataset;
        }
    }
}
