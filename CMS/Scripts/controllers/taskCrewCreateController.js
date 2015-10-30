function TaskCrewCreateController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var taskResource = $resource('/api/tasks/:taskId',
    { taskId: $routeParams.taskId },
    {
        'update': { method: 'PUT' }
    });
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

    $scope.update = function (task) {
        $scope.buttonsDisabled = true;
        task.Crews = [];
        for ( var i = 0; i < $scope.crews.length; i++ ) {
            if ( $scope.crews[i].checked ) {
                delete $scope.crews[i].checked;
                task.Crews.push( $scope.crews[i] );
            }
        }
        if (task.Crews.length > 1) {
            alert('You can only assign 1 crew to a task.');
            $scope.buttonsDisabled = false;
            return;
        }

        taskResource.update({ taskId: task.Id }, task, function () {
            $scope.buttonsDisabled = false;

            $location.path("/properties/1");
            if (!$scope.$$phase) $scope.$apply();
        },
        function () {
            $scope.buttonsDisabled = false;
        });
    };
}

TaskCrewCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskCrewCreateController', TaskCrewCreateController);