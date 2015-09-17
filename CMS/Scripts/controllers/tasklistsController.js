function PropertyTaskListTypesController( $scope, $resource, $location ) {
    var taskListTypesResource = $resource("/api/types/tasklists");
 
    $scope.taskListTypes = taskListTypesResource.query(function () { });
 

    $scope.newTaskListType = function () {
        $location.path("/types/tasklists/new");
        if (!$scope.$$phase) $scope.$apply();
    };

  
   
}

PropertyTaskListTypesController.$inject = ['$scope', '$resource', '$location'];
app.controller( 'PropertyTaskListTypesController', PropertyTaskListTypesController );