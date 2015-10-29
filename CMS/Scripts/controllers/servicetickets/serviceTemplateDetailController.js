function ServiceTemplateDetailController($scope, $resource, $routeParams, $location, Modal) {
    var resource = $resource( '/api/servicetemplates/:serviceTemplateId',
    { serviceTemplateId: $routeParams.serviceTemplateId }, {
        'update': { method: 'PUT' }
    });

    $scope.buttonsDisabled = false;

    $scope.allowDelete = $routeParams.serviceTemplateId > 0;

    if ($routeParams.serviceTemplateId > 0) {
        $scope.serviceTemplate = resource.get({ serviceTemplateId: $routeParams.serviceTemplateId });
    }
        
    $scope.save = function () {
        $scope.buttonsDisabled = true;

        if ($routeParams.serviceTemplateId > 0) {
            resource.update({ serviceTemplateId: $scope.serviceTemplate.Id }, $scope.serviceTemplate, function () { $scope.back(); });
        }
        else {
            resource.save({ serviceTemplateId: null }, $scope.serviceTemplate, function () { $scope.buttonsDisabled = false; $scope.back(); }, function () { $scope.buttonsDisabled = false; });
        }
    };

    $scope.back = function () {
        $location.path( "/servicetemplates" );
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("Service Template", $scope.serviceTemplate.Name, $scope.serviceTemplate, function () {
            $scope.buttonsDisabled = true;
            resource.delete({ serviceTemplateId: $routeParams.serviceTemplateId }, $scope.serviceTemplate, function () {
                $scope.back();
                if (!$scope.$$phase) $scope.$apply();
            }, function () {
                $scope.buttonsDisabled = false;
            });
        }
       );
    };
}

ServiceTemplateDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller('ServiceTemplateDetailController', ServiceTemplateDetailController);