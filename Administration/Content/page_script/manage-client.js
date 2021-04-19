var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("ul").removeClass("in");
    $("#ulManage").addClass("in");
    $("#ulManage_Supplier").addClass("in");
    $("#liManagement").addClass("active");
    $("#liManage_Supplier").addClass("active");
    $("#li_Supplier").addClass("active");

    
    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/ManageClient/GetAll"
                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    ClientID: "ClientID",
                    fields: {
                        ClientID: { type: "ClientID" },
                        FirstName: { type: "string" },
                        LastName: { type: "string" },
                        Email: { type: "string" },
                        CompanyName: { type: "string" },
                        CountryName: { type: "string" },
                        IsActive: { type: "string" },
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
        //dataBound: function () { for (var i = 0; i < $("#StartupGrid").columns.length; i++) { if (i !== 0) { $("#StartupGrid").autoFitColumn(i); } } },

        columns: [{
            template: '<button class="btn btn-primary" title="Details" onclick=SupplierDetail("#: ClientID #")><i class="icon-pencil"></i></button>#if(data.IsActive == "false"){#<button class="btn btn-info" title="Active" onclick=Active("#: ClientID #")><i class="icon-pencil"></i></button>#}else if(data.IsActive == "true"){#<button class="btn btn-info" title="In-Active" onclick=InActive("#: ClientID #")><i class="icon-pencil"></i></button>#}#\
                       <button class= "btn btn-danger" title="Delete" onclick=Delete("#: ClientID #") > <i class="fa fa-trash"></i></button> ',
            width: 200
        },
        {
            template: "#= FirstName # #= LastName #",
            title: "Name",
            width: 130
        },
        { field: "CompanyName", title: "Company", width: 140, filterable: false },
        { field: "Email", title: "Email", width: 180, filterable: false },
        { field: "CountryName", title: "Country", width: 140, filterable: false },
        {
            field: "IsActive",
            title: "Active",
            width: 200,
            template: '#if(data.IsActive == "true"){#<span class="label label-info"> Yes</span>#}else if(data.IsActive == "false"){#<span class="label label-danger"> No  </span>#}#',
            filterable: false

         },

        { field: "CreatedDateTime", title: "Created Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
      
       
        ]
    });
});

function SupplierDetail(SupplierID)
{
   
    window.location.replace(GetBaseURL() + "ManageClient/Details?ClientID=" + ClientID);
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
                        controller: "ManageClient",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Client deleted successfully");
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


function Active(Value) {

    $.confirm({
        title: 'Confirmation?',
        content: 'Active  Request Will Automatically  \'cancel\' in 6 seconds if you don\'t respond.',
        autoClose: 'cancelAction|8000',
        escapeKey: true,
        backgroundDismiss: false,
        typeAnimated: true,
        buttons: {
            deleteUser: {
                text: 'Active',
                btnClass: 'btn-red',
                action: function () {
                    var option = {
                        action: "Active",
                        controller: "ManageClient",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Account set to active successfully");
                            $('#Grid').data('kendoGrid').dataSource.read();
                        }
                        else
                            $.fn.successMsg(response);
                    });
                }
            },
            cancelAction: function () {
                $.alert('Active Request is Canceled');
            }
        }
    });




}


function InActive(Value) {

    $.confirm({
        title: 'Confirmation?',
        content: 'InActive  Request Will Automatically  \'cancel\' in 6 seconds if you don\'t respond.',
        autoClose: 'cancelAction|8000',
        escapeKey: true,
        backgroundDismiss: false,
        typeAnimated: true,
        buttons: {
            deleteUser: {
                text: 'InActive',
                btnClass: 'btn-red',
                action: function () {
                    var option = {
                        action: "InActive",
                        controller: "ManageClient",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Account set to In-active successfully");
                            $('#Grid').data('kendoGrid').dataSource.read();
                        }
                        else
                            $.fn.successMsg(response);
                    });
                }
            },
            cancelAction: function () {
                $.alert('InActive Request is Canceled');
            }
        }
    });




}