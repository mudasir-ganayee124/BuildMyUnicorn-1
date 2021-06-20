var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);


$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("#liTransaction").addClass("active");


    $("#GridTransaction").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/Transaction/GetAllTransaction"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "TransactionID",
                    fields: {
                        TransactionID: { type: "guid" },
                        PaymentMode: { type: "string" },
                        Amount: { type: "string" },
                        TransactionDateTime: { type: "date" },

                        StartupName: { type: "string" },
                        Email: { type: "string" },
                        PlanName: { type: "string" },
                        FirstName: { type: "string" },
                        FirstName: { type: "string" },
                        LastName: { type: "string" },
                        Symbol: { type: "string" },

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



            {
                field: "PlanName",
                title: "Plan Name",

            },
            {
                field: "StartupName",
                title: "Startup Name",

            },
            {

                title: "Amount",
                template: '#: Symbol # #: Amount #',

            },
            { field: "TransactionDateTime", title: "Transaction Date", filterable: false, format: "{0:dd-MMM-yyyy hh-mm-ss}" },
            //{ field: "CreatedDateTime", title: "Created Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
            /*{ field: "ModifiedName", title: "Modified By", width: 120, template: '#if(data.ModifiedName === null){#<span class="badge badge-danger"> No one Modified </span>#}else {##: ModifiedName ##  }#', filterable: false },*/
            //{ field: "ModifiedDateTime", title: "Modified Date", width: 120, template: '#if(data.ModifiedDateTime === null){#<span class="badge badge-danger"> Never Modified </span>#}else {##: kendo.toString(kendo.parseDate(ModifiedDateTime), "dd-MMM-yyyy") ##  }#', filterable: false }
        ]
    });

    $(".jsTransactionLog").on("click", function () {
        $("#GridTransactionLog").kendoGrid({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "/Transaction/GetAllTransactionLog"

                    },
                },
                serverPaging: true,
                schema: {
                    type: 'json',
                    data: 'msg',
                    total: "total",
                    model: {
                        id: "TransactionLogID",
                        fields: {
                            TransactionLogID: { type: "guid" },
                            MerchantTransactionStatus: { type: "string" },
                            Amount: { type: "string" },
                            TransactionStatus: { type: "number" },
                            TransactionLogDateTime: { type: "date" },
                            StartupName: { type: "string" },
                            Email: { type: "string" },
                            PlanName: { type: "string" },
                            FirstName: { type: "string" },
                            FirstName: { type: "string" },
                            LastName: { type: "string" },
                            Symbol: { type: "string" },



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
                { field: "StartupName", title: "StartupName" },
                { field: "PlanName", title: "Plan Name" },
                {

                    title: "Amount",
                    template: '#: Symbol # #: Amount #',

                },
                
                {
                    field: "TransactionStatus",
                    title: "Status",

                    template: '#if(data.TransactionStatus == "0"){#<span class="label label-info"> Success</span>#}else if(data.TransactionStatus == "1"){#<span class="label label-danger"> Failed  </span>#}#',
                    filterable: false

                },
                { field: "MerchantTransactionStatus", title: "Transaction Status", width: 380 },


                { field: "TransactionLogDateTime", width: 180, title: "Transaction Date", filterable: false, format: "{0:dd-MMM-yyyy hh-mm-ss}" },
            ]
        });
    });
});

