﻿function ServiceTicketController($scope, $resource, $routeParams, $location) {
    
    var resource = $resource('/api/servicetickets/:id/:eventDate',
    { id: $routeParams.id }, {
        'update': { method: 'PUT' }
    });

    $scope.isApprove = $routeParams.approve;

    $scope.serviceTicket = resource.get({ eventTaskListId: $routeParams.eventTaskListId, eventDate: $routeParams.eventDate }, function () {

        if (!$routeParams.eventDate)
            $scope.buttonsDisabled = true;

        $scope.serviceTicket.Fields = angular.fromJson($scope.serviceTicket.JsonFields);
        if ($scope.serviceTicket.VisitFromTime) {
            $scope.serviceTicket.FromTime = new Date($scope.serviceTicket.VisitFromTime.substring(0, 19));
        }
        if ($scope.serviceTicket.VisitToTime) {
            $scope.serviceTicket.ToTime = new Date($scope.serviceTicket.VisitToTime.substring(0, 19));
        }
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
    
    $scope.approve = function() {
        $scope.serviceTicket.ApprovedDate = new Date();
        $scope.save();
    }

    $scope.save = function () {
        $scope.buttonsDisabled = true;
        $scope.serviceTicket.JsonFields = angular.toJson($scope.serviceTicket.Fields);
        $scope.serviceTicket.VisitFromTime = $scope.serviceTicket.FromTime;
        var from = $scope.serviceTicket.FromTime;
        var to = $scope.serviceTicket.ToTime;
        $scope.serviceTicket.VisitToTime = new Date(from.getFullYear(), from.getMonth(), from.getDate(), to.getHours(), to.getMinutes(), to.getSeconds(), to.getMilliseconds());
        resource.update({ id: $scope.serviceTicket.Id }, $scope.serviceTicket, function () { $scope.back(); });
    };

    $scope.back = function () {
        if ($scope.isApprove) {
            $location.path("managerdashboard");
        } else {
            $location.path("/crewdashboard/" + $routeParams.crewId);
        }
        
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.showEmployee = function (employee) {
        return employee.IsCrewMember === true || $scope.serviceTicket.ShowAllEmployees === true;
    };

    $scope.getTotal = function (items) {
        var total = 0;
        for (var i = 0; i < items.length; i++) {
            total += items[i].Charge;
        }

        return total;
    }
}

ServiceTicketController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('ServiceTicketController', ServiceTicketController);