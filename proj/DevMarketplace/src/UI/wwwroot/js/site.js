(function ($) {
    var operations = {
        init: function () {
            //Init functions for different 3rd party libraries
        },

        documentReadySetup: function () {

            if ($.fn.sideNav !== 'undefined') {
                $(".button-collapse").sideNav();
            }

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
