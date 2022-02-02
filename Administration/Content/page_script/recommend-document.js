var screenHeight = (window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight);
$('#UpdateForm, #NewForm').parsley();

$(document).ready(() => {

    $("li a, li, li ul").removeClass("active");
    $("#liGrants").addClass("active");

    $("#GridRecommendDocument").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: {
                    url: "/RecommendDocument/GetAll"

                },
            },
            serverPaging: true,
            schema: {
                type: 'json',
                data: 'msg',
                total: "total",
                model: {
                    id: "RecommendDocumentID",
                    fields: {
                        RecommendDocumentID: { type: "guid" },
                        Title: { type: "string" },
                        Description: { type: "string" },
                        VideoUrl: { type: "string" },
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
            template: '<button class="btn btn-info" title="Edit Recommend Document" onclick=Edit("#: RecommendDocumentID #")><i class="icon-pencil"></i></button>\
                      <button class= "btn btn-danger" title="Delete Recommend Document" onclick=Delete("#: RecommendDocumentID #") > <i class="fa fa-trash"></i></button> ',
            width: 100
        },
            { field: "Title", title: "Title", width: 140, filterable: false },
        {
            field: "VideoUrl",
            title: "VideoUrl",
            width: 130
        },
        
        { field: "CreatedName", title: "Created By", width: 120, filterable: false },
        { field: "CreatedDateTime", title: "Created Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },
        { field: "ModifiedName", title: "Modified By", width: 120, template: '#if(data.ModifiedName === null){#<span class="badge badge-danger"> No one Modified </span>#}else {##: ModifiedName ##  }#', filterable: false },
        { field: "ModifiedDateTime", title: "Modified Date", width: 120, template: '#if(data.ModifiedDateTime === null){#<span class="badge badge-danger"> Never Modified </span>#}else {##: kendo.toString(kendo.parseDate(ModifiedDateTime), "dd-MMM-yyyy") ##  }#', filterable: false }
        ]
    });

    $(".jsGrantLog").on("click", function () {
        $("#GridRecommendDocumentFile").kendoGrid({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "/ManageGrants/GetGrantLog"

                    },
                },
                serverPaging: true,
                schema: {
                    type: 'json',
                    data: 'msg',
                    total: "total",
                    model: {
                        //   id: "AppdendixLogID",
                        fields: {
                            AppdendixLogID: { type: "guid" },
                            Name: { type: "string" },
                            CountryName: { type: "string" },
                            FirstName: { type: "string" },
                            LastName: { type: "string" },
                            StartupName: { type: "string" },
                            AmountToBorrow: { type: "string" },
                            YearsToRepay: { type: "string" },
                            InterestRate: { type: "string" },
                            ModifiedName: { type: "string" },
                            ModifiedDateTime: { type: "date" },



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
                { field: "Name", title: "Grant Name", width: 100 },
                { field: "StartupName", title: "Startup Name", width: 100 },
                { field: "CountryName", title: "CountryName", width: 100 },
                { field: "AmountToBorrow", title: "Amount Borrow", width: 100 },
                { field: "InterestRate", title: "Interest Rate", width: 100 },
                { field: "YearsToRepay", title: "Years Repay", width: 100 },
                { field: "ModifiedDateTime", title: "Applied Date", format: "{0:dd-MMM-yyyy}", parseFormats: ["MM/dd/yyyy"], width: 120, filterable: false },



            ]
        });
    });


});

$("#UpdateForm").submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: GetBaseURL() + "RecommendDocument/Update",
        method: "POST",
        data: $('#UpdateForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#UpdateForm')[0].reset();

                $('#UpdateModel').modal('toggle');
                swal("Success!", "Record Updated Successfully", "success");
                $('#GridRecommendDocument').data('kendoGrid').dataSource.read()


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
        url: GetBaseURL() + "RecommendDocument/Add",
        method: "POST",
        data: $('#NewForm').serialize(),
        success: function (response) {
            if (response === "OK") {
                $('#NewForm')[0].reset();
                $('#NewModel').modal('toggle');
                swal("Success!", "Record Added Successfully", "success");
                $('#GridRecommendDocument').data('kendoGrid').dataSource.read();


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
        url: GetBaseURL() + "RecommendDocument/Get/",
        method: "GET",
        data: { ID: Value },
        dataType: 'json',
        success: function (data) {
          
            $("#RecommendDocumentID").val(data.RecommendDocumentID);
            $("#Title").val(data.Title);
            $("#Name").val(data.Name);          
            $("#VideoUrl").val(data.VideoUrl);
            $('#EditDescription').data("wysihtml5").editor.setValue(data.Description);
            //$('iframe').contents().find('#EditDescription').html(data.Description);
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
                        controller: "RecommendDocument",
                        dataType: "text",
                        data: { ID: Value }
                    };
                    $.fn.ajaxCall(option).done(function (response) {

                        if (response === "OK") {
                            $.fn.successMsg("Record deleted successfully");
                            $('#GridRecommendDocument').data('kendoGrid').dataSource.read();
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
