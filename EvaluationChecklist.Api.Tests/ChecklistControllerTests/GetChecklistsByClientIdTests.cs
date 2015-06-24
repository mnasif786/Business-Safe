using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Data.Repository.SafeCheck;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using EvaluationChecklist.ClientDetails;
using EvaluationChecklist.Controllers;
using EvaluationChecklist.Helpers;
using Moq;
using NUnit.Framework;

namespace EvaluationChecklist.Api.Tests.ChecklistControllerTests
{
    [TestFixture]
    public class GetChecklistsByClientIdTests
    {
        private Mock<ICheckListRepository> _checklistRepo;
        private Mock<IClientDetailsService> _clientDetailsService;
        private Mock<IDependencyFactory> _dependencyFactory;

        [SetUp]
        public void Setup()
        {
            _checklistRepo = new Mock<ICheckListRepository>();
            _clientDetailsService = new Mock<IClientDetailsService>();

            _dependencyFactory = new Mock<IDependencyFactory>();
            _dependencyFactory.Setup(x => x.GetInstance<ICheckListRepository>())
                .Returns(_checklistRepo.Object);
            _dependencyFactory.Setup(x => x.GetInstance<IClientDetailsService>())
                .Returns(_clientDetailsService.Object);
        }

        [Test]
        public void Given_a_checklist_then_the_view_model_site_is_set()
        {
            var site = new SiteAddressResponse() {Id = 1212431241,  SiteName = "the site name", Address1 = "Address1", Postcode = "the postcode", };

            _clientDetailsService
                .Setup(x => x.GetSite(It.IsAny<int>(),It.IsAny<int>()))
                .Returns(site);

            _clientDetailsService
                .Setup(x => x.GetSites(It.IsAny<int>()))
                .Returns( new List<SiteAddressResponse>{ site} );

            _checklistRepo
                .Setup(x => x.GetByClientId(It.IsAny<long>()))
                .Returns(new List<Checklist>() { new Checklist() { SiteId = (int?) site.Id} });
                        

            //when
            var target = new ChecklistController(_dependencyFactory.Object);


            var result = target.GetByClientId(123414);
            Assert.That(result.First().Site.SiteName, Is.EqualTo(site.SiteName));
            Assert.That(result.First().Site.Postcode, Is.EqualTo(site.Postcode));
            
        }

        //[Test]
        //public void Given_a_checklist_has_immediate_risk_notifications_attached_When_GetByClientId_called_Then_IRNs_are_returned()
        //{
        //    var site = new SiteAddressResponse() { Id = 1212431241, SiteName = "the site name", Address1 = "Address1", Postcode = "the postcode", };

        //    _clientDetailsService
        //        .Setup(x => x.GetSite(It.IsAny<int>(), It.IsAny<int>()))
        //        .Returns(site);

        //    _clientDetailsService
        //        .Setup(x => x.GetSites(It.IsAny<int>()))
        //        .Returns(new List<SiteAddressResponse> { site });

