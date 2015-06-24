using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SafeCheckTests.QuestionTests
{
    [TestFixture]
    public class QuestionIndustryTests
    {

        [Test]
        public virtual void Given_a_valid_question_When_we_add_an_industry_Then_the_question_is_updated_correctly()
        {
            var question = new Question();

            Assert.AreEqual( 0, question.Industries.Count);

            ChecklistTemplate ind = new ChecklistTemplate();
            ind.Id = Guid.NewGuid();

            question.AddIndustry(ind, null);

            Assert.AreEqual( 1, question.Industries.Count);
            Assert.AreEqual( ind.Id, question.Industries[0].ChecklistTemplate.Id);            
        }

        [Test]
        public virtual void Given_a_industry_is_associated_with_question_When_we_add_the_same_industry_Then_a_duplicate_is_not_added()
        {
            //GIVEN
            var industryId = Guid.NewGuid();
            var question = new Question();
            ChecklistTemplate ind = new ChecklistTemplate();
            ind.Id = industryId;

            question.AddIndustry(ind, null);
            question.AddIndustry(ind, null);

            Assert.AreEqual(1, question.Industries.Count);
            Assert.AreEqual(ind.Id, question.Industries[0].ChecklistTemplate.Id);
        }

        [Test]
        public virtual void Given_industry_association_is_deleted_when_we_add_industry_then_industry_is_restored_rather_than_creating_a_new_one()
        {
            //GIVEN
            var question = new Question();
            ChecklistTemplate ind1 = new ChecklistTemplate();
            ind1.Id = Guid.NewGuid();

            ChecklistTemplate ind2 = new ChecklistTemplate();
            ind2.Id = Guid.NewGuid();

            question.AddIndustry(ind1, null);
            question.AddIndustry(ind2, null);
            question.RemoveIndustry(ind1, null);

            //WHEN
            question.AddIndustry(ind1, null);

            //THEN
            Assert.AreEqual(0, question.Industries.Count(x => x.Deleted));
            Assert.AreEqual(2, question.Industries.Count(x => x.Deleted == false));
            Assert.AreEqual(1, question.Industries.Count(x => !x.Deleted && x.ChecklistTemplate.Id == ind1.Id));
        }

        [Test]
        public virtual void Given_industry_associated_with_question_when_we_remove_industry_then_industry_is_deleted()
        {
            var question = new Question();

            ChecklistTemplate ind1 = new ChecklistTemplate();
            ind1.Id = Guid.NewGuid();

            ChecklistTemplate ind2 = new ChecklistTemplate();
            ind2.Id = Guid.NewGuid();

            question.AddIndustry(ind1, null);
            question.AddIndustry(ind2, null);

            //WHEN
            question.RemoveIndustry(ind1, null);

            //THEN
            Assert.AreEqual(1, question.Industries.Count(x => x.Deleted));
            Assert.AreEqual(1, question.Industries.Count(x => x.Deleted == false));
            Assert.AreEqual(1, question.Industries.Count(x => x.Deleted && x.ChecklistTemplate.Id == ind1.Id));
        }

     
    }
}
