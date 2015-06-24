var businessSafeSystemDocumentsLibraryManager = function () {
    var selectors = {
        documentTypeId: "#DocumentTypeId",
        deleteDocumentsLinks: ".DeleteDocumentIconLink",
        dialog: "#dialogDeleteDocument",
        companyId: "#CompanyId",
        siteId: "#SiteId"
    };

    function initialise(documentTypes, sites) {
        $("#DocumentType").combobox({
            selectedId: $("#DocumentTypeId").val(),
            initialValues: documentTypes,
            url: ''
        });

        $("#Site").combobox({
            selectedId: $("#SiteId").val(),
            initialValues: sites,
            url : window.globalajaxurls.getSites,
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 100
            }
        });
    }

    return { initialise: initialise };
} ();

