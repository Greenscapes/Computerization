function CrewEventTasksController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var eventTasksResource = $resource("/api/eventtasklists/:taskListId", { taskListId: $routeParams.taskListId });
   
    $scope.taskList = eventTasksResource.get({}, function () {
        $scope.property = propertyResource.get({ propertyId: $scope.taskList.PropertyId });
    });
}

CrewEventTasksController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewEventTasksController', CrewEventTasksController);