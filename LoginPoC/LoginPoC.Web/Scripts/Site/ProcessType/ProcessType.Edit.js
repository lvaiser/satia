(function () {

    app.controller("ProcessType.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.processTypeFields = [];
        // TODO request field types by http
        $scope.fieldTypes = ['integer', 'date', 'phoneNumber', 'text'];
 
        const emptyProcessTypeField = {
            name: null,
            type: null,
            isRequired: false,
            fieldType: $scope.fieldTypes[0]
        }

        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked
        };

        function onInit(processType) {
            $scope.processType = processType;
        }

        function onSaveClicked() {
        }

        function newField() {
            $scope.processTypeFields = $scope.processTypeFields.concat(emtpyProccesTypeField);
        }
    }

})();