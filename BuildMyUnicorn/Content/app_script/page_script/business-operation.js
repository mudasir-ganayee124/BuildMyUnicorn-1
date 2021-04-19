
  var thirdPartylist = [];
 var _maxCounter = 1;


$(document).ready(function () {

    console.log($("#jsonModel").val());
    console.log($("#JsonpartyType").val());

    if($("#JsonpartyType").val() != "") 
      thirdPartylist = $.parseJSON($("#JsonpartyType").val());
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

        var BusinessOperationID = $("#BusinessOperationID").val();
        window.location.replace(GetBaseURL() + "Business/BusinessOperation/" + BusinessOperationID);
    });

    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        var CaptureShare = $("#CaptureShare").val();
        $("#MarketShare").val(parseFloat(MarketSize) * parseFloat(CaptureShare));
    });


});

$(".disabled").css("display", "none");
var form = $("#frmBusinessOperation").show();

$("#frmBusinessOperation").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation: true,
    //stepsOrientation: "vertical",
    enableCancelButton: true,
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
        var Model = {"BusinessOperationID":$.trim($("#BusinessOperationID").val()),
                     "ClientID":$.trim($("#ClientID").val()),
                     "EntityState":$.trim($("#EntityState").val()),
                     "WhoDoesWhat":{"FinanceOperationalResponsible":$.trim($("#FinanceOperationalResponsible").val()),"FinanceManagementResponsible":$.trim($("#FinanceManagementResponsible").val()),"OperationResponsible":$.trim($("#OperationResponsible").val()),
                                    "LegalMatterResponsible":$.trim($("#LegalMatterResponsible").val()),"HRresponsible":$.trim($("#HRresponsible").val()),"FacilitiesResponsible":$.trim($("#FacilitiesResponsible").val()),"SalesResponsible":$.trim($("#SalesResponsible").val()),
                                    "MarektingResponsible":$.trim($("#MarektingResponsible").val())},
                                    "Operations":{"ProductProduced":$.trim($("#ProductProduced").val()),"DeliveryMethodID":$.trim($("#DeliveryMethodID").val()),
                                    "ThirdPartyInvovedID":$.trim($("#ThirdPartyInvovedID").val()),"PaymentMethodID":$.trim($("#PaymentMethodID").val()),
                                    "QualityControlMethod":$.trim($("#QualityControlMethod").val()),"StaffWorkID":$.trim($("#StaffWorkID").val()),"Needsoftware":$.trim($("#Needsoftware").val()),"SoftwareType":$.trim($("#SoftwareType").val()),
                                    "StaffCount":$.trim($("#StaffCount").val()),"StaffCountNextYear":$.trim($("#StaffCountNextYear").val()),"InsuranceType":$.trim($("#InsuranceType").val()),"LicenseType":$.trim($("#LicenseType").val()),
                                    "QualificationType":$.trim($("#QualificationType").val())}}


        console.log(Model);
        var url = GetBaseURL() + "Business/AddBusinessOperation";
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify({Model:Model, ModelList : thirdPartylist}),
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Business/BusinessOperation"); }, 1000);
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
}), $("#frmBusinessOperation").validate({
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
     $("."+element).removeClass("hide");
   
   else
      $("."+element).addClass("hide");
 });


$("button").click(function(e){ e.preventDefault();
    _maxCounter = parseInt(_maxCounter) + 1;
    var orig = $("div.jsCloneElement");
    var cloned = $(orig).clone().show();
    cloned.find(".jsCounter").val(_maxCounter);
    cloned.removeClass("jsCloneElement").clone().appendTo("div.jsCloneContainer");
	$(".tagfield").fancymetags({theme: "black"});
  });

$(document).on("change", ".jsThirdPartyType", function() {
   var _element = $(this).closest("div.jsOperationInvolvedParty");
   var _Counter = _element.find(".jsCounter").val();
   var _PartyType = $(this).val();
   $.each(thirdPartylist,function(index,value)
   {
    if(this.Counter==_Counter)
        this.Value1 = _PartyType;
   });
   console.log(thirdPartylist);

});

