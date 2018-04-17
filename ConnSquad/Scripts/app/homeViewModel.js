function HomeViewModel(app, ko) {
    var self = this;
    self.myVal = ko.observable("Type Here!");
    self.register = function () {
        app.routing.changeView("register");
    }
}

app.routing.registerViewModel("home", HomeViewModel);