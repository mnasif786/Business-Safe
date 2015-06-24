function CanDeleteEmployee(employeeId, companyId, successCallBack) {

    this.employeeId = employeeId;
    this.companyId = companyId;

    this.execute = function () {

        $.ajax({
            url: window.globalajaxurls.canDeleteEmployee,
            type: "GET",
            dataType: "json",
            data: {
                employeeId: this.employeeId,
                companyId: this.companyId
            },
            success: function (data) {
                successCallBack(data);                
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };
}

function DeleteEmployee(employeeId, companyId, successCallBack) {

    this.employeeId = employeeId;
    this.companyId = companyId;

    this.execute = function () {

        $.ajax({
            url: window.globalajaxurls.markEmployeeAsDeleted,
            type: "POST",
            dataType: "json",
            data: {
                employeeId: this.employeeId,
                companyId: this.companyId
            },
            success: function (data) {

                if (data.Success === true) {

                    successCallBack();

                } else if (data.Success === false) {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };
}

function ReinstateDeletedEmployee(employeeId, companyId, successCallBack) {

    this.employeeId = employeeId;
    this.companyId = companyId;

    this.execute = function () {

        $.ajax({
            url: window.globalajaxurls.reinstateDeletedEmployee,
            type: "POST",
            dataType: "json",
            data: {
                employeeId: this.employeeId,
                companyId: this.companyId
            },
            success: function (data) {

                if (data.Success === true) {

                    successCallBack();

                } else if (data.Success === false) {
                    window.location.replace(window.globalajaxurls.errorPage);
                }
            },
            error: function () {
                window.location.replace(window.globalajaxurls.errorPage);
            }
        });
    };

}



var employeeSearchForm = function () {
    var selectors = {
        companyId: "#CompanyId",
        sites: "#SiteId",
        deleteLinks: "a.icon-remove",
        reinstateEmployeeLinks: "a.icon-share",
        dialogDeleteEmployee: "#dialogDeleteEmployee",
        dialogReinstateDeletedEmployee: "#dialogReinstateDeletedEmployee",
        dialogCannotRemoveEmployee: "#dialogCannotRemoveEmployee",
        showDeletedLink: "#showDeletedLink",
        showDeletedCheckBox: "#ShowDeleted"
    };

    function initialise(sites) {

        $("#Site").combobox({
            selectedId: $("#SiteId").val(),
            initialValues: sites,
            url : window.globalajaxurls.getSites,
            data: {
                companyId: $("#CompanyId").val(),
                pageLimit: 100
            }
        });

        
        $(selectors.deleteLinks).click(function (event) {
            event.preventDefault();

            var employeeId = $(this).attr('data-id');
            var companyId = $(selectors.companyId).val();

            
            var canDeleteEmployeeCallBack = function (result) {

                if(result.CanDeleteEmployee === false){
                    $(selectors.dialogCannotRemoveEmployee).dialog({
                        buttons: {
                            "Ok": function () {
                                $(this).dialog("close");                               
                            }
                        },
                        resizable: false
                    });
                    return;
                }
                

                if(result.CanDeleteEmployee === true){

                    $(selectors.dialogDeleteEmployee).dialog({
                        buttons: {
                            "Confirm": function () {

                                var successCallBack = function () {
                                    $(selectors.dialogDeleteEmployee).dialog("close");
                                    $('form').submit();
                                };


                                new DeleteEmployee(employeeId, companyId, successCallBack).execute();




                            },
                            "Cancel": function () {
                                $(this).dialog("close");
                            }
                        },
                        resizable: false
                    });
                }                
            };

            new CanDeleteEmployee(employeeId, companyId, canDeleteEmployeeCallBack).execute();            
            
        });

        $(selectors.reinstateEmployeeLinks).click(function (event) {
            event.preventDefault();

            var employeeId = $(this).attr('data-id');
            var companyId = $(selectors.companyId).val();

            $(selectors.dialogReinstateDeletedEmployee).dialog({
                buttons: {
                    "Confirm": function () {

                        var successCallBack = function () {
                            $(selectors.dialogReinstateDeletedEmployee).dialog("close");
                            $('form').submit();
                        };


                        new ReinstateDeletedEmployee(employeeId, companyId, successCallBack).execute();




                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                },
                resizable: false
            });

        });


    }

    $(selectors.showDeletedLink).click(function (event) {
        $(selectors.showDeletedCheckBox).prop("checked", true);
        $('#Search').click();
    });

    return {
        initialise: initialise
    };
} ();

