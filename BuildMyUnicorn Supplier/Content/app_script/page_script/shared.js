// *Plugin for Getting List*
(function ($) {

    $.fn.getList = function () {
        var element = $(this);
        if (typeof (element) != 'undefined' || element != null) {
            var controller = $(element).data('controller');
            var action = $(element).data('action');
        }
        var url = GetBaseURL() + controller + "/" + action;
        $.ajax({
            type: "GET",
            url: url,
            dataType: "html",
            success: function (r) {
                $(element).html(r);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("Status: " + textStatus); console.log("Error: " + errorThrown);
            }
        });
    }

}(jQuery));

$.fn.extend({
    "ajaxCall": function (options) {

        var settings = $.extend({
            contentType: "application/json; charset=utf-8",
            type: 'POST',
            dataType: 'json',
            controller: '',
            action: '',
            data: null
        }, options);

        return $.ajax({
            //contentType: settings.contentType,
            type: settings.type,
            url: GetBaseURL() + settings.controller + "/" + settings.action,
            dataType: settings.dataType,
            data: settings.data,
            success: function (response) {
                settings.response = response;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 403)
                    $.fn.errorMsg("Sorry, your session has expired. Please login again to continue");
                else
                    $.fn.errorMsg("An error occurred: " + errorThrown + " Status: " + XMLHttpRequest.status);


            }
        });
    },

    "successMsg": function (msg) {

        swal("Success!", msg, "success");




    },

    "errorMsg": function (msg) {
        swal("Error!", msg, "error");
    },

    "chkSubmitStatus": function (form) {
        var returnValue = false;
        $(form).find('input:not(".jsIgnoreOrgVal")').each(function () {
            if (String($(this).data("orgvale")) != String($(this).val())) {
                returnValue = true;
                return false;
            }
        });
        return returnValue;
    },

    "closeModal": function () {
        $(".JsClose").trigger("click");
        return false;

    },


});

$('input[type="checkbox"]:not(.ignore)').click(function () {
  
    if ($(this).prop("checked") == true) {
        $(this).prop("checked", true);
        $(this).attr('checked', 'checked');
        $(this).val("true");
        
    }
    else if ($(this).prop("checked") == false) {
        $(this).prop("checked", false);
        $(this).removeAttr('checked', 'checked');
        $(this).val("false");
       

    }
    
});
