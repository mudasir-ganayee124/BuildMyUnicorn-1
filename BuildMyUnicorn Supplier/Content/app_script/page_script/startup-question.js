
$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#StartupQuestionnav").addClass("active");
    $("#StartupQuestionnav ul").addClass("in");

    $("#AddStartupQuestion").click(function () {
        var Title = $("#Title").val();
        if (Title == "") { $.fn.errorMsg("Please enter startup question title"); return false;}
        var SurveyForm = creator.text;
        var Model = { "InterviewID": $("#InterviewID").val(), "QuestionTitle": Title, "QuestionForm": SurveyForm };
        $.ajax({
            url: GetBaseURL() + "StartupQuestion/AddQuestion",
            type: "POST",
            data: JSON.stringify(Model),
            contentType: "application/json",
            success: function (response) {

                if (response == "OK") {

                    $.fn.successMsg("Question Form Updated Successfully");

                }
                else {
                    $.fn.errorMsg(response);
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.fn.errorMsg("Failed", "danger", textStatus + " " + errorThrown);

            }
        });
    });

    //$("#AddNewSurvey").click(function () {
    //    var SurveyTitle = $("#SurveyTitle").val();
    //    var SurveyForm = creator.text; //localStorage.getItem("SaveLoadSurveyCreatorExample");
    //    if (SurveyTitle == "") {
    //        toastMessage("Required", "danger", "Survey title is required");
    //        return false;
    //    }
    //    if (SurveyForm == "") {
    //        toastMessage("Required", "danger", "Survey form is  required");
    //        return false;
    //    }
    //    var Model = { "SurveyTitle": SurveyTitle, "SurveyForm": SurveyForm };
    //    $.ajax({
    //        url: GetBaseURL() + "Questionnaire/AddQuestion",
    //        type: "POST",
    //        data: JSON.stringify(Model),
    //        contentType: "application/json",
    //        dataType: "json",
    //        success: function (response) {
    //            if (response == "OK") {
    //                toastMessage("Survey Created", "success", "Survey form created successfully");
    //                setTimeout(function () { window.location.replace(GetBaseURL() + "Questionnaire"); }, 2000);
    //            }
    //            else {
    //                toastMessage("Failed", "danger", response);
    //            }
    //        },
    //        error: function (XMLHttpRequest, textStatus, errorThrown) {
    //            toastMessage("Survey Created", "success", "Survey form created successfully");
    //            setTimeout(function () { window.location.replace(GetBaseURL() + "Questionnaire"); }, 2000);
    //            //  toastMessage("Failed", "danger", textStatus + " " + errorThrown);
    //        }
    //    });
    //});


});