        //    var checklist1 = new Checklist();
        //    checklist1.Id = Guid.NewGuid();
        //    checklist1.ClientId = 2424;
        //    checklist1.SiteId = (int?) site.Id;
        //    var immediateRiskNotification1 = new ImmediateRiskNotification();
        //    immediateRiskNotification1.Id = Guid.NewGuid();
        //    immediateRiskNotification1.Reference = "Reference 1";
        //    immediateRiskNotification1.Title = "Title 1";
        //    immediateRiskNotification1.SignificantHazardIdentified = "Significant Hazard Identified 1";
        //    immediateRiskNotification1.RecommendedImmediateAction = "Recommended Imediate Action 1";
        //    checklist1.ImmediateRiskNotifications.Add(immediateRiskNotification1);
        //    var immediateRiskNotification2 = new ImmediateRiskNotification();
        //    immediateRiskNotification2.Id = Guid.NewGuid();
        //    immediateRiskNotification2.Reference = "Reference 2";
        //    immediateRiskNotification2.Title = "Title 2";
        //    immediateRiskNotification2.SignificantHazardIdentified = "Significant Hazard Identified 2";
        //    immediateRiskNotification2.RecommendedImmediateAction = "Recommended Imediate Action 2";
        //    checklist1.ImmediateRiskNotifications.Add(immediateRiskNotification2);
        //    var checklist2 = new Checklist();
        //    checklist2.Id = Guid.NewGuid();
        //    checklist2.ClientId = 2424;
        //    checklist1.SiteId = (int?)site.Id;
        //    var immediateRiskNotification3 = new ImmediateRiskNotification();
        //    immediateRiskNotification3.Id = Guid.NewGuid();
        //    immediateRiskNotification3.Reference = "Reference 3";
        //    immediateRiskNotification3.Title = "Title 3";
        //    immediateRiskNotification3.SignificantHazardIdentified = "Significant Hazard Identified 3";
        //    immediateRiskNotification3.RecommendedImmediateAction = "Recommended Imediate Action 3";
        //    checklist2.ImmediateRiskNotifications.Add(immediateRiskNotification3);

        //    _checklistRepo
        //        .Setup(x => x.GetByClientId((long)(checklist1.ClientId)))
        //        .Returns(new List<Checklist>{ checklist1, checklist2 });

        //    var target = new ChecklistController(_dependencyFactory.Object);

        //    var result = target.GetByClientId(checklist1.ClientId.Value);

        //    Assert.That(result.Count, Is.EqualTo(2));
        //    Assert.That(result[0].ImmediateRiskNotifications.Count, Is.EqualTo(2));
        //    Assert.That(result[0].ImmediateRiskNotifications[0].Id, Is.EqualTo(immediateRiskNotification1.Id));
        //    Assert.That(result[0].ImmediateRiskNotifications[0].Reference, Is.EqualTo(immediateRiskNotification1.Reference));
        //    Assert.That(result[0].ImmediateRiskNotifications[0].Title, Is.EqualTo(immediateRiskNotification1.Title));
        //    Assert.That(result[0].ImmediateRiskNotifications[0].SignificantHazard, Is.EqualTo(immediateRiskNotification1.SignificantHazardIdentified));
        //    Assert.That(result[0].ImmediateRiskNotifications[0].RecommendedImmediate, Is.EqualTo(immediateRiskNotification1.RecommendedImmediateAction));
        //    Assert.That(result[0].ImmediateRiskNotifications[1].Id, Is.EqualTo(immediateRiskNotification2.Id));
        //    Assert.That(result[0].ImmediateRiskNotifications[1].Reference, Is.EqualTo(immediateRiskNotification2.Reference));
        //    Assert.That(result[0].ImmediateRiskNotifications[1].Title, Is.EqualTo(immediateRiskNotification2.Title));
        //    Assert.That(result[0].ImmediateRiskNotifications[1].SignificantHazard, Is.EqualTo(immediateRiskNotification2.SignificantHazardIdentified));
        //    Assert.That(result[0].ImmediateRiskNotifications[1].RecommendedImmediate, Is.EqualTo(immediateRiskNotification2.RecommendedImmediateAction));
        //    Assert.That(result[1].ImmediateRiskNotifications.Count, Is.EqualTo(1));
        //    Assert.That(result[1].ImmediateRiskNotifications[0].Id, Is.EqualTo(immediateRiskNotification3.Id));
        //    Assert.That(result[1].ImmediateRiskNotifications[0].Reference, Is.EqualTo(immediateRiskNotification3.Reference));
        //    Assert.That(result[1].ImmediateRiskNotifications[0].Title, Is.EqualTo(immediateRiskNotification3.Title));
        //    Assert.That(result[1].ImmediateRiskNotifications[0].SignificantHazard, Is.EqualTo(immediateRiskNotification3.SignificantHazardIdentified));
        //    Assert.That(result[1].ImmediateRiskNotifications[0].RecommendedImmediate, Is.EqualTo(immediateRiskNotification3.RecommendedImmediateAction));
        
        //}
    }
}
