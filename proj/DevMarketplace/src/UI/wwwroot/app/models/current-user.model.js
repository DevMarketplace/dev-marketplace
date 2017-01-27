System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var CurrentUser;
    return {
        setters:[],
        execute: function() {
            //@injectable()
            CurrentUser = (function () {
                function CurrentUser() {
                }
                return CurrentUser;
            }());
            exports_1("CurrentUser", CurrentUser);
        }
    }
});
//# sourceMappingURL=current-user.model.js.map