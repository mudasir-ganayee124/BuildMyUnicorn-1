
$(document).ready(function () {
    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
    $('.js-switch').each(function () {
        new Switchery($(this)[0], $(this).data());
        this.onchange = function () {
      
            if (this.checked === true)
                $(this).attr('checked', 'checked');
            else
                $(this).removeAttr('checked', 'checked');
        };
    });

   
    console.log($("#jsonModel").val());

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulMarketing").addClass("in");
    $("#li_OnlinePresence").addClass("active");
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

        var OnlinePresenceID = $("#OnlinePresenceID").val();
        window.location.replace(GetBaseURL() + "Marketing/OnlinePresence/" + OnlinePresenceID);
    });



});
var form = $("#frmOnlinePresence").show();

$("#frmOnlinePresence").steps({
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
        if ($("#IsRedirect").val() === "true") { window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=Marketing&ActionName=OnlinePresence&ModuleID=4&SectionID=16"); }
        else location.replace(location.href.substring(0, location.href.lastIndexOf('/')));
     },
    onFinished: function (event, currentIndex) {
      
        var FeatureStatuslist = [];
        $.each($(".jsFeatureStatusContainer"), function () {

            var featureID = $(this).find(".jsFeatureID");
            var featureStatus = $(this).find(".jsFeatureStatus");
            var value3 = "Unchecked";
            if (featureStatus.prop("checked") === true) value3 = "Checked";
            if (featureID.prop("checked") === true) FeatureStatuslist.push({ "Value2": featureID.attr("id"), "value3": value3 });
          

        });
        var Model = {
            "OnlinePresenceID": $.trim($("#OnlinePresenceID").val()),
            "ClientID": $.trim($("#ClientID").val()),
            "EntityState": $.trim($("#EntityState").val()),
            "YourWebsite": {
                
                "HaveRegisteredDomain": $.trim($("#HaveRegisteredDomain").val()),
                "DomainName": $.trim($("#DomainName").val()),
                "WebPageStageID": $.trim($("#WebPageStageID").val()), "WantWebsiteAchieveID": $.trim($("#WantWebsiteAchieveID").val()),
                "ContentWebsiteCreatorID": $.trim($("#ContentWebsiteCreatorID").val()), "VisitorsAccomplish": $.trim($("#VisitorsAccomplish").val()),
                "CTA": $.trim($("#CTA").val()), "TrafficAnticipatePerMonthID": $.trim($("#TrafficAnticipatePerMonthID").val()), "UseDomainEmail": $.trim($("#UseDomainEmail").val() ),
                "SuccessMeasure": $.trim($("#SuccessMeasure").val()), "WebsitePlanMakeMoney": $.trim($("#WebsitePlanMakeMoney").val()),
                "PutWebpageAdds": $.trim($("#PutWebpageAdds").val()), "CompetitorSitelike": $.trim($("#CompetitorSitelike").val()),
                "CompetitorSitedislike": $.trim($("#CompetitorSitedislike").val())
            },
            "SocialHandles": { "Facebook": $.trim($("#Facebook").val()), "Twitter": $.trim($("#Twitter").val()), "Youtube": $.trim($("#Youtube").val()), "Instagram": $.trim($("#Instagram").val()), "TicTok": $.trim($("#TicTok").val()), "Linkedin": $.trim($("#Linkedin").val()) }
        };

        console.log(Model);
        $.ajax({
            url: GetBaseURL() + "Marketing/AddOnlinePresence",
            type: "POST",
            data: JSON.stringify({ Model: Model, ModelList: FeatureStatuslist }),
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Marketing/OnlinePresence"); }, 1000);
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
}), $("#frmOnlinePresence").validate({
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