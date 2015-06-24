using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace BusinessSafe.Application.IntegrationTests.Config.Tests
{
    public abstract class BaseConfigTests
    {
        protected XElement FindElementByAttributesFromElements(string attributeName, string attributeValue, IEnumerable<XElement> elements)
        {
            var descendants = elements
                .Descendants();

            var element = descendants
                .Where(x => x.HasAttributes && x.Attribute(attributeName) != null)
                .SingleOrDefault(x => x.Attribute(attributeName).Value == attributeValue);

            return element;
        }
    }
}
