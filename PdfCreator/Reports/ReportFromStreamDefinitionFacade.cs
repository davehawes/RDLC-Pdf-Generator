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
----------------------------------------------------------------------- */

namespace SeeTheLink.PdfCreator.Reports
{
    using System.Collections.Generic;
    using System.Data;
    using System.IO;

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
                return this.definitionStream;
            }
        }

        public List<SubReportFromStreamDefinitionFacade> SubReports
        {
            get
            {
                return this.subReports;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}
