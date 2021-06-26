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

//  extend addClass() jquery's method to let it accept a callback function:

(function ($) {
    var oAddClass = $.fn.addClass;
    $.fn.addClass = function () {
        for (var i in arguments) {
            var arg = arguments[i];
            if (!!(arg && arg.constructor && arg.call && arg.apply)) {
                setTimeout(arg.bind(this));
                delete arguments[i];
            }
        }
        return oAddClass.apply(this, arguments);
    }

})(jQuery);

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

// *Plugin for Setting Checkbox Property*

(function ($) {

    $.fn.changeCheckbox = function () {

        var element = $(this);
        if (element.is(':checked')) {

            element.attr("checked", "checked").attr("checked", "checked");
        }
        else {
            element.removeAttr("checked", "checked").removeAttr("checked", "checked");
        }

    }

}(jQuery));