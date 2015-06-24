using System;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Common.TemplateEngine
{
    [TestFixture]
    public class RenderTests
    {
        [TestCase("apple")]
        [TestCase("orange")]
        public void Given_that_model_has_name_When_template_is_rendered_Then_text_is_rendered_correctly(string name)
        {
            //Given
            string expectedResult = String.Format("This is a test, the name should appear here : {0}", name);
            var target = new Application.Common.TemplateEngine();

            var testObject = new Fruit {Name = name};

            //When
            var result = target.Render(testObject, "This is a test, the name should appear here : $Model.Name$");

            //Then
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }

    public class Fruit
    {
        public string Name { get; set; }
    }
}
