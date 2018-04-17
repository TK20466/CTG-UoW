angular.module("homepage")
    .service("eventList.service", ["services.requests", function(http) {
        return {
            getFutureEvents: getFutureEvents
        }

        function getFutureEvents(callback, count) {
            if (count == null) count = 5;
            http.get("/api/events/getFutureEvents/" + count).then(function(events) {
                callback(events.data);
            });
        }
    }])
    .directive("eventList", [function() {
        return {
            scope: {
                events: "=",
                loading: "="
            },
            templateUrl: "app/homepage/eventlist/eventlist.html",
            controller: ["$scope", function ($scope) {
                $scope.viewEvent = function (item) {
                    alert("event redirect");
                }
            }]
        };
    }])