
console.log($("#jsonModel").val());
$(document).ready(function () {
   
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulSelling").addClass("in");
    $("#li_Customer").addClass("active");


   

});

GetBuyerPersona();

jQuery("select.image-picker").imagepicker({
    hide_select: false,
    //show_label: true
});

$(document).on("click", ".JsUpdate", function () {

    var CustomerID = $("#CustomerID").val();
    window.location.replace(GetBaseURL() + "Selling/Customer/" + CustomerID);
});

$(document).on("click", ".jsAddAvtar", function () {

  
    $("#AvtarID").val($("#JsimagePicker").val());
    var src = "/assets/images/avtar/Avatar Users2_" + $("#AvtarID").val() + ".png?rand=" + Math.random(); 
    $('#avtarSelected').attr('src', src);
    $('#avtarModal').modal('toggle');

});



$("#frmBuyerPersona").submit(function (event) {
    event.preventDefault(); 
    $.ajax({
        url: GetBaseURL() + "Selling/AddBuyerPersona",
        method: "POST",
        data: $('#frmBuyerPersona').serialize(),
        success: function (response) {

            if (response.Status === "OK") {
                if (typeof showMessage === 'undefined') {
                    CommonFunctions.SuccessMessage("Success", "Buyer Persona updated successfully");
                }
         
                $("#frmBuyerPersona")[0].reset();
                $(".jsCustomerID").val(response.CustomerID);
                GetBuyerPersona();
            }
           
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });  
});
$("#frmEditBuyerPersona").submit(function (event) {
    event.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Selling/AddBuyerPersona",
        method: "POST",
        data: $('#frmEditBuyerPersona').serialize(),
        success: function (response) {

            if (response.Status === "OK") {
                if (typeof showMessage === 'undefined') {
                    CommonFunctions.SuccessMessage("Success", "Buyer Persona updated successfully");
                }

            
               // $(".jsCustomerID").val(response.CustomerID);
                //GetBuyerPersona();
            }


        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
});

$(".tab-wizard").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enablePagination: false,
    titleTemplate: '<span class="step">#index#</span> #title#',
    labels: {
        finish: "Submit"
    },
    onFinished: function (event, currentIndex) {
        console.log($("form").serialize());
        console.log($('form').serializeArray());
        var data = {};
        console.log($("form").serializeArray().map(function (x) { data[x.name] = x.value; }));
        Swal.fire("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");

    }
});

var form = $("#frmBusinessCustomer").show();

$("#frmBusinessCustomer").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation:true,
    enableCancelButton: true,
    //stepsOrientation: "vertical",
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
        if ($("#IsRedirect").val() === "true") { window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=Selling&ActionName=Customer&ModuleID=6&SectionID=14"); }
        else location.replace(location.href.substring(0, location.href.lastIndexOf('/')));  
     },
    onFinished: function (event, currentIndex) {
      
        var Model =
        {
            "CustomerID": $.trim($("#CustomerID").val()), "ClientID": $.trim($("#ClientID").val()), "EntityState": $.trim($("#EntityState").val()),
            "BuyerPurchaseCycle": {
                "RecognitionNeed": $.trim($("#RecognitionNeed").val()), "InformationSearch": $.trim($("#InformationSearch").val()),
                "AlternativeEvaluation": $.trim($("#AlternativeEvaluation").val()), "PurchaseDecision": $.trim($("#PurchaseDecision").val()),
                "PurchaseEvaluation": $.trim($("#PurchaseEvaluation").val())
            },
            "About": $.trim($("#About").val()), "Based": $.trim($("#Based").val()), "Buy": $.trim($("#Buy").val()),
            "Factors": $.trim($("#Factors").val())
        };
    
        console.log(Model);
        var url = GetBaseURL() + "Selling/AddCustomer";
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Selling/Customer"); }, 1000);
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
}), $("#frmBusinessCustomer").validate({
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


function GetBuyerPersona()
{
    $.ajax({
        type: 'POST',
        url: GetBaseURL() + "Selling/GetBuyerPersona",
        dataType: 'html',
        data: {
            CustomerID: $('.jsCustomerID').val()
        },
        success: function (data) {
            $("#buyerPersonaContainer").html(data);
            $("#buyerPersonaModal").modal('hide');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
           
        }
    });

}


$(document).on("click", ".jsDeletebuyerpersona", function () {
    var BuyerPersonaID = $(this).data("id");
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
                $.ajax({
                        url: GetBaseURL() + "Business/DeleteBuyerPersona",
                        method: "POST",
                        data: { BuyerPersonaID: BuyerPersonaID, CustomerID : $("#CustomerID").val() },
                        success: function (html) {
                        
                         CommonFunctions.SuccessMessage("Deleted", "Buyer Persona Deleted successfully");
                        $("#buyerPersonaContainer").html(html);
                    },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            $(".erorLabel").removeClass("invisible");
                            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
                        }
                    });
                   
                }
            },
            cancelAction: function () {
                $.alert('Delete Request is Canceled');
            }
        }
    });

});


