using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class UpdatePremisesInformationTests
    {
        [Test]
        public void Given_all_required_felds_are_available_When_UpdatePremisesInformation_Then_Values_Updated()
        {
            //Given
            const bool premisesProvidesSleepingAccommodation = true;
            const bool premisesProvidesSleepingAccommodationConfirmed = true;
            string buildinguse = "building use";
            string location = "location";
            int numberOfFloors = 5;
            int numberOfPeople = 600;
            string electricityEmergencyShutOff = "Elect shutoff";
            string waterEmergencyShutOff = "Water shutoff";
            string gasEmergencyShutOff = "Gas Shutoff";
            string otherEmergencyShutOff = "other Shutoff";
            var user = new UserForAuditing();
            var result = new FireRiskAssessment();

            //When
            result.UpdatePremisesInformation(premisesProvidesSleepingAccommodation,
                                             premisesProvidesSleepingAccommodationConfirmed, 
                                             location, 
                                             buildinguse, 
                                             numberOfFloors, 
                                             numberOfPeople, 
                                             new EmergencyShutOffParameters()
                                                {
                                                    ElectricityEmergencyShutOff = electricityEmergencyShutOff,
                                                    WaterEmergencyShutOff = waterEmergencyShutOff,
                                                    GasEmergencyShutOff = gasEmergencyShutOff,
                                                    OtherEmergencyShutOff = otherEmergencyShutOff
                                                }, 
                                             user);

            //Then
            Assert.That(result.PremisesProvidesSleepingAccommodation, Is.EqualTo(premisesProvidesSleepingAccommodation));
            Assert.That(result.PremisesProvidesSleepingAccommodationConfirmed, Is.EqualTo(premisesProvidesSleepingAccommodationConfirmed));
            Assert.That(result.Location, Is.EqualTo(location));
            Assert.That(result.BuildingUse, Is.EqualTo(buildinguse));
            Assert.That(result.NumberOfFloors, Is.EqualTo(numberOfFloors));
            Assert.That(result.NumberOfPeople, Is.EqualTo(numberOfPeople));
            Assert.That(result.ElectricityEmergencyShutOff, Is.EqualTo(electricityEmergencyShutOff));
            Assert.That(result.WaterEmergencyShutOff, Is.EqualTo(waterEmergencyShutOff));
            Assert.That(result.GasEmergencyShutOff, Is.EqualTo(gasEmergencyShutOff));
            Assert.That(result.OtherEmergencyShutOff, Is.EqualTo(otherEmergencyShutOff));
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }
    }
}