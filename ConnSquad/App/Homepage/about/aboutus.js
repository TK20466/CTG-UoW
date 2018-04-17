angular.module("homepage")
    .directive("aboutUs", [function () {
        return {
            scope: {
            },
            templateUrl: "app/homepage/about/about.html"
        };
    }])