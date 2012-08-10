// -----------------------------------------------------------------------
// <copyright file="SubReportFromStreamDefinitionFacade.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace PdfCreator.Reports
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SubReportFromStreamDefinitionFacade
    {
        private string name;

        private Stream definition;

        public SubReportFromStreamDefinitionFacade(byte[] definition, string name) : this (new MemoryStream(definition), name)
        {
        }

        public SubReportFromStreamDefinitionFacade(Stream definition, string name)
        {
            this.definition = definition;
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Stream Definition
        {
            get
            {
                return definition;
            }
        }
    }
}
