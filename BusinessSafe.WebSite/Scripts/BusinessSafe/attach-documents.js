var addRowToNewlyUploadedDocumentsTable = function (newData) {

    $('#DocumentsToIncludeTable').parent().fadeIn('fast').siblings('.initial-prompt').hide().siblings('.set-fields-prompt').fadeIn();
    $("#DocumentsToIncludeTable tr:last").after(newData);
    $('#new-documents').fadeIn();
    $('#SaveAddedDocuments').show();

    $("#DocumentsToIncludeTable tr:last").find("#DeleteNewlyAddedDocumentLink").click(function (e) {
        e.preventDefault();
        var deleteArgument = $(this).find("#DeleteArgument").val();
        $(this).parent().parent().remove();

        $.ajax({
            type: "POST",
            url: window.globalajaxurls.deleteDocument + "?enc=" + deleteArgument
        });

        if ($("#DocumentsToIncludeTable tr").size() == 2) {
            $('#DocumentsToIncludeTable').parent().hide().siblings('.initial-prompt').show().siblings('.set-fields-prompt').hide();
            $('#SaveAddedDocuments').hide();
        }
    });
};
