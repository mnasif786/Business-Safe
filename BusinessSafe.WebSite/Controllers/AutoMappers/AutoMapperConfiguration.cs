using System;
using AutoMapper;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class AutoMapperConfiguration
    {

        AutoMapperConfiguration()
        {
            Mapper.CreateMap<DelinkSiteViewModel, DelinkSiteRequest>()
                .ForMember(x => x.CompanyId, y => y.MapFrom(dsvm => dsvm.ClientId));

            Mapper.CreateMap<SiteDetailsViewModel, CreateUpdateSiteRequest>()
                  .ConstructUsing(x => new CreateUpdateSiteRequest(x.SiteStructureId,
                                                             x.SiteId,
                                                             SiteAddressRequestMapper.GetParentId(x),
                                                             x.ClientId,
                                                             x.Name,
                                                             x.Reference,
                                                             x.AddressLine1,
                                                             x.AddressLine2,
                                                             x.AddressLine3,
                                                             x.AddressLine4,
                                                             x.AddressLine5,
                                                             x.County,
                                                             x.SiteClosed));

            Mapper.CreateMap<SiteGroupDetailsViewModel, CreateUpdateSiteGroupRequest>()
                 .ConstructUsing(x => new CreateUpdateSiteGroupRequest()
                 {
                     GroupId = x.GroupId,
                     Name = x.Name,
                     LinkToSiteId = x.GroupLinkToSiteId,
                     LinkToGroupId = x.GroupLinkToGroupId,
                     CompanyId = x.ClientId
                 });

            Mapper.CreateMap<EmergencyContactViewModel, EmployeeEmergencyContactRequest>()
               .ForMember(x => x.Forename, y => y.MapFrom(dsvm => String.IsNullOrWhiteSpace(dsvm.Forename) ? String.Empty : dsvm.Forename))
               .ForMember(x => x.Surname, y => y.MapFrom(dsvm => String.IsNullOrWhiteSpace(dsvm.Surname) ? String.Empty : dsvm.Surname))
               .ForMember(x => x.Telephone1, y => y.MapFrom(dsvm => String.IsNullOrWhiteSpace(dsvm.HomeTelephone) ? String.Empty : dsvm.HomeTelephone))
               .ForMember(x => x.Telephone2, y => y.MapFrom(dsvm => String.IsNullOrWhiteSpace(dsvm.MobileTelephone) ? String.Empty : dsvm.MobileTelephone))
               .ForMember(x => x.Telephone3, y => y.MapFrom(dsvm => String.IsNullOrWhiteSpace(dsvm.WorkTelephone) ? String.Empty : dsvm.WorkTelephone));
        }

        class Nested
        {
            static Nested()
            {
            }

            internal static readonly AutoMapperConfiguration _instance = new AutoMapperConfiguration();
        }

        public static AutoMapperConfiguration Instance
        {
            get { return Nested._instance; }
        }
    }
}