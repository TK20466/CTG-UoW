function EventsManagerController(app, ko, subview, item) {
    var self = this;

    self.subview = ko.observable(subview || "index");
    self.item = ko.observable(item);

    self.events = function() {
        return app.services.events.getEvents();
    }

    self.addEvent = function () {
        app.changeState(function () {
            newEventModel();
            self.subview("add");
        }, "add");
    }

    self.back = function() {
        app.changeState(function () {
            self.subview("index");
        }, "");
    }

    self.add = function (data) {
        app.services.events.createUpdateEvent(self.eventModel);
        self.back();
    }

    function newEventModel() {
        self.eventModel = {
            title: "",
            description: "",
            date: new Date(),
            address: "",
            town: ""
        };
    }

    self.eventModel = {};
}

app.routing.registerViewModel("events", EventsManagerController, true);