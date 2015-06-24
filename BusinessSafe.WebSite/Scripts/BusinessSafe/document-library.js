function GetDocumentSubTypes(documentTypeId, successCallBack) {
    this.execute = function () {
        $.ajax({
            url: window.globalajaxurls.getDocumentSubTypesByDocumentTypeId,
            type: "GET",
            dataType: "json",
            data: { documentTypeId: documentTypeId },
            success: function (data) {
                successCallBack(data);
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };
}



var documentLibraryManager = function () {
    
    function initialise(documentTypes, documentSubTypes) {

        var successCallBack = function (data) {

            $("#DocumentSubType").combobox({
                selectedId: $("#DocumentSubTypeId").val(),
                initialValues: data,
                url: ''
            });

        };

        $("#DocumentType").combobox({
            selectedId: $("#DocumentTypeId").val(),
            initialValues: documentTypes,
            url: '',
            afterSelect: function (event, ui) {

                var documentTypeId = $("#DocumentTypeId").val();

                if (documentTypeId !== '') {
                    new GetDocumentSubTypes(documentTypeId, successCallBack).execute();
                }

                return false;
            } 
        });

        $("#DocumentSubType").combobox({
            selectedId: $("#DocumentSubTypeId").val(),
            initialValues: documentSubTypes,
            url: ''
        });

    }

    return { initialise: initialise };
} ();




