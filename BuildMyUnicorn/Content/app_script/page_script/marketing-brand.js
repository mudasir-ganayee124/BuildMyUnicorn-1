
$(document).ready(function () {

    console.log($("#jsonModel").val());

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketing").addClass("in");
    $("#liBrand").addClass("active");
    //bg-inverse
    $('input[type="checkbox"]').click(function () {
        var id = $(this).attr("id");
        var name = $(this).attr("name");
        if ($(this).prop("checked") == true) {
            $(this).prop("checked", true);
            $(this).attr('checked', 'checked');
            //$("." + id).val(id).trigger("change");
        }
        else if ($(this).prop("checked") == false) {
            $(this).prop("checked", false);
            $(this).removeAttr('checked', 'checked');
            //$("." + id).val("").trigger("change");

        }
        var _anyCheckedInGroup = false;
        var _id = "";
        $.each($("input[name='" + name + "']"), function () {
            if ($(this).prop("checked") == true) {
                _anyCheckedInGroup = true;
                _id = $(this).attr("id");

            }


        });
        $("#_" + name).val(_id).trigger("change");
        //if (_anyCheckedInGroup) {  }
    });

    $(document).on("click", ".JsUpdate", function () {

        var MarketingBrandID = $("#MarketingBrandID").val();
        window.location.replace(GetBaseURL() + "Marketing/Brand/" + MarketingBrandID);
    });
   


});
var form = $("#frmMarketingBrand").show();

$("#frmMarketingBrand").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation: false,
    enableCancelButton: true,
    //stepsOrientation: "vertical",
    enablePagination: true,
    showFinishButtonAlways: true,
    saveState: true,
    titleTemplate: '<span class="step">#index#</span> #title#',
    labels: {
        finish: "Submit"
    },
    onStepChanging: function (event, currentIndex, newIndex) {
     
      
        return currentIndex > newIndex || !(3 === newIndex && Number($("#age-2").val()) < 18) && (currentIndex < newIndex && (form.find(".body:eq(" + newIndex + ") label.error").remove(), form.find(".body:eq(" + newIndex + ") .error").removeClass("error")), form.validate().settings.ignore = ":disabled,:hidden", form.valid())
    },
    onFinishing: function (event, currentIndex) {
        return form.validate().settings.ignore = ":disabled", form.valid()
    },
   onCanceled: function (event) { location.replace(location.href.substring(0, location.href.lastIndexOf('/')));},
    onFinished: function (event, currentIndex) {
        var BrandTouchPoints = [];
        $.each($("input[name='_BrandPouchpoint']:checked"), function () {
            BrandTouchPoints.push($(this).val());

        });
        $("#BrandTouchPointID").val(BrandTouchPoints);
        var Model = {
            "MarketingBrandID": $.trim($("#MarketingBrandID").val()),
            "ClientID": $.trim($("#ClientID").val()),
            "EntityState": $.trim($("#EntityState").val()),
            "BrandingStrategy": {
                "WhatAreWe": $.trim($("#WhatAreWe").val()), "WhyWeHere": $.trim($("#WhyWeHere").val()),
                "WhatDoHowDo": $.trim($("#WhatDoHowDo").val()), "WhatMakesDifferent": $.trim($("#WhatMakesDifferent").val()),
                "WhoWeHereFor": $.trim($("#WhoWeHereFor").val()), "WhatValueMost": $.trim($("#WhatValueMost").val()),
                "OurPersonality": $.trim($("#OurPersonality").val()), "OurAmbition": $.trim($("#OurAmbition").val()),
                "EmotionalSellingPoint": $.trim($("#EmotionalSellingPoint").val())
            }, "BrandGuidelines": {
                "PowerPresentationID": $.trim($("#PowerPresentationID").val()),
                "TemplateDownloaded": $.trim($("#TemplateDownloaded").val()),
            }, "BrandTouchPoints": { "BrandTouchPointID": $.trim($("#BrandTouchPointID").val()) }
        };
   
     

        console.log(Model); 
        $.ajax({
            url: GetBaseURL() + "Marketing/AddMarketingBrand",
            type: "POST",
            data: JSON.stringify({ Model: Model }),
            contentType: "application/json",
            dataType: "json",

            error: function (response) {
                //if (ActionType == "UPDATE")
                //    swal({
                //        title: "Success!",
                //        text: "Idea Submitted Successfuly!",
                //        icon: "success",
                //        button: "Close!",
                //    });


                //else

                //    swal({
                //        title: "Success!",
                //        text: "Idea Updated Successfuly!",
                //        icon: "success",
                //        button: "Close!",
                //    });

                setTimeout(function () { window.location.replace(GetBaseURL() + "Marketing/Brand"); }, 1000);
            },
            success: function (response) {
                alert(response);
            }
        });







        console.log($.parseJSON($("form").serialize()));
        console.log($.parseJSON($('form').serializeArray()));
        // var data = {};
        // $("form").serializeArray().map(function (x) { data[x.name] = x.value; })
        //console.log(data);
        Swal.fire("Form Submitted!", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed lorem erat eleifend ex semper, lobortis purus sed.");
    }
}), $("#frmMarketingBrand").validate({
    ignore: "input[type=hidden]",
    errorClass: "text-danger",
    successClass: "text-success",
    highlight: function (element, errorClass) {
        $(element).removeClass(errorClass)
    },
    unhighlight: function (element, errorClass) {
        $(element).removeClass(errorClass)
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element)
    },
    rules: {
        email: {
            email: !0
        }
    }
});


$(document).on("change", ".jsOptionChange", function () {
    var element = $(this).attr("data-optionchange");
    if ($(this).val() === "1")
        $("." + element).removeClass("hide");

    else
        $("." + element).addClass("hide");
});

$(document).on("click", "#jsDownloadPresentation", function () {
    $("#TemplateDownloaded").val("true");
    window.location.href = '../Content/B39E91B2-DA11-41D0-A20A-F4A097C495B0.pptx';
});
