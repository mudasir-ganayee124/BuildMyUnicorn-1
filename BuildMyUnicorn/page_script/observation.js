
$(document).ready(function () {
   
    console.log($("#jsonModel").val());

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketResearch").addClass("in");
    $("#liObservation").addClass("active");
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

        var ObervationID = $("#ObervationID").val();
        window.location.replace(GetBaseURL() + "MarketResearch/Observation/" + ObervationID);
    });
});
var form = $("#frmOurObservation").show();

$("#frmOurObservation").steps({
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
    onCanceled: function (event) {

        if ($("#IsRedirect").val() === "true") { window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=MarketResearch&ActionName=Observation&ModuleID=2&SectionID=10"); }
        else location.replace(location.href.substring(0, location.href.lastIndexOf('/')));
    },
    onFinished: function (event, currentIndex) {

          var Patterns =   $("#Patterns").val();
          if ($("#AnyPatterns").val() !== "7f5d70c1-5e5b-4411-a05c-224976e6feff") 
              Patterns = ""; 

    var Model = {"ObervationID":$.trim($("#ObervationID").val()),
              "ClientID":$.trim($("#ClientID").val()),
              "EntityState":$.trim($("#EntityState").val()),
              "Observation":$.trim($("#Observation").val()),
              "Collection":$.trim($("#Collection").val()),
              "AnyPatterns":$.trim($("#AnyPatterns").val()),
              "Patterns":Patterns,
              "KeyMoments":$.trim($("#KeyMoments").val()),}


        console.log(Model);
       $.ajax({
            url: GetBaseURL() + "MarketResearch/AddObservation",
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "MarketResearch/Observation"); }, 1000);
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
}), $("#frmOurObservation").validate({
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
   if ($(this).val() === "7f5d70c1-5e5b-4411-a05c-224976e6feff") 
     $("."+element).removeClass("hide");
   
   else
      $("."+element).addClass("hide");
 });
