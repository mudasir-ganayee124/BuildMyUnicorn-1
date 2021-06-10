
if ($("#GrantStatus").val() === "Locked")
{
    $("input").attr("disabled", "disabled");
    $("textarea").attr("disabled", "disabled");
 
}

$(document).on("change", ".jsSaleforecastChange", function () {

    var id = $(this).data("id");
    var Property = $(this).attr("name");
    var Value = $(this).val();
    $.ajax({
        url: GetBaseURL() + "Finance/UpdateSalesForecast",
        type: "POST",
        data: JSON.stringify({ SaleforecastID: id, Property: Property, Value: Value}),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {
            
            $("div#RenderSalesforecast").html(data);
        },
        error: function (response) {
           
           
        }
     
    });
    
});

$(document).on("change", ".jsCashflowforecastChange", function () {

    var id = $(this).data("id");
    //var Property = $(this).attr("name");
    var Value = $(this).val();
    $.ajax({
        url: GetBaseURL() + "Finance/UpdateCashflowforecast",
        type: "POST",
        data: JSON.stringify({ CashflowforecastID: id, Amount: Value }),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {

            $("div#RenderCashflowforecast").html(data);
        },
        error: function (response) {


        }

    });

});

$(document).on("change", ".jsChangeItem", function () {
   
    var element = $(this).closest("tr.selected");
    var id = element.data("id");
    var Monthly = element.find("input.jsPropertyName").val();
    var Notes = element.find("textarea.jsNote").val();
    $.ajax({
        url: GetBaseURL() + "Finance/UpdatePartnerOnePersonalSurviveBudget",
        type: "POST",
        data: JSON.stringify({ PersonalSurvivalBudgetID: id, Monthly: Monthly, Notes: Notes }),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {

            $("div#RenderPartnerOnePersonalSurviveBudget").html(data);
        },
        error: function (response) {


        }

    });

});

$(document).on("change", ".jsChangeItemTwo", function () {

    var element = $(this).closest("tr.selected");
    var id = element.data("id");
    var Monthly = element.find("input.jsPropertyName").val();
    var Notes = element.find("textarea.jsNote").val();
    $.ajax({
        url: GetBaseURL() + "Finance/UpdatePartnerTwoPersonalSurviveBudget",
        type: "POST",
        data: JSON.stringify({ PersonalSurvivalBudgetID: id, Monthly: Monthly, Notes: Notes }),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {

            $("div#RenderPartnerTwoPersonalSurviveBudget").html(data);
        },
        error: function (response) {


        }

    });

});

$(document).on("change", ".jsCalculatorChange", function () {
  
    var id = $("tr.loancalculatorid").data("id");
    var AmountToBorrow = $(document).find("input#AmountToBorrow").val().replace(/,/g, '');
    var YearsToRepay = $(document).find("input#YearsToRepay").val().replace(/,/g, ''); 
   
    $.ajax({
        url: GetBaseURL() + "Finance/UpdateLoanCalculator",
        type: "POST",
        data: JSON.stringify({ LoanCalculatorID: id, AmountToBorrow: AmountToBorrow, YearsToRepay: YearsToRepay }),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {

            $("div#RenderLoancalculator").html(data);
        },
        error: function (response) {


        }

    });

});

$(document).on("change", ".jsUpdateNote", function () {

   
    var id = $(this).data("id");
    var type = $(this).data("type");
    var note = $(this).val();

    $.ajax({
        url: GetBaseURL() + "Finance/UpdateNote",
        type: "POST",
        data: JSON.stringify({ Id: id, Type: type, Note :note }),
        contentType: "application/json",
        dataType: "html",
        success: function (data) {

           // $("div#RenderPartnerTwoPersonalSurviveBudget").html(data);
        },
        error: function (response) {


        }

    });

});

//jsSalesforecastContainer

var form = $("#frmPersonalSurvivalBudget").show();
$("#frmPersonalSurvivalBudget").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation: false,
    enableCancelButton: false,
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
    onCanceled: function (event) { location.replace(location.href.substring(0, location.href.lastIndexOf('/'))); },
    onFinished: function (event, currentIndex) {
        if ($("#GrantStatus").val() === "Locked") return false;
                
        $.ajax({
            url: GetBaseURL() + "Finance/AddGrantSurvivalBudget",
            type: "POST",
            data: JSON.stringify({ GrantID: $("#GrantID").val() }),
            contentType: "application/json",
            dataType: "json",

            error: function (response) {
               

               setTimeout(function () { window.location.replace(GetBaseURL() + "Finance/Grants"); }, 1000);
            },
            success: function (response) {
                alert(response);
            }
        });

        Swal.fire("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");
    }
}), $("#frmPersonalSurvivalBudget").validate({
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