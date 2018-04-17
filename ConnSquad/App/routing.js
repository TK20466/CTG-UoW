angular.module("ctgApp")
.factory("test", function() { return 0; })
.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
    $routeProvider
      .when('/', {
          templateUrl: 'app/homepage/main.html',
          controller: 'homepageController'
      })
      .when('/home', {
          templateUrl: 'app/homepage/main.html',
          controller: 'homepageController'
      })
      .when('/events', {
          templateUrl: 'app/events/events.html',
          //controller: 'LoginCtrl'
      })
      .otherwise({
          redirectTo: '/'
      });

    $locationProvider.html5Mode(true);
}]);