var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
$('#UpdateForm, #NewForm').parsley();

$(document).ready(() => {

 

    $("li a, li, li ul").removeClass("active");
    $("#liModuleCourse").addClass("active");
 

    $("#Grid").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/ManageModuleCourse/GetAll"
                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "ModuleCourseID",
                    fields: {
                        ModuleCourseID: { type: "guid" },
                        ModuleName: { type: "string" },
                        ModuleSectionName: { type: "string" },
                        Title: { type: "string" },
                        Duration: { type: "string" },
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
            template: '<button class="btn btn-info" title="Edit Module Course" onclick=Edit("#: ModuleCourseID #")><i class="icon-pencil"></i></button>\
                      <button class= "btn btn-danger" title="Delete Module Course" onclick=Delete("#: ModuleCourseID #") > <i class="fa fa-trash"></i></button> ',
            width: 100
        },
        { field: "ModuleName", title: "Module Name", width: 140, filterable: true },
        { field: "ModuleSectionName", title: "Module Section", width: 180, filterable: true },


        {
            field: "Title",
            title: "Title",
            width: 130
        },

        {
            field: "Duration",
            title: "Duration",
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
     var ModuleCourseOption = [];
     $.each($(this).find('.jsCourselearnValue'), function () {

           var MCOptionID = $.trim($(this).find("input[name='MCOptionID']").val());
           var Value = $.trim($(this).find("input[name='Value']").val());
           if(Value != "")
           {
              ModuleCourseOption.push({ "MCOptionID": MCOptionID, "Value": Value});
           }
        });
        var Model = {
            "ModuleCourseID": $.trim($(this).find('input[name="ModuleCourseID"]').val()),
            "ModuleID": $.trim($(this).find('select[name="ModuleID"]').val()),
            "ModuleSectionID": $.trim($(this).find('select[name="ModuleSectionID"]').val()),
            "Duration": $.trim($(this).find('input[name="Duration"]').val()),
            "VideoUrl": $.trim($(this).find('input[name="VideoUrl"]').val()),
            "Title": $.trim($(this).find('input[name="Title"]').val()),
            "Description": $.trim($(this).find('textarea[name="Description"]').val()),
            "ModuleCourseOption": ModuleCourseOption
        };

  console.log(Model); 
       
    $.ajax({
        url: GetBaseURL() + "ManageModuleCourse/Update",
        method: "POST",
        data: JSON.stringify({ Model: Model }),
        contentType: "application/json",
        success: function (response) {
            if (response === "OK") 
             {
                $('#UpdateForm')[0].reset();
                $('#UpdateModel').modal('toggle');
                swal("Success!", "Record Updated Successfully", "success");
                $('#Grid').data('kendoGrid').dataSource.read();
             }
            else 
            {
                swal("Error!", response, "error");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(".erorLabel").removeClass("invisible");
            $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
        }
    });
});

$(document).on("click", ".jsCourselearnAdd", function() {
   var Item = '<div class="input-group mb-3 jsCourselearnValue"> <input type="hidden" class="form-control" name="MCOptionID"><input type="text" class="form-control" name="Value" placeholder="Learn" aria-label="Username"><div class="input-group-append"> <span class="input-group-text jsCourselearnRemove" style="cursor:pointer"><i class="ti-minus"></i></span></div></div>';
   $(".jsCourselearnContainer").append(Item);

});

$(document).on("click", ".jsCourselearnRemove", function() {

 $(this).closest("div.input-group").remove();

});

$(document).on("change", "#ModuleID, #EditModuleID", function(){

   bindOption($(this).val());

});

$("#NewForm").submit(function (e) {
    e.preventDefault();
     var ModuleCourseOption = [];
     $.each($(this).find('.jsCourselearnValue'), function () {

           var MCOptionID = $.trim($(this).find("input[name='MCOptionID']").val());
           var Value = $.trim($(this).find("input[name='Value']").val());
           if(Value != "")
           {
              ModuleCourseOption.push({ "MCOptionID": MCOptionID, "Value": Value});
           }
        });
        var Model = {
            "ModuleID": $.trim($(this).find('select[name="ModuleID"]').val()),
            "ModuleSectionID": $.trim($(this).find('select[name="ModuleSectionID"]').val()),
            "Duration": $.trim($(this).find('input[name="Duration"]').val()),
            "VideoUrl": $.trim($(this).find('input[name="VideoUrl"]').val()),
            "Title": $.trim($(this).find('input[name="Title"]').val()),
            "Description": $.trim($(this).find('textarea[name="Description"]').val()),
            "ModuleCourseOption": ModuleCourseOption
        };
       
    $.ajax({
        url: GetBaseURL() + "ManageModuleCourse/Add",
        method: "POST",
        data: JSON.stringify({ Model: Model }),
        contentType: "application/json",
        //dataType: "json",
        success: function (response) {

            if (response === "OK") {
                $('#NewForm')[0].reset();
                $('#NewModel').modal('toggle');
                swal("Success!", "Record Added Successfully", "success");
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

function Edit(ModuleCourseID) {

    $.ajax({
        url: GetBaseURL() + "ManageModuleCourse/Get",
        method: "GET",
        data: { ID : ModuleCourseID },
        dataType: 'json',
        success: function (data) {
            $("#EditMCOptionID").val("");
            $("#EditValue").val("");
            $('.jsCourselearnContainerEdit .jsCourselearnValue').not(':first').remove();
            $("#ModuleCourseID").val(data.ModuleCourseID);
            $("#EditModuleID").val(data.ModuleID);
            bindOption(data.ModuleID);
            $("#EditModuleSectionID").val(data.ModuleSectionID);
            $("#Duration").val(data.Duration);
            $("#Title").val(data.Title);
            $("#VideoUrl").val(data.VideoUrl);
            $("#Description").val(data.Description);
            $.each(data.ModuleCourseOption, function (key, val) {
               if(key == 0)
               {
                  $("#EditMCOptionID").val(val.MCOptionID);
                  $("#EditValue").val(val.Value);
               }
               else {

               var Item = '<div class="input-group mb-3 jsCourselearnValue"> <input type="hidden" class="form-control" name="MCOptionID" value="'+val.MCOptionID+'"><input type="text" class="form-control" name="Value" value="'+val.Value+'"><div class="input-group-append"> <span class="input-group-text jsCourselearnRemove" style="cursor:pointer"><i class="ti-minus"></i></span></div></div>';
               $(".jsCourselearnContainerEdit").append(Item);
               }
              
            });
            
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
                        controller: "ManageModuleCourse",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Record deleted successfully");
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

function bindOption(ModuleID)
{
   $("#ModuleSectionID").html("");
   $("#EditModuleSectionID").html("");
    $.ajax({
            url: GetBaseURL() + "ManageModuleCourse/GetModuleSection",
            method: "POST",
            data: JSON.stringify({ ModuleID: ModuleID }),
            contentType: "application/json",
            async : false,
            success: function (response) {
               var obj = "<option>--Select Module Section--</option>"
                $.each(response, function (key, val) {
                obj += "<option value="+val.ModuleSectionID+">"+val.SectionDisplayAs+"</option>"
                });
                $("#ModuleSectionID").html(obj);
                $("#EditModuleSectionID").html(obj);

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $(".erorLabel").removeClass("invisible");
                $(".errorMessage").text("Status: " + textStatus + "Error: " + errorThrown);
            }
        });
}
