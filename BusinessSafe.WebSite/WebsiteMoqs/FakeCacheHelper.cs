using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.DocumentSubTypeService;
using BusinessSafe.WebSite.Helpers;
using DocumentTypeDto = BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class FakeCacheHelper : ICacheHelper
    {
        public void Add<T>(T o, string key, double minutes)
        {
            throw new NotImplementedException();
        }

        public void Clear(string key)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public bool Load<T>(string key, out T value)
        {
            var documentTypes = new List<DocumentTypeDto>()
                                    {
                                        new DocumentTypeDto() { Id = 125, Title = "Type A" },
                                        new DocumentTypeDto() { Id = 131, Title = "Type B" },
                                        new DocumentTypeDto() { Id = 132, Title = "Type C" },
                                        new DocumentTypeDto() { Id = 1, Title = "Type D" },
                                        new DocumentTypeDto() { Id = 2, Title = "Type E" },
                                        new DocumentTypeDto() { Id = 3, Title = "Type F" }
                                    }.ToArray();

            var documentSubTypes = new List<DocumentSubTypeDto>()
                                    {
                                        new DocumentSubTypeDto()
                                            {
                                                DocumentType = new DocumentSubTypeService.DocumentTypeDto() { Id = 125 }
                                            }
                                    }.ToArray();

            switch (key)
            {
                case "DocumentTypes":
                    value = (T)(object)documentTypes;
                    break;
                case "DocumentSubTypes":
                    value = (T)(object)documentSubTypes;
                    break;
                default:
                    value = default(T);
                    break;
            }

            return true;
        }

        public T Load<T>(string key, Func<object> getFunc, int minutes)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}