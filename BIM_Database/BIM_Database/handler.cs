using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;



namespace BIM_Database
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Handler : IExternalEventHandler
    {
        
        public List<string> datagridview_ids
        {
            get;
            set;
        }
        
        public void Execute(UIApplication app)
        {
            Document document = app.ActiveUIDocument.Document;
            UIDocument uidoc = new UIDocument(document);
            Document doc = uidoc.Document;

            ICollection<ElementId> elementIds = new List<ElementId>();
            List<string> gridvies_ids = datagridview_ids;
            foreach (string id in datagridview_ids)
            {
                ElementId elementId = new ElementId(int.Parse(id));
                elementIds.Add(elementId);
            }



            uidoc.Selection.SetElementIds(elementIds);

        }

        public string GetName()
        {
            return "Event handler is working now!!";
        }
    }
}
