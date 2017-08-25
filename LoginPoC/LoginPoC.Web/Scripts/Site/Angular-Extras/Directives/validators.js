(function () {

    window.directives.directive("validate", function () {

        function isFunction(functionToCheck) {
            var getType = {};
            return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
        }        

        return {
            restrict: 'A',
            require: 'ngModel',
            scope: {
                validate: '&'
            },
            link: function (scope, ele, attrs, ngModel) {

                var options = scope.validate()
                var validators = {};
                if (isFunction(options)) {
                    var a = {}
                    a[options.name] = options;    
                    options = a;
                }

                for (var prop in options) {
                    if (options.hasOwnProperty(prop) && isFunction(options[prop])) {
                        ngModel.$validators[prop] = options[prop];
                    }
                }

            }
        }
    });

})();


