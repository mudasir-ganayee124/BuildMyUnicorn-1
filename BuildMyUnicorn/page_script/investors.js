
$(document).ready(function () {


    console.log($("#jsonModel").val());
    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#ulFinance").addClass("in");
    $("#liInvestors").addClass("active");
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

        var InvestorID = $("#InvestorID").val();
        window.location.replace(GetBaseURL() + "Finance/Investors/" + InvestorID);
    });



});
var form = $("#frmFinancialInvestor").show();

$("#frmFinancialInvestor").steps({
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
        if ($("#IsRedirect").val() === "true") { window.location.replace(GetBaseURL() + "ModuleCourse?ControllerName=Finance&ActionName=Investors&ModuleID=7&SectionID=30"); }
        else location.replace(location.href.substring(0, location.href.lastIndexOf('/')));
    },
    onFinished: function (event, currentIndex) {
        var Model = {
            "InvestorID": $.trim($("#InvestorID").val()),
            "ClientID": $.trim($("#ClientID").val()),
            "EntityState": $.trim($("#EntityState").val()),
            "FriendsFamily": {
                "GrownExcitedCompany": $.trim($("#GrownExcitedCompany").val()), "InvolvementLevelRequired": $.trim($("#InvolvementLevelRequired").val()),
                "Timeframe": $.trim($("#Timeframe").val()), "GetBack": $.trim($("#GetBack").val()),
                "RealRisks": $.trim($("#RealRisks").val()), "LoanOrInvestment": $.trim($("#LoanOrInvestment").val()),
                "Investing": $.trim($("#Investing").val()), "BusinessMoneyPut": $.trim($("#BusinessMoneyPut").val()),
                "PayingCustomer": $.trim($("#PayingCustomer").val()),
                "FullTimeDoing": $.trim($("#FullTimeDoing").val()), "DecisionMakingAbility": $.trim($("#DecisionMakingAbility").val())
            }, "AngelInvestor": {
                "ProblemWantSolving": $.trim($("#ProblemWantSolving").val()), "ProblemKindGroup": $.trim($("#ProblemKindGroup").val()),
                "HowMany": $.trim($("#HowMany").val()), "HowDifferentYou": $.trim($("#HowDifferentYou").val()), "CompeteWith": $.trim($("#CompeteWith").val()),
                "MakeMoney": $.trim($("#MakeMoney").val()), "MakeMoneyInvestors": $.trim($("#MakeMoneyInvestors").val()), "BusinessGrowFast": $.trim($("#BusinessGrowFast").val()),
                "Proprietary": $.trim($("#Proprietary").val()), "TractionMade": $.trim($("#TractionMade").val()), "MileStonesMet": $.trim($("#MileStonesMet").val()),
                "WordoutGet": $.trim($("#WordoutGet").val()), "SalesClose": $.trim($("#SalesClose").val()), "GetStarted": $.trim($("#GetStarted").val()),
                "SpendInvestorsMoney": $.trim($("#SpendInvestorsMoney").val()), "BusinessTeamSuited": $.trim($("#BusinessTeamSuited").val()),
                "IdeaComeup": $.trim($("#IdeaComeup").val()), "DecideTo": $.trim($("#DecideTo").val()), "ObjectionAbout": $.trim($("#ObjectionAbout").val()),
                "YourInvestors": $.trim($("#YourInvestors").val()), "PatentStrong": $.trim($("#PatentStrong").val()), "GrowFaster": $.trim($("#GrowFaster").val()),
                "MarketingExpenseRealize": $.trim($("#MarketingExpenseRealize").val()), "BusinessComparable": $.trim($("#BusinessComparable").val()),
                "WhyNeedInvestors": $.trim($("#WhyNeedInvestors").val()), "SalesMade": $.trim($("#SalesMade").val()), "CompaniesTalked": $.trim($("#CompaniesTalked").val()),
                "Interested": $.trim($("#Interested").val()), "ShownTo": $.trim($("#ShownTo").val()), "EvaluationComeup": $.trim($("#EvaluationComeup").val())
            }, "VC": {
                "GreatManagementTeam": $.trim($("#GreatManagementTeam").val()), "BigMarketOportunity": $.trim($("#BigMarketOportunity").val()),
                "TractionPositiveAchieved": $.trim($("#TractionPositiveAchieved").val()), "FoundersUnderstand": $.trim($("#FoundersUnderstand").val()),
                "InitialInvestorPitchDesk": $.trim($("#InitialInvestorPitchDesk").val()), "BusinessPotentialRisk": $.trim($("#BusinessPotentialRisk").val()),
                "ProductMilestone": $.trim($("#ProductMilestone").val()), "KeyFeatures": $.trim($("#KeyFeatures").val()), "InvestmentCapital": $.trim($("#InvestmentCapital").val()),
                "ExpectedEvaluation": $.trim($("#ExpectedEvaluation").val()), "DifferentiatedTechnology": $.trim($("#DifferentiatedTechnology").val()),
                "ReplicateTechnology": $.trim($("#ReplicateTechnology").val()), "KeyIntellectualProperty": $.trim($("#KeyIntellectualProperty").val()),
                "ViolateIntellectualProperty": $.trim($("#ViolateIntellectualProperty").val()), "ClaimIntellectualProperty": $.trim($("#ClaimIntellectualProperty").val()),
                "OwnedIntellectualProperty": $.trim($("#OwnedIntellectualProperty").val()), "DevelopedIntellectualProperty": $.trim($("#DevelopedIntellectualProperty").val()),
                "CompanyFinancialProjection": $.trim($("#CompanyFinancialProjection").val()), "Margins": $.trim($("#Margins").val()), "SalesMarketingModel": $.trim($("#SalesMarketingModel").val()),
                "ForecastingGrowth": $.trim($("#ForecastingGrowth").val()), "ExistingInvestors": $.trim($("#ExistingInvestors").val()), "AchieveMilestone": $.trim($("#AchieveMilestone").val()),
                "CurrentCash": $.trim($("#CurrentCash").val()), "StructurePlan": $.trim($("#StructurePlan").val())
            }
        };
    

        console.log(Model); 
        $.ajax({
            url: GetBaseURL() + "Finance/AddInvestors",
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

                setTimeout(function () { window.location.replace(GetBaseURL() + "Finance/Investors"); }, 1000);
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
}), $("#frmFinancialInvestor").validate({
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
