using System.Collections.Generic;
using System.IO;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using DocumentTypeDto = BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class FakeStreamingClientDocumentService : IStreamingClientDocumentService
    {
        public GetStreamedClientDocumentByIdResponse GetById(GetStreamedClientDocumentByIdRequest request)
        {
            var fakeStream = GetFakeContent();

            byte[] data;
            using (var br = new BinaryReader(fakeStream))
                data = br.ReadBytes((int)fakeStream.Length);

            return new GetStreamedClientDocumentByIdResponse()
                       {
                           MetaData = new ClientDocumentDto()
                                          {
                                              OriginalFilename = "hello_world.txt",
                                              Extension = ".txt"
                                          },
                           Content = GetFakeContent()
                       };
        }

        private Stream GetFakeContent()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(memoryStream))
                {
                    tw.WriteLine("hello world!");
                }
                return new MemoryStream(memoryStream.ToArray());
            }
        }

        public CreateResponse Create(CreateStreamedClientDocumentRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}