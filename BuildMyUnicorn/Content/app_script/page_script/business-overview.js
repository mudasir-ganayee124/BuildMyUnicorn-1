$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Ideanav").addClass("active");
    $("#Ideanav ul").addClass("in");
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

    $(document).on("click", ".JsUpdateKeyfinding", function () {

        var KeyFindingID = $("#KeyFindingID").val();
        window.location.replace(GetBaseURL() + "KeyFinding/" + KeyFindingID);
    });
    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        var CaptureShare = $("#CaptureShare").val();
        $("#MarketShare").val(parseFloat(MarketSize) * parseFloat(CaptureShare));
    });


});
var form = $("#frm_KeyFinding").show();
$("#frm_KeyFinding").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation: true,
    enableCancelButton: true,
    // stepsOrientation: "vertical",
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
    onCanceled: function (event) { location.replace(location.href.substring(0, location.href.lastIndexOf('/')));},
    onFinished: function (event, currentIndex) {

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
        //var KeyfindingModel = {"KeyFindingID":"00000000-0000-0000-0000-000000000000","ClientID":"00000000-0000-0000-0000-000000000000","ProgressValue":0.0,"BigPictureResearch":{"ResearchCarriedOutBit1":null,"ResearchCarriedOutBit2":null,"ResearchCarriedOutBit3":null,"IndustryTrends":null,"CaptureInitially":null,"MarketSize":0.0,"CaptureShare":0.0,"MarketShare":0.0,"MarketShareCaptureDuration":0,"MarketKeyPlayerID":"00000000-0000-0000-0000-000000000000"},"FocussedResearch":{"IdeaProgressed":null,"CustomerFeedback":false,"FeedbackReceived":null,"FeedbackRate":null,"FeedbackKeyfinding":null}}
        var KeyfindingModel = {
            "KeyFindingID": $.trim($("#KeyFindingID").val()),
            "ClientID": $.trim($("#ClientID").val()),
            "ProgressValue": "0.0",
            "BigPictureResearch": {
                "ResearchCarriedOutBit1": $.trim($("#ResearchCarriedOutBit1").val()),
                "ResearchCarriedOutBit2": $.trim($("#ResearchCarriedOutBit2").val()),
                "ResearchCarriedOutBit3": $.trim($("#ResearchCarriedOutBit3").val()),
                "IndustryTrends": $.trim($("#IndustryTrends").val()),
                "CaptureInitially": $.trim($("#CaptureInitially").val()),
                "MarketSize": $.trim($("#MarketSize").val()),
                "CaptureShare": $.trim($("#CaptureShare").val()),
                "MarketShare": $.trim($("#MarketShare").val()),
                "MarketShareCaptureDuration": $.trim($("#MarketShareCaptureDuration").val()),
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



        var ActionType = $("#ActionType").val();
        var url = GetBaseURL() + "KeyFinding/Add";
        if (ActionType == "UPDATE")
            url = GetBaseURL() + "KeyFinding/Update";


        //JSON.stringify({ SaleItemList: SaleItemList, CustomerID: CustomerID, InvoiceID: $("#InvoiceID").val() }),
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify({ KeyfindingModel: KeyfindingModel, MarketKeyPlayer: KeyPlayerList }),
            contentType: "application/json",
            dataType: "json",

            error: function (response) {
                //if (ActionType == "UPDATE")
                //    swal({
                //        title: "Success!",
                //        text: "Idea Submitted Successfuly!",
                //        icon: "success",
                //        button: "Close!",
                //    });


                //else

                //    swal({
                //        title: "Success!",
                //        text: "Idea Updated Successfuly!",
                //        icon: "success",
                //        button: "Close!",
                //    });

                setTimeout(function () { window.location.replace(GetBaseURL() + "KeyFinding"); }, 1000);
            },
            success: function (response) {
                alert(response);
            }
        });







        console.log($.parseJSON($("form").serialize()));
        console.log($.parseJSON($('form').serializeArray()));
        // var data = {};
        // $("form").serializeArray().map(function (x) { data[x.name] = x.value; })
        //console.log(data);
        Swal.fire("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");
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

$(document).on("click", ".jsAddKeyPlayer", function () {
    $(".jsKeyPlayerContainer").append(KeyPlayerItem);
});

$(document).on("click", ".jsRemoveKeyPlayer", function () {
    $(this).closest('div.row').remove();
});
