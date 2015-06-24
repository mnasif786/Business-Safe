function TaskRowParameterExtractor(button, options) {
    var self = this;

    var getFurtherControlMeasureTaskData = function (options) {

        var tableData = self.tableRow().data();
        
        var data = {
            companyId: $("#CompanyId").val(),
            furtherControlMeasureTaskId: tableData.id,
            IsReoccurring: tableData.ir,
            taskType: options.taskType
        };



        return data;
    };

    this.tableRow = function () {
        return $(button).parent().parent();
    };

    this.furtherControlMeasureData = function () {
        return getFurtherControlMeasureTaskData(options);
    };
}