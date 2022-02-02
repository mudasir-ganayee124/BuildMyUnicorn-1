var option = "";
var data = $("#jsonRoleInCompany").val();
if (typeof data !== "undefined") {
    $.each($.parseJSON(data), function (i, item) {
        option += '<option value="' + item.ID + '">' + item.Value + '</option>'
    });
}


$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulBudiness").addClass("in");
    $("#li_BusinessOverview").addClass("active");
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

        var BusinessOverID = $("#BusinessOverID").val();
        window.location.replace(GetBaseURL() + "Business/BusinessOverview/" + BusinessOverID);
    });

    $(document).on("blur", "#MarketSize, #CaptureShare", function () {
        var MarketSize = $("#MarketSize").val();
        var CaptureShare = $("#CaptureShare").val();
        $("#MarketShare").val(parseFloat(MarketSize) * parseFloat(CaptureShare));
    });


});
var form = $("#frm_business").show();

$("#frm_business").steps({
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
    onCanceled: function (event) { 
        if ($("#IsRedirect").val() === "true") { window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=Business&ActionName=BusinessOverview&ModuleID=3&SectionID=3"); }
        else location.replace(location.href.substring(0, location.href.lastIndexOf('/')));
     },
    onFinished: function (event, currentIndex) {

        SaveFormData();
      }
}), $("#frm_business").validate({
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


var CompanyFounder = '<div class="col-md-12"> <div class="row jsCompanyFounder"> <div class="col-md-3"> <div class="form-group"> <input type="text" class="form-control jsFirstName progress-required" placeholder="FirstName" value=""> </div> </div> <div class="col-md-3"> <div class="form-group"> <input type="text" class="form-control jsLastName progress-required" placeholder="LastName" value=""> </div> </div> <div class="col-md-3"> <div class="form-group"> <input type="text" class="form-control jsEmail progress-required" placeholder="Email" value=""> </div> </div> <div class="col-md-3"> <div class="input-group-append jsRole"> <input type="hidden"  name="RoleInCompany" value="" /><select class="selectpicker form-control jsRoleinCompany" multiple data-style="form-control btn-info" data-roleincompany="" >'+ option+'</select> <button class="btn btn-secondary btn-outline bootstrap-touchspin-up jsRemoveCompanyFounder" type="button">-</button> </div> </div> </div> </div>';



$(document).on("click", ".jsAddCompanyFounder", function () {
    $(".jsCompanyFounderContainer").append(CompanyFounder);
    $('.selectpicker').selectpicker('refresh');
});
$(document).on("change", ".selectpicker", function (e) {
    $(this).closest('div.jsRole').find("input[name$='RoleInCompany']:hidden:first").val($(this).val());
   
});


$(document).on("click", ".jsRemoveCompanyFounder", function () {
    $(this).closest('div.row').remove();
});



$(document).on("change", ".jsOptionChange", function () {
  var element = $(this).attr("data-optionchange");
   if ($(this).val() === "7f5d70c1-5e5b-4411-a05c-224976e6feff") 
     $("."+element).removeClass("hide");
   
   else
      $("."+element).addClass("hide");
 });


function SaveFormData(actionType) {

    var LandlordCostStatus = $("#LandlordCostStatus").val();
    if ($("#CompanyDetails_BusinessRequirePremises").val() !== "7f5d70c1-5e5b-4411-a05c-224976e6feff")
        LandlordCostStatus = "";

    var BusinessModel = {
        "BusinessOverID": $.trim($("#BusinessOverID").val()),
        "ClientID": $.trim($("#ClientID").val()),
        "EntityState": $.trim($("#EntityState").val()),
        "Founder": {
            "IdeaComeup": $.trim($("#IdeaComeup").val()), "BusinessRun": $.trim($("#BusinessRun").val()),
            "PreviousWorkExperience": $.trim($("#PreviousWorkExperience").val()), "Qualification": $.trim($("#Qualification").val()),
            "HobbiesInterest": $.trim($("#HobbiesInterest").val()), "WhyYou": $.trim($("#WhyYou").val())
        }, "CompanyDetails": {
            "CompanyRegisterdName": $.trim($("#CompanyRegisterdName").val()), "Founded": $.trim($("#Founded").val()),
            "CompanyLegalStructureID": $.trim($("#CompanyDetails_CompanyLegalStructureID").val()), "LandlordCostStatus": LandlordCostStatus,
            "CompanyNumber": $.trim($("#CompanyNumber").val()), "BusinessAddress": $.trim($("#BusinessAddress").val()),
            "BusinessPhone": $.trim($("#BusinessPhone").val()), "VatNumber": $.trim($("#VatNumber").val()),
            "NumberofFounder": $.trim($("#NumberofFounder").val()), "BusinessRequirePremises": $.trim($("#CompanyDetails_BusinessRequirePremises").val()),
            "CompanyFounderID": "00000000-0000-0000-0000-000000000000"
        }
    };
    console.log(BusinessModel);

    var option = {
        action: "AddBusinessOverview",
        controller: "Business",
        dataType: "json",
        data: JSON.stringify({ BusinessModel: BusinessModel }),
    };
    $.fn.ajaxCall(option).done(function (response) {
        PATCH = false;
        if (response.ResponseType === "Ok") {
            if (actionType != "PATCH")
                setTimeout(function () { window.location.replace(GetBaseURL() + "Business/BusinessOverview"); }, 1000);
            else {

                $("#BusinessOverID").val(response.BusinessOverID);
                $("#EntityState").val(response.EntityState);
            }
        }
        else
            $.fn.successMsg(response);
    });
}