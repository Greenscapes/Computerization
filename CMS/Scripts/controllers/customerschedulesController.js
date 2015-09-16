function CustomerSchedulesController( $scope, $resource, $location ) {
    var customerschedulesResource = $resource( '/api/customerschedules' );


   
    $scope.properties = customerschedulesResource.query( function () { } );
  }

CustomerSchedulesController.$inject = ['$scope', '$resource', '$location'];

app.controller( 'CustomerSchedulesController', CustomerSchedulesController );






