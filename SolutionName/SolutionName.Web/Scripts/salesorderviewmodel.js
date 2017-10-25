var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
}

salesOrderViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);
    
    self.save = function () {
        debugger
        $.ajax({
            url: "/Sales/Save",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.salesOrderViewModel!=null)
                    ko.mapping.fromJS(data.salesOrderViewModel, {}, self);
            }
        })

    }

    self.flagSalesOrderAsEdited = function () {
       debugger
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }
        
        return true;
    }
}