function ServiceTemplateListController( $scope, $resource, $location ) {
    var resource = $resource( "/api/servicetemplates" );
    $scope.serviceTemplates = resource.query(function () {});

    $scope.new = function () {
        $location.path( "/servicetemplates/new" );
        if (!$scope.$$phase) $scope.$apply();
    };
}

ServiceTemplateListController.$inject = ['$scope', '$resource', '$location'];
app.controller('ServiceTemplateListController', ServiceTemplateListController);