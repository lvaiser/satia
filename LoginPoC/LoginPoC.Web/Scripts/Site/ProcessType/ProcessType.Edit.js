(function () {

    app.controller("ProcessType.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.processTypeFields = [];
        // TODO request field types by http
        $scope.fieldTypes = [
            { id: 1, name: 'integer' },
            { id: 2, name: 'date' },
            { id: 3, name: 'phoneNumber' },
            { id: 4, name: 'text' }
        ];
 
        $scope.emptyProcessTypeField = {
            name: null,
            type: null,
            isRequired: false,
            fieldType: $scope.fieldTypes[0]
        }

        $scope.events = {
            onInit: onInit,
            newField: newField,
            onSaveClicked: onSaveClicked
        };

        function onInit(processType) {
            $scope.processType = processType;
        }

        function onSaveClicked() {
        }

        function newField() {
            $scope.processTypeFields = $scope.processTypeFields.concat(angular.copy($scope.emptyProcessTypeField));
        }
    }

})();