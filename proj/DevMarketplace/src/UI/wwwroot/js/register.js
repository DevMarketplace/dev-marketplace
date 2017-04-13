(function ($) {
    $(document).on("organizationCreated", function (evt, orgInfo) {
        $("#CompanyId option:selected").remove();
        $("#CompanyId").append("<option selected value="+ orgInfo.companyId +">"+ orgInfo.name +"</option>");
    });
})(jQuery)