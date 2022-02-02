
var connectionhubid;
$(function () {
    if ($("#JsModuleID").length != "0" && $("#JsModuleSectionID").length != "0") {
        UpdateModuleMessageRead();
        GetModuleSectionChatMessage();
    }
    GetUnReadChatCount();
    debugger;
    // $.support.cors = true;
    //Set the hubs URL for the connection
    // $.connection.hub.logging = true;
   /* $.connection.hub.url = "https://platform.buildmyunicorn.com/signalr";*/
    $.connection.hub.url = "http://localhost:50342/signalr";
    $.connection.hub.qs = { 'accparam': $(".jsStartupAcceleratorID").val() };
    // $.connection.hub.start();
    //  $.connection.hub.logging = true;
    //var chat = $.connection.hub.proxies.ChatHubClient
    // Declare a proxy to reference the hub.
    debugger;
    var chat = $.connection.chatHubClient;

    //Create a function that the hub can call to broadcast messages.
    chat.client.addChatMessage = function (value) {
        var obj = JSON.parse(value);
        var ModuleID = obj.Module;
        var ModuleSectionID = obj.ModuleSection;
        var isChatDetailPage = $("#ChatDetail").length;
        if (ModuleID == null || ModuleSectionID == null) {
           // alert("I am General Chat");
        }
        //else if (isChatDetailPage == "0" && ModuleID == "" && ModuleSectionID == "")
        //{
        //    if (!IschatPage) {
        //        if (!$('#ChatDialogModal').hasClass('show')) {
        //            $("#ChatDialogModal").modal('show');
        //            //$("ul").find(`[data-slide='${current}']`)
        //            $("a." + obj.ChatSenderID).trigger("click");
        //            return false;
        //        }
        //    }
        //}
        else if (isChatDetailPage == "0" && ModuleID != null && ModuleSectionID != null) {
            //alert("I am Module Chat and add notification");
            $("a.jsNotification").removeClass("display-none");

        }
        else if (isChatDetailPage == "1" && $.trim($("#JsModuleID").val()) != ModuleID && $.trim($("#JsModuleSectionID").val()) != ModuleSectionID) {
            $("a.jsNotification").removeClass("display-none");

            // alert("I am Module Chat and add notification");
        }
        else if (isChatDetailPage == "1" && $.trim($("#JsModuleID").val()) == ModuleID && $.trim($("#JsModuleSectionID").val()) != ModuleSectionID) {
            $("a.jsNotification").removeClass("display-none");

            // alert("I am Module Chat and add notification");
        }
        else if (isChatDetailPage == "1" && $.trim($("#JsModuleID").val()) != ModuleID && $.trim($("#JsModuleSectionID").val()) == ModuleSectionID) {

            /* alert("I am Module Chat and add notification");*/
            $("a.jsNotification").removeClass("display-none");
        }
        else if (isChatDetailPage == "1" && $.trim($("#JsModuleID").val()) == ModuleID && $.trim($("#JsModuleSectionID").val()) == ModuleSectionID) {
            $(".jsChatContentContainer").append('<li class=""> <div class="chat-img"><img src="../assets/images/users/3.jpg" alt="user"></div> <div class="chat-content">  <h5>' + obj.SenderName + '</h5> <div class="box bg-light-inverse">' + obj.MessageText + '</div>  <div class="chat-time">11:00 am</div> </div> </li>');
            $('.jschatscroll').scrollTop($('.jschatscroll')[0].scrollHeight);
            UpdateChatMessageRead(obj);

        }
       
    };
   

    $.connection.hub.start().done(function () {
        connectionhubid = $.connection.hub.id;
        $(".jsSendChat").click(function () {
            var chatMessage = $(".jsChatMessage").val();
            $("#MessageText").val(chatMessage);
            if (chatMessage != "") {

                $(".jsChatContentContainer").append('<li class="reverse">  <div class="chat-content"> <h5>' + $("#SenderName").val() + '</h5> <div class="box bg-light-inverse">' + chatMessage + '</div> <div class="chat-img"><img src="../assets/images/users/3.jpg" alt="user"></div> <div class="chat-time">11:00 am</div> </div> </li>');
                $('.jschatscroll').scrollTop($('.jschatscroll')[0].scrollHeight);
                $(".jsChatMessage").val("");
                var ChatSenderID = $.trim($("#ChatSenderID").val());
                var ChatReceiverID = $.trim($("#ChatReceiverID").val());
                var MessageText = $.trim($("#MessageText").val());              
                var SenderName = $.trim($("#SenderName").val());
                var ModuleID = $.trim($("#JsModuleID").val());
                var ModuleSectionID = $.trim($("#JsModuleSectionID").val());

                chat.server.sendChatMessage(ChatReceiverID, MessageText, ChatSenderID, SenderName, ModuleID, ModuleSectionID);
            }


        });
       
    });

});


$("a.jsActiveClient").click(function () {
     PATCH = true;
    $(".jsActiveClient").removeClass("active");
    $(this).addClass("active");
    /* $("#ChatSenderID").val()*/
    $("#ReceiverName").val($(this).data("startupname"))
    $("#ChatReceiverID").val($(this).data("clientid"))
    /* $("#MessageText").val()*/
    $("#TopicID").val()
    GetChatMessage();

});

$("a.jsModuleSectionActiveClient").click(function () {
    PATCH = true;
    $(".jsActiveClient").removeClass("active");
    $(this).addClass("active");
    $("#ReceiverName").val($(this).data("startupname"))
    $("#ChatReceiverID").val($(this).data("clientid"))
    $("#TopicID").val()
    GetModuleSectionChatMessage();

});

