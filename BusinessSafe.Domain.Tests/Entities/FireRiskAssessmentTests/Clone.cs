using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class CopyTests
    {
        [Test]
        public void Given_a_FRA_when_copy_then_summary_information_is_copied()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.AssessmentDate = DateTime.Now.Date.AddDays(-5);
            fraToCopy.PersonAppointed = "the person appointed";
            fraToCopy.RiskAssessor = new RiskAssessor() { Id = 3245L };

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(fraToCopy.Title, copiedFra.Title);
            Assert.AreEqual(fraToCopy.Reference, copiedFra.Reference);
            Assert.AreEqual(fraToCopy.AssessmentDate, copiedFra.AssessmentDate);
            Assert.AreEqual(fraToCopy.PersonAppointed, copiedFra.PersonAppointed);
            Assert.AreEqual(fraToCopy.RiskAssessor.Id, copiedFra.RiskAssessor.Id);
        }

        [Test]
        public void Given_a_FRA_when_copy_then_createdby_and_createdDate_are_set()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(currentUser.Id, copiedFra.CreatedBy.Id);
            Assert.AreEqual(copiedFra.CreatedOn.Value.Date, DateTime.Now.Date);

        }

        [Test]
        public void Given_a_FRA_when_copy_then_latest_checklist_is_copied()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraChecklist = new FireRiskAssessmentChecklist
                                   {
                                       Id = 1
                                       ,
                                       Checklist = new Checklist { Id = 1 }
                                       ,
                                       CreatedOn = DateTime.Now.AddDays(-123)
                                       ,
                                       CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
                                   };

            var fraChecklist2 = new FireRiskAssessmentChecklist
            {
                Id = 2
                ,
                Checklist = new Checklist { Id = 2 }
                ,
                CreatedOn = DateTime.Now.AddDays(-10)
                ,
                CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
            };

            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.FireRiskAssessmentChecklists.Clear();
            fraToCopy.FireRiskAssessmentChecklists.Add(fraChecklist);
            fraToCopy.FireRiskAssessmentChecklists.Add(fraChecklist2);

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(1, copiedFra.FireRiskAssessmentChecklists.Count);
            Assert.AreEqual(fraChecklist2.Checklist.Id, copiedFra.LatestFireRiskAssessmentChecklist.Checklist.Id);
            Assert.AreEqual(copiedFra.FireRiskAssessmentChecklists.First().Id, 0);
            Assert.AreEqual(copiedFra.FireRiskAssessmentChecklists.First().CreatedOn.Value.Date, DateTime.Now.Date);
            Assert.AreEqual(copiedFra.FireRiskAssessmentChecklists.First().CreatedBy.Id, currentUser.Id);
        }

        [Test]
        public void Given_a_FRA_when_copy_then_checklist_answers_are_copied()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraChecklist = new FireRiskAssessmentChecklist
            {
                Id = 123123
                ,
                Checklist = new Checklist { Id = 123123 }
                ,
                CreatedOn = DateTime.Now.AddDays(-123)
                ,
                CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
            };
            fraChecklist.Answers.Add(
                new FireAnswer
                {
                    AdditionalInfo = "test",
                    CreatedBy = new UserForAuditing { Id = Guid.NewGuid(), CreatedOn = DateTime.Now }
                    ,
                    FireRiskAssessmentChecklist = fraChecklist,
                    Id = 1
                    ,
                    Question = new Question { Id = 1 }
                    ,
                    YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
                });

            fraChecklist.Answers.Add(
                new FireAnswer
                {
                    AdditionalInfo = "test 5",
                    CreatedBy = new UserForAuditing { Id = Guid.NewGuid(), CreatedOn = DateTime.Now }
                    ,
                    FireRiskAssessmentChecklist = fraChecklist,
                    Id = 2
                    ,
                    Question = new Question { Id = 2 }
                    ,
                    YesNoNotApplicableResponse = YesNoNotApplicableEnum.No
                });

            fraChecklist.Answers.Add(
               new FireAnswer
               {
                   AdditionalInfo = "test 5",
                   CreatedBy = new UserForAuditing { Id = Guid.NewGuid(), CreatedOn = DateTime.Now }
                   ,
                   FireRiskAssessmentChecklist = fraChecklist,
                   Id = 3
                   ,
                   Question = new Question { Id = 3 }
                   ,
                   YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable
               });

            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.FireRiskAssessmentChecklists.Clear();
            fraToCopy.FireRiskAssessmentChecklists.Add(fraChecklist);

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.IsTrue(copiedFra.FireRiskAssessmentChecklists
                              .SelectMany(x => x.Answers)
                              .All(x => x.YesNoNotApplicableResponse == YesNoNotApplicableEnum.Yes || x.YesNoNotApplicableResponse == YesNoNotApplicableEnum.NotApplicable), "Only copy the yes and n/a answers");

            Assert.IsTrue(copiedFra.FireRiskAssessmentChecklists
                              .SelectMany(x => x.Answers)
                              .All(x => x.Id == 0), "Copied answers need to be new entities");

            Assert.IsTrue(copiedFra.FireRiskAssessmentChecklists
                              .SelectMany(x => x.Answers)
                              .All(x => x.CreatedBy == currentUser), "Copied answers need to be new entities");

            Assert.IsTrue(copiedFra.FireRiskAssessmentChecklists
                  .SelectMany(x => x.Answers)
                  .All(x => x.CreatedOn.Value.Date == DateTime.Now.Date), "Copied answers need to be new entities");

        }

        [Test]
        public void Given_a_FRA_when_copy_then_checklist_answers_significant_findings_are_not_copied()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraChecklist = new FireRiskAssessmentChecklist
            {
                Id = 123123
                ,
                Checklist = new Checklist { Id = 123123 }
                ,
                CreatedOn = DateTime.Now.AddDays(-123)
                ,
                CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
            };
            fraChecklist.Answers.Add(
                new FireAnswer
                {
                    AdditionalInfo = "test",
                    CreatedBy = new UserForAuditing { Id = Guid.NewGuid(), CreatedOn = DateTime.Now }
                    ,
                    FireRiskAssessmentChecklist = fraChecklist,
                    Id = 1
                    ,
                    Question = new Question { Id = 1 }
                    ,
                    YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
                    ,
                    SignificantFinding = new SignificantFinding()
                });


            fraChecklist.Answers.Add(
               new FireAnswer
               {
                   AdditionalInfo = "test 5",
                   CreatedBy = new UserForAuditing { Id = Guid.NewGuid(), CreatedOn = DateTime.Now }
                   ,
                   FireRiskAssessmentChecklist = fraChecklist,
                   Id = 3
                   ,
                   Question = new Question { Id = 3 }
                   ,
                   YesNoNotApplicableResponse = YesNoNotApplicableEnum.NotApplicable
               });

            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.FireRiskAssessmentChecklists.Clear();
            fraToCopy.FireRiskAssessmentChecklists.Add(fraChecklist);

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.IsTrue(copiedFra.FireRiskAssessmentChecklists
                              .SelectMany(x => x.Answers)
                              .All(x => x.SignificantFinding == null), "Don't copy the significant findings");

        }

        [Test]
        public void Given_a_FRA_when_copy_then_deleted_answers_are_not_cloned()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraChecklist = new FireRiskAssessmentChecklist
            {
                Id = 123123
                ,
                Checklist = new Checklist { Id = 123123 }
                ,
                CreatedOn = DateTime.Now.AddDays(-123)
                ,
                CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }
                ,
                Answers = new List<FireAnswer>()
            };

            fraChecklist.Answers.Add(new FireAnswer
            {
                CreatedBy = fraChecklist.CreatedBy,
                CreatedOn = fraChecklist.CreatedOn
                ,
                Id = 34534,
                Deleted = false,
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
                ,
                Question = new Question { Id = 1 }
            });

            fraChecklist.Answers.Add(new FireAnswer
            {
                CreatedBy = fraChecklist.CreatedBy,
                CreatedOn = fraChecklist.CreatedOn
                ,
                Id = 456,
                Deleted = true,
                YesNoNotApplicableResponse = YesNoNotApplicableEnum.Yes
                ,
                Question = new Question { Id = 2 }
            });


            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.FireRiskAssessmentChecklists.Clear();
            fraToCopy.FireRiskAssessmentChecklists.Add(fraChecklist);

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            var copiedFraChecklist = copiedFra.FireRiskAssessmentChecklists.First();
            Assert.AreEqual(fraToCopy.FireRiskAssessmentChecklists.First().Answers.Count(x => x.Deleted == false), copiedFraChecklist.Answers.Count(x => x.Deleted == false));

        }

        [Test]
        [Ignore]
        public void Given_a_FRA_when_copy_then_premises_information_is_cloned()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.Location = "location test";
            fraToCopy.BuildingUse = "building use test";
            fraToCopy.NumberOfPeople = 12;
            fraToCopy.NumberOfFloors = 456456;
            fraToCopy.GasEmergencyShutOff = "gas test";
            fraToCopy.WaterEmergencyShutOff = "water test";
            fraToCopy.OtherEmergencyShutOff = "other test";

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(fraToCopy.Location, copiedFra.Location);
            Assert.AreEqual(fraToCopy.BuildingUse, copiedFra.BuildingUse);
            Assert.AreEqual(fraToCopy.NumberOfPeople, copiedFra.NumberOfPeople);
            Assert.AreEqual(fraToCopy.NumberOfFloors, copiedFra.NumberOfFloors);
            Assert.AreEqual(fraToCopy.ElectricityEmergencyShutOff, copiedFra.ElectricityEmergencyShutOff);
            Assert.AreEqual(fraToCopy.GasEmergencyShutOff, copiedFra.GasEmergencyShutOff);
            Assert.AreEqual(fraToCopy.WaterEmergencyShutOff, copiedFra.WaterEmergencyShutOff);
            Assert.AreEqual(fraToCopy.OtherEmergencyShutOff, copiedFra.OtherEmergencyShutOff);

        }

        [Test]
        public void Given_a_FRA_when_copy_then_people_at_risk_are_cloned()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            IEnumerable<RiskAssessmentPeopleAtRisk> peopleAtRisk = new List<RiskAssessmentPeopleAtRisk>()
                                                     {
                                                         new RiskAssessmentPeopleAtRisk
                                                             {
                                                                 PeopleAtRisk = new PeopleAtRisk()
                                                                 {
                                                                     Id = 1, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234)
                                                                 }
                                                             },
                                                         new RiskAssessmentPeopleAtRisk
                                                             {
                                                                 PeopleAtRisk = new PeopleAtRisk()
                                                                 {
                                                                     Id = 2, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234)
                                                                 }
                                                             },
                                                         new RiskAssessmentPeopleAtRisk
                                                             {
                                                                 PeopleAtRisk = new PeopleAtRisk()
                                                                 {
                                                                     Id = 3, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234) 
                                                                 }
                                                             }
                                                     };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.PeopleAtRisk = peopleAtRisk.ToList(); // .AttachPeopleAtRiskToRiskAssessment(peopleAtRisk,currentUser);

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.IsTrue(copiedFra.PeopleAtRisk.Count == fraToCopy.PeopleAtRisk.Count());

        }

        [Test]
        public void Given_a_FRA_when_copy_then_sources_of_ignition_are_cloned()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            IEnumerable<FireRiskAssessmentSourceOfIgnition> sourcesOfIgnition = new List<FireRiskAssessmentSourceOfIgnition>()
                                                                  {
                                                                      new FireRiskAssessmentSourceOfIgnition
                                                                          {
                                                                              SourceOfIgnition = new SourceOfIgnition()
                                                                                                     {
                                                                                                         Id = 1,
                                                                                                         CreatedBy =
                                                                                                             new UserForAuditing
                                                                                                                 {
                                                                                                                     Id
                                                                                                                         =
                                                                                                                         Guid
                                                                                                                         .
                                                                                                                         NewGuid
                                                                                                                         ()
                                                                                                                 },
                                                                                                         CreatedOn =
                                                                                                             DateTime.
                                                                                                             Now.AddDays
                                                                                                             (-1234),
                                                                                                         Name =
                                                                                                             "a spark of genius"
                                                                                                     },
                                                                              FireRiskAssessment = fraToCopy
                                                                          },
                                                                      new FireRiskAssessmentSourceOfIgnition
                                                                          {
                                                                              SourceOfIgnition = new SourceOfIgnition()
                                                                                                     {
                                                                                                         Id = 2,
                                                                                                         CreatedBy =
                                                                                                             new UserForAuditing
                                                                                                                 {
                                                                                                                     Id
                                                                                                                         =
                                                                                                                         Guid
                                                                                                                         .
                                                                                                                         NewGuid
                                                                                                                         ()
                                                                                                                 },
                                                                                                         CreatedOn =
                                                                                                             DateTime.
                                                                                                             Now.AddDays
                                                                                                             (-1234)
                                                                                                     },
                                                                              FireRiskAssessment = fraToCopy
                                                                          },
                                                                      new FireRiskAssessmentSourceOfIgnition
                                                                          {
                                                                              SourceOfIgnition = new SourceOfIgnition()
                                                                                                     {
                                                                                                         Id = 3,
                                                                                                         CreatedBy =
                                                                                                             new UserForAuditing
                                                                                                                 {
                                                                                                                     Id
                                                                                                                         =
                                                                                                                         Guid
                                                                                                                         .
                                                                                                                         NewGuid
                                                                                                                         ()
                                                                                                                 },
                                                                                                         CreatedOn =
                                                                                                             DateTime.
                                                                                                             Now.AddDays
                                                                                                             (-1234)
                                                                                                     },
                                                                              FireRiskAssessment = fraToCopy
                                                                          }
                                                                  };

            fraToCopy.FireRiskAssessmentSourcesOfIgnition = sourcesOfIgnition.ToList();

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(fraToCopy.FireRiskAssessmentSourcesOfIgnition.Count(), copiedFra.FireRiskAssessmentSourcesOfIgnition.Count);


        }

        [Test]
        public void Given_a_FRA_when_copy_then_sources_of_fuel_are_cloned()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            IEnumerable<FireRiskAssessmentSourceOfFuel> sourcesOfFuel = new List<FireRiskAssessmentSourceOfFuel>()
                                                                            {
                                                                                new FireRiskAssessmentSourceOfFuel
                                                                                    {
                                                                                        SourceOfFuel = new SourceOfFuel
                                                                                                           {
                                                                                                               Id = 1,
                                                                                                               CreatedBy
                                                                                                                   =
                                                                                                                   new UserForAuditing
                                                                                                                       {
                                                                                                                           Id
                                                                                                                               =
                                                                                                                               Guid
                                                                                                                               .
                                                                                                                               NewGuid
                                                                                                                               ()
                                                                                                                       },
                                                                                                               CreatedOn
                                                                                                                   =
                                                                                                                   DateTime
                                                                                                                   .Now.
                                                                                                                   AddDays
                                                                                                                   (-1234),
                                                                                                               Name =
                                                                                                                   "toxic air events"
                                                                                                           },
                                                                                        FireRiskAssessment = fraToCopy
                                                                                    },
                                                                                new FireRiskAssessmentSourceOfFuel
                                                                                    {
                                                                                        SourceOfFuel =
                                                                                            new SourceOfFuel()
                                                                                                {
                                                                                                    Id = 2,
                                                                                                    CreatedBy =
                                                                                                        new UserForAuditing
                                                                                                            {
                                                                                                                Id =
                                                                                                                    Guid
                                                                                                                    .
                                                                                                                    NewGuid
                                                                                                                    ()
                                                                                                            },
                                                                                                    CreatedOn =
                                                                                                        DateTime.Now.
                                                                                                        AddDays(-1234)
                                                                                                },
                                                                                        FireRiskAssessment = fraToCopy
                                                                                    },
                                                                                new FireRiskAssessmentSourceOfFuel
                                                                                    {
                                                                                        SourceOfFuel =
                                                                                            new SourceOfFuel()
                                                                                                {
                                                                                                    Id = 3,
                                                                                                    CreatedBy =
                                                                                                        new UserForAuditing
                                                                                                            {
                                                                                                                Id =
                                                                                                                    Guid
                                                                                                                    .
                                                                                                                    NewGuid
                                                                                                                    ()
                                                                                                            },
                                                                                                    CreatedOn =
                                                                                                        DateTime.Now.
                                                                                                        AddDays(-1234)
                                                                                                },
                                                                                        FireRiskAssessment = fraToCopy
                                                                                    }
                                                                            };
            fraToCopy.FireRiskAssessmentSourcesOfFuel = sourcesOfFuel.ToList();

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(fraToCopy.FireRiskAssessmentSourcesOfFuel.Count(), copiedFra.FireRiskAssessmentSourcesOfFuel.Count);

        }

        [Test]
        public void Given_a_FRA_when_copy_then_fire_safety_control_measures_are_cloned()
        {
            //given
            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            IEnumerable<FireRiskAssessmentControlMeasure> fireSafetyControlMeasures = new List<FireRiskAssessmentControlMeasure>()
                                                     {
                                                         new FireRiskAssessmentControlMeasure
                                                             {
                                                          FireSafetyControlMeasure = new FireSafetyControlMeasure()
                                                             {
                                                                 Id = 1, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234),Name = "toxic air events"
                                                             },
                                                             RiskAssessment = fraToCopy
                                                             },
                                                         new FireRiskAssessmentControlMeasure
                                                             {
                                                          FireSafetyControlMeasure = new FireSafetyControlMeasure()
                                                             {
                                                                 Id = 2, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234)
                                                             },
                                                             RiskAssessment = fraToCopy
                                                             },
                                                         new FireRiskAssessmentControlMeasure
                                                             {
                                                          FireSafetyControlMeasure = new FireSafetyControlMeasure()
                                                             {
                                                                 Id = 3, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234) 
                                                             },
                                                             RiskAssessment = fraToCopy
                                                             },
                                                     };

            fraToCopy.FireSafetyControlMeasures = fireSafetyControlMeasures.ToList();

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(fraToCopy.FireSafetyControlMeasures.Count(), copiedFra.FireSafetyControlMeasures.Count);

        }

        [Test]
        public void Given_a_FRA_when_copy_then_attached_documents_are_cloned()
        {

            var currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            IEnumerable<RiskAssessmentDocument> documents = new List<RiskAssessmentDocument>()
                                                     {
                                                          new RiskAssessmentDocument()
                                                             {
                                                                 Id = 1, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234),Description = "doc description",DocumentLibraryId = 123
                                                             },
                                                         new RiskAssessmentDocument()
                                                             {
                                                                 Id = 2, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234)
                                                             }
                                                         ,
                                                         new RiskAssessmentDocument()
                                                             {
                                                                 Id = 3, CreatedBy = new UserForAuditing { Id = Guid.NewGuid() }, CreatedOn = DateTime.Now.AddDays(-1234) 
                                                             }
                                                     };
            var fraToCopy = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            fraToCopy.Documents = documents.ToList();

            //when
            var copiedFra = fraToCopy.Copy(currentUser);

            //then
            Assert.AreEqual(fraToCopy.Documents.Count(), copiedFra.Documents.Count);

            Assert.IsTrue(copiedFra.Documents.All(x => x.Id == 0));
            Assert.IsTrue(copiedFra.Documents.All(x => x.CreatedBy.Id == currentUser.Id)); //ensure that all cloned are new entities
            Assert.IsTrue(copiedFra.Documents.All(x => x.CreatedOn.Value.Date == DateTime.Now.Date)); //ensure that all cloned are new entities
            Assert.IsTrue(copiedFra.Documents.Any(x => x.Description == documents.First().Description));

            Assert.AreEqual(fraToCopy, copiedFra.Documents.First().RiskAssessment);
        }
    }
}
