var taskDetailsSummaryViewModelLoader = function () {

    function load(triggerButton, tasksOptions) {

        var data = new TaskRowParameterExtractor(triggerButton, tasksOptions).furtherControlMeasureData();
        var url = window.globalajaxurls.getTaskDetailsSummary;
        
        var callback = function (callbackData) {
            new TaskDetailsSummaryViewModel().initialize(callbackData, triggerButton);        
        };

        AjaxCall.execute(url, callback, data, "GET");
    }       

    return {
        load: load            
    };

}();

function TaskDetailsSummaryViewModel(){
    
    this.initialize = function(data, elementToAttachTo){

        $(elementToAttachTo).attr("rel", "popover");
        $(elementToAttachTo).attr("data-content", data);
        $(elementToAttachTo).attr("data-original-title", "");
        $(elementToAttachTo).attr("title", "");
        $(elementToAttachTo).popover({
            title: "Previous Tasks and Task Schedule"
        });

        $(elementToAttachTo).popover("show");
    };
}