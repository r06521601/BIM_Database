using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Plumbing;
using System.Windows.Forms;

namespace BIM_Database
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Database : IExternalCommand
    {
        public static string me;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Form1 f = new Form1(commandData.Application.ActiveUIDocument);
            f.Show();
            return Result.Succeeded;
        }
    }
}