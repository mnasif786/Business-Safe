using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Users
{
    [Binding]
    public class CreateUserSteps : BaseSteps
    {
        [Given(@"I have an employee using an email that is already registered in Peninsula Online")]
        public void GivenIHaveAnEmployeeUsingAnEmailThatIsAlreadyRegisteredInPeninsulaOnline()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {

            }
        }

    }
}
