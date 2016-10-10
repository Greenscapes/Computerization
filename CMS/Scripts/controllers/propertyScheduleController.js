(function (angular) {
    'use strict';

    var controllerId = 'PropertyScheduleController';

    angular.module('cmsApp').controller(controllerId,
        [
            'propertyService', 'alertService', '$location', '$routeParams',
            function (propertyService, alertService, $location, $routeParams) {

                var vm = this;

                vm.property = {};
                vm.propertyId = '';
                vm.eventTaskLists = [];
                vm.back = back;
                vm.updateFreeService = updateFreeService;

                activate();

                function activate() {
                    vm.propertyId = $routeParams.propertyId;
                    if (vm.propertyId) {
                        propertyService.getProperty(vm.propertyId).then(function (data) {
                            vm.property = data;
                            propertyService.getPropertySchedule(vm.propertyId)
                                .then(function(data) {
                                    vm.eventTaskLists = data;
                                });
                        });
                    }
                }
               
                function back() {
                    $location.path("/properties/" + vm.propertyId);
                };

                function updateFreeService(schedule, eventTaskListId) {
                    var freeServiceUpdate = {
                        serviceDate: schedule.DateTime,
                        isFreeService: schedule.FreeService,
                        eventTaskListId: eventTaskListId,
                        propertyId: vm.propertyId
                    };

                    if (schedule.FreeService && vm.property.NumberOfFreeServiceCalls <= 0) {
                        if (!confirm('This property has no more free services. Do you want to continue?')) {
                            schedule.FreeService = false;
                            return;
                        }  
                    }

                    propertyService.setFreeService(freeServiceUpdate)
                        .then(function(data) {
                            activate();
                        });
                }
            }
        ]
    );
})(angular);