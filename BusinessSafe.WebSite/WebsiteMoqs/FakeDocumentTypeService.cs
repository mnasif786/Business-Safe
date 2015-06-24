using System.Collections.Generic;
using System.Linq;
using BusinessSafe.WebSite.DocumentTypeService;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class FakeDocumentTypeService : IDocumentTypeService
    {
        public DocumentTypeDto[] GetAll()
        {
            return GetDocumentTypes();
        }

        public DocumentTypeDto[] GetForCurrentUser()
        {
            return GetDocumentTypes();
        }

        public DocumentTypeDto[] GetByDepartmentId(long departmentId)
        {
            return GetDocumentTypes();
        }

        public DocumentTypeDto[] GetByIds(long[] ids)
        {
            return GetDocumentTypes().Where(x => ids.ToList().Contains(x.Id.Value)).ToArray();
        }

        private DocumentTypeDto[] GetDocumentTypes()
        {
            var documentTypes = new List<DocumentTypeDto>()
                                    {
                                        new DocumentTypeDto() { Id = 124, Title = "Type A" },
                                        new DocumentTypeDto() { Id = 126, Title = "Type B" },
                                        new DocumentTypeDto() { Id = 136, Title = "Type C" },
                                        new DocumentTypeDto() { Id = 137, Title = "Type D" },
                                        new DocumentTypeDto() { Id = 131, Title = "Type E" },
                                        new DocumentTypeDto() { Id = 142, Title = "Type F" },
                                        new DocumentTypeDto() { Id = 141, Title = "Type G" },
                                    };

            return documentTypes.ToArray();
        }
    }
}