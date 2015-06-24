using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepHelpers
{
    [Binding]
    public class TaskListStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public IEnumerable<BusinessSafeDropDownList> EventListConverter(Table table)
        {
            var bindingList = new BindingList<BusinessSafeDropDownList>();
            foreach (var tableRow in table.Rows)
            {
                var name = GetName(tableRow);
                var values = GetValues(tableRow);

                bindingList.Add(new BusinessSafeDropDownList(name, values));
            }

            return bindingList;
        }

        private static List<string> GetValues(TableRow tableRow)
        {
            return tableRow[1].Trim().Split(',').ToList();
        }

        private static string GetName(TableRow tableRow)
        {
            return tableRow[0].Trim();
        }
    }

    /// <summary>
    /// This class is only used in our Specflow tests (TODO: put them in the right folder)
    /// </summary>
    public class BusinessSafeDropDownList
    {
        public string Name { get; private set; }
        public List<string> ListItemValues { get; private set; }
        public BusinessSafeDropDownList(string name, List<string> listItemValues)
        {
            Name = name;
            ListItemValues = listItemValues;
        }
    }
}
