function EventTaskListController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var taskResource = $resource('/api/tasks/:taskId',
    { taskId: $routeParams.taskId },
    {
        'update': { method: 'PUT' }
    });
    var eventTasksResource = $resource("/api/properties/:propertyId/task/:taskId/eventtasklists");

    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    $scope.task = taskResource.get({ taskId: $routeParams.taskId }, function () {
        $scope.eventTaskLists = eventTasksResource.query({
            propertyId: $routeParams.propertyId, 
            taskId: $routeParams.taskId
        }, function() {
            for (var i = 0; i < $scope.eventTaskLists.length; i++) {
                if ($scope.eventTaskLists[i].Id === $scope.task.EventTaskListId) {
                    $scope.eventTaskLists[i].checked = true;
                    break;
                }
            }
        });
    });

    $scope.update = function (task) {
        $scope.buttonsDisabled = true;
        for (var i = 0; i < $scope.eventTaskLists.length; i++) {
            if ($scope.eventTaskLists[i].checked) {
                delete $scope.eventTaskLists[i].checked;
                task.EventTaskListId = $scope.eventTaskLists[i].Id;
            }
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

EventTaskListController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EventTaskListController', EventTaskListController);