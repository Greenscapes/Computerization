function CustomerScheduleDetailsController($scope, $resource, $location) {
    var propertiesResource = $resource('/api/schedules');
    $scope.properties = propertiesResource.query(function () { });

   
}

CustomerScheduleDetailsController.$inject = ['$scope', '$resource', '$location'];
app.controller('CustomerScheduleDetailsController', CustomerScheduleDetailsController);