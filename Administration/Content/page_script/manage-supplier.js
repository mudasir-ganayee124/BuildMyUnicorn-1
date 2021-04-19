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
                    url: "/ManageSupplier/GetAll"
                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    SupplierID: "SupplierID",
                    fields: {
                        SupplierID: { type: "SupplierID" },
                        FirstName: { type: "string" },
                        LastName: { type: "string" },
                        Email: { type: "string" },
                        CompanyName: { type: "string" },
                        CountryName: { type: "string" },
                        AccountStatus : {type: "string"},
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
            template: '<button class="btn btn-primary" title="Details" onclick=SupplierDetail("#: SupplierID #")><i class="icon-pencil"></i></button>#if(data.IsActive == "false"){#<button class="btn btn-info" title="Active" onclick=Active("#: SupplierID #")><i class="icon-pencil"></i></button>#}else if(data.IsActive == "true"){#<button class="btn btn-info" title="In-Active" onclick=InActive("#: SupplierID #")><i class="icon-pencil"></i></button>#}#\
                       <button class= "btn btn-danger" title="Delete" onclick=Delete("#: SupplierID #") > <i class="fa fa-trash"></i></button> <button class="btn btn-primary" title="Details" onclick=AccountManager("#: SupplierID #")><i class="fa fa-lock"></i></button>',
            width: 200
        },
        {
            template: "#= FirstName # #= LastName #",
            title: "Name",
            width: 130
        },
        { field: "CompanyName", title: "Company", width: 140, filterable: false },
        { field: "CountryName", title: "Country", width: 140, filterable: false },
        {
            field: "AccountStatus",
            title: "Account Status",
            width: 200,
            template: '#if(data.AccountStatus == "1"){#<span class="label label-info"> Approved</span>#}else if(data.AccountStatus == "3"){#<span class="label label-info">Pending</span>#}else if(data.AccountStatus == "2"){#<span class="label label-danger">DisApproved</span>#}else if(data.AccountStatus == "5"){#<span class="label label-danger">Profile not updated</span>#}#',
            filterable: false

         },
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
   
    window.location.replace(GetBaseURL() + "ManageSupplier/Details?SupplierID=" + SupplierID);
}

function AccountManager(SupplierID)
{
  window.location.replace(GetBaseURL() + "ManageSupplier/AccountManager?SupplierID=" + SupplierID);
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
                        controller: "ManageSupplier",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Supplier deleted successfully");
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
                        controller: "ManageSupplier",
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
                        controller: "ManageSupplier",
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