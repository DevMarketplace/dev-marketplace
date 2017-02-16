(function ($) {
    var operations = {
        init: function () {
            if (System != null) {
                System.import("app").catch(function (err) { console.error(err); });
            }

            operations.setAccountMenu();
        },

        setAccountMenu: function() {
            if ($(".hide-on-med-and-down").is(":visible")) {
                if ($("account-menu").length > 0) {
                    $("#nav-desktop").append($("account-menu"));
                    return;
                }
                $("#nav-desktop").append($("#account-menu-item"));
            } else {
                if ($("account-menu").length > 0) {
                    $("#nav-mobile").append($("account-menu"));
                    return;
                }
                $("#nav-mobile").append($("#account-menu-item"));
            }
        },

        documentReadySetup: function () {
            if ($.fn.sideNav !== "undefined") {
                $(".button-collapse").sideNav({ menuWidth: 320 });
            }

            if ($.fn.material_select !== "undefined") {
                $("select").material_select();
            }

            $(window).on("resize orientationchange", function () {
                operations.setAccountMenu();
            });

            $(window).on("scroll", function () {
                if ($(this).scrollTop() > $(".jumbotron").height()) {
                    $(".navigation-bar").addClass("navbar-fixed");
                    $(".navigation-bar > nav").css({ top: 0 });
                } else {
                    $(".navigation-bar > nav").css({ top: $(".jumbotron").height() });
                    $(".navigation-bar").removeClass("navbar-fixed");
                }
            });
        }
    };

    operations.init();
    $(document).ready(operations.documentReadySetup);
})(jQuery);
