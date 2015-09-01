function TaskListCreateController($scope, $resource, $routeParams, $location) {
    var taskListsResource = $resource('/api/tasklists/');
    var taskListTypesResource = $resource('/api/types/tasklists/');

    $scope.taskListTypes = taskListTypesResource.query(function() {});

    $scope.buttonsDisabled = false;
    $scope.headers = [];

    $scope.save = function (taskList) {
        $scope.buttonsDisabled = true;
        taskList.propertyId = $routeParams.propertyId;
        taskListsResource.save(taskList, function () {
            $scope.back();
        });
    };

    $scope.back = function () {
        $location.path("/properties/" + $routeParams.propertyId);
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.addHeader = function() {
        $scope.headers.push({});
    }
}

TaskListCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskListCreateController', TaskListCreateController);