(function ($) {
    $(document).on("organizationCreated", function (evt, orgInfo) {
        $("#companyId").append("<option value="+ orgInfo.companyId +">"+ orgInfo.name +"</option>");
    });
})(jQuery)