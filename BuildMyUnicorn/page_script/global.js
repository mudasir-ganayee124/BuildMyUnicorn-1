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
            contentType: "application/json",
            type: 'POST',
            dataType: 'json',
            controller: '',
            action: '',
            data: null
        }, options);

        return $.ajax({
            contentType: settings.contentType,
            type: settings.type,
            url: GetBaseURL() + settings.controller + "/" + settings.action,
            dataType: settings.dataType,
            data: settings.data,
            success: function (response) {
                settings.response = response;
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
                $.fn.ErrorMessage('warning', msg);
             

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




var CommonFunctions = new function () {

    this.AjaxCall = function (MethodType, URL, Data, DataType, ErrorMessage, contentType) {
        var ReturnResult = "";
        $.ajax({
            type: MethodType,
            url: URL,
            data: Data,
            datatype: DataType,
            async: false,
            cache: false,
            contentType: contentType,

            success: function (result) {
                ReturnResult = result;
            },
            error: function (jqXHR, exception) {
                ReturnResult = "ERROR";
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
             
            }
        });
        return ReturnResult;
    };

    this.SuccessMessage = function (Title, Message) {
        $.toast({
            heading: Title,
            text: Message,
            position: 'top-right',
            loaderBg: '#ff6849',
            icon: "success",
            hideAfter: 1500,
            stack: 18
        });
        

    };

    this.ErrorMessage = function (Title, Message) {
        $.toast({
            heading: Title,
            text: Message,
            position: 'top-right',
            loaderBg: '#ff6849',
            icon: "error",
            hideAfter: 1500,
            stack: 18
        });

      

    };

  
   

    this.GetQueryStringValue = function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    };
};

           

$(document).ajaxSend(function (event, jqxhr, settings) {
    run_ajax_loader();

});

$(document).ajaxError(function () {
    $('.page-wrapper, .ajax-wrapper').waitMe('hide');
});

$(document).ajaxComplete(function () {

    $('.page-wrapper, .ajax-wrapper').waitMe('hide');
});

$(document).ajaxStop(function () {
    $('.page-wrapper, .ajax-wrapper').waitMe('hide');
});
 
 $('input[type="checkbox"]').click(function () {
        var id = $(this).attr("id"); 
        var name = $(this).attr("name");
        if ($(this).prop("checked") == true) {
            $(this).prop("checked", true);
            $(this).attr('checked', 'checked');
            //$("." + id).val(id).trigger("change");
        }
        else if ($(this).prop("checked") == false) {
            $(this).prop("checked", false);
            $(this).removeAttr('checked', 'checked');
           
           
        }
       
       
    });


function isNullAndUndef(variable) {

    return (variable === null || typeof variable === 'undefined' || variable === "");
}


function run_ajax_loader()
{
    $('.page-wrapper, .ajax-wrapper').waitMe({
        effect: 'roundBounce',
        text: 'Please wait...',
        bg: 'transparent',
        color: (localStorage.getItem('ClientThemeMode') == "darkMode")? '#fff': '#000',
        sizeW: '',
        sizeH: ''
    });
}
 