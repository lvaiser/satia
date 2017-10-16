(function () {

    app.controller("Process.Index", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.events = {
            onInit: onInit
        };

        function onInit(model) {
            $scope.processes = model.processes;
            $scope.processTypes = model.processTypes;
            $scope.selectedProcessType = $scope.processTypes[0].id;
        }

        $scope.anyDeprecated = function () {
            var any = $scope.processes.some(function (process) {
                return !process.type.isActive;
            });
            return any;
        }
    }

})();