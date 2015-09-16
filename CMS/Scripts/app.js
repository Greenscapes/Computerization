var app = angular.module("cmsApp", ['ngRoute', 'ngResource', 'ui.bootstrap']);

app.config([
    '$routeProvider', '$resourceProvider',
    function($routeProvider, $resourceProvider) {
        $routeProvider.
            when('/properties/:propertyId/tasklists/:taskListId/tasks/new', {
                templateUrl: 'templates/task-create.html',
                controller: 'TaskCreateController'
            }).
            when('/properties/:propertyId/tasklists/:taskListId/tasks/:taskId', {
                templateUrl: 'templates/task-detail.html',
                controller: 'TaskDetailController'
            }).
            when('/properties/:propertyId/tasklists/new', {
                templateUrl: 'templates/tasklist-create.html',
                controller: 'TaskListCreateController'
            }).
            when('/properties/:propertyId/tasklists/:taskListId', {
                templateUrl: 'templates/tasklist-detail.html',
                controller: 'TaskListDetailController'
            }).
            when('/properties/new', {
                templateUrl: 'templates/property-create.html',
                controller: 'PropertyCreateController'
            }).
            when('/properties/:propertyId', {
                templateUrl: 'templates/property-detail.html',
                controller: 'PropertyDetailController'
            }).
            when('/properties', {
                templateUrl: 'templates/properties.html',
                controller: 'PropertiesController'
            }).
            when('/types/crews/new', {
                templateUrl: 'templates/crewtype-create.html',
                controller: 'CrewTypeCreateController'
            }).
            when('/types/crews/:crewTypeId', {
                templateUrl: 'templates/crewtype-detail.html',
                controller: 'CrewTypeDetailController'
            }).
            when('/types/tasklists/new', {
                templateUrl: 'templates/tasklisttype-create.html',
                controller: 'TaskListTypeCreateController'
            }).
            when('/types/tasklists/:taskListTypeId', {
                templateUrl: 'templates/tasklisttype-detail.html',
                controller: 'TaskListTypeDetailController'
            }).
            when('/types', {
                templateUrl: 'templates/types.html',
                controller: 'TypesController'
            }).
            when('/employees/new', {
                templateUrl: 'templates/employee-create.html',
                controller: 'EmployeeCreateController'
            }).
            when('/employees/:employeeId', {
                templateUrl: 'templates/employee-detail.html',
                controller: 'EmployeeDetailController'
            }).
            when('/employees', {
                templateUrl: 'templates/employees.html',
                controller: 'EmployeesController'
            }).
            when('/crews/:crewId/members/new', {
                templateUrl: 'templates/crewmember-create.html',
                controller: 'CrewMemberCreateController'
            }).
            when('/crews/:crewId/members/:memberId', {
                templateUrl: 'templates/crewmember-detail.html',
                controller: 'CrewMemberDetailController'
            }).
            when('/crews/new', {
                templateUrl: 'templates/crew-create.html',
                controller: 'CrewCreateController'
            }).
            when('/crews/:crewId', {
                templateUrl: 'templates/crew-detail.html',
                controller: 'CrewDetailController'
            }).
            when('/crews', {
                templateUrl: 'templates/crews.html',
                controller: 'CrewsController'
            }).
            when('/customerschedules', {
                templateUrl: 'templates/customerschedules.html',
                controller: 'CustomerSchedulesController'
            }).
             when( '/eventschedules/:propertyId', {
                 templateUrl: 'templates/customerscheduledetails.html',
                 controller: 'EventSchedulesController'
             } ).
             when( '/eventschedules', {
                 templateUrl: 'templates/customerscheduledetails.html',
                 controller: 'EventSchedulesController'
             } ).
            when( '/properties/:propertyId/tasklists/:taskListId/tasks/:taskId/eventschedules', {
                templateUrl: 'templates/customerscheduledetails.html',
                 controller: 'EventSchedulesController'
             } ).
            otherwise({
                redirectTo: '/properties'
            });

        $resourceProvider.defaults.stripTrainingSlashes = false;
    }
]);
