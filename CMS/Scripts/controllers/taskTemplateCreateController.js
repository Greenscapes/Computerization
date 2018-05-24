function TaskTemplateCreateController($scope, $resource, $routeParams, $location) {
    var taskTemplateResource = $resource('/api/taskTemplates/:taskId');

    if ($routeParams.taskId) {
        $scope.task = taskTemplateResource.get({ taskId: $routeParams.taskId });
    }
       
    $scope.save = function (task) {
        $scope.buttonsDisabled = true;
          
        var response = taskTemplateResource.save(task, function() {
            $scope.buttonsDisabled = false;
            $scope.back();
            if (!$scope.$$phase) $scope.$apply();
        },
            function() {
                $scope.buttonsDisabled = false;
            });
      
        $scope.back = function() {
            $location.path("/tasktemplates/");// + "/tasklists/" + $routeParams.taskListId);
            if (!$scope.$$phase) $scope.$apply();
        };
    }

}

TaskTemplateCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskTemplateCreateController', TaskTemplateCreateController);