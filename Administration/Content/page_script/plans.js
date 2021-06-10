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
                    url: "/Plan/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "PlanID",
                    fields: {
                        PlanID: { type: "guid" },
                        PlanName: { type: "string" },
                        Url: { type: "string" },
                        Amount: { type: "string" },
                        Code: { type: "string" },
                        PlanHeading: { type: "string" },
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
            noRecords: "No Plan Found."
        },
        pageable: { refresh: true, pageSizes: ['All', 20, 35, 50, 100], buttonCount: 5 },
       
       columns: [{
            template: '<button class="btn btn-info" title="Edit Plan" onclick=Edit("#: PlanID #")><i class="icon-pencil"></i></button>',
                      
            width: 50
        },
  
         {
            field: "PlanName",
            title: "Name",
            width: 70
        },
 
        { field: "Amount", title: "Amount", width: 60, filterable: false },
        { field: "PlanHeading", title: "Heading", width: 100, filterable: false },
       
        ]
    });
});

$(document).on("click", ".jsAddNewFeature", function() {

    $(".jsFeatureContainer").append('<div class="input-group mb-3 feature"> <input type="text" class="form-control" name="Value" > <input type="text" class="form-control col-md-3" name="" placeholder="Display order"> <div class="input-group-append"> <span class="input-group-text jsRemoveFeature" style="cursor:pointer"><i class="ti-minus"></i></span> </div> </div>');
});

$(document).on("click", ".jsRemoveFeature", function() {
      $(this).closest("div.feature").remove();
});
