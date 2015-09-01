function PropertyDetailController($scope, $resource, $routeParams, $location, Modal) {
    var propertyResource = $resource('/api/properties/:propertyId');
    var taskListsResource = $resource('/api/properties/:propertyId/tasklists', { propertyId: $routeParams.propertyId });
    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    $scope.taskLists = taskListsResource.query(function () { });

    $scope.newTaskList = function () {
        $location.path('/properties/' + $routeParams.propertyId + '/tasklists/new');
    };

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        propertyResource.delete({ propertyId: $routeParams.propertyId }, $scope.property, function () {
                $scope.back();
            },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("property", $scope.property.Name, $scope.property, deleteFunction);
    };

    $scope.back = function () {
        $location.path("/properties");
        if (!$scope.$$phase) $scope.$apply();
    };
}

PropertyDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('PropertyDetailController', PropertyDetailController);