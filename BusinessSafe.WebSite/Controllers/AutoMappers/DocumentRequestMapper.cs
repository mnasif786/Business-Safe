using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using System.Text.RegularExpressions;

using BusinessSafe.WebSite.Helpers;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public interface IDocumentRequestMapper
    {
        List<CreateDocumentRequest> MapCreateRequests(FormCollection formCollection);
        List<long> MapDeleteRequests(FormCollection formCollection);
    }

    public class DocumentRequestMapper : IDocumentRequestMapper
    {
        private IEnumerable<NewlyAddedDocumentGridRowViewModel> CreateNewAddedViewModel(FormCollection formCollection)
        {
            var documentGridRowViewModels = new List<NewlyAddedDocumentGridRowViewModel>();

            var guidRegex =
                new Regex(@"^DocumentGridRow_[\d]+_DocumentLibraryId$");

            foreach (string key in formCollection.Keys)
            {
                if (guidRegex.IsMatch(key))
                {
                    var documentLibraryId = Convert.ToInt64(key.Replace("DocumentGridRow_", "").Replace("_DocumentLibraryId", ""));

                    var documentGridRowViewModel = new NewlyAddedDocumentGridRowViewModel()
                                                       {
                                                           DocumentLibraryId = documentLibraryId,
                                                           Filename =
                                                               formCollection[
                                                                   "DocumentGridRow_" + documentLibraryId.ToString() +
                                                                   "_FileName"],
                                                           Title =
                                                               string.IsNullOrEmpty(
                                                                   formCollection[
                                                                       "DocumentGridRow_" + documentLibraryId.ToString() +
                                                                       "_Title"])
                                                                   ? string.Empty
                                                                   : formCollection[
                                                                       "DocumentGridRow_" + documentLibraryId.ToString() +
                                                                       "_Title"],
                                                           Description =
                                                               formCollection[
                                                                   "DocumentGridRow_" + documentLibraryId.ToString() +
                                                                   "_Description"],
                                                           DocumentTypeId =
                                                               Convert.ToInt64(
                                                                   formCollection[
                                                                       "DocumentGridRow_" + documentLibraryId.ToString() +
                                                                       "_DocumentType"]),
                                                           DocumentOriginTypeId = Convert.ToInt32(formCollection["DocumentGridRow_" + documentLibraryId.ToString() + "_DocumentOriginTypeId"]),
                                                           SiteId =
                                                               string.IsNullOrEmpty(
                                                                   formCollection[
                                                                       "DocumentGridRow_" + documentLibraryId.ToString() +
                                                                       "_Site"])
                                                                   ? default(long)
                                                                   : Convert.ToInt64(
                                                                       formCollection[
                                                                           "DocumentGridRow_" +
                                                                           documentLibraryId.ToString() + "_Site"])
                                                       };
                    documentGridRowViewModels.Add(documentGridRowViewModel);
                }
            }

            return documentGridRowViewModels;
        }

        public List<CreateDocumentRequest> MapCreateRequests(FormCollection formCollection)
        {
            var viewModels = CreateNewAddedViewModel(formCollection);
            var requests = new List<CreateDocumentRequest>();

            foreach (var viewModel in viewModels)
            {
                var request = new CreateDocumentRequest
                                  {
                                      DocumentLibraryId = viewModel.DocumentLibraryId,
                                      DocumentType = (DocumentTypeEnum)viewModel.DocumentTypeId,
                                      Title = viewModel.Title,
                                      SiteId = viewModel.SiteId,
                                      Description = viewModel.Description,
                                      Filename = viewModel.Filename,
                                      DocumentOriginType = (DocumentOriginType)viewModel.DocumentOriginTypeId
                                  };

                requests.Add(request);
            }

            return requests;
        }

        public List<long> MapDeleteRequests(FormCollection formCollection)
        {
            var documentLibraryIdsToDelete = new List<long>();

            var guidRegex =
                new Regex(@"^PreviouslyAddedDocumentsRow_[\d]+_Delete$");

            foreach (string key in formCollection.Keys)
            {
                if (guidRegex.IsMatch(key))
                {
                    if (formCollection[key].Contains("True") || formCollection[key].Contains("true"))
                    {
                        var documentLibraryId =
                            Convert.ToInt64(key.Replace("PreviouslyAddedDocumentsRow_", "").Replace("_Delete", ""));

                        documentLibraryIdsToDelete.Add(documentLibraryId);
                    }
                }
            }

            return documentLibraryIdsToDelete;
        }
    }
}