var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);


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
});
