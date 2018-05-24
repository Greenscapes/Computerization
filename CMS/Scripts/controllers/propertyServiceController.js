(function (angular) {
    'use strict';

    var controllerId = 'PropertyServiceController';

    angular.module('cmsApp').controller(controllerId,
        [
            'servicesService', 'alertService', '$location', '$routeParams',
            function (servicesService, alertService, $location, $routeParams) {

                var vm = this;

                vm.propertyService = {};
                vm.services = [];
                vm.buttonsDisabled = false;
                vm.isUpdate = false;
                vm.propertyId = $routeParams.propertyId;

                vm.save = save;
                vm.back = back;

                activate();

                function activate() {
                    var serviceId = $routeParams.serviceId;
                    if (serviceId) {
                        servicesService.getPropertyService(serviceId)
                            .then(function (data) {
                                vm.isUpdate = true;
                                vm.propertyService = data;
                            });
                    }

                    servicesService.getServices().then(function (data) {
                        vm.services = data;
                    });
                }

                function save() {
                    vm.buttonsDisabled = true;

                    if (vm.isUpdate) {
                        servicesService.updatePropertyService(vm.propertyService).then(function () {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function (error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    } else {
                        vm.propertyService.PropertyId = vm.propertyId;
                        servicesService.createPropertyService(vm.propertyService).then(function () {
                            vm.buttonsDisabled = false;
                            vm.back();
                        }, function (error) {
                            _showValidationErrors(error);
                            vm.buttonsDisabled = false;
                        });
                    }
                };

                function back() {
                    $location.path("/properties/" + vm.propertyId);
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