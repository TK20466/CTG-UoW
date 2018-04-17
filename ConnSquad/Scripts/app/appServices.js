function RegistrationService(app) {
    var self = this;

    self.register = function(model) {
        app.ajax.post("/api/auth/RegisterUser", model).done(function() {
            app.routing.changeView("registration-complete");
        });
    }
}

function EventsService(app) {
    var self = this;

    var events = ko.observableArray();
    self.getEvents = function() {
        if (!events.completed && !events.inProgress) {
            events.inProgress = true;
            app.ajax.get("/api/events/fetch").done(function(data) {
                events(data.results.map(MapPropertyLink));
                events.completed = true;
            });
        }
        return events;
    }

    self.createUpdateEvent = function (model) {
        var uri = "/api/events/create";
        if (model.id) {
            uri = "/api/events/" + model.id;
            app.ajax.put(uri, model).done(function(data, code, req) {
                model.link = uri;
            });
        } else
            app.ajax.post(uri, model).done(function (data, code, req) {
                if (data)
                    model.id = data;
                var location = req.getResponseHeader("location");
                if (location) model.link = location;
            });
        model.location = model.address + " " + model.town;
        events.unshift(model);
    };
}

function MapPropertyLink(model) {
    if (model.link && model.data) {
        model.data.link = model.link;
        return model.data;
    }
    return model;
}