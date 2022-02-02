﻿
$(document).ready(function () {

    $("#JsInterviewKeyFindingConfident").ionRangeSlider({
        min: 1,
        max: 10,
        from: $("#InterviewKeyFindingConfident").val(),
       // grid: true,
        onStart: function (data) { },
        onChange: function (data) {
            //Called every time handle position is changed
            //alert(data.from);
            console.log(data.from);
            $("#InterviewKeyFindingConfident").val(data.from);
        },

        onFinish: function (data) {
            //Called then action is done and mouse is released
            //alert(data.from);
            console.log(data.to); 
        },

        onUpdate: function (data) {
            //Called then slider is changed using Update public method
            //alert(data.from);
            console.log(data.from_percent);
        }
    });

    $("#jsObservationKeyFindingConfident").ionRangeSlider({
        min: 1,
        max: 10,
        from: $("#ObservationKeyFindingConfident").val(),
      //  grid: true,
        onStart: function (data) { },
        onChange: function (data) {
            //Called every time handle position is changed
            //alert(data.from);
            $("#ObservationKeyFindingConfident").val(data.from);
        },

        onFinish: function (data) {
            //Called then action is done and mouse is released
            //alert(data.from);
            console.log(data.to);
        },

        onUpdate: function (data) {
            //Called then slider is changed using Update public method
            //alert(data.from);
            console.log(data.from_percent);
        }
    });

    $("#jsSurveyKeyFindingConfident").ionRangeSlider({
        min: 1,
        max: 10,
        from: $("#SurveyKeyFindingConfident").val(),
       // grid: true,
        onStart: function (data) { },
        onChange: function (data) {
            //Called every time handle position is changed
            //alert(data.from);
            $("#SurveyKeyFindingConfident").val(data.from);
        },

        onFinish: function (data) {
            //Called then action is done and mouse is released
            //alert(data.from);
            console.log(data.to);
        },

        onUpdate: function (data) {
            //Called then slider is changed using Update public method
            //alert(data.from);
            console.log(data.from_percent);
        }
    });

    $("#jsOnlineResearchKeyFindingConfident").ionRangeSlider({
    min: 1,
    max: 10,
    from: $("#OnlineResearchKeyFindingConfident").val(),
    //grid: true,
    onStart: function (data) { },
    onChange: function (data) {
            //Called every time handle position is changed
            //alert(data.from);
        $("#OnlineResearchKeyFindingConfident").val(data.from);
        },
    
        onFinish: function (data) {
            //Called then action is done and mouse is released
           //alert(data.from);
            console.log(data.to);
        },
    
        onUpdate: function (data) {
            //Called then slider is changed using Update public method
           //alert(data.from);
            console.log(data.from_percent);
        }
    });

    console.log($("#jsonModel").val());

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketResearch").addClass("in");
    $("#liKeyFinding").addClass("active");
    //bg-inverse
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
            //$("." + id).val("").trigger("change");

        }
        var _anyCheckedInGroup = false;
        var _id = "";
        $.each($("input[name='" + name + "']"), function () {
            if ($(this).prop("checked") == true) {
                _anyCheckedInGroup = true;
                _id = $(this).attr("id");

            }


        });
        $("#_" + name).val(_id).trigger("change");
        //if (_anyCheckedInGroup) {  }
    });

    $(document).on("click", ".JsUpdate", function () {

        var KeyFindingID = $("#KeyFindingID").val();
        window.location.replace(GetBaseURL() + "MarketResearch/KeyFinding/" + KeyFindingID);
    });



});
var form = $("#frmKeyFinding").show();

$("#frmKeyFinding").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation: false,
    enableCancelButton: true,
    //stepsOrientation: "vertical",
    enablePagination: true,
    showFinishButtonAlways: true,
    saveState: true,
    titleTemplate: '<span class="step">#index#</span> #title#',
    labels: {
        finish: "Submit"
    },
    onStepChanging: function (event, currentIndex, newIndex) {
        return currentIndex > newIndex || !(3 === newIndex && Number($("#age-2").val()) < 18) && (currentIndex < newIndex && (form.find(".body:eq(" + newIndex + ") label.error").remove(), form.find(".body:eq(" + newIndex + ") .error").removeClass("error")), form.validate().settings.ignore = ":disabled,:hidden", form.valid())
    },
    onFinishing: function (event, currentIndex) {
        return form.validate().settings.ignore = ":disabled", form.valid()
    },
    onCanceled: function (event) {

        if ($("#IsRedirect").val() === "true") { window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=MarketResearch&ActionName=KeyFinding&ModuleID=2&SectionID=2"); }
        else location.replace(location.href.substring(0, location.href.lastIndexOf('/')));
    },
    onFinished: function (event, currentIndex) {

    }
}), $("#frmKeyFinding").validate({
    ignore: "input[type=hidden]",
    errorClass: "text-danger",
    successClass: "text-success",
    highlight: function (element, errorClass) {
        $(element).removeClass(errorClass)
    },
    unhighlight: function (element, errorClass) {
        $(element).removeClass(errorClass)
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element)
    },
    rules: {
        email: {
            email: !0
        }
    }
});


$(document).on("change", ".jsOptionChange", function () {
    var element = $(this).attr("data-optionchange");
    if ($(this).val() === "1")
        $("." + element).removeClass("hide");

    else
        $("." + element).addClass("hide");
});

function SaveFormData(actionType) {
    var Model = {
        "KeyFindingID": $.trim($("#KeyFindingID").val()),
        "ClientID": $.trim($("#ClientID").val()),
        "EntityState": $.trim($("#EntityState").val()),
        "MarketResearchResults": {
            "InterviewKeyFinding": $.trim($("#InterviewKeyFinding").val()), "InterviewKeyFindingConfident": $.trim($("#InterviewKeyFindingConfident").val()),
            "ObservationKeyFinding": $.trim($("#ObservationKeyFinding").val()), "ObservationKeyFindingConfident": $.trim($("#ObservationKeyFindingConfident").val()),
            "SurveyKeyFinding": $.trim($("#SurveyKeyFinding").val()), "SurveyKeyFindingConfident": $.trim($("#SurveyKeyFindingConfident").val()),
            "OnlineResearchKeyFinding": $.trim($("#OnlineResearchKeyFinding").val()), "OnlineResearchKeyFindingConfident": $.trim($("#OnlineResearchKeyFindingConfident").val())

        }
    };

    var option = {
        action: "AddKeyfinding",
        controller: "MarketResearch",
        dataType: "json",
        data: JSON.stringify(Model)
    };
    $.fn.ajaxCall(option).done(function (response) {
        PATCH = false;
        if (response.ResponseType === "Ok") {
            if (actionType != "PATCH")
                setTimeout(function () { window.location.replace(GetBaseURL() + "MarketResearch/KeyFinding"); }, 1000);
            else {

                $("#KeyFindingID").val(response.KeyFindingID);
                $("#EntityState").val(response.EntityState);
            }
        }
        else
            $.fn.successMsg(response);
    });

   
}