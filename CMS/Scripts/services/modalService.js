app.factory("Modal", [
    '$modal', function($modal) {
        var modalService = {
            showConfirmDelete: function(itemType, itemDescriptor, item, deleteFunction) {
                return $modal.open({
                    animation: true,
                    templateUrl: "templates/modals/modal-delete-confirm.html",
                    controller: "DeleteConfirmModalController",
                    size: "sm",
                    resolve: {
                        items: function() {
                            return [itemType, itemDescriptor, item, deleteFunction];
                        }
                    }
                });
            }
        };

        return modalService;
    }
]);