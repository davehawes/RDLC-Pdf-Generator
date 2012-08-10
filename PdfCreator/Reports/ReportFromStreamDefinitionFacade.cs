// -----------------------------------------------------------------------
// <copyright file="ReportFromStreamDefinitionFacade.cs" company="Microsoft">
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ReportFromStreamDefinitionFacade : ReportFacadeBase
    {
        private Stream definitionStream;

        private string name;

        private List<SubReportFromStreamDefinitionFacade> subReports = new List<SubReportFromStreamDefinitionFacade>();

        public ReportFromStreamDefinitionFacade(DataSet dataset, byte[] definition, string name)
            : this(dataset, new MemoryStream(definition), name)
        {
        }

        public ReportFromStreamDefinitionFacade(DataSet dataSet, byte[] definition)
            : this(dataSet, definition, string.Empty)
        {
        }

        public ReportFromStreamDefinitionFacade(DataSet dataSet, Stream definition)
            : this(dataSet, definition, string.Empty)
        {
        }

        public ReportFromStreamDefinitionFacade(DataSet dataSet, Stream definition, string name)
            : base(dataSet)
        {
            this.definitionStream = definition;
            this.name = name;
        }

        public Stream Definition
        {
            get
            {
                return definitionStream;
            }
        }

        public List<SubReportFromStreamDefinitionFacade> SubReports
        {
            get
            {
                return subReports;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
