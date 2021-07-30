var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);

$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketResearch").addClass("in");
    $("#liSurvey").addClass("active");
    $("#AddNewTemplate").click(function () {
        var Title = $("#Title").val();
        var Icon = $("#Icon").val();
        var BgColor = $("#BgColor").val();
        if (Title == "") { $.fn.errorMsg("Please enter title"); return false; }
        if (Icon == "") { $.fn.errorMsg("Please select icon"); return false; }
        if (BgColor == "") { $.fn.errorMsg("Please select  background color"); return false; }
        var Template = creator.text;
        var Model = {
            "Title": Title,
            "BgColor": BgColor,
            "Template": Template, "Icon": Icon,
            "SurveyTemplateType": $("#SurveyTemplateType").val()
        };
        $.ajax({
            url: GetBaseURL() + "SurveyTemplates/AddTemplate",
            type: "POST",
            data: JSON.stringify(Model),
            contentType: "application/json",
            success: function (response) {
            
                if (response == "OK") {
                    $.fn.successMsg("Template Created Successfully");
                }
                else {
                    $.fn.errorMsg(response);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.fn.errorMsg("Failed", "danger", textStatus + " " + errorThrown)
                //  toastMessage("Failed", "danger", textStatus + " " + errorThrown);
            }
        });
    });

    $("#UpdateTemplate").click(function () {
        var Title = $("#Title").val();
        var Icon = $("#Icon").val();
        var BgColor = $("#BgColor").val();
        if (Title == "") { $.fn.errorMsg("Please enter title"); return false; }
        if (Icon == "") { $.fn.errorMsg("Please select icon"); return false; }
        if (BgColor == "") { $.fn.errorMsg("Please select  background color"); return false; }
        var Template = creator.text;
        var Model = {
            "Title": Title,
            "BgColor": BgColor,
            "Template": Template, "Icon": Icon,
            "SurveyTemplateType": $("#SurveyTemplateType").val(),
            "TemplateID": $("#TemplateID").val()
            
        };
        $.ajax({
            url: GetBaseURL() + "SurveyTemplates/UpdateTemplate",
            type: "POST",
            data: JSON.stringify(Model),
            contentType: "application/json",
            success: function (response) {

                if (response == "OK") {
                    $.fn.successMsg("Template Updated Successfully");
                }
                else {
                    $.fn.errorMsg(response);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.fn.errorMsg("Failed", "danger", textStatus + " " + errorThrown)
                //  toastMessage("Failed", "danger", textStatus + " " + errorThrown);
            }
        });
    });
  
});

function GetAllSurveyTemplates()
{
    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/SurveyTemplates/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "TemplateID",
                    fields: {
                        TemplateID: { type: "guid" },
                        Title: { type: "string" },
                        SurveyTemplateType: { type: "SurveyTemplateType" },
                        CreatedDateTime: { type: "date" },
                        ModifiedDateTime: { type: "date" },
                        CreatedName: { type: "string" },
                        ModifiedName: { type: "string" }

                    }
                }
            },
            pageSize: 100
        },
        height: (screenHeight > 768) ? screenHeight - 310 : 670,
        sortable: false, groupable: false, filterable: true, reorderable: false, resizable: true, noRecords: true,
        selectable: "row",
        messages: {
            noRecords: "No Record Found."
        },
        pageable: { refresh: true, pageSizes: ['All', 20, 35, 50, 100], buttonCount: 5 },
        // dataBound: function () { for (var i = 0; i < $("#StartupGrid").columns.length; i++) { if (i !== 0) { $("#StartupGrid").autoFitColumn(i); } } },

        columns: [{
            template: '<button class="btn btn-info" title="Edit" onclick=Edit("#: TemplateID #")><i class="icon-pencil"></i></button>\
                      <button class= "btn btn-danger" title="Delete" onclick=Delete("#: TemplateID #") > <i class="fa fa-trash"></i></button> ',
            width: 100
        },
        { field: "Title", title: "Title", width: 140, filterable: false },
        
            {
                field: "SurveyTemplateType",
                title: "Type",
                width: 200,
                template: '#if(data.SurveyTemplateType == "0"){#<span class="label label-info"> Survey</span>#}else if(data.SurveyTemplateType == "1"){#<span class="label label-info">Interview</span>#}#',
                filterable: false

            },
        { field: "CreatedName", title: "Created By", width: 120, filterable: false },
        { field: "CreatedDateTime", title: "Created Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
        { field: "ModifiedName", title: "Modified By", width: 120, template: '#if(data.ModifiedName === null){#<span class="badge badge-danger"> No one Modified </span>#}else {##: ModifiedName ##  }#', filterable: false },
        { field: "ModifiedDateTime", title: "Modified Date", width: 120, template: '#if(data.ModifiedDateTime === null){#<span class="badge badge-danger"> Never Modified </span>#}else {##: kendo.toString(kendo.parseDate(ModifiedDateTime), "dd-MMM-yyyy") ##  }#', filterable: false }
        ]
    });
}

function Edit(Value) {

    window.location.replace(GetBaseURL() + "SurveyTemplates/Edit/" + Value);
 


}

function Delete(Value) {

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
                    var option = {
                        action: "Delete",
                        controller: "SurveyTemplates",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Template deleted successfully");
                            $('#Grid').data('kendoGrid').dataSource.read();
                        }
                        else
                            $.fn.successMsg(response);
                    });
                }
            },
            cancelAction: function () {
                $.alert('Delete Request is Canceled');
            }
        }
    });




}