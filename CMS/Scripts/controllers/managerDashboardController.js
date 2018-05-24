function ManagerDashboardController( $scope, $resource, $routeParams, $location, Modal ) {

    var eventsResource = $resource('/api/servicetickets/manager');
    var eventsResourceApproved = $resource('/api/servicetickets/manager/approved');

    $scope.manager = eventsResource.get({}, function() {
    });

    $scope.managerApproved = null;

    $scope.showApproved = false;

    $scope.launchTicket = function (event, isApprove) {
        var date = event.EventDate.substring(0, 10).split('-');
        date = date[1] + '-' + date[2] + '-' + date[0];
        var today = new Date(date);
        var ticketDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + (today.getDate());
        $location.path('/servicetickets/' + event.EventTaskListId + "/" + ticketDate + "/approve/" + isApprove);
        if (!$scope.$$phase) $scope.$apply();
    }

    $scope.getApproved = function () {
        if ($scope.showApproved && !$scope.managerApproved) {
            $scope.managerApproved = eventsResourceApproved.get({}, function () {
            });
        }   
    }
}

ManagerDashboardController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller( 'ManagerDashboardController', ManagerDashboardController );