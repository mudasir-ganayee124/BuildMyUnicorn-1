$("#jsMainList").getList();

$(document).on('submit', '.todoform', function () {
    event.preventDefault();
    if ($(this).valid) {

        var data = $(this).serializeArray();
        var formdata = new Object();
        for (let i = 0; i < data.length; i++) {
            formdata[data[i].name] = data[i].value;
        }
        formdata.AssignedMappings = getAssignedMappings();

        var isDialog = $(this).attr("data-isDialog");

        $.ajax({
            url: this.action,
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify(formdata),
            success: function (res, status, xhr) {
                toastr.success(
                    'To-do Task!',
                    res.Message,
                    {
                        timeOut: 1000,
                        fadeOut: 1000,
                        onHidden: function () {
                            if (isDialog == "true") {
                                $("#toDoList").getList();
                            }
                            else
                                window.location.href = "/TODO/EDIT/" + res.EntityID;
                        }
                    }
                );
            },
            error: handleError
        });
    }
    else
        return false;
});

$(document).on('click', '.delete', function () {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this record!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var id = $(this).attr("data-id");
                $.ajax({
                    url: "/todo/DeleteTodo",
                    method: 'post',
                    data: { id },
                    success: function (res, status, xhr) {
                        toastr.success(
                            'To-do Task!',
                            'Todo deleted successfully');
                        $("#jsMainList").getList();
                    },
                    error: handleError
                });
            }
        });


})


$("#toDoList").getList();
$("#assignedToDo").getList();

$(document).on('click', '#btnChangeStatus', function () {
    $("#changeStatusModal").modal('show');
    $("#ToDoTaskID").val($(this).attr('data-toDoId'));
});


$(document).on('submit', '#changeStatusForm', function () {
    event.preventDefault();
    $.ajax({
        url: this.action,
        method: 'post',
        contentType: false,
        processData: false,
        data: new FormData(this),
        success: function (res) {
            toastr.success(
                'To-do Task!',
                "Status Updated Successfully");
            $("#assignedToDo").getList();
            $("#changeStatusModal").modal('hide');
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
});

function getAssignedMappings() {
    var selectedOptions = $("#selectedAssigedTo").val();

    var selectedOptionsArray = []
    if (selectedOptions)
        selectedOptionsArray = JSON.parse(selectedOptions);

    var assignedToIds = $('#AssignedTo').val();

    var assignedMappings = [];

    for (let i = 0; i < assignedToIds.length; i++) {

        var obj = { AssignedToId: assignedToIds[i], EntityType: "ToDo" };
        if (selectedOptionsArray.includes(assignedToIds[i])) {
            obj.EntityState = "Old";
        }
        else {
            obj.EntityState = "New";
        }
        assignedMappings.push(obj);
    }

    for (let i = 0; i < selectedOptionsArray.length; i++) {

        if (!assignedToIds.includes(selectedOptionsArray[i])) {
            var obj = { AssignedToId: selectedOptionsArray[i], EntityType: "ToDo", EntityState: "Deleted" };
            assignedMappings.push(obj);
        }
    }
    return assignedMappings;
}

function handleError(xhr) {
    var response = JSON.parse(xhr.responseText);
    toastr.error(response.Error);
};


$('#AssignedTo').multiselect({
    includeSelectAllOption: true
});

