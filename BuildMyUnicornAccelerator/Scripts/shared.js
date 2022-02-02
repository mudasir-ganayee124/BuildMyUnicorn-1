

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
 

});


function GetUnReadChat() {
    $.ajax({
        url: GetBaseURL() + "Chat/GetUnReadChat",
        dataType: "html",
        success: function (chatlst) {
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