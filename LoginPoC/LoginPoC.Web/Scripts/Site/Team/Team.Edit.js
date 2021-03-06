(function () {

    app.controller("Team.Edit", ["$scope", "$http", controller]);

    function controller($scope, $http) {

        $scope.form = {};
        $scope.form.data = {
            selectedAgent: null
        }

        $scope.team = {
            id: null
        };

        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked,
            onSearchAgent: onSearchAgent,
            onSelectAgent: onSelectAgent,
            onRemoveAgentClicked: onRemoveAgentClicked
        };

        $scope.validators = {
            selectErrorMessage: selectErrorMessage
        };

        function onInit(team) {
            $scope.team = team;
        }
        function onSaveClicked()
        {
            if (!validate($scope.pageForm)) {
                $scope.pageForm.$setDirty();
                $.notify("Existen campos con valores inválidos. Corrija los errores y vuelva a intentarlo", "error");
                return;
            }

            var endpoint = $scope.team.id ? "Edit" : "Create";

            return $http.post("/Admin/Team/" + endpoint, $scope.team)
                        .then(function (response) {
                            $.notify("Los datos se actualizaron exitosamente", "success");
                            if (!$scope.team.id) {
                                window.location = "/Admin/Team";
                            }
                        }, function(e)
                        {
                            if (e.status == 400 || e.status == 409)
                            {
                                Utils.showModelErrorCollection(e.data);
                                return;
                            }

                            Utils.onAjaxError(" al guardar los datos del asesor", e);
                        });
        }
        function onSearchAgent(userInputString) {
            return $http.get('/Agent/Index?name=' + userInputString)
                        .then(function(res){

                            res.data.agents = res.data.agents.filter(function (a) {
                                return !$scope.team.users.some(function (u) { return u.id == a.id; });
                            });

                            return res;
                        });
        }
        function onSelectAgent(agent) {

            if (!agent || !agent.originalObject)
                return;

            $scope.team.users.push(agent.originalObject);
            $scope.form.data.selectedAgent = null;
        }
        function onRemoveAgentClicked(agent)
        {
            var index = $scope.team.users.indexOf(agent);
            $scope.team.users.splice(index, 1);
        }

        function validate(form) {
            angular.forEach(form, function (control, name) {
                // Excludes internal angular properties
                if (typeof name === 'string' && name.charAt(0) !== '$') {
                    // To display ngMessages
                    control.$setTouched();
                    control.$setDirty();

                    // Runs each of the registered validators
                    control.$validate();
                }
            });

            return form.$valid
        }
        function selectErrorMessage(control, messages) {
            if (!messages || !control)
                return;

            for (var prop in control.$error) {
                if (control.$error.hasOwnProperty(prop)) {
                    return messages[prop] || null;
                }
            }
        }

        $scope.$watch("form.data.selectedAgent", onSelectAgent)
    }
})();