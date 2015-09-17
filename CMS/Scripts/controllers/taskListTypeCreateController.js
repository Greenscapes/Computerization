function TaskListTypeCreateController($scope, $resource, $routeParams, $location) {
    var taskListTypesResource = $resource('/api/types/tasklists/');

    $scope.buttonsDisabled = false;
    $scope.headers = [];

    $scope.save = function (taskListType) {
        $scope.buttonsDisabled = true;
        taskListType.propertyId = $routeParams.propertyId;
        taskListType.PropertyTaskHeaders = [];
        for (var i = 0; i < $scope.headers.length; i++) {
            if ($scope.headers[i].Name) {
                taskListType.PropertyTaskHeaders.push($scope.headers[i]);
            }
        }
        taskListTypesResource.save(taskListType, function () {
            $scope.back();
        });
    };

    $scope.back = function () {
        $location.path("/types/tasklists");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.addHeader = function() {
        $scope.headers.push({});
    };
}

TaskListTypeCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskListTypeCreateController', TaskListTypeCreateController);