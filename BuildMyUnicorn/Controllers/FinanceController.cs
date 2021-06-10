using BuildMyUnicorn.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business_Model.Model;
using System.ComponentModel;
using Business_Model.Helper;
using Syncfusion.XlsIO;
using Syncfusion.DocIO;
using System.Drawing;
using System.Threading.Tasks;

namespace BuildMyUnicorn.Controllers
{
    public class FinanceController : WebController
    {
        public static List<PersonalSurvivalBudget> BudgetList;
        public static List<SaleforeCast> SaleforecastList;
        public static List<Cashflowforecast> CashflowforecastList;
        public static LoanCalculator LoanCalculator;
        // GET: Business
        public ActionResult Investors(string id)
        {
            int Count = 0;
            if (string.IsNullOrEmpty(id))
                Count = new FinanceManager().ExistFinanceInvestor(Guid.Empty);
            else
            {
                Guid InvestorID;
                bool isValid = Guid.TryParse(id, out InvestorID);
                if (isValid) Count = new FinanceManager().ExistFinanceInvestor(InvestorID);
                else return RedirectToAction("BadRequest", "ErrorHandler");
            }
            int State = Count > 0 && string.IsNullOrEmpty(id) ? (int)EntityState.Old : (int)EntityState.New;
            if (State == (int)EntityState.New && string.IsNullOrEmpty(id))
            {
                ModuleCourselog objlog = new ModuleCourselog();
                objlog.ModuleID = Module.TheBusiness;
                objlog.ModuleSectionID = ModuleSection.TheBusiness_Businessoverview;
                if (new Master().AddModuleCourselog(objlog).Status == (int)ResponseType.Redirect && new Master().ExistModuleCourse((int)Module.TheBusiness, (int)ModuleSection.TheBusiness_Businessoverview) > 0)
                {

                    return RedirectToAction("Index", "ModuleCourse", new
                    {
                        ControllerName = "Finance",
                        ActionName = "Investors",
                        ModuleID = (int)Module.Finance,
                        SectionID = (int)ModuleSection.Finance_FindingInvestors
                    });
                }
            }

            ViewBag.Video = new Master().GetSectionModuleVideo((int)Module.Finance, (int)ModuleSection.Finance_FindingInvestors);
            return View(State);

         }

        public ActionResult Grants()
        {

           
           
            IEnumerable<Grants> Model = new FinanceManager().GetCountGrantByMonth(0, ViewBag.Client.CountryID);
            ViewBag.GrantLog = new FinanceManager().GetClientSurvivalBudget();

            if (Model == null)
                return PartialView("_BadRequest");
            return View(Model);
        }

        public ActionResult PersonalSurvivalBudge(string id)
        {
            ViewBag.GrantID = id.Remove(id.Length - 2); ;
            if (id.Substring(id.Length - 1) == "0")
            {
                GrantSurvivalBudget MOdel = new GrantSurvivalBudget();
                MOdel.GrantID = Guid.Parse(ViewBag.GrantID);
                MOdel.GrantStatus = GrantStatus.Continue;
                new FinanceManager().AddGrantSurvivalBudget(MOdel);
            }
            if (id.Substring(id.Length - 1) == "2")
            {
                ViewBag.GrantStatus = GrantStatus.Locked;
            }
            return View();

        }

        public ActionResult BudgetApplied(string id)
        {
            ViewBag.GrantSurvivalBudget = new FinanceManager().GetGrantSurvivalBudget(Guid.Parse(id));
            ViewBag.GrantID = id;
            return View();

        }

        public ActionResult GetPartnerOnePersonalSurviveBudget(string id)
        {

            BudgetList = new FinanceManager().GetPersonalSurvivalBudget(Guid.Parse(id)).ToList();
            //  ViewBag.BudgetListPartnerOne = BudgetList.Where(x=>x.PartnerCount == 1);
            // ViewBag.ExpensesPartnerOne = BudgetList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Expenses);
            //ViewBag.IncomePartnerOne = BudgetList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Income);
            return PartialView("_PartnerOnePersonalSurviveBudget", BudgetList.Where(x => x.PartnerCount == 1));

        }

        public ActionResult GetPartnerTwoPersonalSurviveBudget(string id)
        {

            BudgetList = new FinanceManager().GetPersonalSurvivalBudget(Guid.Parse(id)).ToList();
            // ViewBag.BudgetListPartnerTwo = BudgetList.Where(x => x.PartnerCount == 2);
            // ViewBag.ExpensesPartnerTwo = BudgetList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Expenses);
            //ViewBag.IncomePartnerTwo = BudgetList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Income);
            return PartialView("_PartnerTwoPersonalSurviveBudget", BudgetList.Where(x => x.PartnerCount == 2));

        }
        public ActionResult GetLoanCalculator(string id)
        {

            LoanCalculator = new FinanceManager().GetLoanCalculator(Guid.Parse(id));
            return PartialView("_LoanCalculatorPartial", LoanCalculator);

        }
        public ActionResult GetSalesforecast(string id)
        {
            SaleforecastList = new FinanceManager().GetSalesforecast(Guid.Parse(id)).ToList();
            ViewBag.SaleforecastList = SaleforecastList;
            return PartialView("_SalesforecastPartial");

        }

