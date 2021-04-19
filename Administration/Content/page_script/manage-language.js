var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
$('#UpdateForm, #NewForm').parsley();

$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("#liLangauage").addClass("active");
    $('input[type="checkbox"]').click(function () {
        var id = $(this).attr("id");
        var name = $(this).attr("name");
        if ($(this).prop("checked") == true) {
            $(this).prop("checked", true);
            $(this).attr('checked', 'checked');
            //$("." + id).val(id).trigger("change");
        }
        else if ($(this).prop("checked") == false) {
            $(this).prop("checked", false);
            $(this).removeAttr('checked', 'checked');
            //$("." + id).val("").trigger("change");

        }


   
    });
    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/ManageLanguage/GetAllLanguage"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "LanguageID",
                    fields: {
                        LanguageID: { type: "guid" },
                        Name: { type: "string" },
                        IsDefault: { type: "string" },
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
            template: '<button class="btn btn-primary" title="Manage Language Modules" onclick=OpenLanguageModules("#: LanguageID #")><i class="mdi mdi-nfc-variant"></i></button>\
                      <button class="btn btn-info" title="Edit language" onclick=Edit("#: LanguageID #")><i class="icon-pencil"></i></button>',
                   //   <button class= "btn btn-danger" title="Delete language" onclick=Delete("#: LanguageID #") > <i class="fa fa-trash"></i></button> ',
            width: 150
        },
        { field: "Name", title: "Name", width: 130 },
        { field: "IsDefault", title: "IsDefault", width: 120, template: '#if(data.IsDefault === "true"){#<span class="badge badge-success"> Yes </span>#}else {#<span class="badge badge-danger"> No </span>#  }#', filterable: false },
        { field: "CreatedName", title: "Created By", width: 120, filterable: false },
        { field: "CreatedDateTime", title: "Created Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
        { field: "ModifiedName", title: "Modified By", width: 120, template: '#if(data.ModifiedName === null){#<span class="badge badge-danger"> No one Modified </span>#}else {##: ModifiedName ##  }#', filterable: false },
        { field: "ModifiedDateTime", title: "Modified Date", width: 120, template: '#if(data.ModifiedDateTime === null){#<span class="badge badge-danger"> Never Modified </span>#}else {##: kendo.toString(kendo.parseDate(ModifiedDateTime), "dd-MMM-yyyy") ##  }#', filterable: false }
        ]
    });
});

$("#UpdateForm").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "ManageLanguage/UpdateLanguage",
        method: "POST",
        data: $('#UpdateForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#UpdateForm')[0].reset();
                $("#EditIsDefault").prop("checked", false);
                $("#EditIsDefault").removeAttr('checked', 'checked');
                $('#UpdateModel').modal('toggle');
                swal("Success!", "Language Updated Successfully", "success");
                $('#Grid').data('kendoGrid').dataSource.read()


            }
            else {
                swal("Error!", response, "error");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
});

$("#NewForm").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "ManageLanguage/AddLanguage",
        method: "POST",
        data: $('#NewForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $("#IsDefault").prop("checked", false);
                $("#IsDefault").removeAttr('checked', 'checked');
                $('#NewForm')[0].reset();
                $('#NewModel').modal('toggle');
                swal("Success!", "Language Added Successfully", "success");
                $('#Grid').data('kendoGrid').dataSource.read();


            }
            else {
                swal("Error!", response, "error");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
});

function Edit(Value) {

    $.ajax({
        url: GetBaseURL() + "ManageLanguage/GetLanguage/",
        method: "GET",
        data: { ID: Value },
        dataType: 'json',
        success: function (data) {
          console.log(data);
            $("#EditIsDefault").prop("checked", false);
            $("#EditIsDefault").removeAttr('checked', 'checked');
            $("#LanguageID").val(data.LanguageID);
            $("#EditName").val(data.Name);
            if(data.IsDefault === true) {
               $("#EditIsDefault").prop("checked", true);
               $("#EditIsDefault").attr('checked', 'checked');
            }
            $('#UpdateModel').modal('toggle');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });


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
                        controller: "Selling",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Startup deleted successfully");
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

function OpenLanguageModules(LanguageID)
{

    $.ajax({
        url:  "/ManageLanguage/LanguageModules",
        method: "POST",
        data: { LanguageID: LanguageID },
        dataType: 'html',
        success: function (response) {
           $("#partialView").html(response);
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
}