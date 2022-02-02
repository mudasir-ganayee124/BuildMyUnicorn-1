var switchery = new Switchery($('.jsThemeswitch')[0],
    {
        color: '#64bd63',
        secondaryColor: '#dfdfdf',
        jackColor: '#fff',
        jackSecondaryColor: '#64bd63'

    });


$(document).ready(function () {
    $('#scroll-sidebar').perfectScrollbar();
    $("textarea:not(.ignore)").each(function () {
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
   
    //    $.jTimeout(
    //        {
    //            'flashTitle': true, //whether or not to flash the tab/title bar when about to timeout, or after timing out
    //            'flashTitleSpeed': 500, //how quickly to switch between the original title, and the warning text
    //            'flashingTitleText': 'Session Timeout Alert', //what to show in the tab/title bar when about to timeout, or after timing out
    //            'originalTitle': document.title, //store the original title of this page

    //            'tabID': false, //each tab needs a unique ID so you can tell which one last updated the timer - false makes it autogenerate one
    //            'timeoutAfter': 3000, //pass this from server side to be fully-dynamic. For PHP: ini_get('session.gc_maxlifetime'); - 1440 is generally the default timeout
    //            'heartbeat': 1, //how many seconds in between checking and updating the timer - warning: this will effect the speed of the countdown prior

    //            'extendOnMouseMove': true, //Whether or not to extend the session when the mouse is moved
    //            'mouseDebounce': 30, //How many seconds between extending the session when the mouse is moved (instead of extending a billion times within 5 seconds)
    //            'onMouseMove': false, //Override the standard $.get() request that uses the extendUrl with your own function.

    //            'extendUrl': GetBaseURL() + 'Login/HeartBeat', //URL to request in order to extend the session.
    //            'logoutUrl': GetBaseURL() + 'Login/Logout', //URL to request in order to force a logout after the timeout. This way you can end a session early based on a shorter timeout OR if the front-end timeout doesn't sync with the backend one perfectly, you don't look like an idiot.
    //            'loginUrl': GetBaseURL() + "Login", //URL to send a customer when they want to log back in

    //            'secondsPrior': 20


    //        }
    //    );
});



$(document).on("click", "div#DialogModuleVideo div.close", function () {
    $('iframe').attr('src', $('iframe').attr('src'));
});

$(document).on("click", ".jsQuestionVideo", function () {
    $(".titlebar_title").text("");
    $("#ModuleVideo").attr('src', $(this).data('videourl'));
    $("#DialogModuleVideo").myOwnDialog("open");
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

$(document).on("click", "div#myDialog3 .close", function () {

    var src = $('iframe').attr('src');
    $('iframe').attr('src', src);

});

$('#frmPasswordChange').parsley();
$('#frmPasswordChangeContributor').parsley();
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

$("#frmPasswordChangeContributor").submit(function (event) {
    event.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Login/ChangeContributorPassword",
        method: "POST",
        data: $('#frmPasswordChangeContributor').serialize(),
        success: function (response) {

            if (response === "OK") {
                CommonFunctions.SuccessMessage("Success", "Password Changed Successfully");
                $("#frmPasswordChangeContributor")[0].reset();
                $("#changePasswordContributorModal").modal('hide');
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
            title: '' + _videoModel.Title + '',
            body_margin: '' + _videoModel.Duration + '',
            body_overflow_x: "hidden",
            body_overflow_y: "scroll",
            movable: true,
            resizable: true,
            touchOutsideForClose: false
        });
    setTimeout(function () { $("#DialogModuleVideo").myOwnDialog("open"); }, 1000);

}

$(document).off('change.patch').on("change.patch", "form.patch textarea, form.patch input, form.patch checkbox, form.patch select", function () {
    PATCH = true;
    SaveFormData("PATCH")
});

$(document).on("click", ".jsNotification", function () {
    debugger;
    if ($("div.jsNotificationlist").hasClass("show")) {
      
        $("div.jsNotificationlist").removeClass("show")
    }
    else {
        $("div.jsNotificationlist").addClass("show")
        GetUnReadChat();
    }

});

$('body').click(function () {

    if ($("div.jsNotificationlist").hasClass("show")) {

        $("div.jsNotificationlist").removeClass("show")
    }
    //jsUnreadChatContainer

});


function GetUnReadChat() {
    $.ajax({
        url: GetBaseURL() + "Chat/GetUnReadChat",
        dataType: "html",
        success: function (chatlst)
        {
            $(".jsUnreadChatContainer").html(chatlst);
            $("div.jsNotificationlist").addClass("show");
        },
        error: function (chatlst) {
        }

    });

}

function GetUnReadChatCount() {
    $.ajax({
        url: GetBaseURL() + "Chat/GetUnreadChatCount",
        success: function (count) {
         
            if (count > 0) {
             
                $("a.jsNotification").removeClass("display-none");
            }
            else {
              
                $("a.jsNotification").addClass("display-none");
            }
        },
        error: function (response) {
        }

    });

}