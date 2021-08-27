
$("#CardName").on("keyup", function () {
    $("#CardName").css("border", "1px solid #272c36");
});
$(function () {
    $(".preloader").fadeOut();
    $('[data-toggle="tooltip"]').tooltip()
    $('form').areYouSure({ 'message': 'Changes you made may not be saved.' });
    $.ajax({
        url: GetBaseURL() + "Register/GetCountryList",
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            console.log(res);

            var options = "<option value=''>--Select your Country--</option>";
            $.each(res.country, function (key, val) {

                options += "<option value=" + val.CountryID + ">" + val.CountryName + "</option>";
            });
            $('#CountryID').html(options).trigger('change');
        }
    });
    $("input,select,textarea").not("[type=submit]").jqBootstrapValidation();
});

$('#frmRegister').parsley();

$("#frmRegister").submit(function (e) {
 
    e.preventDefault();
   // if ($("#Agree").prop("checked") == false) { return false; }
    $.ajax({
        url: GetBaseURL() + "Register/AddCustomer",
        method: "POST",
        data: $('#frmRegister').serialize(),
        success: function (response)
        {     
            if (response.status === "SUCCESS") {
                console.log(response.data);
                $("#frmRegister").slideUp();
                $("#frmPayment").fadeIn();
                $("#PublicID").val(response.data.OrderPublicID);
                $("#ClientID").val(response.data.ClientID);
                $("#OrderID").val(response.data.OrderID);
                RevolutCheckoutPayment(response.data.OrderPublicID);
               // $('<form action="Register/SignupSuccess/" method="post"><input type="text"  name="email" value="' + $("#Email").val() +'"/></form>').appendTo('body').submit().remove();
               
         
            }
            else if (response.status === "FAILED") 
            {
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

$('#Agreement').click(function () {
    if ($(this).prop("checked") == true) {
        $("#Agreement").prop("checked", true);
        $("#Agreement").attr('checked', 'checked')
    }
    else if ($(this).prop("checked") == false) {
        $("#Agreement").prop("checked", false);
        $("#Agreement").removeAttr('checked', 'checked')
    }
});
/*RevolutCheckoutPayment("7001775e-99c2-4b87-883e-b50564c7d4fb");*/

function RevolutCheckoutPayment(OrderPublicID)
{
      
    RevolutCheckout(OrderPublicID).then(function (instance) {

            var card = instance.createCardField({
                target: document.getElementById("card-field"),
                hidePostcodeField: true,
                savePaymentMethodFor: 'merchant',
                name: $("#CardName").val(),
                email: $("#Email").val(),
                phone: $("#Phone").val(),
                locale: "en",
                billingAddress: {
                    countryCode: "",
                    region: "",
                    city: "",
                    streetLine1: "",
                    streetLine2: "",
                    postcode: ""
                },
                shippingAddress: {
                    countryCode: "",
                    region: "",
                    city: "",
                    streetLine1: "",
                    streetLine2: "",
                    postcode: ""
                },
                styles: {
                    default: {
                        color: "#fff",
                        "::placeholder": {
                            color: "#666"
                        }
                    },
                    autofilled: {
                        color: "#fff"
                    }
                },
                onSuccess(message) {
                              _fn_SendCustomerInvoice();
                             
                  //  window.alert("Thank you!" + message);
                   // console.log("key", message);
                },
                onError(error) {
                    $("#button-submit").removeClass("hide");
                    $("#button-wait").addClass("hide");
                    _fn_AddPaymentLog(error);
                    $('.alert').show();
                    $("#responsePaymentMessage").text(error);
                    //window.alert("Oh no :(" + error);
                    //console.log("errorBQE", error);
                },

            });


            document.getElementById("button-submit")
                .addEventListener("click", function () {
                    if ($("#CardName").val() == "") {
                        $("#CardName").css("border", "1px solid #de112e");
                        return false;
                    }
                    $("#button-submit").addClass("hide");
                    $("#button-wait").removeClass("hide");                   
                    card.submit();
                });
        });
    

  
}

function _fn_SendCustomerInvoice()
{

   $.ajax({
        url: GetBaseURL() + "Register/SendInvoice",
        method: "POST",
        data:  { ClientID:  $("#ClientID").val() }, //$('#loginform').serialize(),
        success: function (response) {
           // if (response == "OK") {
               
               $('<form action="Register/SignupSuccess/" method="post"><input type="text"  name="email" value="' + $("#Email").val() +'"/></form>').appendTo('body').submit().remove();
             
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

function _fn_AddPaymentLog(PaymentStatus)
{
    $.ajax({
        url: GetBaseURL() + "Register/AddTransactionLog",
        method: "POST",
        data: { OrderID: $("#OrderID").val(), PaymentStatus: PaymentStatus, TransactionStatus : 'Failed'}, //$('#loginform').serialize(),
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

