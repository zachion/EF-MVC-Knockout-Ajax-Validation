var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};

var salesOrderItemMapping = {
    'SalesOrderItems': {
        key: function (salesOrderItem) {
            return ko.utils.unwrapObservable(salesOrderItem.SalesOrderItemId);
        },
        create: function (options) {
            return ko.utils.unwrapObservable(option.data);
        }
    }
};

salesOrderViewModel = function (date) {
    var self = this;
    ko.mapping.fromJS(data, salesOrderItemMapping, self);
};

salesOrderViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, salesOrderItemMapping, self);

    self.save = function () {
        $.ajax({
            url: "/Sales/Save",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.salesOrderViewModel !== null)
                    ko.mapping.fromJS(data.salesOrderViewModel, {}, self);
                if (data.newLocation !== null)
                    window.location = data.newLocation;
            }
        });
    };

    self.flagSalesOrderAsEdited = function () {
        if (self.ObjectState() !== ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }
        return true;
    };
};