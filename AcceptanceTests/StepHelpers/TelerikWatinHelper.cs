using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepHelpers
{
    public static class TelerikWatinHelper
    {
        public static TableBody GetTableBody(this IE ie, string tableId)
        {
            var tasksListWrapper = ie.Div(Find.ById(tableId));
            var tableBody = tasksListWrapper.Tables[0].TableBodies[0];
            
            return tableBody;
        }
    }
}