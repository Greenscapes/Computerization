function CrewEventTasksController($scope, $resource, $routeParams, $location) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var eventTasksResource = $resource("/api/eventtasklists/:taskListId", { taskListId: $routeParams.taskListId });
   
    $scope.taskList = eventTasksResource.get({}, function () {
        $scope.property = propertyResource.get({ propertyId: $scope.taskList.PropertyId });
    });

    var today = new Date();
    $scope.ticketDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate();

    $scope.back = function () {
        $location.path("/crewdashboard/" + $routeParams.crewId);// + "/tasklists/" + $routeParams.taskListId);
        if (!$scope.$$phase) $scope.$apply();
    };
}

CrewEventTasksController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewEventTasksController', CrewEventTasksController);