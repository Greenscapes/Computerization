function DeleteConfirmModalController($scope, $modalInstance, items) {
    $scope.ItemType = items[0];
    $scope.ItemDescriptor = items[1];
    $scope.delete = function () {
        items[3](items[2]);
        $modalInstance.close();
    }

    $scope.cancelDelete = function () {
        $modalInstance.dismiss('cancel');
    };
};

DeleteConfirmModalController.$inject = ['$scope', '$modalInstance', 'items'];
app.controller('DeleteConfirmModalController', DeleteConfirmModalController);