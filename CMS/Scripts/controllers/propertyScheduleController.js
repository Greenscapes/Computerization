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
            }
        ]
    );
})(angular);