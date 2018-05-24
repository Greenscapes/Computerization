function CrewMembersController($scope, $resource, $location) {
    var crewsResource = $resource("/api/crews");
    $scope.crews = crewsResource.query(function () { });

}

CrewMembersController.$inject = ['$scope', '$resource', '$location'];
app.controller('CrewMembersController', CrewMembersController);