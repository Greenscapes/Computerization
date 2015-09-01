app.factory("Authentication", [
    '$rootScope', '$resource', function ($rootScope, $resource) {
        var authenticationService = {
            getCurrentUser: function() {
                if ($rootScope.currentUser) {
                    return $rootScope.currentUser;
                }

                var currentUserResource = $resource('/api/users/current');
                $rootScope.currentUser = currentUserResource.get();

                return $rootScope.currentUser;
            }
        };

        return authenticationService;
    }
]);