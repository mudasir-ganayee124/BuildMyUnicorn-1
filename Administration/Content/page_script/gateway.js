var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);


$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("#liManage").addClass("active");
    $("#liManage_Startup").addClass("active");

    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/Gateway/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "GatewayID",
                    fields: {
                        GatewayID: { type: "guid" },
                        GatewayName: { type: "string" },
                        BaseAddress: { type: "string" },
                        GatewayAPIID: { type: "string" },
                        GatewayAPIPassword: { type: "string" },
                        GatewayBearerToken: { type: "string" },
                        GatewayAdditional1: { type: "string" },
                        GatewayAdditional2: { type: "string" },
                        ProcessingFee: { type: "number" }
                     
                       
                    }
                }
            },
            pageSize: 100
        },
        height: (screenHeight > 768) ? screenHeight - 310 : 670,
        sortable: false, groupable: false, filterable: true, reorderable: false, resizable: true, noRecords: true,
        selectable: "row",
        messages: {
            noRecords: "No Gateway Found."
        },
        pageable: { refresh: true, pageSizes: ['All', 20, 35, 50, 100], buttonCount: 5 },
       
       columns: [{
            template: '<button class="btn btn-info" title="Edit Gateway" onclick=Edit("#: GatewayID #")><i class="icon-pencil"></i></button>',
                      
            width: 50
        },
  
         {
            field: "GatewayName",
            title: "Gateway",
            width: 70
        },
 
        { field: "BaseAddress", title: "Base Address", width: 100, filterable: false },
        { field: "GatewayAPIID", title: "API ID", width: 100, filterable: false },
        { field: "GatewayAPIPassword", title: "API Password", width: 100, filterable: false },
        { field: "GatewayBearerToken", title: "BearerToken", width: 100, filterable: false },

        { field: "ProcessingFee", title: "Processing Fee", width: 100, filterable: false },
        ]
    });
});
