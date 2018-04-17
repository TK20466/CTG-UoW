angular.module('sharedServices')
    .factory("services.requests",
    [
        "$http", "$location",
        function($http, $location) {
            return {
                get: get,
                post: post,
                view: changeLocation
            }

            function get(path) {
                var ajax = $http.get(path);
                //do more logic
                return ajax;
            }

            function post(path, params) {

            }

            function changeLocation(path) {
                if (path == null) return currentPath();
                $location.path(path);
            }

            function currentPath() {
                return $location.path();
            }
        }
    ])
    .factory("member.service",
    [
        "services.requests", function(http) {
            return {
                getFeaturedMember: getFeaturedMember
            }

            function getFeaturedMember(callback) {
                http.get("/api/members/getFeaturedMember").then(function(response) {
                    callback(response.data);
                });
            }
        }
    ]);