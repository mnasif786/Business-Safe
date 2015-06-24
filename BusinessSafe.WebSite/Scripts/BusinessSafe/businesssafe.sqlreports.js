
var sqlReportsManager = function () 
{
    $(document).ready(function () {
        $('#submit').click(function () {
            $('#validation-error').addClass("hide");
        });
    });

    function initialize(reports, sites, siteGroups) 
    {        
        $("#Site").combobox({
            selectedId: $("#SiteId").val(),
            initialValues: sites,
            url: '',
            afterSelect: function () {               
            }           
        });

        $("#SiteGroup").combobox({
            selectedId: $("#SiteGroupId").val(),
            initialValues: siteGroups,
            url: '',
            afterSelect: function () {
               
            }
        });

        $("#Report").combobox({
            selectedId: $("#ReportId").val(),
            initialValues: reports,
            url: '',
            afterSelect: function () {               
            }
        });
    }

    return { initialize: initialize };
} ();