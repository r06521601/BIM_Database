using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System;

using System.Data;
using System.Linq;



namespace BIM_Database
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class OOD : IExternalEventHandler
    {
        
        public DataTable dataTable
        {
            get;
            set;
        }
        public double timespend
        {
            get;
            set;

        }
        public int number
        {
            get;
            set;

        }

        public void Execute(UIApplication app)
        {
            timespend = 0;
            UIDocument uidoc = app.ActiveUIDocument;
            Autodesk.Revit.DB.Document doc = uidoc.Document;
            
            ICollection<FamilyInstance> familyInstances = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>().ToList();
            ICollection<FamilyInstance> windows = familyInstances.Where(x => x.Category.Name == "窗").ToList();
            ICollection<Level> levels = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().ToList();
            ICollection<FamilyInstance> result = new List<FamilyInstance>();
            DateTime time = DateTime.Now;
            for(int i = 0; i < 100; i++)
            {
                result = windows.Where(x => x.LevelId == levels.Where(l => l.Name == "6F").First().Id).ToList();
            }
            DateTime time2 = DateTime.Now;
            timespend = (time2 - time).TotalMilliseconds;
            number = result.Count;
        }

        public string GetName()
        {
            return "Event handler is working now!!";
        }
    }
}
