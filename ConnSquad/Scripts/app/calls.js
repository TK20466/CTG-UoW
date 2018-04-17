function App(ko) {
    var self = this;

    self.ajax = new DataManager(self);

    self.session = new SessionManager(self);

    self.routing = new RoutingManager(self, ko);

    self.services = {
        registration: new RegistrationService(self),
        events: new EventsService(self)
    }

    self.changeState = function(callback, path) {
        app.routing.saveState();
        callback();
        app.routing.addHistory(path, path);
    }
}

function DataManager(app) {
    var self = this;

    function request(type, url, data) {
        var settings = {
            data: JSON.stringify(data),
            contentType: 'application/json',
            type: type
        };

        if (app.session.isLoggedIn) {
            settings.headers = { "Authorization": "Bearer " + app.session.token }
        }

        var promise = $.ajax(url, settings);

        return promise;
    }

    self.post = function (url, data) {
        return request("POST", url, data);
    }

    self.put = function (url, data) {
        return request("PUT", url, data);
    }

    self.get = function (url) {
        return request("GET", url);
    }
}

function SessionManager(app) {
    var self = this;
    self.token = null;
    self.isLoggedIn = false;

    self.login = function(token) {
        self.token = token;
        self.isLoggedIn = true;
    }

    self.attemptLogin = function(model, wrongDetails, handleValidation) {
        app.ajax.post("/api/auth/token", model).done(function(token) {
            self.login(token);
            if (model.rememberMe)
                self.rememberMe();
            if (app.routing.challengedRoute) {
                app.routing.changeView.apply(this, app.routing.challengedRoute);
                app.routing.challengedRoute = null;
            } else app.routing.changeView("home");
        }).fail(function (xhr) {
            if (xhr.status == 500) {
                alert("Error logging in.  Please try again later");
                return;
            }
            if (xhr.status == 401) {
                if (xhr.statusMessage == "Email verification not completed") {
                    handleValidation();
                } else {
                    wrongDetails();
                }
            }
        });
    }

    self.logout = function() {
        self.token = null;
        self.isLoggedIn = false;
    }

    self.rememberMe = function () {
        Cookies.set("apiToken", self.token, {expires: 7});
    }

    self.getSavedSession = function () {
        var value = Cookies.get("apiToken");
        if (value) {
            self.token = value;
            self.isLoggedIn = true;
            return true;
        }
        return false;

    }
}

function RoutingManager(app, ko) {
    var self = this;
    self.enableView = ko.observable(false);
    self.currentView = ko.observable();
    self.currentPath = ko.observable();
    self.currentTitle = ko.observable();
    self.currentViewData = ko.observable();
    self.challengedRoute = null;

    self.changeView = function (newView) {
        if (self.enableView())
            self.replaceState(self.currentView(), window.location.pathname);
        var challenged = setView(newView);
        if (challenged) return;
        self.pushState(newView, "/" + newView);
        self.currentPath(newView);
    }

    self.addHistory = function (title, path) {
        self.pushState(title, "/" + self.currentPath() + "/" + path);
    }

    self.saveState = function() {
        self.replaceState(self.currentPath(), window.location.pathname);
    }

    function setView(newView) {
        newView = newView.toLowerCase();
        self.enableView(false);
        self.currentView(newView);
        var model = self.getController(newView);
        if (model != null) {
            if (model.requireAuth && !app.session.isLoggedIn) {
                if (!app.session.getSavedSession()) {
                    self.challengedRoute = arguments;
                    self.changeView("login");

                    return true;
                }
            }
            var args = Array.prototype.slice.call(arguments, 1);
            args.unshift(ko);
            args.unshift(app);
            args.unshift(model);
            self.currentViewData(factory.apply(this, args));
        } else self.currentViewData(null);
        self.enableView(true);
        return false;
    }

    self.viewModels = {};

    self.registerViewModel = function(view, model, auth) {
        self.viewModels[view] = model;
        self.viewModels[view].requireAuth = auth;
    }

    self.getController = function(view) {
        return self.viewModels[view];
    }

    self.pushState = function (title, path) {
        if (history == null) {
            console.log("History Not Supported");
            return;
        }
        var viewName = self.currentView();
        history.pushState({ model: null, view: viewName }, title, path);
    }

    self.replaceState = function(title, path) {
        if (history == null) {
            console.log("History Not Supported");
            return;
        }
        var serialize = serializeModel(self.currentViewData());
        var viewName = self.currentView();
        history.replaceState({ model: serialize, view: viewName }, title, path);
    }

    window.onpopstate = function (event) {
        if (event.state == null) setView("home"); //something went wrong
        var view = event.state.view;
        setView(view); //setup the view;
        if (event.state.model)
            applySavedModel(self.currentViewData(), event.state.model);
    };

    self.setupFromPath = function () {
        var windowRoute = window.location.pathname;
        var parts = windowRoute.split("/");
        if (window.location.pathname == "/") {
            return self.changeView("home");
        }

        //throw away first argument because it's "/"
        parts.shift();
        return self.changeView.apply(this, parts);
    }

    function factory(func) {
        return new (func.bind.apply(func, arguments));
    }

    function serializeModel(model) {
        if (model == null) return null;
        var clone = ko.toJS(model); //unwrap observables
        for (var key in clone) {
            if (clone.hasOwnProperty(key)) {
                if (typeof clone[key] == "function")
                    delete clone[key];
            }
        }
        return clone;
    }

    function applySavedModel(viewModel, savedModel) {
        for (var key in savedModel) {
            if (!viewModel.hasOwnProperty(key)) continue;
            if (ko.isObservable(viewModel[key]))
                viewModel[key](savedModel[key]);
            else
                viewModel[key] = savedModel[key];
        }
    }
}

ko.validation.init({
    errorElementClass: 'has-error',
    errorMessageClass: 'help-block',
    decorateInputElement: true
});

ko.validation.rules['mustMatch'] = {
    getValue: function (o) {
        return (typeof o === 'function' ? o() : o);
    },
    validator: function (val, otherField) {
        return val === this.getValue(otherField);
    },
    message: 'The fields must have the same value'
};

ko.validation.registerExtenders();

window.app = new App(ko);