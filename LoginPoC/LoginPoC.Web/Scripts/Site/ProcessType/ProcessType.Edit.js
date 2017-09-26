(function () {

    app.controller("ProcessType.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {
        $scope.processTypeFields = [];
        $scope.processTypeDocuments = [];
 
        $scope.emptyProcessTypeField = {
            name: null,
            type: 1,
            isRequired: false,
        }

        $scope.emptyProcessTypeDocument = {
            name: null,
            isRequired: false
        }

        $scope.emptyProcessType = {
            name: null,
            description: null,
            urlVideo: null
        }

        $scope.events = {
            onInit: onInit,
            newField: newField,
            newDocument: newDocument,
            deleteDocument: deleteDocument,
            deleteField: deleteField,
            onSaveClicked: onSaveClicked
        };

        function onInit(processType, fieldTypes) {
            $scope.fieldTypes = fieldTypes;
            if (processType) {
                $scope.processType = processType;
                $scope.processTypeFields = processType.fields;
                $scope.processTypeDocuments = processType.documents;
            } else {
                $scope.processType = $scope.emptyProcessType
            }
        }

        function onSaveClicked() {
            $scope.processType.documents = $scope.processTypeDocuments;
            $scope.processType.fields = $scope.processTypeFields;

            var action;
            if (!$scope.processType.id) {
                action = "Create";
            } else {
                action = "Edit";
            }

            return $http.post("/Admin/ProcessType/" + action, $scope.processType)
                        .then(function (response) {
                            window.location = '/Admin/ProcessType/Edit/' + response.data.id + '?_=' + Math.random();
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