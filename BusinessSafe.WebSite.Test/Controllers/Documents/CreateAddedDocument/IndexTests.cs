using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.WebSite.Areas.Documents.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.CreateAddedDocument
{
    [TestFixture]
    public class IndexTests
    {
        private CreateAddedDocumentController _target;
        private Mock<IAddedDocumentsService> _addedDocumentsService;

        [SetUp]
        public void SetUp()
        {
            _addedDocumentsService = new Mock<IAddedDocumentsService>();
            _target = new CreateAddedDocumentController(_addedDocumentsService.Object);
        }

        [Test]
        public void Index_Returns_PartialViewResult()
        {
            var result = _target.Index(It.IsAny<long>());

            Assert.IsInstanceOf<PartialViewResult>(result);
        }
    }
}