(function () {

    window.directives.directive("ngFile", [function () {
        return {
            restrict: 'A',
            scope: {
                ngFile: "="
            },
            link: function (scope, element, attributes) {
                element.bind("change", function (changeEvent) {
                    scope.$apply(function () {
                        scope.ngFile = changeEvent.target.files[0];
                    });
                });
            }
        }
    }]);

})();
