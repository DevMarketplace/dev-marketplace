"use strict";
var __extends = undefined && undefined.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() {
        this.constructor = d;
    }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var av_ts_1 = require("av-ts");
var AccountMenu = (function (_super) {
    __extends(AccountMenu, _super);
    function AccountMenu(currentUser) {
        _super.call(this, {
            el: "#account-menu"
        });
        this.currentUser = currentUser;
    }
    return AccountMenu;
})(av_ts_1.Vue);
//# sourceMappingURL=accountMenu.js.map

