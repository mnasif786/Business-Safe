using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.DoesNonEmployeeNameExistGuardTests
{
    [TestFixture]
    [Category("Unit")]
    public class ExecuteTests
    {
        private readonly Mock<INonEmployeeRepository> _repo = new Mock<INonEmployeeRepository>();

        [Test]
        public void Given_that_repo_returns_zero_When_execute_Then_response_should_indicate_false_no_matches()
        {
            //Given
            var target = CreateDoesNonEmployeeAlreadyExistGuard();

            _repo
                .Setup(x => x.GetAllByNameSearch("name", 0, 1))
                .Returns(new List<NonEmployee>());

            //When
            var result = target.Execute(new GuardDefaultExistsRequest("name", 1));

            //Then
            Assert.False(result.Exists);
            Assert.That(result.MatchingResults.Count(),Is.EqualTo(0));
        }

        [Test]
        public void Given_that_repo_returns_one_or_more_When_execute_Then_response_should_indicate_true_and_list_non_employees_who_match()
        {
            //Given
            var target = CreateDoesNonEmployeeAlreadyExistGuard();

            var firstNonEmployee = new NonEmployee() { Name = "Paul Brown" }; 
            var lastNonEmployee = new NonEmployee() { Name = "Paul Smith" };
            _repo
                .Setup(x => x.GetAllByNameSearch("name", 0, 1))
                .Returns(new List<NonEmployee>()
                             {
                                 lastNonEmployee,
                                 firstNonEmployee
                             });

            //When
            var result = target.Execute(new GuardDefaultExistsRequest("name", 1));

            //Then
            Assert.True(result.Exists);
            Assert.That(result.MatchingResults.Count(), Is.EqualTo(2));
            Assert.That(result.MatchingResults.First(), Is.StringStarting(firstNonEmployee.Name));
            Assert.That(result.MatchingResults.Last(), Is.StringStarting(lastNonEmployee.Name));
        }

        private DoesNonEmployeeAlreadyExistGuard CreateDoesNonEmployeeAlreadyExistGuard()
        {
            var target = new DoesNonEmployeeAlreadyExistGuard( _repo.Object);
            return target;
        }
    }
}