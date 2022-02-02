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
                        PlanRecurringID: {type: "guid"},
                        PlanName: { type: "string" },
                        Url: { type: "string" },
                        Amount: { type: "string" },
                        Code: { type: "string" },
                        PlanHeading: { type: "string" },
                        DurationString: { type: "string" },
                        FrequencyString: { type: "string" },
                        ProcessAutomatically: { type: "string" },
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
           template: '<button class="btn btn-info" title="Edit Plan" onclick=Edit("#: PlanID #")><i class="icon-pencil"></i></button>\
                    <button class= "btn btn-success" title="Recurring" onclick=PlanRecurring("#: PlanRecurringID #") > <i class="icon-lock"></i></button>\
                    <button class= "btn btn-danger" title="Delete Plan" onclick=Delete("#: PlanID #") > <i class="fa fa-trash"></i></button>',
                      
            width: 50
        },
  
         {
            field: "PlanName",
            title: "Name",
            width: 70
        },
 
        { field: "Amount", title: "Amount", width: 60, filterable: false },
        { field: "DurationString", title: "Duration", width: 100, filterable: false },
       /* { field: "FrequencyString", title: "Frequency", width: 100, filterable: false },*/
        { field: "FrequencyString", title: "Frequency", width: 100, filterable: false },
        {
            field: "ProcessAutomatically",
            title: "Process",
            width: 100,
            template: '#if(data.FrequencyString == "None"){#<span class="label label-danger"> None</span>#}else if(data.ProcessAutomatically == "true"){#<span class="label label-success"> Automatically</span>#}else if(data.ProcessAutomatically == "false"){#<span class="label label-info">Manually</span>#}#',
            filterable: false

        },
       
        ]
    });
});

$(document).on("click", ".jsAddNewFeature", function() {

    $(".jsFeatureContainer").append('<div class="input-group mb-3 feature"> <input type="text" class="form-control" name="Value" > <input type="text" class="form-control col-md-3" name="ValueDisplayOrder" placeholder="Display order"> <div class="input-group-append"> <span class="input-group-text jsRemoveFeature" style="cursor:pointer"><i class="ti-minus"></i></span> </div> </div>');
});

$(document).on("click", ".jseditAddNewFeature", function () {

    $(".jseditFeatureContainer").append('<div class="input-group mb-3 editfeature"> <input type="text" class="form-control" name="Value" > <input type="text" class="form-control col-md-3" name="ValueDisplayOrder" placeholder="Display order"> <div class="input-group-append"> <span class="input-group-text jseditRemoveFeature" style="cursor:pointer"><i class="ti-minus"></i></span> </div> </div>');
});

$(document).on("click", ".jsRemoveFeature", function() {
      $(this).closest("div.feature").remove();
});

$(document).on("click", ".jseditRemoveFeature", function () {
    $(this).closest("div.editfeature").remove();
});


