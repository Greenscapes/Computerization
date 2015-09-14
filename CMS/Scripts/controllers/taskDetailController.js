function TaskDetailController($scope, $resource, $routeParams, $location, Modal) {
    var taskListResource = $resource('/api/tasklists/:taskListId',
    {
        taskListId: $routeParams.taskListId
    });
    var taskResource = $resource('/api/tasks/:taskId',
    { taskId: $routeParams.taskId },
    {
        'update': { method: 'PUT' }
    });
    var crewsResource = $resource( "/api/crews" );
  
    $scope.task = taskResource.get( { taskId: $routeParams.taskId }, function () {

        $scope.crews = crewsResource.query( function () {
            for ( var i = 0; i < $scope.crews.length; i++ ) {
                for ( var j = 0; j < $scope.task.Crews.length; j++ ) {
                    if ( $scope.crews[i].Id === $scope.task.Crews[j].Id ) {
                        $scope.crews[i].checked = true;
                        break;
                    }
                }
            }
        } );

        $scope.taskList = taskListResource.get({}, function () {
            for (var i = 0; i < $scope.taskList.PropertyTaskHeaders.length; i++) {
                for (var j = 0; j < $scope.task.PropertyTaskDetails.length; j++) {
                    if ($scope.taskList.PropertyTaskHeaders[i].Id === $scope.task.PropertyTaskDetails[j].PropertyTaskHeaderId) {
                        $scope.task.PropertyTaskDetails[j].HeaderName = $scope.taskList.PropertyTaskHeaders[i].Name;
                    }
                }
            }
            if (!$scope.$$phase) $scope.$apply();
        });
    });
     

    $scope.update = function(task) {
        $scope.buttonsDisabled = true;
        taskResource.update({ taskId: task.Id }, task, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    var deleteFunction = function(task) {
        $scope.buttonsDisabled = true;
        taskResource.delete({ taskId: task.Id }, task, function() {
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("task", $scope.task.Location, $scope.task, deleteFunction);
    };

    $scope.back = function() {
        $location.path("/properties/" + $routeParams.propertyId + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    }
};

TaskDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('TaskDetailController', TaskDetailController);