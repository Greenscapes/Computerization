function TaskTemplateController($scope, $resource, $routeParams, $location) {
    var taskResource = $resource('/api/taskTemplates/');

    $scope.tasks = taskResource.query({});
}

TaskTemplateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('TaskTemplateController', TaskTemplateController);