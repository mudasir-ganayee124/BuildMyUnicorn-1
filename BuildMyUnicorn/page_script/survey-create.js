
$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketResearch").addClass("in");
    $("#liSurvey").addClass("active");
    $("#AddNewSurvey").click(function () {
        var SurveyTitle = $("#SurveyTitle").val();
        var SurveyForm = localStorage.getItem("SaveLoadSurveyCreatorExample");
        if (SurveyTitle == "") {
            toastMessage("Required", "danger", "Survey title is required");
            return false;
        }
        if (SurveyForm == "") {
            toastMessage("Required", "danger", "Survey form is  required");
            return false;
        }
        var Model = { "SurveyTitle": SurveyTitle, "SurveyForm": SurveyForm };
        $.ajax({
            url: GetBaseURL() + "Questionnaire/AddSurvey",
            type: "POST",
            data: JSON.stringify(Model),
            contentType: "application/json",
            dataType: "json",
            success: function (response) {
                if (response == "OK") {
                    toastMessage("Survey Created", "success", "Survey form created successfully");
                    setTimeout(function () { window.location.replace(GetBaseURL() + "Questionnaire"); }, 2000);
                }
                else {
                    toastMessage("Failed", "danger", response);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                toastMessage("Survey Created", "success", "Survey form created successfully");
                setTimeout(function () { window.location.replace(GetBaseURL() + "Questionnaire"); }, 2000);
                //  toastMessage("Failed", "danger", textStatus + " " + errorThrown);
            }
        });
    });


 

 
});


function copyToClipboard(element) {

    var copyText = document.getElementById(element);
    copyText.type = 'text';
    copyText.select();
    document.execCommand("copy");
    copyText.type = 'hidden';
    toastMessage("Copy", "success", "Survey link copyied to clipboard");

}

function toastMessage(heading, icon, message) {
    $.toast({
        heading: heading,
        text: message,
        position: 'top-right',
        loaderBg: '#ff6849',
        icon: icon,
        hideAfter: 1500,
        stack: 18
    });
}


