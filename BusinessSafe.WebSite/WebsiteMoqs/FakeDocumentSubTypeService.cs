using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.DocumentSubTypeService;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class FakeDocumentSubTypeService : IDocumentSubTypeService
    {
        public DocumentSubTypeDto[] GetForDocumentType(long documentTypeId)
        {
            var documentType = new DocumentTypeDto() { Id = documentTypeId };

            var documentSubTypes = new List<DocumentSubTypeDto>()
                                       {
                                           new DocumentSubTypeDto()
                                               {
                                                   Id = 1,
                                                   DocumentType = documentType
                                               }
                                       };

            return documentSubTypes.ToArray();
        }

        public DocumentSubTypeDto[] GetByDepartmentId(long departmentId)
        {
            var documentType = new DocumentTypeDto() { Department = new DepartmentDto() { Id = departmentId } };

            var documentSubTypes = new List<DocumentSubTypeDto>()
                                       {
                                           new DocumentSubTypeDto()
                                               {
                                                   Id = 1,
                                                   DocumentType = documentType,
                                                   
                                               },
                                            new DocumentSubTypeDto()
                                               {
                                                   Id = 1,
                                                   DocumentType =  new DocumentTypeDto() { Id = 124, Department = new DepartmentDto() },
                                                   Title = "Fake Sub Type"
                                                   
                                               }
                                       };

            return documentSubTypes.ToArray();
        }
    }
}