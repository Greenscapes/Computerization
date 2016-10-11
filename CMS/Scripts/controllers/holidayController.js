(function (angular) {
    'use strict';

    var controllerId = 'HolidayController';

    angular.module('cmsApp').controller(controllerId,
        [
            'scheduleService', 'alertService', '$location', '$routeParams',
            function (scheduleService, alertService, $location, $routeParams) {

                var vm = this;

                vm.holidays = [];
                vm.holiday = {};
                vm.buttonsDisabled = false;
                vm.inEdit = false;

                vm.save = save;
                vm.newHoliday = newHoliday;
                vm.deleteHoliday = deleteHoliday;

                activate();

                function activate() {
                    scheduleService.getHolidays()
                        .then(function(data) {
                            vm.holidays = data;
                        });
                }

                function newHoliday() {
                    vm.inEdit = true;
                }

                function save() {
                    vm.buttonsDisabled = true;

                    scheduleService.createHoliday(vm.holiday).then(function () {
                        vm.buttonsDisabled = false;
                        vm.inEdit = false;
                        activate();
                    }, function (error) {
                        _showValidationErrors(error);
                        vm.buttonsDisabled = false;
                    });
                };

                function deleteHoliday(id) {
                    scheduleService.deleteHoliday(id)
                        .then(function() {
                            activate();
                        });
                }

                function _showValidationErrors($scope, error) {
                    if (error.data && angular.isObject(error.data)) {
                        //for (var key in error.data) {
                        //    $scope.validationErrors.push(error.data[key]);
                        //}
                    }
                    else {
                        alertService.error("Could not add city");
                    }
                };
            }
        ]
    );
})(angular);