$("#jsStartupLogo").on('change', function () {

    var form_Data = new FormData();
    fileUpload = $(this).get(0);
    var files = fileUpload.files;
    if (files.length > 0) {
        for (var i = 0; i < files.length; i++) {
            var fileName = files[i].name;
            var flExt = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
            if ($.inArray(flExt.toLowerCase(), ['gif', 'png', 'jpg', 'bmp', 'jpeg']) !== -1)
                form_Data.append("file", files[i]);
            else { alert("Invalid File"); return false; }
        }
    }

    $.ajax({
        type: "POST",
        url: GetBaseURL() + 'Startup/FileUpload',
        data: form_Data,
        cache: false,
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {
            if (data !== "!OK") {
                $("#StartupLogo").val(data);
                $("#ImgStartupLogo").attr('src', GetBaseURL() + "Content/Images/" + data + "");
                // $('#imagepreview').html("<div id='my_img' class='dz-preview dz-file-preview dz-error' style='position:relative'><img src='../Content/file_upload/" + data + "' width='100%' class='img-responsives img-thumbnail' style='height:100%' /><div class='dz-error-mark'><span>X</span></div></div>");
            }

        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not Connected, Verify Network/Internet.';
            } else if (jqXHR.status === 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status === 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else if (jqXHR.status === 403) {
                msg = 'Access Denied. Contact Your Administrator.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            sweetAlert("Error in Uploading Image!", msg, "error");
        }
    });

});

$("#frmUpdateStartup").submit(function (e) {

    e.preventDefault();
    $.ajax({
        url: GetBaseURL() + "Startup/UpdateStartup",
        method: "POST",
        data: $('#frmUpdateStartup').serialize(),
        success: function (response) {
            $.toast({
                heading: 'Success',
                text: 'Startup updated successfully',
                position: 'top-right',
                loaderBg: '#ff6849',
                icon: 'success',
                hideAfter: 3500,
                stack: 6
            });
        },
        error: function (response) {
        }
    });


});