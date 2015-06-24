using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.CompanyDefaultsViewModelTests
{
    [TestFixture]
    [Category("Unit")]
    public class CompanyDefaultsViewModelTests
    {
        [Test]
        public void When_create_OrganisationUnitClassification_view_model_Then_should_construct_with_correct_properties()
        {
            // When
            var model = new OrganisationUnitClassificationDefaultAddEdit(new List<Defaults>() {new Defaults()});

            // Then
            Assert.That(model.FormId, Is.EqualTo("organisational-unit-classification"));
            Assert.That(model.SectionHeading, Is.EqualTo("Organisational Unit Classification"));
            Assert.That(model.Label, Is.EqualTo("Enter your organisation unit classifications."));
            Assert.That(model.ColumnHeaderText, Is.EqualTo("Classification"));
            Assert.That(model.TextInputWaterMark, Is.EqualTo("enter new classification..."));
            Assert.That(model.Defaults.Count, Is.EqualTo(1));
            Assert.That(model.DefaultType, Is.EqualTo("OrganisationalUnit"));
        }

        [Test]
        public void When_create_SpecialistSuppliers_view_model_Then_should_construct_with_correct_properties()
        {
            // When
            var model = new SpecialistSuppliersDefaultAddEdit(new List<Defaults>() { new Defaults() });

            // Then
            Assert.That(model.FormId, Is.EqualTo("specialist-suppliers"));
            Assert.That(model.SectionHeading, Is.EqualTo("Specialist Suppliers"));
            Assert.That(model.Label, Is.EqualTo("Enter your specialist suppliers."));
            Assert.That(model.ColumnHeaderText, Is.EqualTo("Specialist Supplier"));
            Assert.That(model.TextInputWaterMark, Is.EqualTo("enter new supplier..."));
            Assert.That(model.Defaults.Count, Is.EqualTo(1));
            Assert.That(model.DefaultType, Is.EqualTo("SpecialistSuppliers"));
        }

        [Test]
        public void When_create_EmployeeGroups_view_model_Then_should_construct_with_correct_properties()
        {
            // When
            var model = new EmployeeGroupsDefaultAddEdit(new List<Defaults>() { new Defaults() });

            // Then
            Assert.That(model.FormId, Is.EqualTo("employee-groups"));
            Assert.That(model.SectionHeading, Is.EqualTo("Employee Groups"));
            Assert.That(model.Label, Is.EqualTo("Enter your employee groups."));
            Assert.That(model.ColumnHeaderText, Is.EqualTo("Employee Group"));
            Assert.That(model.TextInputWaterMark, Is.EqualTo("enter new group..."));
            Assert.That(model.Defaults.Count, Is.EqualTo(1));
            Assert.That(model.DefaultType, Is.EqualTo("EmployeeGroups"));
        }

        [Test]
        public void When_create_PeopleAtRisk_view_model_Then_should_construct_with_correct_properties()
        {
            // When
            var model = new PeopleAtRiskDefaultAddEdit(new List<Defaults>() { new Defaults() });

            // Then
            Assert.That(model.FormId, Is.EqualTo("people-at-risk"));
            Assert.That(model.SectionHeading, Is.EqualTo("People at Risk"));
            Assert.That(model.Label, Is.EqualTo("Enter details of people at risk."));
            Assert.That(model.ColumnHeaderText, Is.EqualTo("Person at Risk"));
            Assert.That(model.TextInputWaterMark, Is.EqualTo("enter new person..."));
            Assert.That(model.Defaults.Count, Is.EqualTo(1));
            Assert.That(model.DefaultType, Is.EqualTo("PeopleAtRisk"));
        }

        [Test]
        public void When_create_Hazards_view_model_Then_should_construct_with_correct_properties()
        {
            // When
            var model = new HazardsDefaultAddEdit(new List<Defaults>() { new Defaults() });

            // Then
            Assert.That(model.FormId, Is.EqualTo("hazards"));
            Assert.That(model.SectionHeading, Is.EqualTo("Hazards"));
            Assert.That(model.Label, Is.EqualTo("Enter your health and safety hazards."));
            Assert.That(model.ColumnHeaderText, Is.EqualTo("Hazard"));
            Assert.That(model.TextInputWaterMark, Is.EqualTo("enter new hazard..."));
            Assert.That(model.Defaults.Count, Is.EqualTo(1));
            Assert.That(model.DefaultType, Is.EqualTo("Hazards"));
            Assert.That(model.AddHeaderViewName, Is.EqualTo("_AddingDefaultHazard"));
        }

        [Test]
        public void When_create_NonEmployees_view_model_Then_should_construct_with_correct_properties()
        {
            // When
            var model = new NonEmployeesDefaultAddEdit(new List<Defaults>() { new Defaults() });

            // Then
            Assert.That(model.FormId, Is.EqualTo("non-employees"));
            Assert.That(model.SectionHeading, Is.EqualTo("Non-Employees"));
            Assert.That(model.Label, Is.EqualTo("Enter details of non-employees involved in r.a."));
            Assert.That(model.ColumnHeaderText, Is.EqualTo("Non-Employee"));
            Assert.That(model.TextInputWaterMark, Is.EqualTo("enter new non-employee..."));
            Assert.That(model.Defaults.Count, Is.EqualTo(1));
            Assert.That(model.DefaultType, Is.EqualTo("NonEmployees"));
            Assert.That(model.AddHeaderViewName, Is.EqualTo("_AddingDefaultNonEmployee"));
        }
    }
}
