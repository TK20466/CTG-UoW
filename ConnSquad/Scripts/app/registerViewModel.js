function RegisterViewModel(app, ko) {
    var self = this;

    self.register = function () {
        if (!self.registrationModel.isValid()) return; //something went wrong
        var model = ko.toJS(self.registrationModel);
        app.services.registration.register(model);
    }

    self.registrationModel = ko.validatedObservable({
        email: ko.observable().extend({ required: true, email: true }),
        firstName: ko.observable().extend({ required: true }),
        lastName: ko.observable().extend({ required: true }),
        legionId: ko.observable().extend({ required: true, min: {
            params: 1,
            message: "Please enter your Legion ID number without prefix... IE: 1138"
        } }),
        password: ko.observable().extend({ required: true, minLength: 8 }),
        userName: ko.observable().extend({ required: true, minLength: 3 }),
        forumHandle: ko.observable().extend({required: true, minLength: 3})
    });

    self.registrationModel().password2 = ko.observable().extend({
        required: true,
        mustMatch: {
            params: self.registrationModel().password,
            message: "Passwords must match"
        }
    });


    self.clickOption = function (data, event) {
        app.changeState(function() {
            self.currentOption(data);
        }, data);
    }
}

app.routing.registerViewModel("register", RegisterViewModel);