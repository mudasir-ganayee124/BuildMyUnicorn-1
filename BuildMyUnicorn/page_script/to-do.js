$('.selectpicker').selectpicker();
$(document).on('submit', '#frmTodo', function () {
    event.preventDefault();
    $.ajax({
        url: GetBaseURL() + "ToDo/Add",
        method: "POST",
        data: $('#frmTodo').serialize(),
        error: function (response) {
            alert(response);
        },
        success: function (response) {
            toastMessage("To do Created", "success", "To do Created created successfully");
        }
    });
});

function toastMessage(heading, icon, message) {
    $.toast({
        heading: heading,
        text: message,
        position: 'top-right',
        loaderBg: '#ff6849',
        icon: icon,
        hideAfter: 1500,
        stack: 18
    });
}