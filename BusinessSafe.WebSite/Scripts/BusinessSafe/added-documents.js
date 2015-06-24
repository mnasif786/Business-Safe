function GetAddNewDocumentsDialog(companyId, successCallBack) {
    this.execute = function () {
        $.ajax({
            url: window.globalajaxurls.getAddNewAddedDocumentsUrl,
            type: "GET",
            data: { companyId: companyId },
            success: function (data) {
                successCallBack(data);
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };
}

function DeleteDocument(data, successfulCallBack) {
    this.execute = function () {
        AjaxCall.execute(window.globalajaxurls.deleteAddedDocument, successfulCallBack, data, "POST");
    };
}

var addedDocumentsManager = function () {
    var selectors = {
        companyId: "#CompanyId",
        documentTypeDDL: "#DocumentTypeId",
        addNewDocumentsLink: "#AddNewDocumentsLink",
        addNewDocumentsDialog: "#addNewDocumentsDialog",
        saveButton: "#SaveAddedDocuments",
        cancelButton: "#CancelButton"
    };

    function initialise() {
        $(selectors.addNewDocumentsLink).click(function (e) {
            e.preventDefault();

            var companyId = $(selectors.companyId).val();

            new GetAddNewDocumentsDialog(companyId, successCallBack).execute();
        });

        var successCallBack = function (data) {
            $(selectors.addNewDocumentsDialog).empty();

            $(selectors.addNewDocumentsDialog).dialog({
                autoOpen: false,
                width: 1100,
                modal: true,
                resizable: false,
                draggable: false
            });

            $(selectors.addNewDocumentsDialog).append(data).dialog('open');
            $(selectors.addNewDocumentsDialog).find('#DocumentsToIncludeTable').parent().hide().siblings('.set-fields-prompt').hide();
            $(selectors.addNewDocumentsDialog).find('#SaveAddedDocuments').hide();
        };

        $(selectors.saveButton).live('click', function (event) {
            event.preventDefault();

            var f = $("#SaveAddedDocumentsForm");

            $.post(f[0].action, f.serialize(), function (result) {
                if (result.Success === true) {
                    $(selectors.addNewDocumentsDialog).dialog('close');
                    window.location.reload();
                }
            }).error(function () {
                window.location.replace(window.globalajaxurls.errorPage);
            });

            return;
        });

        $(selectors.cancelButton).live('click', function (event) {
            event.preventDefault();

            $(selectors.addNewDocumentsDialog).dialog('close');

            return;
        });
    }

    return { initialise: initialise };
} ();

var addedDocumentsLibraryManager = function(){

    var selectors = {
        deleteDocumentsLinks: ".DeleteDocumentIconLink",        
        dialog : "#dialogDeleteDocument",
        companyId : "#CompanyId"        
    };

    function initialise(documentTypes, sites, siteGroups) {
        
        $("#DocumentType").combobox({
            selectedId: $("#DocumentTypeId").val(),
            initialValues: documentTypes,
            url: ''
        });

        $("#Site").combobox({
            selectedId: $("#SiteId").val(),
            initialValues: sites,
            url: ''
        });

        $("#SiteGroup").combobox({
            selectedId: $("#SiteGroupId").val(),
            initialValues: siteGroups,
            url: ''
        });
        
        $(selectors.deleteDocumentsLinks).click(function (event) {
            event.preventDefault();
            var link = this;

            $(selectors.dialog).dialog({
                buttons: {
                    "Confirm": function () {

                        var successfulCallBack = function () {
                            $(selectors).dialog("close");
                            $('form').submit();
                        };

                        var companyId = $(selectors.companyId).val();
                        var documentId = $(link).attr("data-id");
                        var data = {
                            companyId : companyId,
                            documentId : documentId
                        };

                        var deleteDocumentCommand = new DeleteDocument(data, successfulCallBack);
                        deleteDocumentCommand.execute();

                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                resizable: false
            });
            $(selectors.dialog).dialog('open');

        });

        
    }

    return { initialise: initialise };
}();

