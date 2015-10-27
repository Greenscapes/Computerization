function TaskCrewCreateController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var taskResource = $resource('/api/tasks/:taskId',
    { taskId: $routeParams.taskId });
    var crewsResource = $resource("/api/crews");

    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    $scope.task = taskResource.get({ taskId: $routeParams.taskId }, function () {

        $scope.crews = crewsResource.query(function () {
            for (var i = 0; i < $scope.crews.length; i++) {
                for (var j = 0; j < $scope.task.Crews.length; j++) {
                    if ($scope.crews[i].Id === $scope.task.Crews[j].Id) {
                        $scope.crews[i].checked = true;
                        break;
                    }
                }
            }
        });
    });

    $scope.update = function () {
        $location.path("/properties/1");
        if (!$scope.$$phase) $scope.$apply();
    };
}

TaskCrewCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskCrewCreateController', TaskCrewCreateController);