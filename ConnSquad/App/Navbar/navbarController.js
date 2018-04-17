angular.module('sidebarNav')
    .directive("primaryNavBar", function () {
    return {
        scope: {},
        templateUrl: "app/navbar/navbar.html",
        controller: ["services.requests", "$scope", function(http, $scope) {
            $scope.goto = function(page) {
                http.view(page);
            };
            $scope.currentNavItem = resolveView();

            function resolveView() {
                var view = http.view();
                if (view == "/") return "/home";
                return view;
            }
        }]
    }
})