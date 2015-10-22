function EmployeeDetailController($scope, $resource, $routeParams, $location) {
    var employeesResource = $resource('/api/employees/:employeeId',
    { employeeId: $routeParams.employeeId },
    {
        'update': { method: 'PUT' }
    });
    var crewMemberResource = $resource('/api/crews/employee/:employeeId',
    { employeeId: $routeParams.employeeId });

    var crewTypesResource = $resource('/api/types/crewlists');
    var eventschedulesResource = $resource( '/api/eventschedules/:employeeId/events',
    { employeeId: $routeParams.employeeId },
    {
          
    });
  
    $scope.employee = employeesResource.get( {}, function () {
        
        $scope.crewTypes = crewTypesResource.query( function () {
            for ( var i = 0; i < $scope.crewTypes.length; i++ ) {
                for ( var j = 0; j < $scope.employee.CrewTypes.length; j++ ) {
                    if ( $scope.crewTypes[i].Id === $scope.employee.CrewTypes[j].Id ) {
                        $scope.crewTypes[i].checked = true;
                        break;
                    }
                }
            }
            
        });
        $scope.crew = crewMemberResource.get({}, function() {

        });

        $scope.empEvents = [];
        $scope.eventlist = eventschedulesResource.query( {}, function () {

            $scope.empEvents;
            GetEvents( $scope.eventlist );
            loadEvents();

        } );
       
    } );
    
 
    function GetEvents( data ) {
        for ( var i = 0; i < data.length; i++ ) {

            var event = data[i];
            var newEvent = new Object( {
                taskId: event.Id,
                start: new Date( event.StartTime.toString() ),
                end: new Date( event.EndTime.toString() ),
                title: event.Title,
                isAllDay: event.IsAllDay,
                startTimezone: event.StartTimezone,
                endTimezone: event.EndTimezone,
                description: event.Description,
                recurrenceId: event.RecurrenceID,
                recurrenceRule: event.RecurrenceRule,
                recurrenceException: event.RecurrenceException

            } );
            $scope.empEvents.push( newEvent );

            // $( "#scheduler" ).data( "kendoScheduler" ).addEvent( newEvent );
        }

    }

    function loadEvents() {
        var scheduler = $( "#scheduler" ).kendoScheduler( {
            date: new Date(),
            startTime: new Date(),
            height: 600,
            editable: false,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
            dataSource: $scope.empEvents,
        } ).data( "kendoScheduler" );
    }

    
    $scope.buttonsDisabled = false;

    $scope.back = function () {
        $location.path("/employees");
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.update = function(employee) {
        $scope.buttonsDisabled = true;
        employee.CrewTypes = [];

        for (var i = 0; i < $scope.crewTypes.length; i++) {
            if ($scope.crewTypes[i].checked) {
                delete $scope.crewTypes[i].checked;
                employee.CrewTypes.push($scope.crewTypes[i]);
            }
        }

        employeesResource.update({ employeeId: employee.Id }, employee, function() {
                $scope.buttonsDisabled = false;
                $scope.back();
            },
            function() {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.formatName = function (employee) {
        var name = employee.FirstName;
        if (employee.MiddleName && name) {
            name += " " + employee.MiddleName;
        }
        if (employee.LastName && name) {
            name += " " + employee.LastName;
        }

        return name;
    };
};

EmployeeDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location'];
app.controller('EmployeeDetailController', EmployeeDetailController);