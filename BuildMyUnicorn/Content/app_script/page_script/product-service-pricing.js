
$(document).ready(function () {

    console.log($("#jsonModel").val());

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulSelling").addClass("in");
    $("#liProductServiceSelling").addClass("active");
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

        var ProductServicePricingID = $("#ProductServicePricingID").val();
        window.location.replace(GetBaseURL() + "Selling/ProductServicePricing/" + ProductServicePricingID);
    });

    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        var CaptureShare = $("#CaptureShare").val();
        $("#MarketShare").val(parseFloat(MarketSize) * parseFloat(CaptureShare));
    });


});
var form = $("#frmPricingProductService").show();

$("#frmPricingProductService").steps({
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


        var Model = {
            "ProductServicePricingID": $.trim($("#ProductServiceID").val()),
            "ClientID": $.trim($("#ClientID").val()),
            "EntityState": $.trim($("#EntityState").val()),
            "ChosePricingStrategy": {
                "PricingStrategy": $.trim($("#PricingStrategy").val()),
                "ProductValue": $.trim($("#ProductValue").val()),
                "UsersBringValue": $.trim($("#UsersBringValue").val()),
                "ProductUnique": $.trim($("#ProductUnique").val()),
                "CustomersValue": $.trim($("#CustomersValue").val()),
                "CustomersOftenToPay": $.trim($("#CustomersOftenToPay").val()),
                "CustomersWillingToPay": $.trim($("#CustomersWillingToPay").val()),
                "CostDeliverPicture": $.trim($("#CostDeliverPicture").val()),
                "OfferCustomers": $.trim($("#OfferCustomers").val()),
                "OfferOpportunity": $.trim($("#OfferOpportunity").val()),
                "OfferLevels": $.trim($("#OfferLevels").val()),
                "CustomerBuy": $.trim($("#CustomerBuy").val()),
                "PricingStrategyChosen": $.trim($("#PricingStrategyChosen").val())

            }
        };


        $.ajax({
            url: GetBaseURL() + "Selling/AddProductServicePricing",
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Selling/ProductServicePricing"); }, 1000);
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
}), $("#frmPricingProductService").validate({
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
