(function () {

    app.controller("ProcessType.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.processTypeFields = [];
        $scope.processTypeDocuments = [];
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

        $scope.emptyDocument = {
            name: null,
            isRequired: false
        }

        $scope.events = {
            onInit: onInit,
            newField: newField,
            newDocument: newDocument,
            deleteDocument: deleteDocument,
            deleteField: deleteField,
            onSaveClicked: onSaveClicked
        };

        function onInit(processType) {
            $scope.processType = processType;
        }

        function onSaveClicked() {
            $scope.processType.documents = $scope.processTypeDocuments;
            $scope.processType.fields = $scope.processTypeFields;
            return $http.post("/Admin/ProcessTypes/Edit", $scope.processType)
                .then(function (response) {
                    $.notify("Los datos se actualizaron exitosamente", "success");
                }, Utils.onAjaxError.bind(this, " al guardar el tipo de documento"));
        }

        function newField() {
            $scope.processTypeFields = $scope.processTypeFields.concat(angular.copy($scope.emptyProcessTypeField));
        }

        function newDocument() {
            $scope.processTypeDocuments = $scope.processTypeDocuments.concat(angular.copy($scope.emptyProcessTypeDocument));
        }

        function deleteField(field) {
            deleteItemFromArr($scope.processTypeFields, field);
        }

        function deleteDocument(document) {
            deleteItemFromArr($scope.processTypeDocuments, document);
        }

        function deleteItemFromArr(arr, item) {
            arr.splice(arr.indexOf(item), 1);
        }
    }

})();