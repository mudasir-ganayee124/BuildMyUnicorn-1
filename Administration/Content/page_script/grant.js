var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
$('#UpdateForm, #NewForm').parsley();
GetCountryList();

$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("#liGrants").addClass("active");

    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/ManageGrants/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "GrantID",
                    fields: {
                        GrantID: { type: "guid" },
                        Name: { type: "string" },
                        Type: { type: "string" },
                        Description: { type: "string" },
                        Website: { type: "string" },
                        VideoUrl: { type: "string" },
                        CountryName: { type: "string" },
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
            template: '<button class="btn btn-info" title="Edit Grant" onclick=Edit("#: GrantID #")><i class="icon-pencil"></i></button>\
                      <button class= "btn btn-danger" title="Delete Grant" onclick=Delete("#: GrantID #") > <i class="fa fa-trash"></i></button> ',
            width: 100
        },
            { field: "Name", title: "Name", width: 140, filterable: false },
 {
            field: "Type",
            title: "Type",
            width: 130
        },


        {
            field: "Description",
            title: "Description",
            width: 130
        },
        {
            field: "Website",
            title: "Website",
            width: 130
        },
        {
            field: "VideoUrl",
            title: "VideoUrl",
            width: 130
        },
        {
            field: "CountryName",
            title: "CountryName",
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
        url: GetBaseURL() + "ManageGrants/Update",
        method: "POST",
        data: $('#UpdateForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#UpdateForm')[0].reset();
                $('#UpdateModel').modal('toggle');
                swal("Success!", "Grant Updated Successfully", "success");
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
        url: GetBaseURL() + "ManageGrants/Add",
        method: "POST",
        data: $('#NewForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#NewForm')[0].reset();
                $('#NewModel').modal('toggle');
                swal("Success!", "Grant Added Successfully", "success");
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
        url: GetBaseURL() + "ManageGrants/Get/",
        method: "GET",
        data: { ID: Value },
        dataType: 'json',
        success: function (data) {
            $("#GrantID").val(data.GrantID);
            $("#Name").val(data.Name);
            $("#Type").val(data.Type);
            $("#Website").val(data.Website);
            $("#VideoUrl").val(data.VideoUrl);
            $("#Description").val(data.Description); 
            $("#EditCountryID").val(data.CountryID).trigger('change'); 
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
                        controller: "ManageGrants",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Grant deleted successfully");
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

function GetCountryList()
{
    $.ajax({
        url: GetBaseURL() + "ManageGrants/GetCountryList",
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            console.log(res);

            var options = "<option value=''>--Select your Country--</option>";
            $.each(res.country, function (key, val) {

                options += "<option value=" + val.CountryID + ">" + val.CountryName + "</option>";
            });
            $('#CountryID').html(options).trigger('change');
            $('#EditCountryID').html(options).trigger('change');
        }
    });
}