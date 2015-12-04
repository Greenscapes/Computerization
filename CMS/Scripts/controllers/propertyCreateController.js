function PropertyCreateController($scope, $resource, $location, $http) {
    var propertiesResource = $resource('/api/properties');

    $scope.property = {};
    $http.get('/api/properties/getNextReference')
                .success(function (data) {
                    $scope.property.PropertyRefNumber = data;
                });

    $scope.property.State = "FL";
    $scope.save = function(property) {
        $scope.buttonsDisabled = true;
       
        propertiesResource.save(property, function () {
           
            $scope.buttonsDisabled = false;
                $scope.back();
            },
            function(error) {
                _showValidationErrors($scope, error);
                $scope.buttonsDisabled = false;
            },

            function () {
                $scope.buttonsDisabled = false;
            }
            );
    };

    $scope.back = function() {
        $location.path("/properties");
        if (!$scope.$$phase) $scope.$apply();
    };

    function _showValidationErrors($scope,error)
    {
        $scope.validationErrors = [];
        if (error.data && angular.isObject(error.data)) {
            //for (var key in error.data) {
            //    $scope.validationErrors.push(error.data[key]);
            //}
        }
        else {
            $scope.validationErrors.push("Could not add property");

        }
    };
    
    $(document).ready(function () {
        $('#datetimepicker').datepicker({
        }

        );
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();
        var year = d.getFullYear();

        var output =
        (month < 10 ? '0' : '') + month + '/' +
        (day < 10 ? '0' : '') + day + '/' + year;

        $("#datetimepicker").val(output);

        $('#datetimepicker').on('changeDate', function (ev) {
            ('#datetimepicker').valueOf(ev.target.value);
            $scope.property.ContractDate = ev.target.value;
        });
    });
    }


PropertyCreateController.$inject = ['$scope', '$resource', '$location','$http'];
app.controller('PropertyCreateController', PropertyCreateController);