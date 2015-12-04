function ManagerDashboardController( $scope, $resource, $routeParams, $location, Modal ) {

    var eventsResource = $resource('/api/servicetickets/manager');

    $scope.manager = eventsResource.get({}, function() {
    });

    $scope.launchTicket = function (event) {
        var date = event.EventDate.substring(0, 10).split('-');
        date = date[1] + '-' + date[2] + '-' + date[0];
        var today = new Date(date);
        var ticketDate = today.getFullYear() + "-" + (today.getMonth() + 1) + "-" + (today.getDate());
        $location.path('/servicetickets/' + event.EventTaskListId + "/" + ticketDate + "/true");
        if (!$scope.$$phase) $scope.$apply();
    }
}

ManagerDashboardController.$inject = ['$scope', '$resource', '$routeParams', '$location', 'Modal'];
app.controller( 'ManagerDashboardController', ManagerDashboardController );