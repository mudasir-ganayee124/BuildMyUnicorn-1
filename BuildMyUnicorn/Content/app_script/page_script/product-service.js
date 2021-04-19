$(document).ready(function () {

    console.log($("#jsonModel").val());

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulBudiness").addClass("in");
    $("#li_ProductService").addClass("active");
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

        var ProductServiceID = $("#ProductServiceID").val();
        window.location.replace(GetBaseURL() + "Business/ProductService/" + ProductServiceID);
    });

    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        var CaptureShare = $("#CaptureShare").val();
        $("#MarketShare").val(parseFloat(MarketSize) * parseFloat(CaptureShare));
    });


});
var form = $("#frm_ProductService").show();

$("#frm_ProductService").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation: true,
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
    onCanceled: function (event) { location.replace(location.href.substring(0, location.href.lastIndexOf('/')));},
    onFinished: function (event, currentIndex) {

        //var CompanyFounderID = [];

        var PSSelling = [];
        var PSProduce = [];
        var MultipleMaster = [];

        $.each($("input[name='_PSSelling']:checked"), function () {
            PSSelling.push($(this).val());
        });
        $.each($("input[name='_PSProduce']:checked"), function () {
            PSProduce.push($(this).val());
        });
        $.each($(".jsTechnologyRoadMap"), function () {
            var version = $.trim($(this).find(".jsVersion").val());
            MultipleMaster.push({ "Value1": version });
        });

        var Model = {
            "ProductServiceID": $.trim($("#ProductServiceID").val()), "ClientID": $.trim($("#ClientID").val()), "EntityState": $.trim($("#EntityState").val()),
            "AboutProduct": { "PSSelling": PSSelling.join(","), "PSDescription": $.trim($("#PSDescription").val()), "PSProduce": PSProduce.join(","), "PSDevelopStart": $.trim($("#PSDevelopStart").val()), "PSReadylaunch": $.trim($("#PSReadylaunch").val()), "PSVariations": $.trim($("#PSVariations").val()), "PSHaveIPAddress": $.trim($("#PSHaveIPAddress").val()), "PSIPAddress": $.trim($("#PSIPAddress").val()), "PSHaveTradeMark": $.trim($("#PSHaveTradeMark").val()), "PSTradeMark": $.trim($("#PSTradeMark").val()), "PSHaveTechnologyRoadMap": $.trim($("#PSHaveTechnologyRoadMap").val()) },
            "MVP": { "MVPDevelopmentFarID": $.trim($("#MVPDevelopmentFarID").val()), "MVPPrototype": $.trim($("#MVPPrototype").val()), "MVPCreate": $.trim($("#MVPCreate").val()), "MVPReasonID": $.trim($("#MVPReasonID").val()) }
        };

        console.log(Model);
        console.log(MultipleMaster);
        var url = GetBaseURL() + "Business/AddProductService";
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify({ Model: Model, MultipleMaster: MultipleMaster }),
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Business/ProductService"); }, 1000);
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
}), $("#frm_ProductService").validate({
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


var item = '<div class="row jsTechnologyRoadMap"><div class="col-md-11"> <div class="form-group"> <input type="text" class="form-control jsVersion progress-required" placeholder="Version"> </div> </div>  <div class="col-md-1"> <div class="input-group mb-3"> <div class="input-group-append"> <button class="btn btn-secondary btn-outline bootstrap-touchspin-up jsRemove" type="button">-</button> </div> </div> </div> </div>';

$(document).on("click", ".jsAdd", function () {
    $(".jsTechnologyRoadMapContainer").append(item);
});

$(document).on("click", ".jsRemove", function () {
    $(this).closest('div.row').remove();
});

$(document).on("change", "#PSHaveIPAddress", function () {
    $(".jsPSIPAddress").addClass("hide");
    if ($(this).val() === "1") {
        $(".jsPSIPAddress").removeClass("hide");
    }
});
$(document).on("change", "#PSHaveTradeMark", function () {

    $(".jsPSTradeMark").addClass("hide");
    if ($(this).val() === "1") {
        $(".jsPSTradeMark").removeClass("hide");
    }
});
$(document).on("change", "#PSHaveTechnologyRoadMap", function () {

    $(".jsTechnologyRoadMapContainer").addClass("hide");
    if ($(this).val() === "1") {
        $(".jsTechnologyRoadMapContainer").removeClass("hide");
    }
});
$(document).on("change", "#MVPHavePrototype", function () {
    $(".jsMVPHavePrototype").addClass("hide");
    if ($(this).val() === "1") {
        $(".jsMVPHavePrototype").removeClass("hide");
    }
});