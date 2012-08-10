// -----------------------------------------------------------------------
// <copyright file="ReportFromFileDefinitionFacade.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PdfCreator.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ReportFromFileDefinitionFacade : ReportFacadeBase
    {
        private string definitionFilePath;

        public ReportFromFileDefinitionFacade(DataSet dataset, string definitionFilePath) : base(dataset)
        {
            this.definitionFilePath = definitionFilePath;
        }

        public string DefinitionFilePath
        {
            get
            {
                return this.definitionFilePath;
            }
        }
    }
}