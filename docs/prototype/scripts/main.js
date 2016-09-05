(function() {
    $(document).ready(function() {
        $(".button-collapse").sideNav();

        $(window).on("scroll", function() {
            if($(this).scrollTop() > $(".jumbotron").height()) {
                $(".navigation-bar").addClass("navbar-fixed");
                $(".navigation-bar > nav").css({top: 0})
            } else {
                $(".navigation-bar > nav").css({top: $(".jumbotron").height()})
                $(".navigation-bar").removeClass("navbar-fixed");        
            }
        });
    });
})();