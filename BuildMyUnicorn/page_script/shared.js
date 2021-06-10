var switchery = new Switchery($('.jsThemeswitch')[0],
    {
        color: '#64bd63',
        secondaryColor: '#dfdfdf',
        jackColor: '#fff',
        jackSecondaryColor: '#64bd63'
     
    });


$(document).ready(function () {
    $('#scroll-sidebar').perfectScrollbar();
  $("textarea").each(function () {
        this.style.height = "5px";
        this.style.height = (this.scrollHeight) + "px";
  });
 

    if (localStorage.getItem("ClientThemeMode") === null) localStorage.setItem('ClientThemeMode', 'darkMode');
    changeTheme(localStorage.getItem('ClientThemeMode'));

    var changeCheckbox = document.querySelector('.jsThemeswitch');

    changeCheckbox.onchange = function () {
      
        if (changeCheckbox.checked === false) 
            changeTheme('darkMode');
            else 
            changeTheme('lightMode');
        };
});

$(document).on("click", "div#DialogModuleVideo div.close", function() {
$('iframe').attr('src', $('iframe').attr('src'));
});

$(document).ready(function () {

            /* Get iframe src attribute value i.e. YouTube video url
            and store it in a variable */
            var url = $("#ModuleVideo").attr('src');


            /* Assign empty url value to the iframe src attribute when
            modal hide, which stop the video playing */
            $("#DialogModuleVideo").on('hide.bs.modal', function () {
                $("#ModuleVideo").attr('src', '');
            });

            /* Assign the initially stored url back to the iframe src
            attribute when modal is displayed again */
            $("#DialogModuleVideo").on('show.bs.modal', function () {
                $("#ModuleVideo").attr('src', url);
            });


        });
$(document).on("click", "div#myDialog3 .close", function() {

 var src = $('iframe').attr('src');
 $('iframe').attr('src', src);

});
$('#frmPasswordChange').parsley();
//$(".parsley-required").css("color","red !important")

$("#frmPasswordChange").submit(function (event) {
    event.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Login/ChangePassword",
        method: "POST",
        data: $('#frmPasswordChange').serialize(),
        success: function (response) {

            if (response === "OK") {
                CommonFunctions.SuccessMessage("Success", "Password Changed Successfully");
                $("#frmPasswordChange")[0].reset();
                $("#changePasswordModal").modal('hide');
            }
            else {
                CommonFunctions.ErrorMessage("Failed", response);
            }
           

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });  

});



function auto_grow_idea_textarea(element) {

    element.style.height = "5px";
    element.style.height = (element.scrollHeight) + "px";
}

function changeTheme(target) {
    var element = $('#jsThemeswitch');
    $("#jsThemeLink").remove();
    var cssLink = document.createElement('link');
    cssLink.rel = 'stylesheet';
    cssLink.id = "jsThemeLink";
    cssLink.type = 'text/css';
   
    var isIEBrowser = navigator.userAgent.indexOf("MSIE") > -1 || navigator.userAgent.indexOf("rv:") > -1;
    switch (target) {
        case "darkMode":
           
            cssLink.href = GetBaseURL() + 'dist/theme-mode/style-dark.min.css';
            $("body").removeClass("skin-default").addClass("skin-default-dark");
            localStorage.setItem('ClientThemeMode', 'darkMode');
            setSwitchery(switchery, false);
           break;

        case "lightMode":
        
            cssLink.href = GetBaseURL() + 'dist/theme-mode/style-light.min.css';     
            $("body").removeClass("skin-default-dark").addClass("skin-default");
            localStorage.setItem('ClientThemeMode', 'lightMode');
          
            setSwitchery(switchery, true);
            break;


        default:
    }
    $("head").append(cssLink);
}

function setSwitchery(switchElement, checkedBool) {
    if ((checkedBool && !switchElement.isChecked()) || (!checkedBool && switchElement.isChecked())) {
        switchElement.setPosition(true);
        switchElement.handleOnchange(true);
    }
}
if (typeof $("#jsonModuleVideo").val() !== "undefined") {
    var _videoModel = JSON.parse($("#jsonModuleVideo").val());
    $("div.jsModuleVideo").data();
        $("#DialogModuleVideo").myOwnDialog(
            {
                autoClose: false,
                width: "400",
                pos_y: "50",
                height: "300",
                bg_color: 'white',
                title: ''+_videoModel.Title+'',
                body_margin: ''+_videoModel.Duration+'',
                body_overflow_x: "hidden",
                body_overflow_y: "scroll",
                movable: true,
                resizable: true,
                touchOutsideForClose: false
            });
setTimeout(function () { $("#DialogModuleVideo").myOwnDialog("open"); }, 1000);

}
