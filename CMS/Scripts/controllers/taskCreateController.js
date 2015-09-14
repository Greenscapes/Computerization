function TaskCreateController($scope, $resource, $routeParams, $location) {
    var taskListResource = $resource('/api/tasklists/:taskListId',
    {
        taskListId: $routeParams.taskListId
    });
    var tasksResource = $resource('/api/tasks');

    $scope.task = {};
    $scope.taskList = taskListResource.get({}, function() {
        $scope.task.PropertyTaskDetails = [];
        for (var i = 0; i < $scope.taskList.PropertyTaskListType.PropertyTaskHeaders.length; i++) {
            var newTaskDetail = {
                PropertyTaskHeaderId: $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Id,
                HeaderName: $scope.taskList.PropertyTaskListType.PropertyTaskHeaders[i].Name
            };

            $scope.task.PropertyTaskDetails.push(newTaskDetail);
        }
        if (!$scope.$$phase) $scope.$apply();
    } );

    var crewsResource = $resource( "/api/crews" );
    $scope.crews = crewsResource.query( function () { } );

    $scope.save = function(task) {
        $scope.buttonsDisabled = true;
        task.PropertyTaskListId = $routeParams.taskListId;
        task.crews = [];
        for ( var i = 0; i < $scope.crews.length; i++ ) {
            if ( $scope.crews[i].checked ) {
                delete $scope.crews[i].checked;
                task.crews.push( $scope.crews[i] );
            }
        }
        tasksResource.save(task, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
                if (!$scope.$$phase) $scope.$apply();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.back = function() {
        $location.path("/properties/" + $routeParams.propertyId + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    };
}

TaskCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskCreateController', TaskCreateController);