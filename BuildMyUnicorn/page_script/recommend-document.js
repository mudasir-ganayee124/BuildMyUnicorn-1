

$(document).on("click", ".jsSubscribe", function () {
   
    $.ajax({
        url: GetBaseURL() + "ThirdParty/AddPackageOrder",
        method: "POST",      // data: $('#frmRegister').serialize(),
        data: JSON.stringify({ id: $(this).data("packageid") }),
        contentType: "application/json",
        //dataType: "json",
        success: function (response) {
           
            if (response.status === "SUCCESS") {
                 $("#OrderID").val(response.data.OrderID);
                 RevolutCheckoutPayment(response.data.OrderPublicID);
                // alert();
            }
            else if (response.status === "FAILED") {
                $('.alert').show();
                $("#responseMessage").text(response.msg);

            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
           
            $('.alert').show();
            $("#responseMessage").text("Status: " + textStatus + "Error: " + errorThrown);

        }
    });
    
});

function RevolutCheckoutPayment(OrderPublicID) {
    //var OrderPublicID = "6f875bce-ac95-4439-b423-52d59f47c753";
    RevolutCheckout(OrderPublicID).then(function (instance) {

        var card = instance.createCardField({
            target: document.getElementById("card-field"),
            hidePostcodeField: true,
            savePaymentMethodFor: 'merchant',
            styles: {
                default: {
                    color: "#00c292",
                    "::placeholder": {
                        color: "#666"
                    }
                },
                autofilled: {
                    color: "#000"
                }
            },
            onSuccess(message) {
                _fn_Invoice();

                //  window.alert("Thank you!" + message);
                // console.log("key", message);
            },
            onError(error) {
                $("#button-submit").removeClass("hide");
                $("#button-wait").addClass("hide");
                // _fn_AddPaymentLog(error);
                $('.alert').show();
                $("#responsePaymentMessage").text(error);
                //window.alert("Oh no :(" + error);
                //console.log("errorBQE", error);
            },

        });


        document.getElementById("button-submit")
            .addEventListener("click", function () {
                $("#button-submit").addClass("hide");
                $("#button-wait").removeClass("hide");
                card.submit();
            });
    });
     $("#checkoutModel").modal("show");


}

function _fn_Invoice() {

    $.ajax({
        url: GetBaseURL() + "ThirdParty/SendPackageInvoice",
        method: "POST",
        data: { OrderID: $("#OrderID").val()  },
        success: function (response) {
            $("#checkoutModel").modal("hide");
          //  alert("Package subscribed successfully");
           // var ii = "2B32681D-8B18-4C76-B65F-2BA51DC8962C";
           // window.location.replace(GetBaseURL() + "RecommendDocumentation/Questions/" + ii);
            confirm(function () {
                window.location.replace(GetBaseURL() + "RecommendDocumentation/Questions/" + $("#SupplierID").val());
               // console.log('confirmed!');
            }, function () {
                console.log('denied');
            });
            $('.ja_body').contents().filter((_, el) => el.nodeType === 3).remove();
            $('.ja_body').prepend("Package subscribed successfully! \n Do you like to provide more Business details");
            //var r = confirm("Package subscribed successfully! \n Do you like to provide more busness details");
            //if (r == true) {
            //    window.location.replace(GetBaseURL() + "RecommendDocumentation/Questions/" + $("#SupplierID").val());
            //} 
            
           
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
}