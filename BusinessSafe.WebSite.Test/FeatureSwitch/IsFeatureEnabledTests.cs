using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.HtmlHelpers;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.FeatureSwitch
{
    [TestFixture]
    [Category("Unit")]
    public class IsFeatureEnabledTests
    {

        [Test]
        public void Given_feature_switch_not_in_config_When_request_if_feature_is_enabled_Then_should_return_true()
        {
            // Given
            var user = new CustomPrincipal(new UserDto(), new CompanyDto());

            // When
            var result = FeatureSwitchChecker.Enabled(FeatureSwitches.FeatureSwitch_SqlReports_For_GRA, user);

            // Then
            Assert.True(result);
        }

        [Test]
        public void Given_feature_switch_is_in_config_and_turned_on_When_request_if_feature_is_enabled_Then_should_return_true()
        {
            // Given
            var user = new CustomPrincipal(new UserDto(), new CompanyDto());

            // When
            var result = FeatureSwitchChecker.Enabled(FeatureSwitches.FeatureSwitch_SqlReports_For_PRA, user);

            // Then
            Assert.True(result);
            
        }

        [Test]
        public void Given_feature_switch_is_in_config_and_turned_off_When_request_if_feature_is_enabled_Then_should_return_false()
        {
            // Given
            const string email = "some.email@doesnt.matter.com";
            var user = new CustomPrincipal(new UserDto()
            {
                Employee = new EmployeeDto()
                {
                    MainContactDetails = new EmployeeContactDetailDto { Email = email }
                }
            }, new CompanyDto());

            // When
            var result = FeatureSwitchChecker.Enabled(FeatureSwitches.FeatureSwitch_PersonalRiskAssessments, user);

            // Then
            Assert.False(result);

        }

        [Test]
        public void Given_feature_switch_is_in_config_and_turned_off_and_user_doesnt_have_email_When_request_if_feature_is_enabled_Then_should_return_false()
        {
            // Given
            var user = new CustomPrincipal(new UserDto(), new CompanyDto());

            // When
            var result = FeatureSwitchChecker.Enabled(FeatureSwitches.FeatureSwitch_PersonalRiskAssessments, user);

            // Then
            Assert.False(result);

        }

        [Test]
        public void Given_feature_switch_is_in_config_and_turned_off_but_user_name_is_supplied_show_all_features_name_When_request_if_feature_is_enabled_Then_should_return_true()
        {
            // Given
            const string email = "paul.cooke@hotmail.com";
            var user = new CustomPrincipal(new UserDto()
            {
                Employee = new EmployeeDto()
                {
                    MainContactDetails = new EmployeeContactDetailDto { Email = email }
                }
            }, new CompanyDto());
            
            // When
            var result = FeatureSwitchChecker.Enabled(FeatureSwitches.FeatureSwitch_PersonalRiskAssessments, user);

            // Then
            Assert.True(result);

        }

        [Test]
        public void Given_feature_switch_is_in_config_and_turned_on_and_user_name_is_supplied_show_all_features_name_When_request_if_feature_is_enabled_Then_should_return_true()
        {
            // Given
            const string email = "paul.cooke@hotmail.com";
            var user = new CustomPrincipal(new UserDto()
            {
                Employee = new EmployeeDto()
                {
                    MainContactDetails = new EmployeeContactDetailDto { Email = email }
                }
            }, new CompanyDto());
            

            // When
            var result = FeatureSwitchChecker.Enabled(FeatureSwitches.FeatureSwitch_SqlReports_For_PRA, user);

            // Then
            Assert.True(result);

        }

        [Test]
        public void Given_feature_switch_is_in_config_and_turned_off_and_user_name_supplied_is_not_allowed_to_show_all_features_name_When_request_if_feature_is_enabled_Then_should_return_false()
        {
            // Given
            const string email = "some.name@hotmail.com";
            var user = new CustomPrincipal(new UserDto()
                                               {
                                                   Employee = new EmployeeDto()
                                                                  {
                                                                      MainContactDetails = new EmployeeContactDetailDto { Email = email }
                                                                  }
                                               }, new CompanyDto());
            

            // When
            var result = FeatureSwitchChecker.Enabled(FeatureSwitches.FeatureSwitch_PersonalRiskAssessments, user);

            // Then
            Assert.False(result);

        }

     
    }

  
}