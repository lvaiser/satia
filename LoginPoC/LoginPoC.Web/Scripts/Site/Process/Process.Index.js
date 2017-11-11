(function () {

    app.controller("Process.Index", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.events = {
            onInit: onInit
        };

        $scope.filtered = {};

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

        $scope.getStatusLabel = function(status)
        {
            if (status == "Submitted")
                return "Enviado";

            if (status == "Draft")
                return "Borrador";

            if (status == "Approved")
                return "Aprobado";

            if (status == "Rejected")
                return "Rechazado";
        
            if (status == "Archived")
                return "Archivado";
            return "";
        }
    }

})();