using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using System.Linq;
using Business_Model.Model;
using System.Drawing;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Collections;

namespace BuildMyUnicorn.Controllers
{
    public class BusinessPlanFileController : WebController
    {
        // GET: BusinessPlanFile
        public ActionResult Index()
        {
            Client Clientobj = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
            _Business BusinessModel = new BusinessManager().GetBusinessOverview();
            if (BusinessModel == null) { BusinessModel = new _Business(); }
            _ProductService ProductServiceModel = new BusinessManager().GetProductService();
            if (ProductServiceModel == null) { ProductServiceModel = new _ProductService(); }
            _BusinessOperation BusinessOperationModel = new BusinessManager().GetBusinessOperation();
            if (BusinessOperationModel == null) { BusinessOperationModel = new _BusinessOperation(); }
            IEnumerable<ClientTeam> TeamList = new ClientManager().GetClientTeam();
            if (TeamList == null) { TeamList = new List<ClientTeam>();  }
            IEnumerable<CompetitorAnalysis> CompetitorAnalysisModel = new BusinessManager().GetCompetitorAnalysis();
            if (CompetitorAnalysisModel == null) { CompetitorAnalysisModel = new List<CompetitorAnalysis>(); }
            _PricingProductService PriceModel = new SellingManager().GetPricingProductService();
            if (PriceModel == null) { PriceModel = new _PricingProductService(); }
            IEnumerable<_Marketing> MarketingPlanModel = new MarketingManager().GetMarketingPlan();
            if (MarketingPlanModel == null) { MarketingPlanModel = new List<_Marketing>(); }
            SWOT SWOTModel = new BusinessManager().GetCompetitorSWOT();
            if (SWOTModel == null) { SWOTModel = new SWOT(); }
            _Customer CustomerModel = new SellingManager().GetCustomers();
            if (CustomerModel == null) { CustomerModel = new _Customer(); }
            IEnumerable<BuyerPersona> PersonaModel = new SellingManager().GetCustomerbuyerPersona(CustomerModel.CustomerID);
            if (PersonaModel == null) { PersonaModel = new List<BuyerPersona>(); }
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            _KeyFinding KeyFindingModel = new MarketResearchManager().GetKeyFinding();
            if (KeyFindingModel == null) { KeyFindingModel = new _KeyFinding(); }

            // Creating a new document.
            WordDocument document = new WordDocument();
            //Adding a new section to the document.
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new System.Drawing.SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = System.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText("Your Business Idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 30f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.Black;

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("1. Business Information") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;
            //textRange.CharacterFormat.HighlightColor = Color.Yellow;


            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Firstly, please provide some basic information about your business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Name") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //1-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((Clientobj.StartupName != null ? Clientobj.StartupName : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //2-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("When will/did your business start trading?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessModel.Founded != null ? BusinessModel.Founded.ToString() : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Founders") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            IWTable tableFounder = section.AddTable();
            WTableRow rowHeaderFounder = tableFounder.AddRow();
            
            tableFounder.ResetCells(1, 4);
            tableFounder[0, 0].Width = 102f;
            tableFounder[0, 0].AddParagraph().AppendText("Name");
            tableFounder[0, 1].Width = 180f;
            tableFounder[0, 1].AddParagraph().AppendText("National Insurance Numbers");
            tableFounder[0, 2].Width = 90f;
            tableFounder[0, 2].AddParagraph().AppendText("Phone Number");
            tableFounder[0, 3].Width = 100f;
            tableFounder[0, 3].AddParagraph().AppendText("Email");
                  WTableCell cellFounder = rowHeaderFounder.AddCell();

            int i = 2;
            foreach (var item in TeamList)
            {
                if (item.RoleInCompany != null)
                    if (item.RoleInCompany.Contains("15a27065-6b7b-49d0-b3b9-cabd751204ed") || item.RoleInCompany.Contains("8856ff49-5620-4eb5-af56-b82cdc3d889c"))
                    {

                        WTableRow row = tableFounder.AddRow(true, false);
                        //for (int m = 1; m <= 4; m++)
                        //{
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.FirstName != null ? item.FirstName : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText("");
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.Phone != null ? item.Phone.ToString() : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.Email != null ? item.Email.ToString() : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        //}
                        i = i + 1;
                    }
            }


            
            //5-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Website Address (if applicable)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //5-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("No website yet") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //----------------------------Section-2--------------------------------//

            IWSection section2 = document.AddSection();
            IWTextRange textrange = new WTextRange(document);
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("2. Description of your business idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("What does/will your business do?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Provide a brief description of the main activities of your business.\rList the range of different products and services you will offer.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((ProductServiceModel.PSDescription != null ? ProductServiceModel.PSDescription.ToString() : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (ProductServiceModel.PSDescription == null || ProductServiceModel.PSDescription.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            //2-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How did you arrive at this business idea?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessModel.IdeaComeup != null) ? BusinessModel.IdeaComeup.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            if (BusinessModel.IdeaComeup == null ||  BusinessModel.IdeaComeup.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Does your business require premises?  If so, please provide details of the costs involved and the current status of discussions with the landlord.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            // Sub Heading of Q-3
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please provide a web link to the property on an Estate Agent or landlord website.\rIf available please provide a copy of communications with the landlord, including any documentation such as draft lease agreements.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(BusinessModel.LandlordCostStatus != null /*&& BusinessModel.BusinessRequirePremises == true*/ ? BusinessModel.LandlordCostStatus.ToString() : "No Business Premises Required") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Do you or your business require any specialist training, licences, certificates or insurances to trade? (e.g. health & safety certificates, food hygiene, public liability insurance, trade qualifications, certificates from training courses)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            // Question Heading 3-Q
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please list any legal requirements here and provide any documentation/certificates to help support your application.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;



            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessOperationModel.LicenseType != null ? "Yes, I need specifice license to wok\r" + BusinessOperationModel.LicenseType : "No I do not need specific license to work\r")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (BusinessOperationModel.LicenseType == null ||  BusinessOperationModel.LicenseType.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessOperationModel.QualificationType != null ? "Yes I need specific qualifications\r" + BusinessOperationModel.QualificationType : "No I do not need specific qualification")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if( BusinessOperationModel.QualificationType == null || BusinessOperationModel.QualificationType.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("List the people involved (including yourself) and their roles in the business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;


            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("You can also add details of you and your team’s knowledge and experience in your industry.\rWhat skills and qualities do you have that will make your business a success?\rPlease also provide a CV for those people in the business who are applying for a loan.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //(TeamList.FirstOrDefault().TeamInfo != null ? TeamList.FirstOrDefault().TeamInfo : "")

            //3-A-Section-1
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((TeamList.Count() > 0 && TeamList.FirstOrDefault().TeamInfo != null ? TeamList.FirstOrDefault().TeamInfo : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            IWTable tableTeam = section2.AddTable();
            tableTeam.ResetCells(1, 4);
            tableTeam[0, 0].Width = 102f;
            tableTeam[0, 0].AddParagraph().AppendText("Name");
            tableTeam[0, 1].Width = 180f;
            tableTeam[0, 1].AddParagraph().AppendText("Role at Company");
            tableTeam[0, 2].Width = 90f;
            tableTeam[0, 2].AddParagraph().AppendText("Linkedin");
            tableTeam[0, 3].Width = 100f;
            tableTeam[0, 3].AddParagraph().AppendText("Short Bio");
           // tableTeam.TableFormat.IsAutoResized = true;
            WTableRow rowHeaderTeam = tableTeam.AddRow();
            WTableCell cellTeam = rowHeaderTeam.AddCell();
          
            i = 2;
            List<MasterCommon> RoleList = new Master().GetOptionMasterList((int)OptionType.RoleInCompany).ToList();
            foreach (var item in TeamList)
            {
                //string RolesValues = "";
                //if (item.RoleInCompany != null)
                //{
                //    List<string> Roles = item.RoleInCompany.Split(',').ToList<string>();
                    
                //    foreach (var _Roles in Roles)
                //    {
                //        if (_Roles != null && Guid.Parse(_Roles) != Guid.Empty)
                //        {
                //            RolesValues += RoleList.Where(x => x.ID == Guid.Parse(_Roles)).Select(x => x.Value).FirstOrDefault().ToString() + "  ";
                //        }
                //    }
                //}

                WTableRow row = tableTeam.AddRow(true, false);
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.FirstName != null ? item.FirstName : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText("");
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.LinkedProfile != null ? item.LinkedProfile.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.ShortBio != null ? item.ShortBio.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
            }

            //----------------------------Section-3--------------------------------//
            IWSection section3 = document.AddSection();
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("3. Your Market ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Who are your customers?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How would you describe your typical customer?  Think about their:- age - gender - location - interests - any other relevant information that helps you to define who will buy your products/services. Try to identify different groups of customers.  e.g. you may sell to a certain segment of the general public, but also be able to agree contracts with organisations – these would be considered two different groups of customers.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Describe your typical customer") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.About != null) ? CustomerModel.About.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (CustomerModel.About == null || CustomerModel.About.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Where are they based") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.Based != null) ? CustomerModel.Based.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (CustomerModel.Based == null ||  CustomerModel.Based.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Why are they going to buy from you") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.Buy != null) ? CustomerModel.Buy.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (CustomerModel.Buy == null  || CustomerModel.Buy.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            IWTable tablePersona = section3.AddTable();
            WTableRow rowHeaderPersona = tablePersona.AddRow();
            //tableFounder.TableFormat.HorizontalAlignment=HorizontalAlignment.Distribute;
            tablePersona.TableFormat.Borders.BorderType = BorderStyle.None;
            tablePersona.TableFormat.IsBreakAcrossPages = true;
            tablePersona.IndentFromLeft = 5;
            WTableCell cellPersona = rowHeaderPersona.AddCell();
            cellPersona.AddParagraph().AppendText("Name");
            cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
            cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            // cellPersona.CharacterFormat.Bold = true;
            cellPersona = rowHeaderPersona.AddCell();
            cellPersona.AddParagraph().AppendText("Job Title");
            cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
            cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            i = 2;
            foreach (var item in PersonaModel)
            {

                WTableRow row = tablePersona.AddRow(true, false);
                cellFounder = row.AddCell();
                cellFounder.AddParagraph().AppendText((item.Name != null ? item.Name : ""));
                cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellFounder = row.AddCell();
                cellFounder.AddParagraph().AppendText((item.JobTitle != null ? item.JobTitle : ""));
                cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
                if (i == 6) break;
            }

            //2-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How you will advertise / promote your business?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Describe how customers will find out about your product/service - what methods you will use to promote the business, provide details of target audience, medium chosen, costs, frequency?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Examples: adverts in papers or trade magazines, direct mail shot, leaflets, brochures, flyers, email, exhibitions, events and trade fairs, canvassing, personal contact with clients, online advertising (Google, Facebook etc), online banners, business websites") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How you will monitor the response?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            IWTable tableMarketPlan = section3.AddTable();
            tableMarketPlan.TableFormat.IsAutoResized = true;
            WTableRow rowHeaderMarketPlan = tableMarketPlan.AddRow();
            tableMarketPlan.TableFormat.Borders.BorderType = BorderStyle.None;
             
            tableMarketPlan.IndentFromLeft = 5;
            WTableCell cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Goals");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            textRange.CharacterFormat.Bold = true;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("KPI's");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Find Buyers");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Unique Selling Proposition");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Plan to Reach Audience");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Track KPI's");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            i = 2;
            List<MasterCommon>  GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals).ToList();
            List<MasterCommon> AudienceList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach).ToList();
           
            foreach (var item in MarketingPlanModel)
            {

                WTableRow row = tableMarketPlan.AddRow(true, false);
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.GoalID.ToString() != null && item.GoalID != Guid.Empty ? GoalList.Where(x => x.ID == item.GoalID).Select(x => x.Value).FirstOrDefault().ToString(): ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.KPIS != null ? item.KPIS.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.FindBuyers != null ? item.FindBuyers.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.AudianceReachID.ToString() != null && item.AudianceReachID != Guid.Empty ? GoalList.Where(x => x.ID == item.AudianceReachID).Select(x => x.Value).FirstOrDefault().ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.TrackKPIS != null ? item.TrackKPIS.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //cellMarketPlan = row.AddCell();
                //cellMarketPlan.AddParagraph().AppendText((item.ShortBio != null ? item.ShortBio.ToString() : ""));
                //cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                //cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
            }

            //3-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Competitor Analysis Table") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 2;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Regardless of how unique your business idea is, you will always have competitors. Let us know who they are and what makes them successful.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            IWTable tableCompetitior = section3.AddTable();
            WTableRow rowHeadeCompetitorr = tableCompetitior.AddRow();
            WTableCell cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Competitor");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Location");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Price");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("What do they do well");
            i = 1;
            foreach (var item in CompetitorAnalysisModel)
            {

                WTableRow row = tableCompetitior.AddRow(true, false);
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Name != null ? item.Name : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Location != null ? item.Location : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Pricing != null ? item.Pricing : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Offering != null ? item.Offering : ""));
                i = i + 1;
            }


            //4-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("SWOT Analysis. What are the strengths and weaknesses of your business idea?  What opportunities and threats could it face?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            IWTable tableSWOT = section3.AddTable();
            WTableRow rowHeaderSWOT = tableSWOT.AddRow();
            WTableCell cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Strengths\r Internal characteristics of the business that give it an advantage over others");
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Weaknesses \r Internal characteristics that place the business at a disadvantage relative to others");
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Strengths != null ? SWOTModel.Strengths : ""));
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Weaknesses != null ? SWOTModel.Weaknesses : ""));
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Opportunities\r External factors that the business could exploit to its advantage");
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Threats \r External factors that could cause trouble for the business");
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Opportunities != null ? SWOTModel.Opportunities : ""));
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Threats != null ? SWOTModel.Threats : ""));
            //----------------------------Section-4--------------------------------//

            IWSection section4 = document.AddSection();
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("4. Pricing and Selling ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;


            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-2
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("List the prices you will charge for the different products and services you offer.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Describe how you will set prices for your product service. Include details of prices charged by the business and how they are calculated.\rTake into account your costs, hourly rate, the going rates in your industry, competition, demand etc.\rRemember that what people will pay for your product is determined by the value they place upon it, not its cost of production to you.\rDon’t worry if you don’t know all of this information yet just enter whatever you can.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.PricingStrategy != null ? PriceModel.PricingStrategy : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //1-Q-Section-3
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Have you made any sales so far, do you have any pre-orders, or customers waiting to purchase your product(s) and service(s)?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("If you do not have any sales or pre-orders, what research have you done to identify that there is a demand for your products/services?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Desk Research (any statistical evidence from internet, magazines, newspapers etc.)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Primary Research (any questionnaires/surveys you have done, or conversations you have had)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the Interview?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.InterviewKeyFinding != null) ? KeyFindingModel.InterviewKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.InterviewKeyFinding == null || KeyFindingModel.InterviewKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the observation?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.ObservationKeyFinding != null) ? KeyFindingModel.ObservationKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.ObservationKeyFinding == null || KeyFindingModel.ObservationKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the survey?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.SurveyKeyFinding != null) ? KeyFindingModel.SurveyKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.SurveyKeyFinding == null || KeyFindingModel.SurveyKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the online research?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.OnlineResearchKeyFinding != null) ? KeyFindingModel.OnlineResearchKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.OnlineResearchKeyFinding == null || KeyFindingModel.OnlineResearchKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //1-Q-Section-3
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Are there certain periods of the year when your sales will vary?  If so, when will you have high sales, average sales and low sales, and why?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Retailers may find that the run up to Christmas is the busiest time of the year but for garden maintenance businesses December may be one of the quietest months.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.SalesCertainPeriod != null ? PriceModel.SalesCertainPeriod : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

           
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Which suppliers will you need to use to deliver your product or service?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Why was this supplier(s) chosen? (quality, cost, reputation etc)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("If required, have you got a suppliers contract in place?\rWill you need to pay your suppliers upfront, or will you have a credit account?\rIs there a back-up plan in place if this supplier lets you down?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.PricingStrategy != null ? PriceModel.PricingStrategy : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            HTMLExport export = new HTMLExport();
            document.SaveOptions.HTMLExportImageAsBase64 = true;
            //Saves the document as html file
            export.SaveAsXhtml(document, Server.MapPath("~/Content/" + "Sample.html"));
           
            // document.Save("sample.docx", FormatType.Docx, HttpContext.ApplicationInstance.Response, HttpContentDisposition.Attachment);

            //return File("application/vnd.ms-word", "sample.docx");
            return View();
        }

        public ActionResult Download()
        {
            Client Clientobj = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
            _Business BusinessModel = new BusinessManager().GetBusinessOverview();
            if (BusinessModel == null) { BusinessModel = new _Business(); }
            _ProductService ProductServiceModel = new BusinessManager().GetProductService();
            if (ProductServiceModel == null) { ProductServiceModel = new _ProductService(); }
            _BusinessOperation BusinessOperationModel = new BusinessManager().GetBusinessOperation();
            if (BusinessOperationModel == null) { BusinessOperationModel = new _BusinessOperation(); }
            IEnumerable<ClientTeam> TeamList = new ClientManager().GetClientTeam();
            if (TeamList == null) { TeamList = new List<ClientTeam>(); }
            IEnumerable<CompetitorAnalysis> CompetitorAnalysisModel = new BusinessManager().GetCompetitorAnalysis();
            if (CompetitorAnalysisModel == null) { CompetitorAnalysisModel = new List<CompetitorAnalysis>(); }
            _PricingProductService PriceModel = new SellingManager().GetPricingProductService();
            if (PriceModel == null) { PriceModel = new _PricingProductService(); }
            IEnumerable<_Marketing> MarketingPlanModel = new MarketingManager().GetMarketingPlan();
            if (MarketingPlanModel == null) { MarketingPlanModel = new List<_Marketing>(); }
            SWOT SWOTModel = new BusinessManager().GetCompetitorSWOT();
            if (SWOTModel == null) { SWOTModel = new SWOT(); }
            _Customer CustomerModel = new SellingManager().GetCustomers();
            if (CustomerModel == null) { CustomerModel = new _Customer(); }
            IEnumerable<BuyerPersona> PersonaModel = new SellingManager().GetCustomerbuyerPersona(CustomerModel.CustomerID);
            if (PersonaModel == null) { PersonaModel = new List<BuyerPersona>(); }
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            _KeyFinding KeyFindingModel = new MarketResearchManager().GetKeyFinding();
            if (KeyFindingModel == null) { KeyFindingModel = new _KeyFinding(); }

            // Creating a new document.
            WordDocument document = new WordDocument();
            //Adding a new section to the document.
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new System.Drawing.SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = System.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText("Your Business Idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 30f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.Black;

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("1. Business Information") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;
            //textRange.CharacterFormat.HighlightColor = Color.Yellow;


            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Firstly, please provide some basic information about your business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Name") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //1-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((Clientobj.StartupName != null ? Clientobj.StartupName : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //2-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("When will/did your business start trading?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessModel.Founded != null ? BusinessModel.Founded.ToString() : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Founders") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            IWTable tableFounder = section.AddTable();
            WTableRow rowHeaderFounder = tableFounder.AddRow();

            tableFounder.ResetCells(1, 4);
            tableFounder[0, 0].Width = 102f;
            tableFounder[0, 0].AddParagraph().AppendText("Name");
            tableFounder[0, 1].Width = 180f;
            tableFounder[0, 1].AddParagraph().AppendText("National Insurance Numbers");
            tableFounder[0, 2].Width = 90f;
            tableFounder[0, 2].AddParagraph().AppendText("Phone Number");
            tableFounder[0, 3].Width = 100f;
            tableFounder[0, 3].AddParagraph().AppendText("Email");
            WTableCell cellFounder = rowHeaderFounder.AddCell();

            int i = 2;
            foreach (var item in TeamList)
            {
                if (item.RoleInCompany != null)
                    if (item.RoleInCompany.Contains("15a27065-6b7b-49d0-b3b9-cabd751204ed") || item.RoleInCompany.Contains("8856ff49-5620-4eb5-af56-b82cdc3d889c"))
                    {

                        WTableRow row = tableFounder.AddRow(true, false);
                        //for (int m = 1; m <= 4; m++)
                        //{
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.FirstName != null ? item.FirstName : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText("");
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.Phone != null ? item.Phone.ToString() : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.Email != null ? item.Email.ToString() : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        //}
                        i = i + 1;
                    }
            }



            //5-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Website Address (if applicable)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //5-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("No website yet") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //----------------------------Section-2--------------------------------//

            IWSection section2 = document.AddSection();
            IWTextRange textrange = new WTextRange(document);
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("2. Description of your business idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("What does/will your business do?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Provide a brief description of the main activities of your business.\rList the range of different products and services you will offer.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((ProductServiceModel.PSDescription != null ? ProductServiceModel.PSDescription.ToString() : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (ProductServiceModel.PSDescription == null || ProductServiceModel.PSDescription.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            //2-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How did you arrive at this business idea?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessModel.IdeaComeup != null) ? BusinessModel.IdeaComeup.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            if (BusinessModel.IdeaComeup == null || BusinessModel.IdeaComeup.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Does your business require premises?  If so, please provide details of the costs involved and the current status of discussions with the landlord.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            // Sub Heading of Q-3
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please provide a web link to the property on an Estate Agent or landlord website.\rIf available please provide a copy of communications with the landlord, including any documentation such as draft lease agreements.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(BusinessModel.LandlordCostStatus != null /*&& BusinessModel.BusinessRequirePremises == true*/ ? BusinessModel.LandlordCostStatus.ToString() : "No Business Premises Required") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Do you or your business require any specialist training, licences, certificates or insurances to trade? (e.g. health & safety certificates, food hygiene, public liability insurance, trade qualifications, certificates from training courses)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            // Question Heading 3-Q
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please list any legal requirements here and provide any documentation/certificates to help support your application.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;



            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessOperationModel.LicenseType != null ? "Yes, I need specifice license to wok\r" + BusinessOperationModel.LicenseType : "No I do not need specific license to work\r")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (BusinessOperationModel.LicenseType == null || BusinessOperationModel.LicenseType.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessOperationModel.QualificationType != null ? "Yes I need specific qualifications\r" + BusinessOperationModel.QualificationType : "No I do not need specific qualification")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (BusinessOperationModel.QualificationType == null || BusinessOperationModel.QualificationType.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("List the people involved (including yourself) and their roles in the business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;


            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("You can also add details of you and your team’s knowledge and experience in your industry.\rWhat skills and qualities do you have that will make your business a success?\rPlease also provide a CV for those people in the business who are applying for a loan.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //(TeamList.FirstOrDefault().TeamInfo != null ? TeamList.FirstOrDefault().TeamInfo : "")

            //3-A-Section-1
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((TeamList.Count() > 0 && TeamList.FirstOrDefault().TeamInfo != null ? TeamList.FirstOrDefault().TeamInfo : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            IWTable tableTeam = section2.AddTable();
            tableTeam.ResetCells(1, 4);
            tableTeam[0, 0].Width = 102f;
            tableTeam[0, 0].AddParagraph().AppendText("Name");
            tableTeam[0, 1].Width = 180f;
            tableTeam[0, 1].AddParagraph().AppendText("Role at Company");
            tableTeam[0, 2].Width = 90f;
            tableTeam[0, 2].AddParagraph().AppendText("Linkedin");
            tableTeam[0, 3].Width = 100f;
            tableTeam[0, 3].AddParagraph().AppendText("Short Bio");
            // tableTeam.TableFormat.IsAutoResized = true;
            WTableRow rowHeaderTeam = tableTeam.AddRow();
            WTableCell cellTeam = rowHeaderTeam.AddCell();

            i = 2;
            List<MasterCommon> RoleList = new Master().GetOptionMasterList((int)OptionType.RoleInCompany).ToList();
            foreach (var item in TeamList)
            {
                //string RolesValues = "";
                //if (item.RoleInCompany != null)
                //{
                //    List<string> Roles = item.RoleInCompany.Split(',').ToList<string>();

                //    foreach (var _Roles in Roles)
                //    {
                //        if (_Roles != null && Guid.Parse(_Roles) != Guid.Empty)
                //        {
                //            RolesValues += RoleList.Where(x => x.ID == Guid.Parse(_Roles)).Select(x => x.Value).FirstOrDefault().ToString() + "  ";
                //        }
                //    }
                //}

                WTableRow row = tableTeam.AddRow(true, false);
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.FirstName != null ? item.FirstName : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText("");
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.LinkedProfile != null ? item.LinkedProfile.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.ShortBio != null ? item.ShortBio.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
            }

            //----------------------------Section-3--------------------------------//
            IWSection section3 = document.AddSection();
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("3. Your Market ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Who are your customers?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How would you describe your typical customer?  Think about their:- age - gender - location - interests - any other relevant information that helps you to define who will buy your products/services. Try to identify different groups of customers.  e.g. you may sell to a certain segment of the general public, but also be able to agree contracts with organisations – these would be considered two different groups of customers.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Describe your typical customer") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.About != null) ? CustomerModel.About.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (CustomerModel.About == null || CustomerModel.About.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Where are they based") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.Based != null) ? CustomerModel.Based.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (CustomerModel.Based == null || CustomerModel.Based.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Why are they going to buy from you") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.Buy != null) ? CustomerModel.Buy.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (CustomerModel.Buy == null || CustomerModel.Buy.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            IWTable tablePersona = section3.AddTable();
            WTableRow rowHeaderPersona = tablePersona.AddRow();
            //tableFounder.TableFormat.HorizontalAlignment=HorizontalAlignment.Distribute;
            tablePersona.TableFormat.Borders.BorderType = BorderStyle.None;
            tablePersona.TableFormat.IsBreakAcrossPages = true;
            tablePersona.IndentFromLeft = 5;
            WTableCell cellPersona = rowHeaderPersona.AddCell();
            cellPersona.AddParagraph().AppendText("Name");
            cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
            cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            // cellPersona.CharacterFormat.Bold = true;
            cellPersona = rowHeaderPersona.AddCell();
            cellPersona.AddParagraph().AppendText("Job Title");
            cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
            cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            i = 2;
            foreach (var item in PersonaModel)
            {

                WTableRow row = tablePersona.AddRow(true, false);
                cellFounder = row.AddCell();
                cellFounder.AddParagraph().AppendText((item.Name != null ? item.Name : ""));
                cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellFounder = row.AddCell();
                cellFounder.AddParagraph().AppendText((item.JobTitle != null ? item.JobTitle : ""));
                cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
                if (i == 6) break;
            }

            //2-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How you will advertise / promote your business?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Describe how customers will find out about your product/service - what methods you will use to promote the business, provide details of target audience, medium chosen, costs, frequency?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Examples: adverts in papers or trade magazines, direct mail shot, leaflets, brochures, flyers, email, exhibitions, events and trade fairs, canvassing, personal contact with clients, online advertising (Google, Facebook etc), online banners, business websites") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How you will monitor the response?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            IWTable tableMarketPlan = section3.AddTable();
            tableMarketPlan.TableFormat.IsAutoResized = true;
            WTableRow rowHeaderMarketPlan = tableMarketPlan.AddRow();
            tableMarketPlan.TableFormat.Borders.BorderType = BorderStyle.None;

            tableMarketPlan.IndentFromLeft = 5;
            WTableCell cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Goals");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            textRange.CharacterFormat.Bold = true;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("KPI's");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Find Buyers");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Unique Selling Proposition");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Plan to Reach Audience");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Track KPI's");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            i = 2;
            List<MasterCommon> GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals).ToList();
            List<MasterCommon> AudienceList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach).ToList();

            foreach (var item in MarketingPlanModel)
            {

                WTableRow row = tableMarketPlan.AddRow(true, false);
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.GoalID.ToString() != null && item.GoalID != Guid.Empty ? GoalList.Where(x => x.ID == item.GoalID).Select(x => x.Value).FirstOrDefault().ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.KPIS != null ? item.KPIS.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.FindBuyers != null ? item.FindBuyers.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.AudianceReachID.ToString() != null && item.AudianceReachID != Guid.Empty ? GoalList.Where(x => x.ID == item.AudianceReachID).Select(x => x.Value).FirstOrDefault().ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.TrackKPIS != null ? item.TrackKPIS.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //cellMarketPlan = row.AddCell();
                //cellMarketPlan.AddParagraph().AppendText((item.ShortBio != null ? item.ShortBio.ToString() : ""));
                //cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                //cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
            }

            //3-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Competitor Analysis Table") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 2;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Regardless of how unique your business idea is, you will always have competitors. Let us know who they are and what makes them successful.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            IWTable tableCompetitior = section3.AddTable();
            WTableRow rowHeadeCompetitorr = tableCompetitior.AddRow();
            WTableCell cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Competitor");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Location");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Price");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("What do they do well");
            i = 1;
            foreach (var item in CompetitorAnalysisModel)
            {

                WTableRow row = tableCompetitior.AddRow(true, false);
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Name != null ? item.Name : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Location != null ? item.Location : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Pricing != null ? item.Pricing : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Offering != null ? item.Offering : ""));
                i = i + 1;
            }


            //4-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("SWOT Analysis. What are the strengths and weaknesses of your business idea?  What opportunities and threats could it face?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            IWTable tableSWOT = section3.AddTable();
            WTableRow rowHeaderSWOT = tableSWOT.AddRow();
            WTableCell cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Strengths\r Internal characteristics of the business that give it an advantage over others");
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Weaknesses \r Internal characteristics that place the business at a disadvantage relative to others");
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Strengths != null ? SWOTModel.Strengths : ""));
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Weaknesses != null ? SWOTModel.Weaknesses : ""));
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Opportunities\r External factors that the business could exploit to its advantage");
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Threats \r External factors that could cause trouble for the business");
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Opportunities != null ? SWOTModel.Opportunities : ""));
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Threats != null ? SWOTModel.Threats : ""));
            //----------------------------Section-4--------------------------------//

            IWSection section4 = document.AddSection();
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("4. Pricing and Selling ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;


            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-2
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("List the prices you will charge for the different products and services you offer.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Describe how you will set prices for your product service. Include details of prices charged by the business and how they are calculated.\rTake into account your costs, hourly rate, the going rates in your industry, competition, demand etc.\rRemember that what people will pay for your product is determined by the value they place upon it, not its cost of production to you.\rDon’t worry if you don’t know all of this information yet just enter whatever you can.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.PricingStrategy != null ? PriceModel.PricingStrategy : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //1-Q-Section-3
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Have you made any sales so far, do you have any pre-orders, or customers waiting to purchase your product(s) and service(s)?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("If you do not have any sales or pre-orders, what research have you done to identify that there is a demand for your products/services?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Desk Research (any statistical evidence from internet, magazines, newspapers etc.)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Primary Research (any questionnaires/surveys you have done, or conversations you have had)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the Interview?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.InterviewKeyFinding != null) ? KeyFindingModel.InterviewKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.InterviewKeyFinding == null || KeyFindingModel.InterviewKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the observation?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.ObservationKeyFinding != null) ? KeyFindingModel.ObservationKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.ObservationKeyFinding == null || KeyFindingModel.ObservationKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the survey?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.SurveyKeyFinding != null) ? KeyFindingModel.SurveyKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.SurveyKeyFinding == null || KeyFindingModel.SurveyKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the online research?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.OnlineResearchKeyFinding != null) ? KeyFindingModel.OnlineResearchKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.OnlineResearchKeyFinding == null || KeyFindingModel.OnlineResearchKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //1-Q-Section-3
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Are there certain periods of the year when your sales will vary?  If so, when will you have high sales, average sales and low sales, and why?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Retailers may find that the run up to Christmas is the busiest time of the year but for garden maintenance businesses December may be one of the quietest months.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.SalesCertainPeriod != null ? PriceModel.SalesCertainPeriod : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Which suppliers will you need to use to deliver your product or service?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Why was this supplier(s) chosen? (quality, cost, reputation etc)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("If required, have you got a suppliers contract in place?\rWill you need to pay your suppliers upfront, or will you have a credit account?\rIs there a back-up plan in place if this supplier lets you down?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.PricingStrategy != null ? PriceModel.PricingStrategy : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            HTMLExport export = new HTMLExport();
            document.SaveOptions.HTMLExportImageAsBase64 = true;
            //Saves the document as html file
          //  export.SaveAsXhtml(document, Server.MapPath("~/Content/" + "Sample.html"));

            document.Save("sample.docx", FormatType.Docx, HttpContext.ApplicationInstance.Response, HttpContentDisposition.Attachment);

            return File("application/vnd.ms-word", "sample.docx");
            //return View();
        }
    }

    public class DownloadController : Controller
    {
        // GET: BusinessPlanFile


        public ActionResult Index()
        {
            Client Clientobj = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
            _Business BusinessModel = new BusinessManager().GetBusinessOverview();
            if (BusinessModel == null) { BusinessModel = new _Business(); }
            _ProductService ProductServiceModel = new BusinessManager().GetProductService();
            if (ProductServiceModel == null) { ProductServiceModel = new _ProductService(); }
            _BusinessOperation BusinessOperationModel = new BusinessManager().GetBusinessOperation();
            if (BusinessOperationModel == null) { BusinessOperationModel = new _BusinessOperation(); }
            IEnumerable<ClientTeam> TeamList = new ClientManager().GetClientTeam();
            if (TeamList == null) { TeamList = new List<ClientTeam>(); }
            IEnumerable<CompetitorAnalysis> CompetitorAnalysisModel = new BusinessManager().GetCompetitorAnalysis();
            if (CompetitorAnalysisModel == null) { CompetitorAnalysisModel = new List<CompetitorAnalysis>(); }
            _PricingProductService PriceModel = new SellingManager().GetPricingProductService();
            if (PriceModel == null) { PriceModel = new _PricingProductService(); }
            IEnumerable<_Marketing> MarketingPlanModel = new MarketingManager().GetMarketingPlan();
            if (MarketingPlanModel == null) { MarketingPlanModel = new List<_Marketing>(); }
            SWOT SWOTModel = new BusinessManager().GetCompetitorSWOT();
            if (SWOTModel == null) { SWOTModel = new SWOT(); }
            _Customer CustomerModel = new SellingManager().GetCustomers();
            if (CustomerModel == null) { CustomerModel = new _Customer(); }
            IEnumerable<BuyerPersona> PersonaModel = new SellingManager().GetCustomerbuyerPersona(CustomerModel.CustomerID);
            if (PersonaModel == null) { PersonaModel = new List<BuyerPersona>(); }
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            _KeyFinding KeyFindingModel = new MarketResearchManager().GetKeyFinding();
            if (KeyFindingModel == null) { KeyFindingModel = new _KeyFinding(); }

            // Creating a new document.
            WordDocument document = new WordDocument();
            //Adding a new section to the document.
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new System.Drawing.SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = System.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText("Your Business Idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 30f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.Black;

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("1. Business Information") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;
            //textRange.CharacterFormat.HighlightColor = Color.Yellow;


            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Firstly, please provide some basic information about your business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Name") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //1-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((Clientobj.StartupName != null ? Clientobj.StartupName : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //2-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("When will/did your business start trading?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessModel.Founded != null ? BusinessModel.Founded.ToString() : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Founders") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            IWTable tableFounder = section.AddTable();
            WTableRow rowHeaderFounder = tableFounder.AddRow();

            tableFounder.ResetCells(1, 4);
            tableFounder[0, 0].Width = 102f;
            tableFounder[0, 0].AddParagraph().AppendText("Name");
            tableFounder[0, 1].Width = 180f;
            tableFounder[0, 1].AddParagraph().AppendText("National Insurance Numbers");
            tableFounder[0, 2].Width = 90f;
            tableFounder[0, 2].AddParagraph().AppendText("Phone Number");
            tableFounder[0, 3].Width = 100f;
            tableFounder[0, 3].AddParagraph().AppendText("Email");
            WTableCell cellFounder = rowHeaderFounder.AddCell();

            int i = 2;
            foreach (var item in TeamList)
            {
                if (item.RoleInCompany != null)
                    if (item.RoleInCompany.Contains("15a27065-6b7b-49d0-b3b9-cabd751204ed") || item.RoleInCompany.Contains("8856ff49-5620-4eb5-af56-b82cdc3d889c"))
                    {

                        WTableRow row = tableFounder.AddRow(true, false);
                        //for (int m = 1; m <= 4; m++)
                        //{
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.FirstName != null ? item.FirstName : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText("");
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.Phone != null ? item.Phone.ToString() : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        cellFounder = row.AddCell();
                        cellFounder.AddParagraph().AppendText((item.Email != null ? item.Email.ToString() : ""));
                        cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                        cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        //}
                        i = i + 1;
                    }
            }



            //5-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Website Address (if applicable)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //5-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("No website yet") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //----------------------------Section-2--------------------------------//

            IWSection section2 = document.AddSection();
            IWTextRange textrange = new WTextRange(document);
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("2. Description of your business idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("What does/will your business do?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Provide a brief description of the main activities of your business.\rList the range of different products and services you will offer.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((ProductServiceModel.PSDescription != null ? ProductServiceModel.PSDescription.ToString() : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (ProductServiceModel.PSDescription == null || ProductServiceModel.PSDescription.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            //2-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How did you arrive at this business idea?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessModel.IdeaComeup != null) ? BusinessModel.IdeaComeup.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            if (BusinessModel.IdeaComeup == null || BusinessModel.IdeaComeup.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Does your business require premises?  If so, please provide details of the costs involved and the current status of discussions with the landlord.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            // Sub Heading of Q-3
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please provide a web link to the property on an Estate Agent or landlord website.\rIf available please provide a copy of communications with the landlord, including any documentation such as draft lease agreements.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(BusinessModel.LandlordCostStatus != null /*&& BusinessModel.BusinessRequirePremises == true*/ ? BusinessModel.LandlordCostStatus.ToString() : "No Business Premises Required") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Do you or your business require any specialist training, licences, certificates or insurances to trade? (e.g. health & safety certificates, food hygiene, public liability insurance, trade qualifications, certificates from training courses)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            // Question Heading 3-Q
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please list any legal requirements here and provide any documentation/certificates to help support your application.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;



            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessOperationModel.LicenseType != null ? "Yes, I need specifice license to wok\r" + BusinessOperationModel.LicenseType : "No I do not need specific license to work\r")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (BusinessOperationModel.LicenseType == null || BusinessOperationModel.LicenseType.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessOperationModel.QualificationType != null ? "Yes I need specific qualifications\r" + BusinessOperationModel.QualificationType : "No I do not need specific qualification")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (BusinessOperationModel.QualificationType == null || BusinessOperationModel.QualificationType.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("List the people involved (including yourself) and their roles in the business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;


            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("You can also add details of you and your team’s knowledge and experience in your industry.\rWhat skills and qualities do you have that will make your business a success?\rPlease also provide a CV for those people in the business who are applying for a loan.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //(TeamList.FirstOrDefault().TeamInfo != null ? TeamList.FirstOrDefault().TeamInfo : "")

            //3-A-Section-1
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((TeamList.Count() > 0 && TeamList.FirstOrDefault().TeamInfo != null ? TeamList.FirstOrDefault().TeamInfo : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            IWTable tableTeam = section2.AddTable();
            tableTeam.ResetCells(1, 4);
            tableTeam[0, 0].Width = 102f;
            tableTeam[0, 0].AddParagraph().AppendText("Name");
            tableTeam[0, 1].Width = 180f;
            tableTeam[0, 1].AddParagraph().AppendText("Role at Company");
            tableTeam[0, 2].Width = 90f;
            tableTeam[0, 2].AddParagraph().AppendText("Linkedin");
            tableTeam[0, 3].Width = 100f;
            tableTeam[0, 3].AddParagraph().AppendText("Short Bio");
            // tableTeam.TableFormat.IsAutoResized = true;
            WTableRow rowHeaderTeam = tableTeam.AddRow();
            WTableCell cellTeam = rowHeaderTeam.AddCell();

            i = 2;
            List<MasterCommon> RoleList = new Master().GetOptionMasterList((int)OptionType.RoleInCompany).ToList();
            foreach (var item in TeamList)
            {
                //string RolesValues = "";
                //if (item.RoleInCompany != null)
                //{
                //    List<string> Roles = item.RoleInCompany.Split(',').ToList<string>();

                //    foreach (var _Roles in Roles)
                //    {
                //        if (_Roles != null && Guid.Parse(_Roles) != Guid.Empty)
                //        {
                //            RolesValues += RoleList.Where(x => x.ID == Guid.Parse(_Roles)).Select(x => x.Value).FirstOrDefault().ToString() + "  ";
                //        }
                //    }
                //}

                WTableRow row = tableTeam.AddRow(true, false);
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.FirstName != null ? item.FirstName : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText("");
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.LinkedProfile != null ? item.LinkedProfile.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.ShortBio != null ? item.ShortBio.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
            }

            //----------------------------Section-3--------------------------------//
            IWSection section3 = document.AddSection();
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("3. Your Market ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Who are your customers?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How would you describe your typical customer?  Think about their:- age - gender - location - interests - any other relevant information that helps you to define who will buy your products/services. Try to identify different groups of customers.  e.g. you may sell to a certain segment of the general public, but also be able to agree contracts with organisations – these would be considered two different groups of customers.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Describe your typical customer") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.About != null) ? CustomerModel.About.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (CustomerModel.About == null || CustomerModel.About.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Where are they based") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.Based != null) ? CustomerModel.Based.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (CustomerModel.Based == null || CustomerModel.Based.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Why are they going to buy from you") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((CustomerModel.Buy != null) ? CustomerModel.Buy.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            if (CustomerModel.Buy == null || CustomerModel.Buy.Length < 100)
                paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            IWTable tablePersona = section3.AddTable();
            WTableRow rowHeaderPersona = tablePersona.AddRow();
            //tableFounder.TableFormat.HorizontalAlignment=HorizontalAlignment.Distribute;
            tablePersona.TableFormat.Borders.BorderType = BorderStyle.None;
            tablePersona.TableFormat.IsBreakAcrossPages = true;
            tablePersona.IndentFromLeft = 5;
            WTableCell cellPersona = rowHeaderPersona.AddCell();
            cellPersona.AddParagraph().AppendText("Name");
            cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
            cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            // cellPersona.CharacterFormat.Bold = true;
            cellPersona = rowHeaderPersona.AddCell();
            cellPersona.AddParagraph().AppendText("Job Title");
            cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
            cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            i = 2;
            foreach (var item in PersonaModel)
            {

                WTableRow row = tablePersona.AddRow(true, false);
                cellFounder = row.AddCell();
                cellFounder.AddParagraph().AppendText((item.Name != null ? item.Name : ""));
                cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellFounder = row.AddCell();
                cellFounder.AddParagraph().AppendText((item.JobTitle != null ? item.JobTitle : ""));
                cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
                if (i == 6) break;
            }

            //2-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How you will advertise / promote your business?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Describe how customers will find out about your product/service - what methods you will use to promote the business, provide details of target audience, medium chosen, costs, frequency?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Examples: adverts in papers or trade magazines, direct mail shot, leaflets, brochures, flyers, email, exhibitions, events and trade fairs, canvassing, personal contact with clients, online advertising (Google, Facebook etc), online banners, business websites") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How you will monitor the response?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            IWTable tableMarketPlan = section3.AddTable();
            tableMarketPlan.TableFormat.IsAutoResized = true;
            WTableRow rowHeaderMarketPlan = tableMarketPlan.AddRow();
            tableMarketPlan.TableFormat.Borders.BorderType = BorderStyle.None;

            tableMarketPlan.IndentFromLeft = 5;
            WTableCell cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Goals");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            textRange.CharacterFormat.Bold = true;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("KPI's");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Find Buyers");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Unique Selling Proposition");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Plan to Reach Audience");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellMarketPlan = rowHeaderMarketPlan.AddCell();
            cellMarketPlan.AddParagraph().AppendText("Track KPI's");
            cellMarketPlan.CellFormat.BackColor = Color.LightSkyBlue;
            cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            i = 2;
            List<MasterCommon> GoalList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_Goals).ToList();
            List<MasterCommon> AudienceList = new Master().GetOptionMasterList((int)OptionType.MarketingPlan_AudianceReach).ToList();

            foreach (var item in MarketingPlanModel)
            {

                WTableRow row = tableMarketPlan.AddRow(true, false);
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.GoalID.ToString() != null && item.GoalID != Guid.Empty ? GoalList.Where(x => x.ID == item.GoalID).Select(x => x.Value).FirstOrDefault().ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.KPIS != null ? item.KPIS.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.FindBuyers != null ? item.FindBuyers.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.AudianceReachID.ToString() != null && item.AudianceReachID != Guid.Empty ? GoalList.Where(x => x.ID == item.AudianceReachID).Select(x => x.Value).FirstOrDefault().ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellMarketPlan = row.AddCell();
                cellMarketPlan.AddParagraph().AppendText((item.TrackKPIS != null ? item.TrackKPIS.ToString() : ""));
                cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //cellMarketPlan = row.AddCell();
                //cellMarketPlan.AddParagraph().AppendText((item.ShortBio != null ? item.ShortBio.ToString() : ""));
                //cellMarketPlan.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                //cellMarketPlan.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                i = i + 1;
            }

            //3-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Competitor Analysis Table") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 2;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Regardless of how unique your business idea is, you will always have competitors. Let us know who they are and what makes them successful.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            IWTable tableCompetitior = section3.AddTable();
            WTableRow rowHeadeCompetitorr = tableCompetitior.AddRow();
            WTableCell cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Competitor");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Location");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("Price");
            cellCompetitor.CellFormat.BackColor = Color.LightSkyBlue;
            cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellCompetitor = rowHeadeCompetitorr.AddCell();
            cellCompetitor.AddParagraph().AppendText("What do they do well");
            i = 1;
            foreach (var item in CompetitorAnalysisModel)
            {

                WTableRow row = tableCompetitior.AddRow(true, false);
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Name != null ? item.Name : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Location != null ? item.Location : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Pricing != null ? item.Pricing : ""));
                cellCompetitor.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellCompetitor.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellCompetitor = row.AddCell();
                cellCompetitor.AddParagraph().AppendText((item.Offering != null ? item.Offering : ""));
                i = i + 1;
            }


            //4-Q-Section-3
            paragraph = section3.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("SWOT Analysis. What are the strengths and weaknesses of your business idea?  What opportunities and threats could it face?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            IWTable tableSWOT = section3.AddTable();
            WTableRow rowHeaderSWOT = tableSWOT.AddRow();
            WTableCell cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Strengths\r Internal characteristics of the business that give it an advantage over others");
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Weaknesses \r Internal characteristics that place the business at a disadvantage relative to others");
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Strengths != null ? SWOTModel.Strengths : ""));
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Weaknesses != null ? SWOTModel.Weaknesses : ""));
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Opportunities\r External factors that the business could exploit to its advantage");
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText("Threats \r External factors that could cause trouble for the business");
            rowHeaderSWOT = tableSWOT.AddRow(true, false);
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Opportunities != null ? SWOTModel.Opportunities : ""));
            cellSWOT = rowHeaderSWOT.AddCell();
            cellSWOT.AddParagraph().AppendText((SWOTModel.Threats != null ? SWOTModel.Threats : ""));
            //----------------------------Section-4--------------------------------//

            IWSection section4 = document.AddSection();
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("4. Pricing and Selling ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;


            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-2
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("List the prices you will charge for the different products and services you offer.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Describe how you will set prices for your product service. Include details of prices charged by the business and how they are calculated.\rTake into account your costs, hourly rate, the going rates in your industry, competition, demand etc.\rRemember that what people will pay for your product is determined by the value they place upon it, not its cost of production to you.\rDon’t worry if you don’t know all of this information yet just enter whatever you can.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.PricingStrategy != null ? PriceModel.PricingStrategy : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //1-Q-Section-3
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Have you made any sales so far, do you have any pre-orders, or customers waiting to purchase your product(s) and service(s)?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("If you do not have any sales or pre-orders, what research have you done to identify that there is a demand for your products/services?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Desk Research (any statistical evidence from internet, magazines, newspapers etc.)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Primary Research (any questionnaires/surveys you have done, or conversations you have had)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the Interview?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.InterviewKeyFinding != null) ? KeyFindingModel.InterviewKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.InterviewKeyFinding == null || KeyFindingModel.InterviewKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the observation?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.ObservationKeyFinding != null) ? KeyFindingModel.ObservationKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.ObservationKeyFinding == null || KeyFindingModel.ObservationKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the survey?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.SurveyKeyFinding != null) ? KeyFindingModel.SurveyKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.SurveyKeyFinding == null || KeyFindingModel.SurveyKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Your key findings from the online research?") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((KeyFindingModel.OnlineResearchKeyFinding != null) ? KeyFindingModel.OnlineResearchKeyFinding.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (KeyFindingModel.OnlineResearchKeyFinding == null || KeyFindingModel.OnlineResearchKeyFinding.Length < 200)
                paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //1-Q-Section-3
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Are there certain periods of the year when your sales will vary?  If so, when will you have high sales, average sales and low sales, and why?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("e.g. Retailers may find that the run up to Christmas is the busiest time of the year but for garden maintenance businesses December may be one of the quietest months.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.SalesCertainPeriod != null ? PriceModel.SalesCertainPeriod : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";


            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Which suppliers will you need to use to deliver your product or service?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Why was this supplier(s) chosen? (quality, cost, reputation etc)") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("If required, have you got a suppliers contract in place?\rWill you need to pay your suppliers upfront, or will you have a credit account?\rIs there a back-up plan in place if this supplier lets you down?") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;
            paragraph = section4.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((PriceModel.PricingStrategy != null ? PriceModel.PricingStrategy : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            HTMLExport export = new HTMLExport();
            document.SaveOptions.HTMLExportImageAsBase64 = true;
            //Saves the document as html file
            //  export.SaveAsXhtml(document, Server.MapPath("~/Content/" + "Sample.html"));

            document.Save("sample.docx", FormatType.Docx, HttpContext.ApplicationInstance.Response, HttpContentDisposition.Attachment);

            return File("application/vnd.ms-word", "sample.docx");
            //return View();
        }
    }
}