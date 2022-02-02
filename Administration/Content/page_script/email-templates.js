

$('#EmailTemplateBody').wysihtml5({
    "font-styles": true, //Font styling, e.g. h1, h2, etc.
    "emphasis": true, //Italics, bold, etc.
    "lists": true, //(Un)ordered lists, e.g. Bullets, Numbers.
    "html": true, //Button which allows you to edit the generated HTML.
    "link": true, //Button to insert a link.
    "image": true, //Button to insert an image.
    "color": true //Button to change color of font
});

$('input[type="checkbox"]').click(function () {

    if ($(this).prop("checked") == true) {
        $(this).prop("checked", true);
        $(this).val(true);
        $(this).attr('checked', 'checked');
    }
    else if ($(this).prop("checked") == false) {
        $(this).prop("checked", false);
        $(this).removeAttr('checked', 'checked');
        $(this).val(false);
    }
});

$(document).on("change", "#EmailTemplateID", function () {

    if ($(this).val() == "") { $("#divtemplatedetail").css("display", "none"); return false;}
    $.ajax({
        url: GetBaseURL() + "EmailTemplates/GetEmailTemplateData",
        method: "GET",
        data: { EmailTemplateID: $(this).val() },
        dataType: 'json',
        success: function (data) {
  
            $("#divtemplatedetail").css("display", "block");
            $("#EditEmailTemplateID").val(data.emailtemplate.EmailTemplateID);
            $("#EmailTemplateSubject").val(data.emailtemplate.EmailTemplateSubject);
            $('#EmailTemplateBody').data("wysihtml5").editor.setValue(data.emailtemplate.EmailTemplateBody);
            if (data.emailtemplate.IsActive == true) {
                $("#IsActive").val(true);
                $("#IsActive").prop('checked', true).attr("checked", "checked");
            }
            else {
                $("#IsActive").val(false);
                $("#IsActive").prop('checked', false).removeAttr("checked", "checked");
            }
            $("#UpdatedBy").val(data.emailtemplate.ModifiedName);
            $("#ModifiedDateTime").val(moment(data.emailtemplate.ModifiedDateTime).format('MM/DD/YYYY'));
         
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });

});

$("#frmEmailTemplate").submit(function (e) {
 
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "EmailTemplates/UpdateEmailTemplate",
        method: "POST",
        data: $('#frmEmailTemplate').serialize(),
        success: function (response) {
            if (response === "OK") {
                   swal("Success!", "Template Updated Successfully", "success");
           
            }
            else {
                swal("Error!", response, "error");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
});