var app = angular.module("cmsApp", ['ngRoute', 'ngResource', 'ui.bootstrap', 'kendo.directives', 'angular.filter']);

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
            when('/properties/:propertyId/crew/:crewId/route/:routeId', {
                templateUrl: 'templates/crewroute.html',
                controller: 'CrewRouteController'
            }).
            when('/properties/:propertyId/tasklists/:taskListId', {
                templateUrl: 'templates/tasklist-detail.html',
                controller: 'TaskListDetailController'
            }).
            when('/properties/:propertyId/tasks/:taskId/crew', {
                templateUrl: 'templates/taskcrew-create.html',
                controller: 'TaskCrewCreateController'
            }).
            when('/properties/:propertyId/tasks/:taskId/schedule', {
                templateUrl: 'templates/eventtasklists.html',
                controller: 'EventTaskListController'
            }).
            when('/properties/:propertyId/schedule/new', {
                 templateUrl: 'templates/eventtasklist-create.html',
                 controller: 'EventTaskListCreateController'
            }).
            when('/properties/:propertyId/schedule/:eventTaskId', {
                templateUrl: 'templates/eventtasklist-create.html',
                controller: 'EventTaskListCreateController'
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
            when('/types/crewlists/new', {
                templateUrl: 'templates/crewtype-create.html',
                controller: 'CrewTypeCreateController'
            }).
            when('/types/crewlists/:crewTypeId', {
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
            } ).
             when( '/types/tasklists', {
                 templateUrl: 'templates/tasklists.html',
                 controller: 'PropertyTaskListTypesController'
             } ).
              when( '/types/crewlists', {
                  templateUrl: 'templates/crewlists.html',
                  controller: 'CrewListsController'
              } ).
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
             when( '/eventschedules/:eventscheduleId', {
                 templateUrl: 'templates/customerscheduledetails.html',
                 controller: 'EventSchedulesController'
             } ).
            when( '/properties/:propertyId/tasklists/:taskListId/tasks/:taskId/eventschedules', {
                templateUrl: 'templates/customerscheduledetails.html',
                 controller: 'EventSchedulesController'
            } ).
            when( '/eventschedules/:employeeId/events', {
            templateUrl: 'templates/customerscheduledetails.html',
            controller: 'EventSchedulesController'
            } ).
              when( '/eventschedules/:propertyId/:allProperty/propertyevents', {
                  templateUrl: 'templates/property-detail.html',
                  controller: 'EventSchedulesController'
              } ).
             when( '/eventschedules/:year/:month/:date/:crewid/events', {
                 templateUrl: 'templates/property-detail.html',
                 controller: 'EventSchedulesController'
             } ).
              when( '/customerroutes', {
                  templateUrl: 'templates/customers-routes.html',
                  controller: 'CustomersRoutesController'
              }).
            when('/creweventtasks/:taskListId', {
                templateUrl: 'templates/creweventtasks.html',
                controller: 'CrewEventTasksController'
            }).
               when( '/eventnotes/:eventid', {
                   templateUrl: 'templates/eventnotes.html',
                   controller: 'EventNotesController'
               } ).
            
        when( '/eventnotes', {
            templateUrl: 'templates/eventnotes.html',
            controller: 'EventNotesController'
        }).
            when('/servicetickets/:id/:eventDate', {
                templateUrl: 'templates/servicetickets/serviceticket.html',
                controller: 'ServiceTicketController'
            }).
                  when('/servicetemplates', {
                      templateUrl: 'templates/servicetickets/servicetemplate-list.html',
                      controller: 'ServiceTemplateListController'
                  }).

      when('/servicetemplates/:serviceTemplateId', {
          templateUrl: 'templates/servicetickets/servicetemplate-detail.html',
          controller: 'ServiceTemplateDetailController'
      }).

                  when('/servicetemplates/new', {
                      templateUrl: 'templates/servicetickets/servicetemplate-detail.html',
                      controller: 'ServiceTemplateDetailController'
                  }).
            when('/tasktemplates', {
                templateUrl: 'templates/taskTemplates.html',
                controller: 'TaskTemplateController'
            }).
            when('/tasktemplates/new', {
                templateUrl: 'templates/tasktemplate-create.html',
                controller: 'TaskTemplateCreateController'
            }).
            when('/tasktemplates/:taskId', {
                templateUrl: 'templates/tasktemplate-create.html',
                controller: 'TaskTemplateCreateController'
            }).
            when('/schedule', {
                templateUrl: 'templates/schedule.html',
                controller: 'ScheduleController'
            }).
            otherwise({
                redirectTo: '/customerroutes'
            });

        $resourceProvider.defaults.stripTrainingSlashes = false;
    }
]);
