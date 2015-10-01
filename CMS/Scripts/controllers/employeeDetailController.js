function EmployeeDetailController($scope, $resource, $routeParams, $location) {
    var employeesResource = $resource('/api/employees/:employeeId',
    { employeeId: $routeParams.employeeId },
    {
        'update': { method: 'PUT' }
    });
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

        } );
        $scope.empEvents = [];
        $scope.eventlist = eventschedulesResource.query( {}, function () {

            for ( var k = 0; k < $scope.eventlist.length; k++ ) {
                var empevent = $scope.eventlist[k];
                var taskEvent = new Object( {
                    start: empevent.StartTime.toString(),
                    end: empevent.EndTime.toString(),
                    text: empevent.Title,
                    id: empevent.Id.toString()
                } );

                $scope.empEvents.push( taskEvent );
                loadEvents();

            }

        } );
       
    } );
    
    var nav = new DayPilot.Navigator( "nav" );
    nav.showMonths = 3;
    nav.skipMonths = 3;
    nav.selectMode = "week";
    nav.onTimeRangeSelected = function ( args ) {
        dp.startDate = args.day;
        dp.update();
        loadEvents();
    };
    nav.init();
   
    var dp = new DayPilot.Calendar( "dp" );
    dp.viewType = "Week";
    dp.headerDateFormat = "dddd <br/>d MMMM yyyy";
    dp.headerHeight = "40";
    dp.columnHeaderHeightAutoFit = "false"

    dp.onTimeRangeSelected = function ( args ) {
       

    }
        dp.init();
        loadEvents();


        function loadEvents() {
            var start = dp.visibleStart();
            var end = dp.visibleEnd();
            dp.events.list = $scope.empEvents;
            dp.update();
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