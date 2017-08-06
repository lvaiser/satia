(function () {

    app.controller("User.MyAccount", ["$scope", "$http", controller]);

    function controller($scope, $http) {

        var empty = {
            enum: { key: 0, value: "-" },
            idCero: { id: 0, name: "-" }
        };

        $scope.form = {};
        $scope.form.data = {

            genders: [empty.enum],
            maritialStatuses: [empty.enum],
            countries: [empty.idCero],

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
            onInit: onInit
        };

        function loadCountries() {

            return $http.get("/Common/Country/ListAll")
                        .then(function (response) {
                            response.data.unshift(empty.idCero);
                            $scope.form.data.countries = response.data;
                            return response.data;
                        }, Utils.onAjaxError.bind(this, " al intentar cargar la lista de paises"));
        }

        function onInit(user, genders, maritialStatuses) {
            $scope.user = user;

            genders.unshift(empty.enum);
            maritialStatuses.unshift(empty.enum);

            $scope.form.data.genders = genders;
            $scope.form.data.maritialStatuses = maritialStatuses;

            loadCountries();
        }
    }

})();