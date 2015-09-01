function TypesController($scope, $resource, $location) {
    var taskListTypesResource = $resource("/api/types/tasklists");
    var crewTypesResource = $resource("/api/types/crews");
    $scope.taskListTypes = taskListTypesResource.query(function () { });
    $scope.crewTypes = crewTypesResource.query(function () { });

    $scope.newTaskListType = function () {
        $location.path("/types/tasklists/new");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.newCrewType = function () {
        $location.path("/types/crews/new");
        if (!$scope.$$phase) $scope.$apply();
    };
}

TypesController.$inject = ['$scope', '$resource', '$location'];
app.controller('TypesController', TypesController);