$(document).ready(function () {
  
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketResearch").addClass("in");
    $("#liOnlineResearch").addClass("active");
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
        var _id="";
        $.each($("input[name='"+name+"']"), function () {
            if ($(this).prop("checked") == true) {
                _anyCheckedInGroup = true;
                _id = $(this).attr("id");
            
            }


        });
        $("#_" + name).val(_id).trigger("change");
        //if (_anyCheckedInGroup) {  }
    });

    $('.jsPercentage').each(function (index, value) {
        if ($(this).val() !== "") {
            $(this).val(Number.parseFloat($(this).val()).toFixed(2).replace(/[^0-9,.]/gi, '') + '%');
        }
    });

    $('.jsCurrency').each(function (index, value) {
        if ($(this).val() !== "") {
            var inputValue = Number.parseFloat($(this).val()).toFixed(2);
            $(this).val(`$${inputValue}`);
        }
    
    });
    $('.jsYear').each(function (index, value) {
        $(this).val(Number.parseInt($(this).val()).toFixed(0).replace(/[^0-9,.]/gi, '') + ' years');
    });


    $(document).on("blur", ".jsCurrency", function () {
        var inputValue = $(this).val(); 
        if (inputValue) {
           if (inputValue.charAt(0) === "$") {
               inputValue = inputValue.substring(1);
               
            }
        }
        inputValue = Number.parseFloat(inputValue).toFixed(2);
        $(this).val(`$${inputValue}`);
    });

    $(document).on("click", ".JsUpdateKeyfinding", function () {

        var OnlineResearchID = $("#OnlineResearchID").val();
        window.location.replace(GetBaseURL() + "MarketResearch/OnlineResearch/" + OnlineResearchID);
    });
    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        if (MarketSize)
        {
            if (MarketSize.charAt(0) === "$") {
                MarketSize = MarketSize.substring(1);  }
        }      
        var CaptureShare = $("#CaptureShare").val();
        var MarketShare = (parseFloat(MarketSize) * parseFloat(CaptureShare)) / 100; 
        if (isNaN(MarketShare) === false)
         $("#MarketShare").val(`$${Number.parseFloat(MarketShare).toFixed(2)}`);
            
         
        
    });

    $(document).on("blur", ".jsPercentage", function () {
        $(this).val(Number.parseFloat($(this).val()).toFixed(2).replace(/[^0-9,.]/gi, '') + '%');
    });
    $(document).on("blur", ".jsYear", function () {
        $(this).val(Number.parseInt($(this).val()).toFixed(0).replace(/[^0-9,.]/gi, '') + ' years');
    });

 });
var form = $("#frm_KeyFinding").show();
$("#frm_KeyFinding").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation:true,
    enableCancelButton: true,
    // stepsOrientation: "vertical",
    enablePagination: true,
    showFinishButtonAlways: true,
    saveState:true,
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
        if ($("#IsRedirect").val() === "true") { window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=MarketResearch&ActionName=OnlineResearch&ModuleID=2&SectionID=12"); }
        else location.replace(location.href.substring(0, location.href.lastIndexOf('/')));
    },
    onFinished: function (event, currentIndex) {
        SaveFormData();
       
    }
}), $("#frm_KeyFinding").validate({
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



var KeyPlayerItem = '<div class="row jsKeyMarketPlayer"><div class="col-md-6"> <div class="form-group"> <input type="text" class="form-control jsCompanyName progress-required" placeholder="Company"> </div> </div> <div class="col-md-5"> <div class="form-group"> <input type="text" class="form-control jsWebsite progress-required" placeholder="Website"> </div> </div> <div class="col-md-1"> <div class="input-group mb-3"> <div class="input-group-append"> <button class="btn btn-secondary btn-outline bootstrap-touchspin-up jsRemoveKeyPlayer" type="button">-</button> </div> </div> </div> </div>';

$(document).on("click", ".jsAddKeyPlayer", function() {
    $(".jsKeyPlayerContainer").append(KeyPlayerItem);
});

$(document).on("click", ".jsRemoveKeyPlayer", function() {
    $(this).closest('div.row').remove();
});

function SaveFormData(actionType) {
    var IdeaProgressed = [];
    var FeedbackReceived = [];
    var KeyPlayerList = [];


    $.each($("input[name='_IdeaProgressed']:checked"), function () {
        IdeaProgressed.push($(this).val());
    });
    $.each($("input[name='_FeedbackReceived']:checked"), function () {
        FeedbackReceived.push($(this).val());
    });

    $.each($(".jsKeyMarketPlayer"), function () {
        var CompnayName = $.trim($(this).find(".jsCompanyName").val());
        var Website = $.trim($(this).find(".jsWebsite").val());


        KeyPlayerList.push({ "Company": $(this).find(".jsCompanyName").val(), "Website": $(this).find(".jsWebsite").val() });

    });

    var OnlineResearch = {
        "OnlineResearchID": $.trim($("#OnlineResearchID").val()),
        "ClientID": $.trim($("#ClientID").val()),
        "EntityState": $.trim($("#EntityState").val()),
        "ProgressValue": "0.0",
        "BigPictureResearch": {
            "ResearchCarriedOutBit1": $.trim($("#ResearchCarriedOutBit1").val()),
            "ResearchCarriedOutBit2": $.trim($("#ResearchCarriedOutBit2").val()),
            "ResearchCarriedOutBit3": $.trim($("#ResearchCarriedOutBit3").val()),
            "IndustryTrends": $.trim($("#IndustryTrends").val()),
            "CaptureInitially": $.trim($("#CaptureInitially").val()),
            "MarketSize": $.trim($("#MarketSize").val().substring(1)),
            "CaptureShare": $.trim($("#CaptureShare").val()),
            "MarketShare": $.trim($("#MarketShare").val().replace('$', '')),
            "MarketShareCaptureDuration": $.trim($("#MarketShareCaptureDuration").val().replace('years', '')),
            "MarketKeyPlayerID": $.trim($("#MarketKeyPlayerID").val())
        },
        "FocussedResearch": {
            "IdeaProgressed": IdeaProgressed.join(","),
            "CustomerFeedback": $.trim($("#FocussedResearch_CustomerFeedback").val()),
            "FeedbackReceived": FeedbackReceived.join(","),
            "FeedbackRate": $.trim($("#FocussedResearch_FeedbackRate").val()),
            "FeedbackKeyfinding": $.trim($("#FeedbackKeyfinding").val())
        }
    };

    var option = {
        action: "AddOnlineResearch",
        controller: "MarketResearch",
        dataType: "json",
        data: JSON.stringify({ OnlineResearch: OnlineResearch, MarketKeyPlayer: KeyPlayerList }),
    };
    $.fn.ajaxCall(option).done(function (response) {
        PATCH = false;
        if (response.ResponseType === "Ok") {
            if (actionType != "PATCH")
                setTimeout(function () { window.location.replace(GetBaseURL() + "MarketResearch/OnlineResearch"); });
            else {

                $("#OnlineResearchID").val(response.OnlineResearchID);
                $("#EntityState").val(response.EntityState);
            }
        }
        else
            $.fn.successMsg(response);
    });




    
}
