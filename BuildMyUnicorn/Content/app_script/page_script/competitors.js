console.log($("#jsonModel").val());
$(document).ready(function () {

    console.log($("#jsonModel").val());
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulBudiness").addClass("in");
    $("#li_RunningBusiness").addClass("active");
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

        var ClientID = $("#ClientID").val();
        //window.location.replace(GetBaseURL() + "Business/Competitors");
        window.location.replace(GetBaseURL() + "Business/Competitors?Edit=" + true);
    });

    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        var CaptureShare = $("#CaptureShare").val();
        $("#MarketShare").val(parseFloat(MarketSize) * parseFloat(CaptureShare));
    });


});

$(".disabled").css("display", "none");
var form = $("#frm_CompetitorAnalysis").show();

$("#frm_CompetitorAnalysis").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation: true,
    enableCancelButton: true,
    //stepsOrientation: "vertical",
    enablePagination: true,
    showFinishButtonAlways: false,
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

        var CompetitorAnalysis = [];
        $.each($(".jsContainerCompetitorAnalysis tr"), function () {
            var CompetitorAnalysisID = $.trim($(this).find(".jsCompetitorAnalysisID").val());
            var Name = $.trim($(this).find(".jsName").val());
            var Location = $.trim($(this).find(".jsLocation").val());
            var Website = $.trim($(this).find(".jsWebsite").val());
            var Offering = $.trim($(this).find(".jsOffering").val());
            var Pricing = $.trim($(this).find(".jsPricing").val());
            var Differentiator = $.trim($(this).find(".jsDifferentiator").val());
            CompetitorAnalysis.push({ "CompetitorAnalysisID": CompetitorAnalysisID, "Name": Name, "Location": Location, "Website": Website, "Offering": Offering, "Pricing": Pricing, "Differentiator": Differentiator});
        });
        var SWOT = {
            "EntityState": $.trim($("#EntityState").val()),
            "SWOTID": $.trim($("#SWOTID").val()),
            "Strengths": $.trim($("#Strengths").val()),
            "Weaknesses": $.trim($("#Weaknesses").val()),
            "Opportunities": $.trim($("#Opportunities").val()),
            "Threats": $.trim($("#Threats").val()),
            "CompetitorAnalysis": CompetitorAnalysis
        };
        var Model = {
            "EntityState": $.trim($("#EntityState").val()),
            "ClientID": $.trim($("#ClientID").val()),
            "SWOT": SWOT,
            "CompetitorAnalysis": CompetitorAnalysis  };


        console.log(CompetitorAnalysis);
        console.log(SWOT);
        var url = GetBaseURL() + "Business/AddCompetitors";
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify({ Model: Model }),
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Business/Competitors"); }, 1000);
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
}), $("#frm_RunningBusiness").validate({
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


$(document).on("change", "#RBHaveEquipmentSoftware", function () {

    $(".jsEquipmentSoftware").addClass("hide");
    if ($(this).val() === "1") {
        $(".jsEquipmentSoftware").removeClass("hide");
    }
});

var item = '<tr><td><input type="text" class="form-control progress-required jsName" name="Name" /> </td> <td> <input type="text" class="form-control progress-required jsLocation" name="Location" /> </td> <td> <input type="text" class="form-control progress-required jsWebsite" name="Website" /> </td> <td> <input type="text" class="form-control progress-required jsOffering" name="Offering" /> </td> <td> <input type="text" class="form-control progress-required jsPricing" name="Pricing" /> </td> <td> <input type="text" class="form-control progress-required jsDifferentiator" name="Differentiator"/> </td> <td style="cursor:pointer" class="jsRemoveCompetitorAnalysis"> - </td> </tr>'

$(document).on("click", ".jsAddCompetitorAnalysis", function () {

    $(".jsContainerCompetitorAnalysis").append(item);
});

$(document).on("click", ".jsRemoveCompetitorAnalysis", function () {
    $(this).closest('tr').remove();
});
