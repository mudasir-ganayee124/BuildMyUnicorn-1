$("#jsMainList").getList();
$(document).ready(function () {
   
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketResearch").addClass("in");
    $("#liInterview").addClass("active");
    $("#btnAddInterview").click(function () {
        var SurveyTitle = $("#Title").val();
        var SurveyForm = $("#Form").val();
        if (SurveyTitle == "") {
            toastMessage("Required", "danger", "Interview title is required");
            return false;
        }
        if (SurveyForm == "") {
            toastMessage("Required", "danger", "Interview form is  required");
            return false;
        }
        var Model = { "Title": SurveyTitle, "Form": SurveyForm };
        $.ajax({
            url: GetBaseURL() + "Interview/Add",
            type: "POST",
            data: JSON.stringify(Model),
            contentType: "application/json",
            dataType: "json",
           success: function (response) {
                if (response == "OK") {
                    toastMessage("Interview Created", "success", "Survey form created successfully");
                    setTimeout(function () { window.location.replace(GetBaseURL() + "Interview"); }, 2000);
                }
                else {
                    toastMessage("Failed", "danger", response);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                toastMessage("Interview Created", "success", "Interview form created successfully");
                setTimeout(function () { window.location.replace(GetBaseURL() + "Interview"); }, 2000);
              //  toastMessage("Failed", "danger", textStatus + " " + errorThrown);
            }
        });
    });
    $(document).on("click", ".jsDeleteInterview", function () {
        var InterviewID = $(this).data("interview-id");
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover the record!",
            //icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: "/Interview/Delete",
                        data: { InterviewID: InterviewID },
                        success: function (data) {
                            if (data !== "OK") {
                               // $("tr." + SurveyID).addClass("hide");
                               // sweetAlert("Error Deleting Role!", data, "error");
                            }
                            $("#jsMainList").getList();
                            //else {
                            //    swal("Deleted!", "Role Deleted Successfully", "success");
                            //    $('#tbl_roles').data('kendoGrid').dataSource.read();
                            //}
                        },
                        error: function (jqXHR, exception) {
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not Connected, Verify Network/Internet.';
                            } else if (jqXHR.status === 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status === 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else if (jqXHR.status === 403) {
                                msg = 'Access Denied. Contact Your Administrator.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                           // sweetAlert("Error Deleting Role!", msg, "error");
                        }
                    });
                    //swal("Poof! Your imaginary file has been deleted!", {
                    //    icon: "success",
                    //});
                } 
            });
        
    });

    $(document).on("click", ".jsActiveSurvey", function () {
        var SurveyID = $(this).data("survey-id");
        swal({
            title: "Are you sure?",
            text: "Once Active, your survey will be available to user(s)!",
            //icon: "warning",
            buttons: true,
            dangerMode: false,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: "/INterview/EditSurveyStatus",
                        data: { SurveyID: SurveyID },
                        success: function (data) {
                            if (data !== "OK") {
                                // $("tr." + SurveyID).addClass("hide");
                                // sweetAlert("Error Deleting Role!", data, "error");
                            }
                            $("#jsMainList").getList();
                            //else {
                            //    swal("Deleted!", "Role Deleted Successfully", "success");
                            //    $('#tbl_roles').data('kendoGrid').dataSource.read();
                            //}
                        },
                        error: function (jqXHR, exception) {
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not Connected, Verify Network/Internet.';
                            } else if (jqXHR.status === 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status === 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else if (jqXHR.status === 403) {
                                msg = 'Access Denied. Contact Your Administrator.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                            // sweetAlert("Error Deleting Role!", msg, "error");
                        }
                    });
                    //swal("Poof! Your imaginary file has been deleted!", {
                    //    icon: "success",
                    //});
                }
            });

    });

    $(document).on("click", ".jsInActiveSurvey", function () {
        var SurveyID = $(this).data("survey-id");
        swal({
            title: "Are you sure?",
            text: "Once in active no one will have access to the survey",
           // icon: "warning",
            buttons: true,
            dangerMode: false,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        type: "POST",
                        url: "/Questionnaire/EditSurveyStatus",
                        data: { SurveyID: SurveyID },
                        success: function (data) {
                            if (data !== "OK") {
                                // $("tr." + SurveyID).addClass("hide");
                                // sweetAlert("Error Deleting Role!", data, "error");
                            }
                            $("#jsMainList").getList();
                            //else {
                            //    swal("Deleted!", "Role Deleted Successfully", "success");
                            //    $('#tbl_roles').data('kendoGrid').dataSource.read();
                            //}
                        },
                        error: function (jqXHR, exception) {
                            var msg = '';
                            if (jqXHR.status === 0) {
                                msg = 'Not Connected, Verify Network/Internet.';
                            } else if (jqXHR.status === 404) {
                                msg = 'Requested page not found. [404]';
                            } else if (jqXHR.status === 500) {
                                msg = 'Internal Server Error [500].';
                            } else if (exception === 'parsererror') {
                                msg = 'Requested JSON parse failed.';
                            } else if (exception === 'timeout') {
                                msg = 'Time out error.';
                            } else if (exception === 'abort') {
                                msg = 'Ajax request aborted.';
                            } else if (jqXHR.status === 403) {
                                msg = 'Access Denied. Contact Your Administrator.';
                            } else {
                                msg = 'Uncaught Error.\n' + jqXHR.responseText;
                            }
                            // sweetAlert("Error Deleting Role!", msg, "error");
                        }
                    });
                    //swal("Poof! Your imaginary file has been deleted!", {
                    //    icon: "success",
                    //});
                }
            });

    });
});





$("#MyForm").submit(function (e) {
    e.preventDefault();
    $('#MyForm input[type="text"]').each(function () {
        alert($(this).attr('name'));
        alert($(this).val());
    });
    $('#MyForm select[type="text"]').each(function () {
        alert($(this).attr('name'));
        alert($(this).val());
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

function toastMessage(heading,icon, message)
{
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


