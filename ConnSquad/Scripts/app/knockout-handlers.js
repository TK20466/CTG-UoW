ko.bindingHandlers.dateTimePicker = {
    init: function (element, valueAccessor, allBindingsAccessor, vm) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().dateTimePickerOptions || {};
        element = $(element).closest(".date");
        $(element).datetimepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "dp.change", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                if (event.date != null && !(event.date instanceof Date)) {
                    value(event.date.toDate());
                } else {
                    value(event.date);
                }
            } else {
                var property = getBindingProperty(element.find("input"), "dateTimePicker");
                vm[property] = event.date.toDate();
            }
        });

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var picker = $(element).data("DateTimePicker");
            if (picker) {
                picker.destroy();
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        element = $(element).closest(".date");
        var picker = $(element).data("DateTimePicker");
        //when the view model is updated, update the widget
        if (picker) {
            var koDate = ko.utils.unwrapObservable(valueAccessor());

            //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
            koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

            picker.date(koDate);
        }
    }
};

function getBindingProperty(element, bindingName) {
    var allBinds = element[0].dataset.bind.split(",");
    var bind = "";
    for (var i = 0; i < allBinds.length; i++) {
        if (~allBinds[i].indexOf(bindingName)) {
            bind = allBinds[i];
            break;
        }
    }

    bind = bind.replace(bindingName, "");
    bind = bind.replace(":", "");
    bind = bind.trim();
    return bind;
}