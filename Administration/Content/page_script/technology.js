var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
$('#UpdateForm, #NewForm').parsley();

$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("#liManage").addClass("active");
    $("#liManage_Startup").addClass("active");

    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/Technology/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "ID",
                    fields: {
                        ID: { type: "guid" },
                        Value: { type: "string" },
                        DisplayOrder: { type: "string" },
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
            noRecords: "No Start Up Found."
        },
        pageable: { refresh: true, pageSizes: ['All', 20, 35, 50, 100], buttonCount: 5 },
        // dataBound: function () { for (var i = 0; i < $("#StartupGrid").columns.length; i++) { if (i !== 0) { $("#StartupGrid").autoFitColumn(i); } } },

        columns: [{
            template: '<button class="btn btn-info" title="Edit Startup" onclick=Edit("#: ID #")><i class="icon-pencil"></i></button>\
                      <button class= "btn btn-danger" title="Delete Startup" onclick=Delete("#: ID #") > <i class="fa fa-trash"></i></button> ',
            width: 100
        },
        { field: "Value", title: "Technology", width: 140, filterable: false },


        {
            field: "DisplayOrder",
            title: "DisplayOrder",
            width: 130
        },
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
        url: GetBaseURL() + "Technology/Update",
        method: "POST",
        data: $('#UpdateForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#UpdateForm')[0].reset();
                $('#UpdateModel').modal('toggle');
                swal("Success!", "Startup Updated Successfully", "success");
                $('#Grid').data('kendoGrid').dataSource.read();


            }
            else {
                swal("Error!", "Technology Updated Successfully", "error");
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
        url: GetBaseURL() + "Technology/Add",
        method: "POST",
        data: $('#NewForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#NewForm')[0].reset();
                $('#NewModel').modal('toggle');
                swal("Success!", "Technology Added Successfully", "success");
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
        url: GetBaseURL() + "Technology/Get/",
        method: "GET",
        data: { ID: Value },
        dataType: 'json',
        success: function (data) {

            $("#ID").val(data.ID);
            $("#Value").val(data.Value);
            $("#DisplayOrder").val(data.DisplayOrder);
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
                        controller: "Technology",
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