        public ActionResult GetCashflowforecast(string id)
        {
            CashflowforecastList = new FinanceManager().GetCashflowforecast(Guid.Parse(id)).ToList();
            ViewBag.CashflowforecastList = CashflowforecastList;
            ViewBag.Expenditure = CashflowforecastList.Where(x => x.ForecastType == (int)OptionType.Cashflowforecast_Expenditure);
            ViewBag.Income = CashflowforecastList.Where(x => x.ForecastType == (int)OptionType.Cashflowforecast_Income);
            return PartialView("_CashflowforecastPartial", CashflowforecastList);

        }

        public void UpdateSurvivalBudgetModel(PersonalSurvivalBudget Model)
        {
            var data = BudgetList;
            //PersonalType
            //PartnerCount
            //OptionMasterID
            //PersonalSurvivalBudgetID
        }



        public ActionResult PitchDecks()
        {
            return View();
        }
        public ActionResult PitchDeckPage(string id)
        {
            switch (id)
            {
                case "1":
                    return PartialView("_PitchDeckPage_1");

                case "2":
                    return PartialView("_PitchDeckPage_2");
                case "3":
                    return PartialView("_PitchDeckPage_3");
                case "4":
                    return PartialView("_PitchDeckPage_4");
                case "5":
                    return PartialView("_PitchDeckPage_5");
                case "6":
                    return PartialView("_PitchDeckPage_6");
            }
            return PartialView("_PitchDeckPage_1");
        }
        public ActionResult New(int Type)
        {
            if ((int)ModuleSection.Finance_FindingInvestors == Type)
            {
                Investor obj = new Investor();
                _Investor Model = new FinanceManager().GetFinanceInvestor();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetInvestorDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {
                    obj = GetInvestorModelData(Model);
                });


                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_NewInvestorPartial", obj);

            }
            return PartialView("_NewFinanceInvestorPartial");

        }

        public ActionResult Detail(int Type)
        {
            if ((int)ModuleSection.Finance_FindingInvestors == Type)
            {
                Investor obj = new Investor();
                _Investor Model = new FinanceManager().GetFinanceInvestor();
                var GetModelDependency = Task.Factory.StartNew(() =>
                {
                    GetInvestorDataDependency();

                });
                var GetModelData = Task.Factory.StartNew(() =>
                {
                    obj = GetInvestorModelData(Model);
                });


                Task.WaitAll(new Task[] { GetModelData, GetModelDependency });
                return PartialView("_DetailInvestorPartial", obj);

            }

            return PartialView("_NewFinanceInvestorPartial");
        }

        public string AddInvestors(Investor Model)
        {
            if (Model.EntityState == EntityState.New)
                Model.InvestorID = Guid.NewGuid();

            return new FinanceManager().AddFinanceInvestor(Model);
        }

        public string AddGrantSurvivalBudget(Guid GrantID)
        {
            GrantSurvivalBudget Model = new GrantSurvivalBudget();
            Model.GrantID = GrantID;
            Model.GrantStatus = GrantStatus.Locked;
            return new FinanceManager().AddGrantSurvivalBudget(Model);
        }

        public string CheckModuleCourse(int State, int SectionValue)
        {
            if (State == 0)
            {
                string getValue = "0";
                string getClientID = string.Empty;
                string LoginUserID = User.Identity.Name.ToString();
                string SectionName = Enum.GetName(typeof(ModuleSection), SectionValue);
                string CookieID = SectionName.ToString() + LoginUserID;
                if (Request.Cookies[CookieID.ToString()] != null)
                {
                    HttpCookie aCookie = Request.Cookies[CookieID.ToString()];
                    getValue = aCookie.Values["Status"];
                }
                else
                {
                    HttpCookie appCookie = new HttpCookie(CookieID.ToString());
                    appCookie.Values["Status"] = "0";
                    appCookie.Values["ClientID"] = User.Identity.Name.ToString();
                    appCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(appCookie);
                }
                return ResponseType.NotRedirect.ToString();
                // ModuleCourse objCourse = new Master().GetSingleModuleCourse((int)Module.TheBusiness, SectionValue);

                //if (getValue == "0" && objCourse.ModuleCourseID != Guid.Empty)
                //    return ResponseType.Redirect.ToString();
                //else return ResponseType.NotRedirect.ToString();

            }
            else
                return ResponseType.NotRedirect.ToString();

        }

