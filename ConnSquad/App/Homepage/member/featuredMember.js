angular.module("homepage")
    .directive("featuredMember", [function () {
        return {
            scope: {
                model: '='
            },
            templateUrl: "app/homepage/member/member.html",
            controller: ["$scope", function ($scope) {
                if ($scope.model && $scope.model.loading == null) {
                    $scope.model.loading = true;
                }
            }]
        };
    }])