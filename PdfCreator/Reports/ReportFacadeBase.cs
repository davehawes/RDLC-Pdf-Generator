// -----------------------------------------------------------------------
// <copyright file="ReportFacade.cs" company="Microsoft">
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
    public abstract class ReportFacadeBase
    {
        public ReportFacadeBase(DataSet dataSet)
        {
            this.reportDataSet = dataSet;
        }

        private DataSet reportDataSet;

        public DataSet ReportDataSet
        {
            get
            {
                return reportDataSet;
            }
        }
    }
}
