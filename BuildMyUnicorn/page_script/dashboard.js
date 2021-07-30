﻿
$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Dashboardnav").addClass("active");
    GetSubscribedPackages();
    GetCountryGrant(0)
});
$("#frm_UpdateProfile").submit(function (e) {

    e.preventDefault();
    var RoleInCompany = [];
    $.each($("input[name='_RoleInCompany']:checked"), function () {
        RoleInCompany.push($(this).val());

    });


    $("#RoleInCompany").val(RoleInCompany.join(","));
    $.ajax({
        url: GetBaseURL() + "Team/UpdateClientProfile",
        method: "POST",
        data: $('#frm_UpdateProfile').serialize(),
        success: function (response) {
            location.reload();
        },
        error: function (response) {
        }
    });


});

 $("#ImageUpload").on('change', function () {
    
var form_Data = new FormData();
var fileUpload = $("#ImageUpload").get(0);ImageID
var files = fileUpload.files;  
if (files.length > 0) {
    for (var i = 0; i < files.length; i++) {
        var fileName = files[i].name;
        var flExt = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
        if ($.inArray(flExt.toLowerCase(), ['gif', 'png', 'jpg', 'bmp', 'jpeg']) !== -1)
            form_Data.append("file", files[i]);
        else { alert("Invalid File");return false; }
    }
}
     
        $.ajax({
            type: "POST",
            url: GetBaseURL() + 'Team/FileUpload',
            data: form_Data,
            cache: false,
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                if (data !== "!OK") {
                    $("#ImageID").val(data);
                    $("#profileImage").attr('src', GetBaseURL() + "Content/Images/" + data + "");
                    // $('#imagepreview').html("<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='../Content/file_upload/" + data + "' width='100%' class='img-responsives img-thumbnail' style='height:100%' /><div class='dz-error-mark'><span>X</span></div></div>");
                }
                //if (data != "!OK") {
                //    $("#ImageID").val(data);
                //    $('#imagepreview').html("<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='../Content/file_upload/" + data + "' width='100%' class='img-responsives img-thumbnail' style='height:100%' /><div class='dz-error-mark'><span>X</span></div></div>");
                //}

            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not Connected, Verify Network/Internet.';
                } else if (jqXHR.status === 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status === 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else if (jqXHR.status === 403) {
                    msg = 'Access Denied. Contact Your Administrator.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                sweetAlert("Error in Uploading Image!", msg, "error");
            }
        });

 });



$('input[type="checkbox"]').click(function () {
    var id = $(this).attr("id");
    var name = $(this).attr("name");
    if ($(this).prop("checked") == true) {
        $(this).prop("checked", true);
        $(this).attr('checked', 'checked');
    }
    else if ($(this).prop("checked") == false) {
        $(this).prop("checked", false);
        $(this).removeAttr('checked', 'checked');


    }


});


$(document).on("change",".jsGrantCountry", function() {
 GetCountryGrant($(this).val());
});

$(document).on("click", ".jsDownloadPackageInvoice", function () {
   
    window.location.replace(GetBaseURL() + "ThirdParty/GenerateInvoice/"+ $(this).attr("data-orderid"));
    

});

function GetCountryGrant(Month)
{
 
$.ajax({
        url: GetBaseURL() + "Dashboard/GetCountryGrant",
        type: "POST",
        data: JSON.stringify({ Month: Month }),
        contentType: "application/json",
        dataType: "html",
        success: function (html) {
             $("#_RenderCountryGrant").html(html);
        },
        error: function (html) {
         
               
        },
       
    });
}

function GetSubscribedPackages() {

    $.ajax({
        url: GetBaseURL() + "Dashboard/GetSubscribedPackages",
        type: "POST",
        dataType: "html",
        success: function (html) {
            $("#_RenderSubscribedPackages").html(html);
        },
        error: function (html) {


        },

    });
}