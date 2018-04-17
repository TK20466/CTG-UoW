angular.module("homepage")
    .controller("homepageController", ["$scope", "eventList.service", "member.service", function ($scope, eventsService, memberService) {
        $scope.events = {
            loading: true,
            items: []
        }
        $scope.featuredMember = {
            loading: true,
            name: "",
            imperialId: 0,
            image: ""
        }

        initialize();

        function initialize() {
            getEvents();
            getFeaturedMember();
        }


        function getEvents() {
            eventsService.getFutureEvents(function (events) {
                for(var i = 0; i < events.length; i++)
                $scope.events.items = events;
                $scope.events.loading = false;
            });
        }

        function getFeaturedMember() {
            memberService.getFeaturedMember(function(member) {
                $scope.featuredMember.name = member.name;
                $scope.featuredMember.imperialId = member.imperialId;
                $scope.featuredMember.image = member.profileImage.path
                $scope.featuredMember.loading = false;
            });
        }
    }])