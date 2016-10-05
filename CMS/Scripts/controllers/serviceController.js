(function (angular) {
    'use strict';

    var controllerId = 'ServiceController';

    angular.module('cmsApp').controller(controllerId,
        [
            'servicesService', 'alertService', '$location', '$routeParams',
            function (servicesService, alertService, $location, $routeParams) {

                var vm = this;

                vm.service = {};
                vm.buttonsDisabled = false;
                vm.isUpdate = false;

                vm.save = save;
                vm.back = back;

                activate();

                function activate() {
                    var serviceId = $routeParams.serviceId;
                    if (serviceId) {
                        servicesService.getCity(serviceId).then(function (data) {
                            vm.service = data;
                            vm.isUpdate = true;
                        });
                    }
                }

                function save() {
                    vm.buttonsDisabled = true;

                    if (vm.isUpdate) {
                        servicesService.updateService(vm.service).then(function () {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function (error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    } else {
                        servicesService.createService(vm.service).then(function () {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function (error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    }
                };

                function back() {
                    $location.path("/services");
                };

                function _showValidationErrors($scope, error) {
                    if (error.data && angular.isObject(error.data)) {
                        //for (var key in error.data) {
                        //    $scope.validationErrors.push(error.data[key]);
                        //}
                    }
                    else {
                        alertService.error("Could not add service");
                    }
                };
            }
        ]
    );
})(angular);