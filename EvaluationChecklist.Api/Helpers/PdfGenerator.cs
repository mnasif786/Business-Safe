using System.Drawing;
using EvoPdf;
using System.Linq;

public interface IPDFGenerator
{
    byte[] CreateComplianceReviewReport(string actionPlanHtml, string executiveSummaryHTML, string headerText, string contentPath);
}

public enum PageOrientation
{
    // Summary:
    //     Portrait
    Portrait = 0,
    //
    // Summary:
    //     Landscape
    Landscape = 1,
}

public class EvoPDFGenerator : IPDFGenerator
{
    private const string LICENSE_KEY = "wE5dT1xcT11eWk9bQV9PXF5BXl1BVlZWVg==";
    private void AddHeader(PdfConverter pdfConverter, string headerText)
    {
        //enable header
        pdfConverter.PdfDocumentOptions.ShowHeader = true;
        // set the header height in points
        pdfConverter.PdfHeaderOptions.HeaderHeight = 20;

        var headerTextElement = new TextElement(0, 0, headerText,
                                                new System.Drawing.Font(new System.Drawing.FontFamily("Arial"), 10,
                                                                        System.Drawing.GraphicsUnit.Point));
        headerTextElement.EmbedSysFont = true;
        headerTextElement.TextAlign = HorizontalTextAlign.Center;
        headerTextElement.VerticalTextAlign = VerticalTextAlign.Middle;
        pdfConverter.PdfHeaderOptions.AddElement(headerTextElement);



    }

    private void AddSummaryFooter(PdfConverter pdfConverter, string contentPath)
    {
        //enable footer
        pdfConverter.PdfDocumentOptions.ShowFooter = true;
        pdfConverter.PdfFooterOptions.FooterHeight = 85;
      
        //write the footer

        var summaryFooterUrl = contentPath + "/templates/SummaryFooter.htm";
        var footerHtml = new HtmlToPdfElement(0, 5, 0, pdfConverter.PdfFooterOptions.FooterHeight, summaryFooterUrl, 1024, 0);

        footerHtml.FitHeight = true;
        pdfConverter.PdfFooterOptions.AddElement(footerHtml);
    }

    private void AddReportFooter(PdfConverter pdfConverter)
    {
        //enable footer
        pdfConverter.PdfDocumentOptions.ShowFooter = true;
        // set the footer height in points
        pdfConverter.PdfFooterOptions.FooterHeight = 20;
        //write the page number
        var footerText = new TextElement(0, 0, "&p;",
                                         new System.Drawing.Font(new System.Drawing.FontFamily("Arial"), 10,
                                                                 System.Drawing.GraphicsUnit.Point));
        footerText.EmbedSysFont = true;
        footerText.TextAlign = HorizontalTextAlign.Center;
        pdfConverter.PdfFooterOptions.AddElement(footerText);

    }


    public byte[] CreateComplianceReviewReport(string actionPlanHtml, string executiveSummaryHTML, string headerText, string contentPath)
    {
        var actionPlanPdf = CreateActionPlanPdf(actionPlanHtml, headerText);
        var executiveSummaryPdf = CreateExecutiveSummaryPdf(executiveSummaryHTML, headerText, contentPath);

        //for each page in the letter add to the start on action plan pdf. Using this rather than append document because AppendDocument doesn't number the pages correctly.
        for (var pageIndex = 0; pageIndex < executiveSummaryPdf.Pages.Count; pageIndex++)
        {
            actionPlanPdf.Pages.Insert(pageIndex, executiveSummaryPdf.Pages[pageIndex]);
        }

        var pdfBytes = actionPlanPdf.Save();

        return pdfBytes;
    }

    private Document CreateActionPlanPdf(string actionPlanHtml, string headerText)
    {
        var pdfConverterActionPlan = new PdfConverter();

        pdfConverterActionPlan.LicenseKey = LICENSE_KEY;// "B4mYiJubiJiInIaYiJuZhpmahpGRkZE=";
        pdfConverterActionPlan.PdfDocumentOptions.EmbedFonts = true;
        pdfConverterActionPlan.PdfDocumentOptions.LeftMargin = 20;
        pdfConverterActionPlan.PdfDocumentOptions.RightMargin = 20;
        pdfConverterActionPlan.PdfDocumentOptions.TopMargin = 10;
        pdfConverterActionPlan.PdfDocumentOptions.BottomMargin = 0;
        pdfConverterActionPlan.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        pdfConverterActionPlan.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression;
        pdfConverterActionPlan.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Landscape;

        AddHeader(pdfConverterActionPlan, headerText);
        AddReportFooter(pdfConverterActionPlan);

        var actionPlanPdf = pdfConverterActionPlan.GetPdfDocumentObjectFromHtmlString(actionPlanHtml);

        return actionPlanPdf;
    }

    private Document CreateExecutiveSummaryPdf(string executiveSummaryHTML, string headerText, string contentPath)
    {
        var pdfConvertorExecutiveSummary = new PdfConverter();
        
        pdfConvertorExecutiveSummary.PrepareRenderPdfPageEvent += pdfConvertorExecutiveSummary_PrepareRenderPdfPageEvent;
        pdfConvertorExecutiveSummary.LicenseKey = LICENSE_KEY;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.EmbedFonts = true;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.LeftMargin = 56;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.RightMargin = 30;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.TopMargin = 10;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.BottomMargin = 0;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression;
        pdfConvertorExecutiveSummary.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;

        AddSummaryFooter(pdfConvertorExecutiveSummary, contentPath);

        if (string.IsNullOrEmpty(executiveSummaryHTML))
        {
            executiveSummaryHTML = "<html><body></body></html>";
        }
        var executiveSummaryPdf = pdfConvertorExecutiveSummary.GetPdfDocumentObjectFromHtmlString(executiveSummaryHTML);
        
        pdfConvertorExecutiveSummary.PrepareRenderPdfPageEvent -= pdfConvertorExecutiveSummary_PrepareRenderPdfPageEvent;

        return executiveSummaryPdf;
    }

    void pdfConvertorExecutiveSummary_PrepareRenderPdfPageEvent(PrepareRenderPdfPageParams eventParams)
    {
        if (eventParams.PageNumber > 1)
        {
            eventParams.Page.Margins.Top = 56;
            eventParams.Page.Margins.Bottom = 56;
            eventParams.Page.ShowFooter = false;
        }
    }
}


