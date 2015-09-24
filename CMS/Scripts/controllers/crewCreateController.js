function CrewCreateController($scope, $resource, $routeParams, $location) {
    var crewsResource = $resource('/api/crews');
    var crewTypesResource = $resource("/api/types/crewlists");
    var employeesResource = $resource( "/api/employees" );
  
 
    $scope.crewTypes = crewTypesResource.query();

    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/crews");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.save = function(crew) {
        $scope.buttonsDisabled = true;

        crew.CrewMembers = [];

        for ( var i = 0; i < $scope.employees.length; i++ ) {
            if ( $scope.employees[i].checked ) {
                delete $scope.employees[i].checked;
                
                var cremember = new Object( {
                    EmployeeId: $scope.employees[i].Id,
                    IsCrewLeader: $scope.employees[i].IsCrewLeader
                }
                );
                crew.CrewMembers.push( cremember );
            }
        }
        crewsResource.save(crew, function() {
            $scope.buttonsDisabled = false;

            $scope.back();
        },
            function() {
                $scope.buttonsDisabled = false;
            } );

    };
   

   
    $scope.selected = function ( crew ) {
        
        var allEmployees = employeesResource.query( function () {
            var crewtypeEmployees =[]
            for ( var i = 0; i < allEmployees.length; i++ ) {
                var employee = allEmployees[i];
                var crewTypeMember = false;
                for ( var j = 0; j < employee.CrewTypes.length; j++ ) {
                    if ( employee.CrewTypes[j].Id == crew.CrewTypeId ) {
                        crewTypeMember = true;
                        break;
                    }
                }
                if ( crewTypeMember ) {
                    crewtypeEmployees.push( allEmployees[i] );
                }
            }
            $scope.employees = crewtypeEmployees;
        } );
       
      
        

    }
}


CrewCreateController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('CrewCreateController', CrewCreateController);