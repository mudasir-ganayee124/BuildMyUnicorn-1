var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
debugger;

$(document).ready(() => {
  
    $("li a, li, li ul").removeClass("active");
    $("#liManage").addClass("active");
    $("#liManage_Startup").addClass("active");

    $("#GridOrder").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/Order/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "OrderID",
                    fields: {
                        OrderID: { type: "guid" },
                        StartupName: { type: "string" },
                        PlanName: { type: "string" },
                        Amount: { type: "number" },
                        Code: { type: "string" },
                        Orderstatus: { type: "string" },
                        OrderDateTime: { type: "date" }
                     
                       
                    }
                }
            },
            pageSize: 100
        },
        height: (screenHeight > 768) ? screenHeight - 310 : 670,
        sortable: false, groupable: false, filterable: true, reorderable: false, resizable: true, noRecords: true,
        selectable: "row",
        messages: {
            noRecords: "No Order Found."
        },
        pageable: { refresh: true, pageSizes: ['All', 20, 35, 50, 100], buttonCount: 5 },
        // dataBound: function () { for (var i = 0; i < $("#StartupGrid").columns.length; i++) { if (i !== 0) { $("#StartupGrid").autoFitColumn(i); } } },

        columns: [
         {
            field: "StartupName",
            title: "StartupName",
            width: 130
        },
 
        { field: "PlanName", title: "PlanName", width: 100, filterable: false },
       {
            template: '  #: Amount # #: Code # ',
            width: 100
        },
        

         {
            field: "OrderStatus",
            title: "Status",
            width: 100,
             template: '#if(data.OrderStatus == "1"){#<span class="label label-danger"> Pending</span>#}else if(data.OrderStatus == "0"){#<span class="label label-info">Completed</span>#}else if(data.OrderStatus == "2"){#<span class="label label-danger">Rejected</span>#}else if(data.OrderStatus == "3"){#<span class="label label-danger">Progress</span>#}#',
            filterable: false

         },
        { field: "OrderDateTime", title: "Order Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
        
        ]
    });

    $(".jsRecurringOrder").on("click", function () {
        $("#GridRecurringOrder").kendoGrid({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "/Order/GetAllRecurringOrder"

                    },
                },
                serverPaging: true,
                schema: {
                    type: 'json',
                    data: 'msg',
                    total: "total",
                    model: {
                        id: "OrderID",
                        fields: {
                            OrderID: { type: "guid" },
                            StartupName: { type: "string" },
                            FirstName: { type: "string" },
                            LastName: { type: "string" },
                            PlanName: { type: "string" },
                            Amount: { type: "number" },
                            OrderDateTime: { type: "date" },
                            ProcessAutomatically: { type: "string" },
                            NextOrderDateTime: { type: "date" },
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
            
            columns: [
            
            { field: "StartupName", title: "StartupName", width: 100 },
            {
                template: "#= FirstName # #= LastName #",
                title: "Name",
                width: 130
            },
            { field: "PlanName", title: "PlanName", width: 100 },
            { field: "Amount", title: "Amount", width: 60, filterable: false },
            { field: "OrderDateTime", title: "Order Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
            { field: "NextOrderDateTime", title: "Recurring Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
            {
                field: "ProcessAutomatically",
                title: "Process",
                width: 100,
                template: '#if(data.FrequencyString == "None"){#<span class="label label-danger"> None</span>#}else if(data.ProcessAutomatically == "true"){#<span class="label label-success"> Automatically</span>#}else if(data.ProcessAutomatically == "false"){#<span class="label label-info" onclick=ProcessManuallyOrder("#: OrderID #") style="cursor:pointer">Manually</span>#}#',
                filterable: false

                },

            ]
        });
    });
});

function ProcessManuallyOrder(OrderID) {

    $.ajax({
        url: GetBaseURL() + "Order/ProcessOrderAutomatically",
        method: "POST",
        data: { OrderID: OrderID },
        success: function (response) {
            $.fn.successMsg("Order recurring completed successfully");
            
            $('#GridRecurringOrder').data('kendoGrid').dataSource.read();


            //}
            //else {
            //    swal("Error!", response, "error");
            //}
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
}