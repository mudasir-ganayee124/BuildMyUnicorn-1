
$(function () {
    $(".preloader").fadeOut();
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

function RevolutCheckoutPayment(OrderPublicID)
{
      
   RevolutCheckout(OrderPublicID).then(function (instance) {

            var card = instance.createCardField({
                target: document.getElementById("card-field"),
                hidePostcodeField: true,
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
                    debugger;
                    window.alert("Oh no :(" + error);
                    console.log("errorBQE", error);
                },

            });


            document.getElementById("button-submit")
                .addEventListener("click", function () {
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

