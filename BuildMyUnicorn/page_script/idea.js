
$(document).ready(function () {
  
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#Ideanav").addClass("active");
    $("#Ideanav ul").addClass("in");
 
  
 $(document).on("click", ".JsUpdateIdea", function () {
   
        var IdeaID = $("#IdeaID").val();
        window.location.replace(GetBaseURL() + "Idea/Index/" + IdeaID);
 });

var form = $(".validation-wizard").show();
$(".validation-wizard").steps({
    headerTag: "h6",
    bodyTag: "section",
    transitionEffect: "fade",
    enableAllSteps: true,
    enableFinishButton: true,
    enableKeyNavigation:true,
    enableCancelButton: true,
    //stepsOrientation: "vertical",
    enablePagination: true,
    showFinishButtonAlways: true,
    saveState:true,
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
 
        if ($("#IsRedirect").val() === "true")
        {   window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=Idea&ActionName=Index&ModuleID=1&SectionID=1");}
           else  location.replace(location.href.substring(0, location.href.lastIndexOf('/')));
         
        
    },
    onFinished: function (event, currentIndex) {
        var StartupType = [];
        var StartupTechnology = [];
        var SellType = [];
        var ProductCharge = [];
        var MoneyRaisePlan = [];
      

        $.each($("input[name='StartupType']:checked"), function () {
            StartupType.push($(this).val());
        });
        $.each($("input[name='StartupTechnology']:checked"), function () {
            StartupTechnology.push($(this).val());
        });
        $.each($("input[name='SellType']:checked"), function () {
            SellType.push($(this).val());
        });
        $.each($("input[name='ProductCharge']:checked"), function () {
            ProductCharge.push($(this).val());
        });
        $.each($("input[name='MoneyRaisePlan']:checked"), function () {
            MoneyRaisePlan.push($(this).val());
        });

          var DomainName =   $("#DomainName").val();
          var CompanyName =   $("#CompanyName").val();
          if ($("#CompanySetup").val() !== "7f5d70c1-5e5b-4411-a05c-224976e6feff") 
              CompanyName = ""; 

          if ($("#HaveGotDomain").val() !== "7f5d70c1-5e5b-4411-a05c-224976e6feff") 
              DomainName = ""; 
        var Model = {
            "AboutYou": { "Entrepreneur": $.trim($("#Entrepreneur").val()), "YearsDoing": $.trim($("#YearsDoing").val()), "Experience": $.trim($("#Experience").val()), "Priorities": $.trim($("#Priorities").val()), "EndGoal": $.trim($("#EndGoal").val()) },
            "Company": { "CompanySetup": $.trim($("#CompanySetup").val()), "CompanyName": CompanyName, "HaveGotDomain": $.trim($("#HaveGotDomain").val()), "DomainName": DomainName, "Cofounder": $.trim($("#Cofounder").val()), "SupportTechnically": $.trim($("#SupportTechnically").val()), "BuildFrom": $.trim($("#BuildFrom").val()), "BrandThought": $.trim($("#BrandThought").val()), "CompanyMission": $.trim($("#CompanyMission").val()), "CompanyLookFeel": $.trim($("#CompanyLookFeel").val()) },
            "IdeaSelling": { "SellType": SellType.join(","), "ProductBuy": $.trim($("#ProductBuy").val()), "ProductCharge": ProductCharge.join(","), "ChargeGoing": $.trim($("#ChargeGoing").val()), "SellTo": $.trim($("#SellTo").val()), "CustomerFindPlan": $.trim($("#CustomerFindPlan").val()), "SaleStaffPlan": $.trim($("#SaleStaffPlan").val()) },
            "Money": { "BusinessCost": $.trim($("#BusinessCost").val()), "Affort": $.trim($("#Affort").val()), "MoneyRaisePlan": MoneyRaisePlan.join(","), "ProfitableMake": $.trim($("#ProfitableMake").val()), "ProfitableThinkTime": $.trim($("#ProfitableThinkTime").val()) },
            "IdeaBreakDown": { "StartupType": StartupType.join(","), "StartupTechnology": StartupTechnology.join(","), "ProblemSolve": $.trim($("#ProblemSolve").val()), "ProblemSolver": $.trim($("#ProblemSolver").val()), "FeedBackReceived": $.trim($("#FeedBackReceived").val()), "FeedBackFrom": $.trim($("#FeedBackFrom").val()), "ProductDemand": $.trim($("#ProductDemand").val()), "Niche": $.trim($("#Niche").val()), "InMarketAlready": $.trim($("#InMarketAlready").val()), "SpaceExist": $.trim($("#SpaceExist").val()), "Scalable": $.trim($("#Scalable").val()) },
            "IdeaID": $.trim($("#IdeaID").val()),
            "EntityState": $.trim($("#EntityState").val()),
            "IdeaExplain": $.trim($("#IdeaExplain").val()),
            "ProgressValue": $.trim($("#ProgressValue").val())
        };

        var option = {
                       action: "AddIdea",
                       controller: "Idea",
                       dataType: "json",
                       data: JSON.stringify(Model)
                    };
                    $.fn.ajaxCall(option).done(function (response) {               
                        if (response === "Ok") 
                            setTimeout(function () { window.location.replace(GetBaseURL() + "Idea"); });
                        else
                        $.fn.successMsg(response);
                    });
  }
}), $(".validation-wizard").validate({
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
});


$(document).on("change", ".jsOptionChange", function () {

  var element = $(this).attr("data-optionchange");
   if ($(this).val() === "7f5d70c1-5e5b-4411-a05c-224976e6feff") 
     $("."+element).removeClass("hide");
   
   else
      $("."+element).addClass("hide");
 });
