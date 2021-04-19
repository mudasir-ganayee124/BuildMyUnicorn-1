
    $("li a, li, li ul").removeClass("active");
    $("ul").removeClass("in");
    $("#ulManage").addClass("in");
    $("#ulManage_Supplier").addClass("in");
    $("#liManagement").addClass("active");
    $("#liManage_Supplier").addClass("active");
    $("#li_Supplier").addClass("active");
    $('input[type="checkbox"]').click(function () {
           
            if ($(this).prop("checked") == true) {
                $(this).prop("checked", true);
                $(this).attr('checked', 'checked');
           $(this).attr('checked', 'checked');
            }
            else if ($(this).prop("checked") == false) {
                $(this).prop("checked", false);
                $(this).removeAttr('checked', 'checked');
              }
           
        });

    $(document).on("click", ".jsUpdateAccountManager", function() {
       
       var element = $(this).closest("div.jsAccountManagerContainer");
       var tableRow = element.find("tr.active");
       var Model = [];
       $(tableRow).each(function() {
          
          Model.push({ "ID": $(this).find("input.jsID").val(), "EntityID": $(this).find("input.jsEntityID").val(), "Comment": $(this).find("textarea.jsComment").val(), "IsApproved": $(this).find(".IsApproved").attr("checked") ? 1 : 0 });
         
        });
        $.ajax({
            url: GetBaseURL() + "ManageSupplier/UpdateAccountManager",
            type: "POST",
            data: JSON.stringify({ Model: Model}),
            contentType: "application/json",
            dataType: "json",
            error: function (response) {
                 $.fn.successMsg("Account updated successfully");
            }, 
            success: function (response) {
                alert(response);
            }
        });

   
    });