(function (angular) {
    'use strict';

    var controllerId = 'ServiceController';

    angular.module('cmsApp').controller(controllerId,
        [
            'servicesService', 'alertService', '$location', '$routeParams', 'Modal',
            function (servicesService, alertService, $location, $routeParams, Modal) {

                var vm = this;

                vm.service = {};
                vm.buttonsDisabled = false;
                vm.isUpdate = false;

                vm.save = save;
                vm.back = back;
                vm.confirmDelete = confirmDelete;

                activate();

                function activate() {
                    var serviceId = $routeParams.serviceId;
                    if (serviceId) {
                        servicesService.getService(serviceId).then(function (data) {
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

                var deleteFunction = function (service) {
                    servicesService.deleteService(vm.service.Id)
                        .then(function() {
                            vm.back();
                        }, function(error) {
                            alert("Cannot delete this service since it is being used by a property");
                        });
                };

                function confirmDelete() {
                    Modal.showConfirmDelete("service", vm.service.Name, vm.service, deleteFunction);
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