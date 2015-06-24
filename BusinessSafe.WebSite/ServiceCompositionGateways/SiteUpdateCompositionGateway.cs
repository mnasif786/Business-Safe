using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Extensions;
using NServiceBus;

namespace BusinessSafe.WebSite.ServiceCompositionGateways
{
    public class SiteUpdateCompositionGateway : ISiteUpdateCompositionGateway
    {
        private readonly IClientService _clientService;
        private readonly IBus _bus;

        public SiteUpdateCompositionGateway(
            IClientService clientService,
            IBus bus
            )
        {
            _clientService = clientService;
            _bus = bus;
        }

        public bool SendEmailIfRequired(SiteDetailsViewModel viewModel)
        {
            var existingSiteFromClientService = _clientService.GetSite(viewModel.ClientId, viewModel.SiteId);
            var clientCAN = _clientService.GetCompanyDetails(viewModel.ClientId).CAN;

            if (viewModel.SiteId != default(long))
            {
                if (existingSiteFromClientService != null && ((viewModel.AddressLine1 != existingSiteFromClientService.AddressLine1 ||
                                               !viewModel.AddressLine2.IsEqualWithNullOrWhiteSpace(existingSiteFromClientService.AddressLine2) ||
                                               !viewModel.AddressLine3.IsEqualWithNullOrWhiteSpace( existingSiteFromClientService.AddressLine3)  ||
                                               !viewModel.AddressLine4.IsEqualWithNullOrWhiteSpace(existingSiteFromClientService.AddressLine4)   ||
                                               !viewModel.AddressLine5.IsEqualWithNullOrWhiteSpace(existingSiteFromClientService.AddressLine5) ||
                                               !viewModel.County.IsEqualWithNullOrWhiteSpace(existingSiteFromClientService.County) ||
                                               !viewModel.Telephone.IsEqualWithNullOrWhiteSpace(existingSiteFromClientService.Telephone) ||
                                               !viewModel.Postcode.IsEqualWithNullOrWhiteSpace(existingSiteFromClientService.Postcode)) || viewModel.SiteStatusUpdated != SiteStatus.NoChange))
                {
                    _bus.Send(new SendSiteDetailsUpdatedEmail()
                    {
                        ActioningUserName = viewModel.ActioningUserName,
                        CompanyId = viewModel.ClientId,
                        CAN = clientCAN,

                        SiteContactUpdated = viewModel.SiteContact,
                        NameUpdated = viewModel.Name,
                        AddressLine1Updated = viewModel.AddressLine1,
                        AddressLine2Updated = viewModel.AddressLine2,
                        AddressLine3Updated = viewModel.AddressLine3,
                        AddressLine4Updated = viewModel.AddressLine4,
                        AddressLine5Updated = viewModel.AddressLine5,
                        CountyUpdated =  viewModel.County,
                        PostcodeUpdated = viewModel.Postcode,
                        TelephoneUpdated = viewModel.Telephone,

                        SiteContactCurrent = existingSiteFromClientService.SiteContact,
                        NameCurrent = viewModel.NameBeforeUpdate,
                        AddressLine1Current = existingSiteFromClientService.AddressLine1,
                        AddressLine2Current = existingSiteFromClientService.AddressLine2,
                        AddressLine3Current = existingSiteFromClientService.AddressLine3,
                        AddressLine4Current = existingSiteFromClientService.AddressLine4,
                        AddressLine5Current = existingSiteFromClientService.AddressLine5,
                        CountyCurrent =  existingSiteFromClientService.County,
                        PostcodeCurrent = existingSiteFromClientService.Postcode,
                        TelephoneCurrent = existingSiteFromClientService.Telephone,
                        SiteStatusCurrent = EnumHelper.GetEnumDescription(viewModel.SiteStatusCurrent),
                        SiteStatusUpdated = EnumHelper.GetEnumDescription(viewModel.SiteStatusUpdated)
                    });
                    return true;
                }
            }
            return false;
        }
    }
}