function GetChatMessage() {
    var Model = {
        "ChatSenderID": $.trim($("#ChatSenderID").val()),
        "ChatReceiverID": $.trim($("#ChatReceiverID").val())


    }
    $(".jsChatContentContainer").html("");
    $.ajax({
        url: GetBaseURL() + "Chat/GetChatMessage",
        type: "POST",
        data: JSON.stringify(Model),
        contentType: "application/json",
        dataType: "json",
        success: function (chatlst) {
            if (chatlst.length != "0") {
                $.each(chatlst, function (key, val) {
                    // Chat Sender
                    if ($("#ChatReceiverID").val() == val.ChatReceiverID)
                        $(".jsChatContentContainer").append('<li class="reverse">  <div class="chat-content"> <h5>' + $("#SenderName").val() + '</h5> <div class="box bg-light-inverse">' + val.MessageText + '</div> <div class="chat-img"><img src="../assets/images/users/3.jpg" alt="user"></div> <div class="chat-time">11:00 am</div> </div> </li>');
                    else // Chat Receiver
                        $(".jsChatContentContainer").append('<li class=""> <div class="chat-img"><img src="../assets/images/users/3.jpg" alt="user"></div> <div class="chat-content">  <h5>' + $("#ReceiverName").val() + '</h5> <div class="box bg-light-inverse">' + val.MessageText + '</div>  <div class="chat-time">11:00 am</div> </div> </li>');
                    lastMessageDateTime = moment(val.MessageDateTime).format("YYYY-MM-DD HH:mm:ss.SSS");
                });

                $('.jschatscroll').scrollTop($('.jschatscroll')[0].scrollHeight);
            }
            else {
                lastMessageDateTime = "2022-01-27 01:00:00.000";
            }

            //$(".jsChatContentContainer").append('<li class="reverse">  <div class="chat-content"> <h5>Client2</h5> <div class="box bg-light-inverse">' + chatMessage + '</div> <div class="chat-img"><img src="../assets/images/users/3.jpg" alt="user"></div> <div class="chat-time">11:00 am</div> </div> </li>');
            //$('.jschatscroll').scrollTop($('.jschatscroll')[0].scrollHeight);
            //$(".jsChatMessage").val("");
        },
        error: function (chatlst) {


        }

    });

}


function GetModuleSectionChatMessage() {
    debugger;
    var Model = {
        "ChatSenderID": $.trim($("#ChatSenderID").val()),
        "ChatReceiverID": $.trim($("#ChatReceiverID").val()),
        "Module": $.trim($("#JsModuleID").val()),
        "ModuleSection": $.trim($("#JsModuleSectionID").val())
    }
    $(".jsChatContentContainer").html("");
    $.ajax({
        url: GetBaseURL() + "Chat/GetModuleSectionChatMessage",
        type: "POST",
        data: JSON.stringify(Model),
        contentType: "application/json",
        dataType: "json",
        success: function (chatlst) {
            if (chatlst.length != "0") {
                $.each(chatlst, function (key, val) {
                    // Chat Sender
                    if ($("#ChatReceiverID").val() == val.ChatReceiverID)
                        $(".jsChatContentContainer").append('<li class="reverse">  <div class="chat-content"> <h5>' + $("#SenderName").val() + '</h5> <div class="box bg-light-inverse">' + val.MessageText + '</div> <div class="chat-img"><img src="../assets/images/users/3.jpg" alt="user"></div> <div class="chat-time">11:00 am</div> </div> </li>');
                    else // Chat Receiver
                        $(".jsChatContentContainer").append('<li class=""> <div class="chat-img"><img src="../assets/images/users/3.jpg" alt="user"></div> <div class="chat-content">  <h5>' + $("#ReceiverName").val() + '</h5> <div class="box bg-light-inverse">' + val.MessageText + '</div>  <div class="chat-time">11:00 am</div> </div> </li>');
                    lastMessageDateTime = moment(val.MessageDateTime).format("YYYY-MM-DD HH:mm:ss.SSS");
                });

                $('.jschatscroll').scrollTop($('.jschatscroll')[0].scrollHeight);
            }
            else {
                lastMessageDateTime = "2022-01-27 01:00:00.000";
            }


        },
        error: function (chatlst) {


        }

    });

}

function UpdateChatMessageRead(obj) {
   
    $.ajax({
        url: GetBaseURL() + "Chat/UpdateChatMessageRead",
        type: "POST",
        data: JSON.stringify(obj),
        contentType: "application/json",
        dataType: "json",
        success: function (response) {



        },
        error: function (chatlst) {


        }

    });

}

function UpdateModuleMessageRead() {

    // if ($("#JsModuleID").lenght != "0" && $("#JsModuleSectionID").lenght != "0") {
    var obj = {
        "Module": $.trim($("#JsModuleID").val()),
        "ModuleSection": $.trim($("#JsModuleSectionID").val()),
        "ChatSenderID": $.trim($("#ChatSenderID").val()),
        "ChatReceiverID": $.trim($("#ChatReceiverID").val())
    }

    $.ajax({
        url: GetBaseURL() + "Chat/UpdateModuleMessageRead",
        type: "POST",
        data: JSON.stringify(obj),
        contentType: "application/json",
        dataType: "json",
        success: function (response) {



        },
        error: function (chatlst) {


        }

    });
}