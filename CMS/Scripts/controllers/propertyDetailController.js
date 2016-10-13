function PropertyDetailController($scope, $resource, $routeParams, $location, $q, servicesService, Modal) {
    var propertyResource = $resource('/api/properties/:propertyId', { propertyId: $routeParams.propertyId }, 
        {
            'update': { method: 'PUT' }
        });
    var taskListsResource = $resource('/api/properties/:propertyId/tasks', { propertyId: $routeParams.propertyId });
    var servicesResource = $resource('/api/properties/:propertyId/services', { propertyId: $routeParams.propertyId });
    var eventTasksResource = $resource( "/api/properties/:propertyId/eventtasklists" );
    var customersResource = $resource( "/api/customers" );
    var citiesResource = $resource( "/api/cities" );
    var taskResource = $resource('/api/tasks/:taskId',
   { taskId: $routeParams.taskId },
   {
       'update': { method: 'PUT' }
   });

    var eventschedulesResource = $resource( '/api/eventschedules/:propertyId/:allProperty/propertyevents',
   {
       propertyId: $routeParams.propertyId,
       allProperty:true
   },
   {

   } );

    $scope.liSortByField = "Location";
    $scope.itemReverse = false;

    $scope.eventTaskListId = '';
    $scope.property = propertyResource.get({ propertyId: $routeParams.propertyId });
    $scope.eventTaskLists = eventTasksResource.query({ propertyId: $routeParams.propertyId });
    $scope.customers = customersResource.query( function () {
    } );
    $scope.cities = citiesResource.query( function () { 
    } );

    $scope.tasks = taskListsResource.query( function () {
    } );

    $scope.services = servicesResource.query(function() {

    });

    $scope.newTask = function () {
        $location.path('/properties/' + $routeParams.propertyId + '/tasklists/' + $scope.property.TaskListId + '/tasks/new');
    };

    $scope.newService = function () {
        $location.path('/properties/' + $routeParams.propertyId + '/service/');
    };

    $scope.newEventTaskList = function() {
        $location.path('/properties/' + $routeParams.propertyId + '/schedule/new');
    };

    $scope.assignTasks = function () {
        if (!$scope.eventTaskListId) {
            alert("You need to select an event task list");
            return;
        }
        var prom = [];
        for (var i = 0; i < $scope.tasks.length; i++) {
            if ($scope.tasks[i].selected) {
                prom.push(updateTask($scope.tasks[i]));
            }
        }
        $q.all(prom).then(function() {
            $scope.tasks = taskListsResource.query(function() {});
        });
    };

    var updateTask = function (task) {
        var deferred = $q.defer();
        var promise = deferred.promise;
        task.EventTaskListId = $scope.eventTaskListId;
        taskResource.update({ taskId: task.Id }, task, function () {
            deferred.resolve();
        });
        return promise;
    }

    var deleteFunction = function () {
        $scope.buttonsDisabled = true;
        $scope.buttonsDisabled = true;
        $scope.property.PropertyType = 3;

        propertyResource.update({ propertyId: $routeParams.propertyId }, $scope.property, function () {
            $scope.back();
        },
            function () {
                $scope.buttonsDisabled = false;
            });
    };

    $scope.confirmDelete = function () {
        Modal.showConfirmDelete("property", $scope.property.Name, $scope.property, deleteFunction);
    };

    $scope.back = function () {
      if( $scope.property.PropertyType == undefined ) {
                          $location.path( "/properties/type/1" );
                          }
                    else {
                        $location.path( "/properties/type/" +$scope.property.PropertyType );
                    }
        if (!$scope.$$phase) $scope.$apply();
    };

    $scope.unallocateTask = function(task) {
        task.EventTaskListId = null;
        taskResource.update({ taskId: task.Id }, task, function () {
            $scope.tasks = taskListsResource.query(function () {
            } );
            $scope.eventTaskLists = eventTasksResource.query({ propertyId: $routeParams.propertyId });
        });
    }

    var deleteTaskFunction = function (task) {
        taskResource.delete({ taskId: task.Id }, task, function () {
            $scope.tasks = taskListsResource.query(function () {
            });
        },
            function () {
                $scope.buttonsDisabled = false;
            } );

       
    };

    var deleteServiceFunction = function (service) {
        servicesService.deletePropertyService(service.Id)
            .then(function() {
                $scope.services = servicesResource.query(function() {
                });
            });
        $scope.buttonsDisabled = false;
    };

    $scope.confirmDeleteTask = function (task) {
        Modal.showConfirmDelete("task", task.Location + " - " + task.Description, task, deleteTaskFunction);
    };

    $scope.confirmDeleteService = function (service) {
        Modal.showConfirmDelete("service", service.Name, service, deleteServiceFunction);
    };

    $scope.update = function ( property ) {
        $scope.buttonsDisabled = true;

        propertyResource.update( { propertyId: property.Id }, property, function () {
            $scope.back();
        }, function () {

        } );
    };

    $( document ).ready( function () {
        $( '#datetimepicker' ).datepicker( {
        }

        );
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();
        var year = d.getFullYear();

        var output =
        ( month < 10 ? '0' : '' ) + month + '/' +
        ( day < 10 ? '0' : '' ) + day + '/' + year;

        $( "#datetimepicker" ).val( output );

        $( '#datetimepicker' ).on( 'changeDate', function ( ev ) {
            ( '#datetimepicker' ).valueOf( ev.target.value );
            $scope.property.ContractDate = ev.target.value;
        } );
    } );
}

PropertyDetailController.$inject = ['$scope', '$resource', '$routeParams', '$location', '$q', 'servicesService', 'Modal'];
app.controller('PropertyDetailController', PropertyDetailController);