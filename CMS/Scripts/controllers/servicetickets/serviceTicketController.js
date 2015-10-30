function ServiceTicketController($scope, $resource, $routeParams, $location) {
    
    var serviceTicektResource = $resource('/api/servicetickets/:serviceTicketId');

    $scope.serviceTicket = serviceTicektResource.get({ serviceTicketId: $routeParams.serviceTicketId }, function () {
        $scope.serviceTicket.Fields = JSON.parse($scope.serviceTicket.JsonFields);
    });

    $scope.Item = null;

    $scope.removeItem = function (list, item) {
        list.splice(list.indexOf(item), 1);
    }

    $scope.selectItem = function (item) {
        $scope.Item = item;
    }

    $scope.addItem = function (list, item) {
        $scope.Item = angular.copy(item);
        list.push($scope.Item);
    }
    
    $scope.update = function (serviceTicket) {
        $scope.serviceTicket.Notes = JSON.stringify($scope.serviceTicket.Fields);
    }
}

ServiceTicketController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('ServiceTicketController', ServiceTicketController);