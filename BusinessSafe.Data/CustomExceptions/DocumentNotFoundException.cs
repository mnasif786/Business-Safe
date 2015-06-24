using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class DocumentNotFoundException : ArgumentException
    {
        public DocumentNotFoundException()
        { }

        public DocumentNotFoundException(long documentId)
            : base(string.Format("Document not for found. Document Id requested {0}", documentId))
        {

        }
    }
}