$("#NewForm").submit(function (e) {
    e.preventDefault();

    var _AttributeList = [];
    $(".feature").each(function () {
        if ($.trim($(this).find("input[name='Value']").val()) != "")
        _AttributeList.push({ Attribute: $(this).find("input[name='Value']").val(), DisplayOrder: $(this).find("input[name='ValueDisplayOrder']").val()});


    });
    
    var Model = {
        "PlanName": $.trim($(this).find('input[name="PlanName"]').val()),
        "CurrencyID": $.trim($(this).find('select[name="CurrencyID"]').val()),
        "Amount": $.trim($(this).find('input[name="Amount"]').val()),
        "Duration": $.trim($(this).find('select[name="Duration"]').val()),
        "PlanHeading": $.trim($(this).find('input[name="PlanHeading"]').val()),
        "Method": $.trim($(this).find('select[name="Method"]').val()),
        "PlanSubHeading": $.trim($(this).find('input[name="PlanSubHeading"]').val()),
        "Url": $.trim($(this).find('input[name="Url"]').val()),
        "DisplayOrder": $.trim($(this).find('input[name="DisplayOrder"]').val()),
        "PlanAttribute": _AttributeList
    };
    debugger;
    $.ajax({
        url: GetBaseURL() + "Plan/Add",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({ Model: Model }),
        success: function (response) {
            if (response === "OK") {


                $('#NewForm')[0].reset();
                $('#NewModel').modal('toggle');
                swal("Success!", "Plan Added Successfully", "success");
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

$("#UpdateForm").submit(function (e) {
    e.preventDefault();

    var _AttributeList = [];
    $(".editfeature").each(function () {
        if ($.trim($(this).find("input[name='Value']").val()) != "")
            _AttributeList.push({ Attribute: $(this).find("input[name='Value']").val(), DisplayOrder: $(this).find("input[name='ValueDisplayOrder']").val() });


    });

    var Model = {
        "PlanID": $.trim($(this).find('input[name="PlanID"]').val()),
        "PlanName": $.trim($(this).find('input[name="PlanName"]').val()),
        "CurrencyID": $.trim($(this).find('select[name="CurrencyID"]').val()),
        "Duration": $.trim($(this).find('select[name="Duration"]').val()),
        "Method": $.trim($(this).find('select[name="Method"]').val()),
        "Amount": $.trim($(this).find('input[name="Amount"]').val()),
        "PlanHeading": $.trim($(this).find('input[name="PlanHeading"]').val()),
        "PlanSubHeading": $.trim($(this).find('input[name="PlanSubHeading"]').val()),
        "Url": $.trim($(this).find('input[name="Url"]').val()),
        "DisplayOrder": $.trim($(this).find('input[name="DisplayOrder"]').val()),
        "PlanAttribute": _AttributeList
    };
    debugger;
    $.ajax({
        url: GetBaseURL() + "Plan/Update",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({ Model: Model }),
        success: function (response) {
            if (response === "OK") {


                $('#UpdateForm')[0].reset();
                $('#UpdateModel').modal('toggle');
                swal("Success!", "Plan Updated Successfully", "success");
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

$("#UpdateRecurring").submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Plan/UpdateRecurring",
        method: "POST",
        data: $('#UpdateRecurring').serialize(),
        success: function (response) {
            if (response === "OK") {


                $('#UpdateRecurring')[0].reset();
                $('#PlanRecurringModel').modal('toggle');
                swal("Success!", "Plan Updated Successfully", "success");
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
        url: GetBaseURL() + "Plan/Get/",
        method: "GET",
        data: { ID: Value },
        dataType: 'json',
        success: function (obj) {
            $(".jseditFeatureContainer").empty();
            var form = $("#UpdateForm");
                form.find('input[name="PlanID"]').val(obj.PlanID),
                form.find('input[name="PlanName"]').val(obj.PlanName),
                form.find('input[name="Amount"]').val(obj.Amount),
                form.find('select[name="CurrencyID"]').val(obj.CurrencyID).trigger("change"),
                    form.find('select[name="Method"]').val(obj.Method).trigger("change"),
                form.find('select[name="Duration"]').val(obj.Duration).trigger("change"),
                form.find('input[name="PlanHeading"]').val(obj.PlanHeading),
                form.find('input[name="PlanSubHeading"]').val(obj.PlanSubHeading),
                form.find('input[name="Url"]').val(obj.Url),
                form.find('input[name="DisplayOrder"]').val(obj.DisplayOrder);
                $(".jseditFeatureContainer").append('<div class="input-group mb-3 editfeature"> <input type="text" class="form-control jsAttribute" name="Value"  > <input type="text" class="form-control col-md-3 jsDisplayOrder" name="ValueDisplayOrder" placeholder="Display order" > <div class="input-group-append"> <span class="input-group-text jseditAddNewFeature" style="cursor:pointer"><i class="ti-plus"></i></span> </div> </div>');
                if(obj.PlanAttribute != "");
                $.each(obj.PlanAttribute, function (key, val) {
                    if (key == 0) {
                        $(".jsAttribute").val(val.Attribute);
                        $(".jsDisplayOrder").val(val.DisplayOrder);
                        
                    }
                    else
                        $(".jseditFeatureContainer").append('<div class="input-group mb-3 editfeature"> <input type="text" class="form-control" name="Value" value="'+val.Attribute+'" > <input type="text" class="form-control col-md-3" name="ValueDisplayOrder" placeholder="Display order" value="'+val.DisplayOrder+'"> <div class="input-group-append"> <span class="input-group-text jseditRemoveFeature" style="cursor:pointer"><i class="ti-minus"></i></span> </div> </div>');
                    
                  
                });
                $('#UpdateModel').modal('toggle');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });


}

function PlanRecurring(Value)
{
    $.ajax({
        url: GetBaseURL() + "Plan/GetPlanRecurring/",
        method: "GET",
        data: { ID: Value },
        dataType: 'json',
        success: function (data) {
            $("#PlanRecurringID").val(Value);
            var process = data.ProcessAutomatically == true ? "true" : "false";
            $("#Frequency").val(data.Frequency).trigger("change");
            $("#ProcessAutomatically").val(process).trigger("change");
            $('#PlanRecurringModel').modal('toggle');
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
                        controller: "Plan",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Plan deleted successfully");
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