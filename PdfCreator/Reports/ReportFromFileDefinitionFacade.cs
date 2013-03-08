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