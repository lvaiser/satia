(function () {

    app.controller("User.MyAccount", ["$scope", "$http", controller]);

    function controller($scope, $http) {

        var empty = {
            enum: { key: 0, value: "-" },
            idCero: { id: 0, name: "-" },
            culture: { id: null, displayName: "-"}
        };

        $scope.form = {};
        $scope.form.data = {

            genders: [empty.enum],
            maritalStatuses: [empty.enum],
            countries: [empty.idCero],
            cultures: [empty.culture],

            datePickerOptions: {
                "singleDatePicker": true,
                "autoApply": true,
                "locale": {
                    "format": "DD/MM/YYYY",
                    "separator": " - ",
                    "fromLabel": "Desde",
                    "toLabel": "Hasta",
                    "daysOfWeek": [
                        "Dom",
                        "Lun",
                        "Mar",
                        "Mie",
                        "Jue",
                        "Vie",
                        "Sab"
                    ],
                    "monthNames": [
                        "Enero",
                        "Febrero",
                        "Marzo",
                        "Abril",
                        "Mayo",
                        "Junio",
                        "Julio",
                        "Agosto",
                        "Septiembre",
                        "Octubre",
                        "Noviembre",
                        "Diciembre"
                    ],
                    "firstDay": 1
                }
            }
        }

        $scope.user = {
            id: null
        };

        $scope.events = {
            onInit: onInit,
            onSaveClicked: onSaveClicked
        };

        function loadCountries()
        {
            return $http.get("/Common/Country/ListAll")
                        .then(function (response) {
                            response.data.unshift(empty.idCero);
                            $scope.form.data.countries = response.data;
                            return response.data;
                        }, Utils.onAjaxError.bind(this, " al intentar cargar la lista de paises"));
        }
        function loadCultures() {

            return $http.get("/Common/Culture/ListAll")
                        .then(function (response) {
                            response.data.unshift(empty.culture);
                            $scope.form.data.cultures = response.data;
                            return response.data;
                        }, Utils.onAjaxError.bind(this, " al intentar cargar la lista de culturas"));
        }

        function onInit(user, genders, maritalStatuses) {
            $scope.user = user;
            $scope.user.birthDate = user.birthDate || "";

            genders.unshift(empty.enum);
            maritalStatuses.unshift(empty.enum);

            $scope.form.data.genders = genders;
            $scope.form.data.maritalStatuses = maritalStatuses;

            loadCountries();
            loadCultures();
        }
        function onSaveClicked()
        {
            return $http.post("/Common/User/SaveProfile", $scope.user)
                        .then(function (response) {
                            $.notify("Los datos se actualizaron exitosamente", "success");
                        }, Utils.onAjaxError.bind(this, " al intentar cargar la lista de paises"));
        }
    }

})();