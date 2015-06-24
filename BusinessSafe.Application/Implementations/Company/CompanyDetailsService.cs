using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Messages.Emails.Commands;

using NServiceBus;

namespace BusinessSafe.Application.Implementations.Company
{
    public class CompanyDetailsService : ICompanyDetailsService
    {
        private readonly IClientService _clientService;
        private readonly IPeninsulaLog _log;
        private readonly IBus _bus;

        public CompanyDetailsService(
            IClientService clientService,
            IPeninsulaLog log,
            IBus bus)
        {
            _clientService = clientService;
            _log = log;
            _bus = bus;
        }

        public CompanyDetailsDto GetCompanyDetails(long companyId)
        {
            _log.Add(new object[] { companyId });

            try
            {
                return _clientService.GetCompanyDetails(companyId);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void Update(CompanyDetailsRequest request)
        {
            _log.Add(request);

            try
            {
                _bus.Send(new SendCompanyDetailsUpdatedEmail()
                              {
                                  CompanyId = request.Id,
                                  CAN = request.CAN,
                                  ActioningUserName = request.ActioningUserName,
                                  ExistingCompanyDetailsInformation = MapCompanyDetailsInformation(request.ExistingCompanyDetails),
                                  NewCompanyDetailsInformation = MapCompanyDetailsInformation(request.NewCompanyDetails)
                              }
                 );
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private static Messages.Emails.Commands.CompanyDetailsInformation MapCompanyDetailsInformation(Request.CompanyDetailsInformation details)
        {
            return new Messages.Emails.Commands.CompanyDetailsInformation()
                   {
                       CompanyName = details.CompanyName,
                       AddressLine1 = details.AddressLine1,
                       AddressLine2 = details.AddressLine2,
                       AddressLine3 = details.AddressLine3,
                       AddressLine4 = details.AddressLine4,
                       Postcode = details.Postcode,
                       Telephone = details.Telephone,
                       Website = details.Website,
                       MainContact = details.MainContact,
                       BusinessSafeContactName = details.BusinessSafeContactName
                   };
        }
    }
}