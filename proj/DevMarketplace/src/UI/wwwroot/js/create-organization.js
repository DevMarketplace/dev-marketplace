(function ($) {
    var operations = {
        init: function () {
            $("#create-organization").on("click", function (e) {
                e.preventDefault();
                operations.openCreateOrganizationModal();
            });

            $("#create-organization-modal").modal({
                dismissible: false,
                opacity: .5,
                in_duration: 200,
                out_duration: 150,
                starting_top: '5%',
                ending_top: '5%'
            });

            $("#create-organization-modal .close-button").on("click", function (e) {
                e.preventDefault();
                $("#create-organization-modal").modal("close");
            });
        },

        openCreateOrganizationModal: function () {
            $("#create-organization-modal").modal("open");
        }
    }

    operations.init();
})(jQuery);