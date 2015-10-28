function ServiceTicketController($scope, $resource, $routeParams, $location) {
    
    var serviceTicektResource = $resource('/api/servicetickets/:serviceTicketId');

    $scope.serviceTicket = serviceTicektResource.get({ serviceTicketId: $routeParams.serviceTicketId }, function () {
        $scope.serviceTicket.Fields = {
            ZoneNumber: 10,
            Hello: function () { alert(JSON.stringify($scope.serviceTicket.Fields)); },
            Description: 'hello'
        };

            //JSON.parse($scope.serviceTicket.JsonFields);
    });
}

ServiceTicketController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('ServiceTicketController', ServiceTicketController);