        int IsNotAnyNullOrEmpty(object myObject)
        {
            int Count = 0;
            foreach (System.Reflection.PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (!string.IsNullOrEmpty(value))
                    {
                        Count++;
                    }
                }
            }
            return Count;
        }

        public decimal PercentComplete(int Count, decimal Unit)
        {
            decimal Completed = 0.00m;
            if (Count > 0)
                for (int i = 1; i <= Count; i++)
                {
                    Completed += Unit;
                }
            return Completed;
        }

        public ActionResult UpdateSalesforecast(Guid SaleforecastID, string Property, decimal Value)
        {
            SaleforecastList = new FinanceManager().UpdateSalesforecast(SaleforecastID, Property, Value).ToList();
            ViewBag.SaleforecastList = SaleforecastList;
            return PartialView("_SalesforecastPartial");
        }

        public ActionResult UpdateCashflowforecast(Guid CashflowforecastID, decimal Amount)
        {
            CashflowforecastList = new FinanceManager().UpdateCashflowforecast(CashflowforecastID, Amount).ToList();
            ViewBag.SaleforecastList = CashflowforecastList;
            ViewBag.CashflowforecastList = CashflowforecastList;
            ViewBag.Expenditure = CashflowforecastList.Where(x => x.ForecastType == (int)OptionType.Cashflowforecast_Expenditure);
            ViewBag.Income = CashflowforecastList.Where(x => x.ForecastType == (int)OptionType.Cashflowforecast_Income);
            return PartialView("_CashflowforecastPartial", CashflowforecastList);


        }

        public ActionResult UpdatePartnerOnePersonalSurviveBudget(Guid PersonalSurvivalBudgetID, decimal Monthly, string Notes)
        {
            BudgetList = new FinanceManager().UpdatePersonalSurviveBudget(PersonalSurvivalBudgetID, Monthly, Notes).ToList();
            return PartialView("_PartnerOnePersonalSurviveBudget", BudgetList.Where(x => x.PartnerCount == 1));
        }

        public ActionResult UpdatePartnerTwoPersonalSurviveBudget(Guid PersonalSurvivalBudgetID, decimal Monthly, string Notes)
        {
            BudgetList = new FinanceManager().UpdatePersonalSurviveBudget(PersonalSurvivalBudgetID, Monthly, Notes).ToList();
            return PartialView("_PartnerTwoPersonalSurviveBudget", BudgetList.Where(x => x.PartnerCount == 2));
        }
        public ActionResult UpdateLoanCalculator(Guid LoanCalculatorID, decimal AmountToBorrow, int YearsToRepay)
        {
            LoanCalculator = new FinanceManager().UpdateLoanCalculator(LoanCalculatorID, AmountToBorrow, YearsToRepay);
            return PartialView("_LoanCalculatorPartial", LoanCalculator);
        }

        public string UpdateNote(Guid Id, int Type, string Note)
        {
            return new FinanceManager().UpdateNote(Id, Type, Note);
        }

        public string GeneratePersonlSurviveBudgetExcel(string id)
        {
            BudgetList = new FinanceManager().GetPersonalSurvivalBudget(Guid.Parse(id)).ToList();
            List<PersonalSurvivalBudget> ModelPartnerOne = BudgetList.Where(x => x.PartnerCount == 1).ToList();
            List<PersonalSurvivalBudget> ModelPartnerTwo = BudgetList.Where(x => x.PartnerCount == 2).ToList();
            List<PersonalSurvivalBudget> ExpenseList = ModelPartnerOne.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Expenses).ToList();
            List<PersonalSurvivalBudget> IncomeList = ModelPartnerOne.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Income).ToList();
            LoanCalculator = new FinanceManager().GetLoanCalculator(Guid.Parse(id));
            SaleforecastList = new FinanceManager().GetSalesforecast(Guid.Parse(id)).ToList();
            List<int> ItemNumberList = SaleforecastList.Select(o => o.ItemNumber).Distinct().ToList();
            decimal TotalUnitsSold = 0.0m; decimal TotalUnitPrice = 0.0m; decimal TotalSubTotal = 0.0m;
            decimal TotalSalesTotal = 0.0m;
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 1).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 2).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 3).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 4).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 5).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 6).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 7).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 8).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 9).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 10).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 11).Sum(x => x.SubTotalSales);
            TotalSalesTotal += SaleforecastList.Where(x => x.Month == 12).Sum(x => x.SubTotalSales);
            String Filename = new ClientManager().GetMainClientID(Guid.Parse(User.Identity.Name)).ToString() + ".xls";
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Excel2016;
                IWorksheet worksheet;
                //Create a workbook
                IWorkbook workbook = application.Workbooks.Create(1);

                worksheet = workbook.Worksheets.Create("PSB Partner One");
                // IWorksheet worksheet = workbook.Worksheets[0];


                worksheet.IsGridLinesVisible = true;

                //Merge cells
                worksheet.Range["B2:E2"].Merge();
                worksheet.Range["B4:E4"].Merge();
                worksheet.Range["B6:E6"].Merge();
                worksheet.Range["B8:E8"].Merge();

                //Enter text to the cell D1 and apply formatting.
                worksheet.Range["B2"].Text = "Personal Survive Budget";
                worksheet.Range["B2"].CellStyle.Font.Bold = true;
                worksheet.Range["B2"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B2"].CellStyle.Font.Size = 18;
                worksheet.Range["B2"].RowHeight = 25;
                worksheet.Range["B2:E2"].ColumnWidth = 15;


                worksheet.Range["B4:E4"].CellStyle.WrapText = true;
                worksheet.Range["B4:E4"].RowHeight = 55;
                worksheet.Range["B4"].Text = "The Personal Survival Budget works out the amount you need \n from your business to live and whether you can afford to take on a\nStart Up Loan. If your personal expenses are greater than your\npersonal income, your business will need to cover these expenses.";

                worksheet.Range["B6"].Text = "INSTRUCTIONS FOR COMPLETING:";
                worksheet.Range["B6"].CellStyle.Font.Bold = true;
                worksheet.Range["B6"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B6"].CellStyle.Font.Size = 18;
                worksheet.Range["B6"].RowHeight = 25;
                worksheet.Range["B6:E6"].ColumnWidth = 15;

                worksheet.Range["B8:E8"].CellStyle.WrapText = true;
                worksheet.Range["B8:E8"].RowHeight = 220;
                worksheet.Range["B8:E8"].ColumnWidth = 30;
                worksheet.Range["B8"].Text = "- These are your personal income and costs and should not include any income/costs from the business.\n- Only include costs you are responsible for, do not include anything paid for by a \n   partner or other household member.\n  - Update the personal income and expense categories as necessary.  Some line items may not \n  be applicable to you, so just skip over them.\n- Input any comments or assumptions you have made in the notes column.\n- Enter figures into the white boxes only, the spreadsheet will calculate the rest.\n- Individual living costs vary, so start by thinking through all income and costs that you will incur. \n     You may find it helpful to review your last 3-months of bank statements to see how much you normally spend, in order to accurately build your personal survival budget.\n- If you are receiving housing or council tax benefits you should put the full amount of rent and council \n   tax payable in your survival budget as once you start trading you may lose these or have them reduced.\n- If you are planning to have another job while running your business, please let us know whether \n   you currently hold this position.";

                worksheet.Range["B9"].Text = "PERSONAL EXPENSES";
                worksheet.Range["C9"].Text = "Monthly £";
                worksheet.Range["D9"].Text = "Yearly £";
                worksheet.Range["E9"].Text = "Notes";
                int i = 10;
                Decimal TotalMonthlyExpense = 0; Decimal TotalYearlyExpense = 0;
                foreach (var item in ExpenseList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Expenses).OrderBy(x => x.DisplayOrder))
                {
                    worksheet.Range["B" + i + ""].Text = item.Value.ToString();
                    worksheet.Range["C" + i + ""].Text = item.Monthly != 0 ? item.Monthly.ToString("n2") : "";
                    worksheet.Range["D" + i + ""].Text = item.Monthly != 0 ? (item.Monthly * 12).ToString("n2") : "";
                    worksheet.Range["E" + i + ""].Text = !string.IsNullOrEmpty(item.Notes) ? item.Notes.ToString() : "";
                    TotalMonthlyExpense = TotalMonthlyExpense + item.Monthly;
                    TotalYearlyExpense = TotalYearlyExpense + item.Monthly * 12;
                    i = i + 1;
                }
                worksheet.Range["B" + i + ""].Text = "TOTAL EXPENSES";
                worksheet.Range["C" + i + ""].Text = TotalMonthlyExpense != 0 ? TotalMonthlyExpense.ToString("n2") : "";
                worksheet.Range["D" + i + ""].Text = TotalYearlyExpense != 0 ? TotalYearlyExpense.ToString("n2") : "";
                i = i + 1;
                worksheet.Range["B" + i + ""].Text = "PERSONAL Income";
                worksheet.Range["C" + i + ""].Text = "Monthly £";
                worksheet.Range["D" + i + ""].Text = "Yearly £";
                worksheet.Range["E" + i + ""].Text = "Notes";

                Decimal TotalMonthlyIncome = 0; Decimal TotalYearlyIncome = 0;
                foreach (var item in IncomeList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Income).OrderBy(x => x.DisplayOrder))
                {
                    worksheet.Range["B" + i + ""].Text = item.Value.ToString();
                    worksheet.Range["C" + i + ""].Text = item.Monthly != 0 ? item.Monthly.ToString("n2") : "";
                    worksheet.Range["D" + i + ""].Text = item.Monthly != 0 ? (item.Monthly * 12).ToString("n2") : "";
                    worksheet.Range["E" + i + ""].Text = !string.IsNullOrEmpty(item.Notes) ? item.Notes.ToString() : "";
                    TotalMonthlyIncome = TotalMonthlyIncome + item.Monthly;
                    TotalYearlyIncome = TotalYearlyIncome + item.Monthly * 12;
                    i = i + 1;
                }
                worksheet.Range["B" + i + ""].Text = "PERSONAL INCOME";
                worksheet.Range["C" + i + ""].Text = TotalMonthlyIncome != 0 ? TotalMonthlyIncome.ToString("n2") : "";
                worksheet.Range["D" + i + ""].Text = TotalYearlyIncome != 0 ? TotalYearlyIncome.ToString("n2") : "";

                //*********** WORKSHEET- 2 ************************//
                // workbook.Worksheets.Create("New_Sheet" + (i).ToString());
                ExpenseList = ModelPartnerTwo.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Expenses).ToList();
                IncomeList = ModelPartnerTwo.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Income).ToList();
                worksheet = workbook.Worksheets.Create("PSB Partner Two");
                //workbook = application.Workbooks.Create(2);
                //  worksheet = workbook.Worksheets[1];

                worksheet.IsGridLinesVisible = true;

                //Merge cells
                worksheet.Range["B2:E2"].Merge();
                worksheet.Range["B4:E4"].Merge();
                worksheet.Range["B6:E6"].Merge();
                worksheet.Range["B8:E8"].Merge();

                //Enter text to the cell D1 and apply formatting.
                worksheet.Range["B2"].Text = "Personal Survive Budget";
                worksheet.Range["B2"].CellStyle.Font.Bold = true;
                worksheet.Range["B2"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B2"].CellStyle.Font.Size = 18;
                worksheet.Range["B2"].RowHeight = 25;
                worksheet.Range["B2:E2"].ColumnWidth = 15;


                worksheet.Range["B4:E4"].CellStyle.WrapText = true;
                worksheet.Range["B4:E4"].RowHeight = 55;
                worksheet.Range["B4"].Text = "The Personal Survival Budget works out the amount you need \n from your business to live and whether you can afford to take on a\nStart Up Loan. If your personal expenses are greater than your\npersonal income, your business will need to cover these expenses.";

                worksheet.Range["B6"].Text = "INSTRUCTIONS FOR COMPLETING:";
                worksheet.Range["B6"].CellStyle.Font.Bold = true;
                worksheet.Range["B6"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B6"].CellStyle.Font.Size = 18;
                worksheet.Range["B6"].RowHeight = 25;
                worksheet.Range["B6:E6"].ColumnWidth = 15;

                worksheet.Range["B8:E8"].CellStyle.WrapText = true;
                worksheet.Range["B8:E8"].RowHeight = 220;
                worksheet.Range["B8:E8"].ColumnWidth = 30;
                worksheet.Range["B8"].Text = "- These are your personal income and costs and should not include any income/costs from the business.\n- Only include costs you are responsible for, do not include anything paid for by a \n   partner or other household member.\n  - Update the personal income and expense categories as necessary.  Some line items may not \n  be applicable to you, so just skip over them.\n- Input any comments or assumptions you have made in the notes column.\n- Enter figures into the white boxes only, the spreadsheet will calculate the rest.\n- Individual living costs vary, so start by thinking through all income and costs that you will incur. \n     You may find it helpful to review your last 3-months of bank statements to see how much you normally spend, in order to accurately build your personal survival budget.\n- If you are receiving housing or council tax benefits you should put the full amount of rent and council \n   tax payable in your survival budget as once you start trading you may lose these or have them reduced.\n- If you are planning to have another job while running your business, please let us know whether \n   you currently hold this position.";

                worksheet.Range["B9"].Text = "PERSONAL EXPENSES";
                worksheet.Range["C9"].Text = "Monthly £";
                worksheet.Range["D9"].Text = "Yearly £";
                worksheet.Range["E9"].Text = "Notes";
                i = 10;
                TotalMonthlyExpense = 0; TotalYearlyExpense = 0;
                foreach (var item in ExpenseList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Expenses).OrderBy(x => x.DisplayOrder))
                {
                    worksheet.Range["B" + i + ""].Text = item.Value.ToString();
                    worksheet.Range["C" + i + ""].Text = item.Monthly != 0 ? item.Monthly.ToString("n2") : "";
                    worksheet.Range["D" + i + ""].Text = item.Monthly != 0 ? (item.Monthly * 12).ToString("n2") : "";
                    worksheet.Range["E" + i + ""].Text = !string.IsNullOrEmpty(item.Notes) ? item.Notes.ToString() : "";
                    TotalMonthlyExpense = TotalMonthlyExpense + item.Monthly;
                    TotalYearlyExpense = TotalYearlyExpense + item.Monthly * 12;
                    i = i + 1;
                }
                worksheet.Range["B" + i + ""].Text = "TOTAL EXPENSES";
                worksheet.Range["C" + i + ""].Text = TotalMonthlyExpense != 0 ? TotalMonthlyExpense.ToString("n2") : "";
                worksheet.Range["D" + i + ""].Text = TotalYearlyExpense != 0 ? TotalYearlyExpense.ToString("n2") : "";
                i = i + 1;
                worksheet.Range["B" + i + ""].Text = "PERSONAL Income";
                worksheet.Range["C" + i + ""].Text = "Monthly £";
                worksheet.Range["D" + i + ""].Text = "Yearly £";
                worksheet.Range["E" + i + ""].Text = "Notes";

                TotalMonthlyIncome = 0; TotalYearlyIncome = 0;
                foreach (var item in IncomeList.Where(x => x.PersonalType == (int)OptionType.PersonalSurvivalBudget_Income).OrderBy(x => x.DisplayOrder))
                {
                    worksheet.Range["B" + i + ""].Text = item.Value.ToString();
                    worksheet.Range["C" + i + ""].Text = item.Monthly != 0 ? item.Monthly.ToString("n2") : "";
                    worksheet.Range["D" + i + ""].Text = item.Monthly != 0 ? (item.Monthly * 12).ToString("n2") : "";
                    worksheet.Range["E" + i + ""].Text = !string.IsNullOrEmpty(item.Notes) ? item.Notes.ToString() : "";
                    TotalMonthlyIncome = TotalMonthlyIncome + item.Monthly;
                    TotalYearlyIncome = TotalYearlyIncome + item.Monthly * 12;
                    i = i + 1;
                }
                worksheet.Range["B" + i + ""].Text = "PERSONAL INCOME";
                worksheet.Range["C" + i + ""].Text = TotalMonthlyIncome != 0 ? TotalMonthlyIncome.ToString("n2") : "";
                worksheet.Range["D" + i + ""].Text = TotalYearlyIncome != 0 ? TotalYearlyIncome.ToString("n2") : "";

                //*********** WORKSHEET-  3************************//
                // workbook.Worksheets.Create("New_Sheet" + (i).ToString());

                worksheet = workbook.Worksheets.Create("Loan Amount");
                //Merge cells
                worksheet.Range["B2:E2"].Merge();
                worksheet.Range["B4:E4"].Merge();
                worksheet.Range["B6:D6"].Merge();
                worksheet.Range["B7:D7"].Merge();
                worksheet.Range["B9:D9"].Merge();
                worksheet.Range["B10:D10"].Merge();
                worksheet.Range["B11:D11"].Merge();
                worksheet.Range["B13:D13"].Merge();
                worksheet.Range["B15:E15"].Merge();
                worksheet.Range["B17:E17"].Merge();

                worksheet.Range["B2"].Text = "LOAN CALCULATOR";
                worksheet.Range["B2"].CellStyle.Font.Bold = true;
                worksheet.Range["B2"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B2"].CellStyle.Font.Size = 18;
                worksheet.Range["B2"].RowHeight = 25;
                worksheet.Range["B2:E2"].ColumnWidth = 15;


                worksheet.Range["B4:E4"].CellStyle.WrapText = true;
                worksheet.Range["B4:E4"].RowHeight = 60;
                worksheet.Range["B4"].Text = "Enter the amount you want to borrow and the number of years you want to repay in the white boxes.\n Your repayments will automatically be copied across into your \n cashflow forecast (see the 3 separate tabs at the bottom of the spreadsheet).";

                worksheet.Range["B6"].Text = "Amount to borrow (min £1000, max £25,000)*";
                worksheet.Range["E6"].Text = "100";
                worksheet.Range["B7"].Text = "Years to repay (minimum 1, maximum 5)";
                worksheet.Range["E7"].Text = "100";

                worksheet.Range["B9"].Text = "Interest (6.0% p.a. fixed)";
                worksheet.Range["E9"].Text = "100";
                worksheet.Range["B10"].Text = "Capital";
                worksheet.Range["E10"].Text = "100";
                worksheet.Range["B11"].Text = "Total to repay";
                worksheet.Range["E11"].Text = "100";

                worksheet.Range["B13"].Text = "Approximate monthly repayments**";
                worksheet.Range["E13"].Text = "100";

                worksheet.Range["B15:E15"].CellStyle.WrapText = true;
                worksheet.Range["B15:E15"].RowHeight = 55;
                worksheet.Range["B15"].Text = "'*PLEASE NOTE: Our average loan size is around £7,000 - please carefully consider how much you need to borrow to start your business.";


                worksheet.Range["B17:E17"].CellStyle.WrapText = true;
                worksheet.Range["B17:E17"].RowHeight = 60;
                worksheet.Range["B17"].Text = "**The calculator is for illustrative purposes only.Start Up Loans are personal loans for business use only and subject to status.The monthly repayment amounts quoted are estimates which may increase or decrease slightly depending on the number of days between approval and when the loan is advanced.";

                //*********** WORKSHEET-  3************************//
                // workbook.Worksheets.Create("New_Sheet" + (i).ToString());

                worksheet = workbook.Worksheets.Create("Sale Forecast Year 1");
                worksheet.Range["B2:E2"].Merge();
                worksheet.Range["B4:E4"].Merge();

                worksheet.Range["B2"].Text = "YEAR 1 SALES FORECAST";
                worksheet.Range["B2"].CellStyle.Font.Bold = true;
                worksheet.Range["B2"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["B2"].CellStyle.Font.Size = 18;
                worksheet.Range["B2"].RowHeight = 25;
                worksheet.Range["B2:E2"].ColumnWidth = 15;

                worksheet.Range["B4:E4"].CellStyle.WrapText = true;
                worksheet.Range["B4:E4"].RowHeight = 60;
                worksheet.Range["B4"].Text = "This spreadsheet helps you work out how much sales revenue you expect to generate each month.Enter the number of units \n you expect to sell and the sale price of each unit. Add more rows if you need to. Separate cash sales \n from credit sales(you sell your product/ service but don not get paid until 30 days). If you plan to charge VAT please add that onto  \n your unit price (20%). Do not enter figures in the grey cells.";

                worksheet.Range["B6"].Text = "MONTH";
                worksheet.Range["C6"].Text = "Month 1";
                worksheet.Range["D6"].Text = "Month 2";
                worksheet.Range["E6"].Text = "Month 3";
                worksheet.Range["F6"].Text = "Month 4";
                worksheet.Range["G6"].Text = "Month 5";
                worksheet.Range["H6"].Text = "MOnth 6";
                worksheet.Range["I6"].Text = "MOnth 7";
                worksheet.Range["J6"].Text = "Month 8";
                worksheet.Range["K6"].Text = "Month 9";
                worksheet.Range["L6"].Text = "Month 10";
                worksheet.Range["M6"].Text = "Month 11";
                worksheet.Range["N6"].Text = "Month 12";
                worksheet.Range["O6"].Text = "";

                worksheet.Range["B8"].Text = "SALES";


                 i = 9;
                foreach (var ItemNumber in ItemNumberList.OrderBy(p => p))
                {

                    List<SaleforeCast> SaleforecastModel = SaleforecastList.Where(x => x.ItemNumber == ItemNumber).ToList();
                    worksheet.Range["B"+i+""].Text = SaleforecastModel.Select(x => x.Item).FirstOrDefault();
                    worksheet.Range["O"+i+""].Text = "Totals";
                    i = i + 1;
                    worksheet.Range["B"+i+""].Text = "Units sold";
                    foreach (var item in SaleforecastModel.OrderBy(x => x.Month))
                    {
                        TotalUnitsSold = TotalUnitsSold + item.UnitsSold;
                        if(item.Month == 1) worksheet.Range["C"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["D"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["E"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["F"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["G"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["H"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["I"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["J"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["K"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["L"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["M"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["N"+i+""].Text = item.UnitsSold != 0 ? item.UnitsSold.ToString("n2") : "";
                    }
                    worksheet.Range["O"+i+""].Text = TotalUnitsSold.ToString("n2");
                    i = i + 1;
                    worksheet.Range["B"+i+""].Text = ">Unit Price";
                    foreach (var item in SaleforecastModel.OrderBy(x => x.Month))
                    {
                        TotalUnitPrice = TotalUnitPrice + @item.UnitPrice;
                        if (item.Month == 1) worksheet.Range["C"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["D"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["E"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["F"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["G"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["H"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["I"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["J"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["K"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["L"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["M"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["N"+i+""].Text = item.UnitPrice != 0 ? item.UnitPrice.ToString("n2") : "";
                    }
                    worksheet.Range["O"+i+""].Text = TotalUnitPrice.ToString("n2");

                    i = i + 1;
                    worksheet.Range["B"+i+""].Text = ">Sub-total";
                    foreach (var item in SaleforecastModel.OrderBy(x => x.Month))
                    {
                        TotalSubTotal = TotalSubTotal + @item.SubTotalSales;
                        if (item.Month == 1) worksheet.Range["C"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["D"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["E"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["F"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["G"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["H"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["I"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["J"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["K"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["L"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["M"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                        if (item.Month == 1) worksheet.Range["N"+i+""].Text = item.SubTotalSales != 0 ? item.SubTotalSales.ToString("n2") : "";
                    }
                    worksheet.Range["O"+i+""].Text = TotalSubTotal.ToString("n2");

                }

              
                workbook.SaveAs(Server.MapPath("~/Content/"+ Filename));
               
            }
            return Filename;

        }

        public void GetInvestorDataDependency()
        {
            ViewBag.TitleFriendsFamily = TypeDescriptor.GetProperties(typeof(Business_Model.Model.FriendsFamily)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleAngelInvestor = TypeDescriptor.GetProperties(typeof(Business_Model.Model.AngelInvestor)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
            ViewBag.TitleVC = TypeDescriptor.GetProperties(typeof(Business_Model.Model.VC)).Cast<PropertyDescriptor>().ToDictionary(p => p.Name, p => p.Description);
            ViewBag.Language = new Master().GetDefaultModuleLanguage((int)Module.Finance, (int)ModuleSection.Finance_FindingInvestors);
        }

        public Investor GetInvestorModelData(_Investor Model)
        {
            Investor obj = new Investor();
            FriendsFamily objFriendsFamily = new FriendsFamily();
            AngelInvestor objAngelInvestor = new AngelInvestor();
            VC objVC = new VC();

            if (Model != null)
            {
                obj.ClientID = Model.ClientID;
                obj.InvestorID = Model.InvestorID;
                obj.EntityState = EntityState.Old;
                objFriendsFamily.BusinessMoneyPut = Model.BusinessMoneyPut;
                objFriendsFamily.DecisionMakingAbility = Model.DecisionMakingAbility;
                objFriendsFamily.FullTimeDoing = Model.FullTimeDoing;
                objFriendsFamily.GetBack = Model.GetBack;
                objFriendsFamily.GrownExcitedCompany = Model.GrownExcitedCompany;
                objFriendsFamily.Investing = Model.Investing;
                objFriendsFamily.InvolvementLevelRequired = Model.InvolvementLevelRequired;
                objFriendsFamily.LoanOrInvestment = Model.LoanOrInvestment;
                objFriendsFamily.PayingCustomer = Model.PayingCustomer;
                objFriendsFamily.RealRisks = Model.RealRisks;
                objFriendsFamily.Timeframe = Model.Timeframe;
                objFriendsFamily.BusinessMoneyPut = Model.BusinessMoneyPut;
                objAngelInvestor.BusinessComparable = Model.BusinessComparable;
                objAngelInvestor.BusinessGrowFast = Model.BusinessGrowFast;
                objAngelInvestor.BusinessTeamSuited = Model.BusinessTeamSuited;
                objAngelInvestor.CompaniesTalked = Model.CompaniesTalked;
                objAngelInvestor.CompeteWith = Model.CompeteWith;
                objAngelInvestor.DecideTo = Model.DecideTo;
                objAngelInvestor.EvaluationComeup = Model.EvaluationComeup;
                objAngelInvestor.GetStarted = Model.GetStarted;
                objAngelInvestor.GrowFaster = Model.GrowFaster;
                objAngelInvestor.HowDifferentYou = Model.HowDifferentYou;
                objAngelInvestor.HowMany = Model.HowMany;
                objAngelInvestor.IdeaComeup = Model.IdeaComeup;
                objAngelInvestor.Interested = Model.Interested;
                objAngelInvestor.MakeMoney = Model.MakeMoney;
                objAngelInvestor.MakeMoneyInvestors = Model.MakeMoneyInvestors;
                objAngelInvestor.MarketingExpenseRealize = Model.MarketingExpenseRealize;
                objAngelInvestor.MileStonesMet = Model.MileStonesMet;
                objAngelInvestor.ObjectionAbout = Model.ObjectionAbout;
                objAngelInvestor.PatentStrong = Model.PatentStrong;
                objAngelInvestor.ProblemKindGroup = Model.ProblemKindGroup;
                objAngelInvestor.ProblemWantSolving = Model.ProblemWantSolving;
                objAngelInvestor.Proprietary = Model.Proprietary;
                objAngelInvestor.SalesClose = Model.SalesClose;
                objAngelInvestor.SalesMade = Model.SalesMade;
                objAngelInvestor.ShownTo = Model.ShownTo;
                objAngelInvestor.SpendInvestorsMoney = Model.SpendInvestorsMoney;
                objAngelInvestor.TractionMade = Model.TractionMade;
                objAngelInvestor.WhyNeedInvestors = Model.WhyNeedInvestors;
                objAngelInvestor.WordoutGet = Model.WordoutGet;
                objAngelInvestor.YourInvestors = Model.YourInvestors;
                objVC.AchieveMilestone = Model.AchieveMilestone;
                objVC.BigMarketOportunity = Model.BigMarketOportunity;
                objVC.BusinessPotentialRisk = Model.BusinessPotentialRisk;
                objVC.ClaimIntellectualProperty = Model.ClaimIntellectualProperty;
                objVC.CompanyFinancialProjection = Model.CompanyFinancialProjection;
                objVC.CurrentCash = Model.CurrentCash;
                objVC.DevelopedIntellectualProperty = Model.DevelopedIntellectualProperty;
                objVC.DifferentiatedTechnology = Model.DifferentiatedTechnology;
                objVC.ExistingInvestors = Model.ExistingInvestors;
                objVC.ExpectedEvaluation = Model.ExpectedEvaluation;
                objVC.ForecastingGrowth = Model.ForecastingGrowth;
                objVC.FoundersUnderstand = Model.FoundersUnderstand;
                objVC.GreatManagementTeam = Model.GreatManagementTeam;
                objVC.InitialInvestorPitchDesk = Model.InitialInvestorPitchDesk;
                objVC.InvestmentCapital = Model.InvestmentCapital;
                objVC.KeyFeatures = Model.KeyFeatures;
                objVC.KeyIntellectualProperty = Model.KeyIntellectualProperty;
                objVC.Margins = Model.Margins;
                objVC.OwnedIntellectualProperty = Model.OwnedIntellectualProperty;
                objVC.ProductMilestone = Model.ProductMilestone;
                objVC.ReplicateTechnology = Model.ReplicateTechnology;
                objVC.SalesMarketingModel = Model.SalesMarketingModel;
                objVC.StructurePlan = Model.StructurePlan;
                objVC.TractionPositiveAchieved = Model.TractionPositiveAchieved;
                objVC.ViolateIntellectualProperty = Model.ViolateIntellectualProperty;
            }
            else
                obj.EntityState = EntityState.New;
            
            
            obj.AngelInvestor = objAngelInvestor;
            obj.FriendsFamily = objFriendsFamily;
            obj.VC = objVC;
            return obj;
        }
      
    }
}
