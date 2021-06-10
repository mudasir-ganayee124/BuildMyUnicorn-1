$('#loginform').parsley();

$('#frm_ResetPassword').parsley();
$("#loginform").submit(function (e) {

    e.preventDefault();
    $(".erorLabel").addClass("invisible");

    $.ajax({
        url: GetBaseURL() + "Login/ValidateUser",
        method: "POST",
        data: $('#loginform').serialize(),
        success: function (response) {
            if (response == "OK") {
                window.location.replace(GetBaseURL() + "Dashboard");
            }
            else {
                alert(response);
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text(response);}
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });  
  


});


$("#frm_Password").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Register/UpdatePassword",
        method: "POST",
        data: $('#frm_Password').serialize(),
        success: function (response) {
            if (response == "OK") {
                window.location.replace(GetBaseURL() + "Dashboard");
            }
            else {
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

$("#frm_ResetPassword").submit(function (e) {

    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Account/UpdateForgotPassword",
        method: "POST",
        data: $(this).serialize(),
        success: function (response) {
            if (response == "OK") {
                window.location.replace(GetBaseURL() + "Dashboard");
           
                
              
            }
            else {
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

$(".frmForgotPassword").submit(function (e) {
    $(".jsResetPasswordMsg").addClass("hide");
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Register/SendPasswordResetLink",
        method: "POST",
        data: $(this).serialize(),
        success: function (response) {
            if (response == "OK") {
                 window.location.replace(GetBaseURL() + "Register/ResetPasswordSuccess");
            }
            else {
                $(".jsResetPasswordMsg").removeClass("hide");
                $("#responseMessage").text(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });



});

window.Parsley.addValidator('uppercase', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var uppercases = value.match(/[A-Z]/g) || [];
        return uppercases.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) uppercase letter.'
    }
});

//has lowercase
window.Parsley.addValidator('lowercase', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var lowecases = value.match(/[a-z]/g) || [];
        return lowecases.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) lowercase letter.'
    }
});

//has number
window.Parsley.addValidator('number', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var numbers = value.match(/[0-9]/g) || [];
        return numbers.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) number.'
    }
});

//has special char
window.Parsley.addValidator('special', {
    requirementType: 'number',
    validateString: function (value, requirement) {
        var specials = value.match(/[^a-zA-Z0-9]/g) || [];
        return specials.length >= requirement;
    },
    messages: {
        en: 'Your password must contain at least (%s) special characters.'
    }
});
