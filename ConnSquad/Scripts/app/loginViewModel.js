function LoginViewModel(app, ko) {
    var self = this;

    self.loginModel = ko.validatedObservable({
        password: ko.observable().extend({ required: true }),
        userName: ko.observable().extend({ required: true }),
        rememberMe: false
    });

    self.login = function() {
        var serialized = ko.toJS(self.loginModel);
        app.session.attemptLogin(serialized, function() {
            self.wrongDetails(true);
        });
    }

    self.wrongDetails = ko.observable();
}

app.routing.registerViewModel("login", LoginViewModel);