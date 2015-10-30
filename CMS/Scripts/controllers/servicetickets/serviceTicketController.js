function ServiceTicketController($scope, $resource, $routeParams, $location) {
    
    var resource = $resource('/api/servicetickets/:serviceTicketId',
    { serviceTicketId: $routeParams.serviceTicketId }, {
        'update': { method: 'PUT' }
    });

    $scope.serviceTicket = resource.get({ serviceTicketId: $routeParams.serviceTicketId }, function () {
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
    
    $scope.save = function () {
        $scope.buttonsDisabled = true;

        $scope.serviceTicket.JsonFields = JSON.stringify($scope.serviceTicket.Fields);

        if ($routeParams.serviceTicketId > 0) {
            resource.update({ serviceTicketId: $scope.serviceTicket.Id }, $scope.serviceTicket, function () { $scope.back(); });
        }
        else {
            resource.save({ serviceTicketId: null }, $scope.serviceTicket, function () { $scope.buttonsDisabled = false; $scope.back(); }, function () { $scope.buttonsDisabled = false; });
        }
    };

    $scope.back = function () {
        $location.path("/");
        if (!$scope.$$phase) $scope.$apply();
    };
}

ServiceTicketController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('ServiceTicketController', ServiceTicketController);