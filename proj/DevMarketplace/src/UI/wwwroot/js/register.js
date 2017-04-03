(function ($) {
    $(document).on("organizationCreated", function (evt, orgInfo) {
        $("#CompanyId").append("<option value="+ orgInfo.companyId +">"+ orgInfo.name +"</option>");
    });
})(jQuery)