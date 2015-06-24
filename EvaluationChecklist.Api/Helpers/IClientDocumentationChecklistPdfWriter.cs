namespace EvaluationChecklist.Helpers
{
    public interface IClientDocumentationChecklistPdfWriter
    {
        void WriteToClientDocumentation(string fileName, byte[] pdfBytes, int clientId);
    }
}