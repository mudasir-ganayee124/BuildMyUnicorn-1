var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
$('#UpdateForm, #NewForm').parsley();

$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("#liAppendix").addClass("active");


    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/Appnedix/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "AppendixID",
                    fields: {
                        AppendixID: { type: "guid" },
                        Keyword: { type: "string" },
                        Category: { type: "string" },
                       
                        Definition: { type: "string" },
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

        columns: [
       // {
          //  template: '<button class="btn btn-info" title="Edit Charge" onclick=Edit("#: ID #")><i class="icon-pencil"></i></button>\
                //      <button class= "btn btn-danger" title="Delete Charge" onclick=Delete("#: ID #") > <i class="fa fa-trash"></i></button> ',
          //  width: 100
      //  },
         

        {
            field: "Category",
            title: "Category",
            width: 80
        },
        {
            field: "Definition",
            title: "Definition",
            width: 400
        },
       // { field: "CreatedName", title: "Created By", width: 120, filterable: false },
        //{ field: "CreatedDateTime", title: "Created Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
        //{ field: "ModifiedName", title: "Modified By", width: 120, template: '#if(data.ModifiedName === null){#<span class="badge badge-danger"> No one Modified </span>#}else {##: ModifiedName ##  }#', filterable: false },
        //{ field: "ModifiedDateTime", title: "Modified Date", width: 120, template: '#if(data.ModifiedDateTime === null){#<span class="badge badge-danger"> Never Modified </span>#}else {##: kendo.toString(kendo.parseDate(ModifiedDateTime), "dd-MMM-yyyy") ##  }#', filterable: false }
        ]
    });

$(".jsAppendixLog").on("click", function() { 
 $("#GridLog").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/Appnedix/GetAllLog"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "AppdendixLogID",
                    fields: {
                        AppdendixLogID: { type: "guid" },
                        Keyword: { type: "string" },
                        AppendixType: { type: "string" },
                        IsFound: { type: "string" },
                        QueryDate: { type: "date" },  
                         CountryName: { type: "string" },
                        UserName: { type: "string" },
                        StartupName: { type: "string" },

                       

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
            template: '<button class= "btn btn-danger" title="Delete Log" onclick=Delete("#: AppdendixLogID #") > <i class="fa fa-trash"></i></button> ',
            width: 80
        },
   { field: "StartupName", title: "StartupName", width: 100 },
          { field: "UserName", title: "UserName", width: 100 },
          { field: "CountryName", title: "CountryName", width: 100 },
          { field: "Keyword", title: "Keyword", width: 100 },

        { field: "Keyword", title: "Keyword /  Message", width: 100 },
{
                    field: "AppendixType",
                    title: "AppendixType",
                    width: 100,
                    template: '#if(data.AppendixType === "1"){#<span class="label label-success"> Keyword </span>#} else {#<span class="label label-info">  Message  </span>#  }#',
                    filterable: false

                },
{
                    field: "IsFound",
                    title: "IsFound",
                    width: 80,
                    template: '#if(data.IsFound === "true"){#<span class="label label-success"> Yes </span>#} else if(data.IsFound === "false" ) {#<span class="label label-danger">  No  </span>#  }#',
                    filterable: false

                },
        // { field: "IsFound", title: "IsFound", width: 120, template: '#if(data.IsFound == true){#<span class="badge badge-danger"> Keyword Search </span>#}else {##: <span class="badge badge-danger"> Message </span> ##  }#', filterable: false },
         { field: "QueryDate", title: "Date", width: 120, template: '#if(data.QueryDate === null){#<span class="badge badge-danger"> - </span>#}else {##: kendo.toString(kendo.parseDate(QueryDate), "dd-MMM-yyyy") ##  }#', filterable: true }
        ]
    });
});
});

$("#UpdateForm").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Charge/Update",
        method: "POST",
        data: $('#UpdateForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#UpdateForm')[0].reset();
                $('#UpdateModel').modal('toggle');
                swal("Success!", "Charge Updated Successfully", "success");
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

$("#NewForm").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Charge/Add",
        method: "POST",
        data: $('#NewForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#NewForm')[0].reset();
                $('#NewModel').modal('toggle');
                swal("Success!", "Startup Added Successfully", "success");
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
        url: GetBaseURL() + "Charge/Get/",
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
                        controller: "Appnedix",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Log deleted successfully");
                            $('#GridLog').data('kendoGrid').dataSource.read();
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

$(".jsbtnImport").on("click", function () {
        var file_data = $("#file").prop("files")[0];
        if ($('#file').val()) {
            var file_data = $("#file").prop("files")[0];
            var form_data = new FormData();
            form_data.append("file", file_data);

            $.ajax({
                type: "POST",
                url: "/Appnedix/ImportFile",
                data: form_data,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if(data == "OK")
                    {
                         $.fn.successMsg("Appendix inported successfully");
                         $('#Grid').data('kendoGrid').dataSource.read();
                         $(".jsbtnImportRemove").trigger("click");
                    }
                    else
                     
                        $.fn.errorMsg(data);
                     


                },
                error: function (jqXHR, exception) {
                    ReturnResult = "ERROR";
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
                    
                }

            });



        }

        else {

            CommonFunctions.ErrorMessage("No File Uploaded", "Please upload the File");
        }
    
});
