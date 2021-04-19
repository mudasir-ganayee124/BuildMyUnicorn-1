
$(document).ready(function () {

    console.log($("#jsonModel").val());
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketing").addClass("in");
    $("#liMarketing").addClass("active");
    $('.jsPercentage').each(function(index, value) {
     // $(this).val($(this).val().replace(/[^0-9]/gi, '') + '%');
      $(this).val(Number.parseFloat($(this).val()).toFixed(2).replace(/[^0-9,.]/gi, '') + '%');
    });
  $('.jsCurrency').each(function(index, value) {
         $(this).val($(this).val().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
    });

});




$(document).on("click", "#jsSubmitMarketPlan", function (e) {
  
    e.preventDefault();
    var Model = {
        "MarketingPlanID": $.trim($("#MarketingPlanID").val()),
        "ClientID": $.trim($("#ClientID").val()),
        "EntityState": $.trim($("#EntityState").val()),
        "MarketingPlan": {
            "PlanName": $.trim($("#PlanName").val()), "GoalID": $.trim($("#GoalID").val()),
            "KPIS": $.trim($("#KPIS").val()), "BuyerPersonaID": $.trim($("#BuyerPersonaID").val()),
            "FindBuyers": $.trim($("#FindBuyers").val()), "SellingProposition": $.trim($("#SellingProposition").val()),
            "AudianceReachID": $.trim($("#AudianceReachID").val()),
            "TrackKPIS": $.trim($("#TrackKPIS").val()),
        }, "MarketingBudget": {
            "Impressions": $.trim($("#Impressions").val().replace(/,/g, '')),
            "CTR": $.trim($("#CTR").val().replace('%', '')), "ConversionRate": $.trim($("#ConversionRate").val().replace('%', '')),
            "QualifiedLeadsPercentage": $.trim($("#QualifiedLeadsPercentage").val().replace('%', '')),
            "QualifiedLeadConversionPercentage": $.trim($("#QualifiedLeadConversionPercentage").val().replace('%', '')),
            "AvgCostPerClick": $.trim($("#AvgCostPerClick").val().replace(/,/g, '')), "AvgSale": $.trim($("#AvgSale").val().replace(/,/g, '')),
            "GoodsCostPercentage": $.trim($("#GoodsCostPercentage").val().replace('%', '')), "Clicks": $.trim($("#Clicks").val()),
            "Leads": $.trim($("#Leads").val()), "QualifiedLeads": $.trim($("#QualifiedLeads").val()),
            "SalesNumber": $.trim($("#SalesNumber").val()), "Revenue": $.trim($("#Revenue").val()),
            "GoodsCostSold": $.trim($("#GoodsCostSold").val()), "AdvertisingBudget": $.trim($("#AdvertisingBudget").val()),
            "ProfitAfterAdvertising": $.trim($("#ProfitAfterAdvertising").val()), "ProfitPercentage": $.trim($("#ProfitPercentage").val()),
            "ProfitPerClick": $.trim($("#ProfitPerClick").val()), "TargetCostPerLead": $.trim($("#TargetCostPerLead").val())


        }
    };
    console.log(Model);
    

    $.ajax({
        url: GetBaseURL() + "Marketing/AddMarketPlan",
        type: "POST",
        data: JSON.stringify({ Model: Model }),
        contentType: "application/json",
        dataType: "json",
        error: function (response) {
               setTimeout(function () { window.location.replace(GetBaseURL() + "Marketing/Marketing"); }, 1000);
        },
        success: function (response) {
            alert(response);
        }
    });
});


$("#frmBuyerPersona").submit(function (event) {
    event.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Marketing/AddBuyerPersona",
        method: "POST",
        data: $('#frmBuyerPersona').serialize(),
        success: function (response) {

            if (response.Status === "OK") {
                if (typeof showMessage === 'undefined') {
                    CommonFunctions.SuccessMessage("Success", "Buyer Persona Added successfully");
                }

                $("#frmBuyerPersona")[0].reset();
                $("#CustomerID").val(response.CustomerID);
                GetBuyerPersonaList(response.BuyerPersonaID);
            }


        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
});

$(document).on("change","#BuyerPersonaID",function(){  
 var BuyerPersonaID =  $("#BuyerPersonaID").val();
if(BuyerPersonaID == null) return false;
 $.ajax({
        url: GetBaseURL() + "Marketing/GetbuyerPersona",
        type: "POST",
        data: { BuyerPersonaID :BuyerPersonaID},
        success: function (obj) { console.log(obj);
            $.trim($("#jsName").val(obj.Name));
            $.trim($("#jsJobTitle").val(obj.JobTitle));
            $.trim($("#jsIncomeID").val(obj.IncomeValue));
            $.trim($("#jsGenderID").val(obj.GenderValue));
            $.trim($("#jsAgeID").val(obj.AgeValue));
            $.trim($("#jsResponsibility").val(obj.Responsibility));
            $.trim($("#jsLocation").val(obj.Location));
            $.trim($("#jsPainPoints").val(obj.PainPoints));
            $.trim($("#jsGoals").val(obj.Goals));
            $.trim($("#jsWantsKnow").val(obj.WantsKnow));
            $.trim($("#jsNotWantsKnow").val(obj.NotWantsKnow));
            $.trim($("#jsDriveBuy").val(obj.DriveBuy));
            $.trim($("#jsBuyFrom").val(obj.BuyFrom));
            $.trim($("#jsFindus").val(obj.Findus));
          
         
           
        },
        error: function (obj) {
            alert(obj);
        }
    });


});

$(document).on("blur", ".jsCurrency", function () {
    $(this).val($(this).val().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
 });

$(document).on("blur", ".jsPercentage", function () {

  
  $(this).val(Number.parseFloat($(this).val()).toFixed(2).replace(/[^0-9,.]/gi, '') + '%');
});
$(document).on("blur", ".jsBudgetChange", function () {


    var Model =
    {
        "Impressions": $.trim($("#Impressions").val().replace(/,/g , '')),
        "CTR": $.trim($("#CTR").val().replace('%', '')),
        "ConversionRate": $.trim($("#ConversionRate").val().replace('%', '')),
        "QualifiedLeadsPercentage": $.trim($("#QualifiedLeadsPercentage").val().replace('%', '')),
        "GoodsCostPercentage": $.trim($("#GoodsCostPercentage").val().replace('%', '')),
        "QualifiedLeadConversionPercentage": $.trim($("#QualifiedLeadConversionPercentage").val().replace('%', '')),
        "AvgCostPerClick": $.trim($("#AvgCostPerClick").val().replace(/,/g , '')),
        "AvgSale": $.trim($("#AvgSale").val().replace(/,/g , '')),
        "Clicks": $.trim($("#Clicks").val()),
        "Leads": $.trim($("#Leads").val()),
        "Revenue": $.trim($("#Revenue").val()),
        "QualifiedLeads": $.trim($("#QualifiedLeads").val()),
        "SalesNumber": $.trim($("#SalesNumber").val()),
        "GoodsCostSold": $.trim($("#GoodsCostSold").val()),
        "AdvertisingBudget": $.trim($("#AdvertisingBudget").val()),
        "ProfitAfterAdvertising": $.trim($("#ProfitAfterAdvertising").val()),
        "ProfitPercentage": $.trim($("#ProfitPercentage").val().replace('%', '')),
        "ProfitPerClick": $.trim($("#ProfitPerClick").val()),
        "TargetCostPerLead": $.trim($("#TargetCostPerLead").val())
    };
    $.ajax({
        url: GetBaseURL() + "Marketing/CalculateMarketBudget",
        type: "POST",
        data: JSON.stringify({ Model: Model }),
        contentType: "application/json",
        dataType: "json",
        success: function (obj) {
            $.trim($("#Impressions").val(obj.Impressions.toFixed(0)));
            $("#CTR").val($("#CTR").val().replace(/[^0-9]/gi, '') + '%');
            $.trim($("#CTR").val(obj.CTR.toFixed(2)));
            $.trim($("#ConversionRate").val(obj.ConversionRate.toFixed(0)));
            $.trim($("#QualifiedLeadsPercentage").val(obj.QualifiedLeadsPercentage.toFixed(2)));
            $.trim($("#GoodsCostPercentage").val(obj.GoodsCostPercentage.toFixed(2)));
            $.trim($("#QualifiedLeadConversionPercentage").val(obj.QualifiedLeadConversionPercentage.toFixed(2)));
            $.trim($("#AvgCostPerClick").val(obj.AvgCostPerClick.toFixed(2)));
            $.trim($("#AvgSale").val(obj.AvgSale.toFixed(2)));
            $.trim($("#Clicks").val(obj.Clicks.toFixed(0)));
            $.trim($("#Leads").val(obj.Leads.toFixed(0)));
            $.trim($("#Revenue").val(obj.Revenue.toFixed(2)));
            $.trim($("#QualifiedLeads").val(obj.QualifiedLeads.toFixed(0)));
            $.trim($("#SalesNumber").val(obj.SalesNumber.toFixed(0)));
            $.trim($("#GoodsCostSold").val(obj.GoodsCostSold.toFixed(2)));
            $.trim($("#AdvertisingBudget").val(obj.AdvertisingBudget.toFixed(2)));
            $.trim($("#ProfitAfterAdvertising").val(obj.ProfitAfterAdvertising.toFixed(2)));
            $.trim($("#ProfitPercentage").val(obj.ProfitPercentage.toFixed(2)));
            $.trim($("#ProfitPerClick").val(obj.ProfitPerClick.toFixed(2)));
            $.trim($("#TargetCostPerLead").val(obj.TargetCostPerLead.toFixed(2)));

            $("#jsImpressions").text(obj.Impressions.toFixed(0));
            $("#jsCTR").text(obj.CTR.toFixed(0));
            $("#jsConversionRate").text(obj.ConversionRate.toFixed(0));
            $("#jsQualifiedLeadsPercentage").text(obj.QualifiedLeadsPercentage.toFixed(0));
            $("#jsGoodsCostPercentage").text(obj.GoodsCostPercentage.toFixed(0));
            $("#jsQualifiedLeadConversionPercentage").text(obj.QualifiedLeadConversionPercentage.toFixed(0));
            $("#jsAvgCostPerClick").text(obj.AvgCostPerClick.toFixed(0));
            $("#jsAvgSale").text(obj.AvgSale.toFixed(0));
            $("#jsClicks").text(obj.Clicks.toFixed(0));
            $("#jsLeads").text(obj.Leads.toFixed(0));
            $("#jsRevenue").text(obj.Revenue.toFixed(0));
            $("#jsQualifiedLeads").text(obj.QualifiedLeads.toFixed(0));
            $("#jsSalesNumber").text(obj.SalesNumber.toFixed(0));
            $("#jsGoodsCostSold").text(obj.GoodsCostSold.toFixed(0));
            $(".jsAdvertisingBudget").text(obj.AdvertisingBudget.toFixed(0));
            $("#jsProfitAfterAdvertising").text(obj.ProfitAfterAdvertising.toFixed(0));
            $("#jsProfitPercentage").text(obj.ProfitPercentage.toFixed(0));
            $("#jsProfitPerClick").text(obj.ProfitPerClick.toFixed(0));
            $("#jsTargetCostPerLead").text(obj.TargetCostPerLead.toFixed(0));
            $('.jsPercentage').each(function(index, value) {
                 $(this).val(Number.parseFloat($(this).val()).toFixed(0).replace(/[^0-9,.]/gi, '') + '%');
            });
            $('.jsCurrency').each(function(index, value) {
                 $(this).val($(this).val().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
            });


           
        },
        error: function (obj) {
            alert(obj);
        }
    });

});

function GetBuyerPersonaList(BuyerPersonaID)
{

    $.ajax({
        type: 'POST',
        url: GetBaseURL() + "Marketing/GetBuyerPersonaList",
        data: { CustomerID :$("#CustomerID").val()},
        success: function (data) {
          var option = "<option> - Please select - </option>";
        $.each(data, function(index, value) {

            option += "<option value='"+value.BuyerPersonaID+"'> "+value.Name+" </option>";
         
        });
 $("#buyerPersonaModal").modal('hide');
        $("#BuyerPersonaID").html(option);
        $('#BuyerPersonaID').val(BuyerPersonaID);
         $('#BuyerPersonaID').select2().trigger('change');
     //   $("#BuyerPersonaID").val(BuyerPersonaID).Change();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
           
        }
    });

}

$(document).on("click", ".jsDeletePlan", function() {
 var MarketingPlanID = $(this).data("id");
 $.confirm({
        title: 'Confirmation?',
        content: 'Delete  Request Will Automatically  \'cancel\' in 6 seconds if you don\'t respond.',
        autoClose: 'cancelAction|8000',
        escapeKey: true,
        backgroundDismiss: false,
        typeAnimated: true,
        buttons: {
            deleteUser: {
                text: 'Delete',
                btnClass: 'btn-red',
                action: function () {
                    var option = {
                        action: "Delete",
                        controller: "Marketing",
                        dataType: "text",
                        data: { ID: MarketingPlanID }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Marketing Plan deleted successfully");
                           setTimeout(function(){ location.reload(); }, 1000); 
                        }
                        else
                            $.fn.successMsg(response);
                    });
                }
            },
            cancelAction: function () {
                $.alert('Delete Request is Canceled');
            }
        }
    });

});
$.fn.extend({

    "ajaxCall": function (options) {

        var settings = $.extend({
            contentType: "application/json; charset=utf-8",
            type: 'POST',
            dataType: 'json',
            controller: '',
            action: '',
            data: null
        }, options);

        return $.ajax({
            //contentType: settings.contentType,
            type: settings.type,
            url: GetBaseURL() + settings.controller + "/" + settings.action,
            dataType: settings.dataType,
            data: settings.data,
            success: function (response) {
                settings.response = response;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 403)
                    $.fn.errorMsg("Sorry, your session has expired. Please login again to continue");
                else
                    $.fn.errorMsg("An error occurred: " + errorThrown + " Status: " + XMLHttpRequest.status);


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