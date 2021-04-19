
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

        var RunningBusinessID = $("#RunningBusinessID").val();
        window.location.replace(GetBaseURL() + "Business/RunningBusiness/" + RunningBusinessID);
    });

    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        var CaptureShare = $("#CaptureShare").val();
        $("#MarketShare").val(parseFloat(MarketSize) * parseFloat(CaptureShare));
    });


});

$(".disabled").css("display", "none");
var form = $("#frm_RunningBusiness").show();

$("#frm_RunningBusiness").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: false,
    enableFinishButton: true,
    enableKeyNavigation: false,
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

        var Model = {
            "EntityState": $.trim($("#EntityState").val()), "RunningBusinessID": $.trim($("#RunningBusinessID").val()),
            "ClientID": $.trim($("#ClientID").val()), "RBProducedID": $.trim($("#RBProducedID").val()),
            "RBDeliveryMethodID": $.trim($("#RBDeliveryMethodID").val()),
            "RBThirdPartyInvovedID": $.trim($("#RBThirdPartyInvovedID").val()),
            "RBPaymentMethodID": $.trim($("#RBPaymentMethodID").val()),
            "RBQualityControlMethod": $.trim($("#RBQualityControlMethod").val()),
            "RBStaffWorkID": $.trim($("#RBStaffWorkID").val()),
            "RBHaveEquipmentSoftware": $.trim($("#RBHaveEquipmentSoftware").val()), "RBEquipmentSoftware": $.trim($("#RBEquipmentSoftware").val()),
            "RBStaffNumber": $.trim($("#RBStaffNumber").val()), "RBStaffNumberNextYear": $.trim($("#RBStaffNumberNextYear").val()), "RBHaveInsurance": $.trim($("#RBHaveInsurance").val()),
            "RBNeedSpecificLicense": $.trim($("#RBNeedSpecificLicense").val()), "RBNeedSpecificeQualification": $.trim($("#RBNeedSpecificeQualification").val())
        };

        console.log(Model);
        var url = GetBaseURL() + "Business/AddRunningBusiness";
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify({ Model: Model}),
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Business/RunningBusiness"); }, 1000);
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
