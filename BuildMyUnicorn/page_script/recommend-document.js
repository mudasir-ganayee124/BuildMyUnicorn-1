$(document).on("click", ".jsSubscribe", function () {
    $.ajax({
        url: GetBaseURL() + "ThirdParty/AddPackageOrder",
        method: "POST",      // data: $('#frmRegister').serialize(),
        data: JSON.stringify({ PackageID: $(this).data("packageid") }),
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
            // if (response == "OK") {

           // $('<form action="Register/SignupSuccess/" method="post"><input type="text"  name="email" value="' + $("#Email").val() + '"/></form>').appendTo('body').submit().remove();

            // }
            // else {
            //    alert(response);
            //  $(".erorLabel").removeClass("invisible");
            //$(".errorMessage").text(response);}
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
}