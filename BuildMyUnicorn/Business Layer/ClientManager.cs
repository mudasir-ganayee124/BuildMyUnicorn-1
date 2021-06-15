using ALMS_DAL;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace BuildMyUnicorn.Business_Layer
{
    public class ClientManager
    {
        public string AddNewClient(Client Model)
        {
            //Thread AddinGateway = new Thread(delegate ()
            //{
            //    AddCustomerinGateway(Model);
            //});
            //AddinGateway.Start();
            //while (AddinGateway.IsAlive)
            //{
            //    Thread.Sleep(500);
            //  //  Application.DoEvents();
            //}
            Guid user_AffiliateLinkID = Model.AffiliateLinkID;
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StartupName", ParamterValue = Model.StartupName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MemberType", ParamterValue =  MemberType.Contributor, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Keygen.Random()), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Phone", ParamterValue = Model.Phone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }
                
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client", parameters);
            if (result > 0) 
            {


               
                //List<Task> taskList = new List<Task>();
                //taskList.Add(Task.Factory.StartNew(() => AddCustomerinGateway(Model)));


                //return result;

                List<ParametersCollection> Customerparameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
                Client Customer = obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_email", Customerparameters);
                Guid Ref_id = Guid.NewGuid();
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                string ForgotPasswordURL = strUrl;
                string EncryptedID = Encryption.EncryptGuid(Ref_id.ToString());
                ForgotPasswordURL = ForgotPasswordURL + "/Register/EmailVerification?refid=" + EncryptedID;
                string ForgotEmailTemplate = EmailTemplates.Templates["FP"];
                ForgotEmailTemplate = ForgotEmailTemplate.Replace("@URL", ForgotPasswordURL).Replace("@NAME", Customer.FirstName + " " + Customer.LastName);
                string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];

                //Finally Send Mail and save data Async
                Thread email_sender_thread = new Thread(delegate ()
                {
                    EmailSender emailobj = new EmailSender();
                    emailobj.SendMail(SenderEmail, Model.Email, "Build my Unicorn Account", ForgotEmailTemplate);
                });

                Thread SaveRestLink = new Thread(delegate ()
                {
                    List<ParametersCollection> parametersConfirmation = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ID", ParamterValue = Ref_id, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Customer.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },

            };
                    obj.Execute(CommandType.StoredProcedure, "sp_add_email_confirmation", parametersConfirmation);


                });
                int NetworkType = 1;
                if (user_AffiliateLinkID != Guid.Empty)
                {
                    NetworkType = 2;
                }
                Thread UpdateNetwork = new Thread(delegate ()
                {
                    List<ParametersCollection> parametersNetwork = new List<ParametersCollection>() {
                    new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Customer.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                    new ParametersCollection { ParamterName = "@EntityType", ParamterValue = _EntityType.Client, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                    new ParametersCollection { ParamterName = "@AffiliateLinkID", ParamterValue = user_AffiliateLinkID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                    new ParametersCollection { ParamterName = "@NetworkType", ParamterValue = NetworkType, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                    new ParametersCollection { ParamterName = "@Status", ParamterValue = 1, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

                 }; obj.Execute(CommandType.StoredProcedure, "sp_update_account_network", parametersNetwork);
                });
                
               
                email_sender_thread.IsBackground = true;
                email_sender_thread.Start();
                SaveRestLink.Start();
                UpdateNetwork.Start();
                //Task.WaitAll(taskList.ToArray());
                //Thread.Sleep(400);
                return "OK";
            }
            else
            {
                return "Email is already registered";
            }
        }

        public string AddTeamMemeber(ClientTeam Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TeamClientID", ParamterValue = Model.TeamClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MemberType", ParamterValue = Model.MemberType, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RoleInCompany", ParamterValue = Model.RoleInCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Phone", ParamterValue = Model.Phone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Password", ParamterValue = string.IsNullOrEmpty(Model.Password) ? Model.Password :Encryption.Encrypt(Model.Password), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ShortBio", ParamterValue = Model.ShortBio, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityState", ParamterValue = Model.EntityState, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_team", parameters);
            if (Model.MemberType == MemberType.Contributor && Model.EntityState == EntityState.New)
            {
                string strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                string ContributorTemplate = EmailTemplates.Templates["CT"];
                string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
                ContributorTemplate = ContributorTemplate.Replace("@URL", strUrl).Replace("@NAME", Model.FirstName + " " + Model.LastName).Replace("@UNM", Model.Email.ToString()).Replace("@PSW", Model.Password.ToString());
                //  EmailSender emailobj = new EmailSender();
                //emailobj.SendMail(SenderEmail, Model.Email, "Build my Unicorn Contributor Account", ContributorTemplate);
                Thread email_sender_thread = new Thread(delegate ()
                 {
                     EmailSender emailobj = new EmailSender();
                     emailobj.SendMail(SenderEmail, Model.Email, "Build my Unicorn Contributor Account", ContributorTemplate);
                 });
                email_sender_thread.IsBackground = true;
                email_sender_thread.Start();
            }
            return result > 0 ? "OK" : "Error in Adding Team";


        }

        public string AddClientSurvey(Survey Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SurveyTitle", ParamterValue = Model.SurveyTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@SurveyForm", ParamterValue = Model.SurveyForm, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_survey", parameters);
            return result > 0 ? "OK" : "Error in Adding Survey";


        }


        public string AddTeamInfo(string TeamInfo)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@TeamInfo", ParamterValue = TeamInfo, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_client_team_info", parameters);
            return result > 0 ? "OK" : "Error in Adding Team info";


        }

        public void AddSurveyData(List<SurveyData> ModelList, Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            //List<ParametersCollection> parameters = new List<ParametersCollection>() {
            //    new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
            //    new ParametersCollection { ParamterName = "@SurveyTitle", ParamterValue = Model.SurveyTitle, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
            //    new ParametersCollection { ParamterName = "@SurveyForm", ParamterValue = Model.SurveyForm, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
            //    new ParametersCollection { ParamterName = "@CreatedBy", ParamterValue = Convert.ToInt16(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input }
            //};
            ModelList.ForEach(x => x.SurveyDataID = new Guid());
            ModelList.ForEach(x => x.SurveyID = SurveyID);
            ModelList.ForEach(x => x.CreatedDateTime = DateTime.Now);
            DataTable dtSurveyData = Extensions.ListToDataTable(ModelList);
            obj.ExecuteBulkInsert("sp_add_survey_data", dtSurveyData, "UT_Survey_Data", "@DataTable");
        }

        public async Task<string> AddCustomerinGateway(Client Model)
        {
          
            var queryPlan = $@"SELECT *, tbl_currency.Code FROM tbl_plan INNER JOIN  dbo.tbl_currency ON tbl_currency.CurrencyID = tbl_plan.CurrencyID where PlanID = '{Model.PlanID}'";
            var queryGateway = $@"select * from tbl_gateway WHERE GatewayType = '{(int)GatewayType.Revolut}'";
            Gateway gateway = SharedManager.GetSingle<Gateway>(queryGateway);
            Plan Plan = SharedManager.GetSingle<Plan>(queryPlan);
            CreateCustomerRes reqCustomer = new CreateCustomerRes();
            Order OrderObj = new Order();
            reqCustomer.BusinessName = Model.StartupName;
            reqCustomer.FullName = Model.FirstName + " " + Model.LastName;
            reqCustomer.Phone = Model.Phone;
            reqCustomer.Email = Model.Email;
            Result<CreateCustomerRes> Customers = await new RevolutManager().Post<CreateCustomerRes>("customers", gateway, reqCustomer);
            return Customers.Value.id.ToString();
            //CreateOrderReq reqOrder = new CreateOrderReq();
            //reqOrder.Amount = Convert.ToDouble(Plan.Amount);
            //reqOrder.CaptureMode = CaptureModeEnum.AUTOMATIC;
            //reqOrder.Currency = Plan.Code.ToString();
            //reqOrder.CustomerEmail = Model.Email;
            //reqOrder.Description = Plan.PlanName.ToString();
            //reqOrder.MerchantCustomerExtRef = Customer.Value.id.ToString();
            //reqOrder.MerchantOrderExtRef = Customer.Value.id.ToString();
            //reqOrder.SettlementCurrency = Plan.Code.ToString();
            //Result<CreateOrderReq> order = await new RevolutManager().Post<CreateOrderReq>("orders", gateway, reqOrder);
            //OrderObj.ClientID = Model.ClientID;
            //OrderObj.OrderStatus = OrderStatus.PENDING;
            //OrderObj.PlanID = Model.PlanID;
            //OrderObj.GatewayClientID = Guid.Parse(Customer.Value.id);
            //OrderObj.GatewayOrderID = Guid.Parse(order.Value.Id);
            //OrderObj.OrderPublicID = Guid.Parse(order.Value.PublicId);
            //AddNewOrder(OrderObj);




        }

        public async Task<string> AddOrderinGateway(Client Model, string CustomerID)
        {
            var queryPlan = $@"SELECT *, tbl_currency.Code FROM tbl_plan INNER JOIN  dbo.tbl_currency ON tbl_currency.CurrencyID = tbl_plan.CurrencyID where PlanID = '{Model.PlanID}'";
            var queryGateway = $@"select * from tbl_gateway WHERE GatewayType = '{(int)GatewayType.Revolut}'";
            Gateway gateway = SharedManager.GetSingle<Gateway>(queryGateway);
            Plan Plan = SharedManager.GetSingle<Plan>(queryPlan);
         
            CreateOrderReq reqOrder = new CreateOrderReq();
            reqOrder.InternalAmount = Plan.Amount;
            reqOrder.CaptureMode = CaptureModeEnum.AUTOMATIC;
            reqOrder.Currency = Plan.Code.ToString();
            reqOrder.CustomerEmail = Model.Email;
            reqOrder.Description = Plan.PlanName.ToString();
            reqOrder.MerchantCustomerExtRef = CustomerID.ToString();
            reqOrder.MerchantOrderExtRef = CustomerID.ToString();
            reqOrder.SettlementCurrency = Plan.Code.ToString();
            Result<CreateOrderReq> order = await new RevolutManager().Post<CreateOrderReq>("orders", gateway, reqOrder);
            return order.Value.PublicId;

 
        }


        public void AddNewOrder(Order Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@OrderID", ParamterValue = Model.OrderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@PlanID", ParamterValue = Model.PlanID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@OrderStatus", ParamterValue = Model.OrderStatus, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@OrderPublicID", ParamterValue = Model.OrderPublicID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@GatewayClientID", ParamterValue = Model.GatewayClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@GatewayOrderID", ParamterValue = Model.GatewayOrderID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },


            };
            obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_order", parameters);
            //if (result > 0) return "OK"; else return "Order failed";

        }

        public Client GetSingleClient(Guid ClientID)
        {
            var query = $@"SELECT tbl_client.* FROM tbl_client WHERE ClientID = '{ClientID}'";
            return SharedManager.GetSingle<Client>(query);
        }

        public Client GetSingleClientByEmail(string Email)
        {
            var query = $@"SELECT tbl_client.* FROM tbl_client where Email  =  '{Email}' AND IsActive = 1 AND IsDeleted = 0";
            return SharedManager.GetSingle<Client>(query);
        }

        public Order GetClientOrder(Guid ClientID)
        {
            var query = $@"select * from tbl_order where ClientID  =  '{ClientID}'";
            return SharedManager.GetSingle<Order>(query);
        }

        public Client GetClient(Guid ClientID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_id", parameters);
        }

        public Guid GetMainClientID(Guid ClientID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            Client Model = obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_id", parameters);
            if (Model.MemberType == MemberType.Contributor)
            {
                if (Model.TeamClientID != Guid.Empty)
                     return Model.TeamClientID;
                else return Model.ClientID;
            }
            else return Model.ClientID;
        }

        public IEnumerable<ClientTeam> GetClientTeam()
        {
            Guid ClientID  = new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name));
            var query = $@"SELECT dbo.tbl_client.ClientID, dbo.tbl_client.TeamClientID, dbo.tbl_client.FirstName, dbo.tbl_client.LastName, dbo.tbl_client.MemberType, dbo.tbl_client.RoleInCompany, dbo.tbl_client.Email, dbo.tbl_client.LinkedProfile, dbo.tbl_client.ImageID, dbo.tbl_client.ShortBio, dbo.tbl_client.Phone, dbo.tbl_client.CountryID, dbo.tbl_client.CreatedBy, dbo.tbl_client.ModifiedBy, dbo.tbl_client.CreatedDateTime, dbo.tbl_client.ModifiedDateTime, dbo.tbl_client.IsActive, tbl_countries.CountryName, (SELECT TeamInfo FROM tbl_client WHERE ClientID = '{ new ClientManager().GetMainClientID(Guid.Parse(HttpContext.Current.User.Identity.Name))}') AS TeamInfo FROM dbo.tbl_client LEFT JOIN tbl_countries ON tbl_client.CountryID = tbl_countries.CountryID WHERE TeamClientID = '{ClientID}' OR ClientID = '{ClientID}' ";
            return SharedManager.GetList<ClientTeam>(query);
           
        }

        public IEnumerable<Survey> GetClientAllSurveyForm()
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<Survey>(CommandType.StoredProcedure, "sp_get_client_all_survey", parameters);
        }

        public Survey GetClientSurveyForm(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<Survey>(CommandType.StoredProcedure, "sp_get_client_survey", parameters);
        }

        public IEnumerable<SurveyData> GetSurveyData(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetList<SurveyData>(CommandType.StoredProcedure, "sp_get_survey_data", parameters);
        }
        public string UpdateClientProfile(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@StartupName", ParamterValue = Model.StartupName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Phone", ParamterValue = Model.Phone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MemberType", ParamterValue = Model.MemberType, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RoleInCompany", ParamterValue = Model.RoleInCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ShortBio", ParamterValue = Model.ShortBio, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_profile", parameters);
            return result > 0 ? "OK" : "Error in Adding Team";


        }
        public string UpdateTeamProfile(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@RoleInCompany", ParamterValue = Model.RoleInCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ShortBio", ParamterValue = Model.ShortBio, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }
            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_profile", parameters);
            return result > 0 ? "OK" : "Error in Adding Team";


        }


        //public string UpdateClientTeam(ClientTeam Model)
        //{
        //    DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
        //    List<ParametersCollection> parameters = new List<ParametersCollection>() {
        //        new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@RoleInCompany", ParamterValue = Model.RoleInCompany, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@FirstName", ParamterValue = Model.FirstName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@LastName", ParamterValue = Model.LastName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@Phone", ParamterValue = Model.Phone, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@ImageID", ParamterValue = Model.ImageID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@LinkedProfile", ParamterValue = Model.LinkedProfile, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@ShortBio", ParamterValue = Model.ShortBio, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@CountryID", ParamterValue = Model.CountryID, ParamterType = DbType.Int32, ParameterDirection = ParameterDirection.Input },
        //        new ParametersCollection { ParamterName = "@ModifiedBy", ParamterValue = Guid.Parse(HttpContext.Current.User.Identity.Name), ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }

        //    };
        //    int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_client_team", parameters);
        //    return result > 0 ? "OK" : "Error in Adding Team";


        //}


        public string UpdateCustomerPassword(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Model.Password), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = _EntityType.Client, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_password", parameters);
            if (result > 0) return "OK"; else return "Password update Failed, Please Try again";

        }

        public void UpdateCustomerEmailConfirmation(Client Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ID", ParamterValue = Model.ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            obj.Execute(CommandType.StoredProcedure, "sp_update_email_confirmation", parameters);

        }

        public void UpdateCustomerCustomerForgotPassword(Client Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ForgotPasswordID", ParamterValue = Model.ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            obj.Execute(CommandType.StoredProcedure, "sp_update_forgot_password", parameters);

        }

        public string UpdateSurveyStatus(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_survey_active_status", parameters);
            return result > 0 ? "OK" : "Error in Survey Active/Inactive";
        }

        public string ValidateCustomerLogin(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            Client Customer = obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_email", parameters);
            if (Customer != null)
            {


                if (!Customer.IsDeleted)
                {
                    if (Customer.IsActive)
                    {

                        if (Encryption.Encrypt(Model.Password) == Customer.Password)
                        {

                            FormsAuthentication.SetAuthCookie(Customer.ClientID.ToString(), true); return "OK";
                        }
                        else
                        {
                            return "Invalid Username or Password";
                        }
                    }
                    else
                    {
                        return "Your account is not activated, Confirm your email";


                    }
                }
                else
                {
                    return "Invalid Username or Password";
                }
            }
            else
            {
                return "Invalid Username or Password";
            }
        }

        public string ChangePassword(ChangePassword Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@Email", ParamterValue = Model.Email, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            Client Customer = obj.GetSingle<Client>(CommandType.StoredProcedure, "sp_get_client_by_email", parameters);
            if (Customer != null && Model.ClientID == Customer.ClientID)
            {
                if (Encryption.Encrypt(Model.CurrentPassword) == Customer.Password)
                {

                    List<ParametersCollection> parameterPassword = new List<ParametersCollection>() {
                         new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                         new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Model.NewPassword), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                         new ParametersCollection { ParamterName = "@Type", ParamterValue = 1, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

                     };
                    int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_password", parameterPassword);
                    if (result > 0) return "OK"; else return "Password update Failed, Please Try again";
                }
                else
                {
                    return "Incorect Current Password";
                }
            }
            else
            {
                return "Password update Failed, Please Try again";
            }
        }


        public string[] ConfirmEmail(string refid)
        {
            string[] returnvalue = new string[4];
            try
            {

                Guid ConfirmationID = Guid.Parse(Encryption.DecryptGuid(refid));
                DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
                List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@id", ParamterValue = ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
                EmailConfirmation link = obj.GetSingle<EmailConfirmation>(CommandType.StoredProcedure, "sp_get_email_confirmation", parameters);
                if (link != null)
                {
                    if (link.LinkUsed == false)
                    {
                        TimeSpan varTime = DateTime.Now - link.ExpiryDateTime;
                        double fractionalMinutes = varTime.TotalMinutes;

                        if (fractionalMinutes <= 107787779999990)
                        {
                            returnvalue[0] = "OK";
                            returnvalue[1] = link.EntityID.ToString();
                            returnvalue[2] = ConfirmationID.ToString();
                            // returnvalue[3] = new ClientManager().GetClient(link.EntityID).FirstName.ToString();
                            return returnvalue;
                        }
                        else

                        {
                            returnvalue[0] = "Link Expired";
                            returnvalue[1] = "0";
                            returnvalue[2] = "0";
                            returnvalue[3] = "";
                            return returnvalue;
                        }
                    }

                    else

                    {
                        returnvalue[0] = "Link Is Already Used";
                        returnvalue[1] = "0";
                        returnvalue[2] = "0";
                        returnvalue[3] = "";
                        return returnvalue;
                    }
                }

                else

                {
                    returnvalue[0] = "Invalid Query String";
                    returnvalue[1] = "0";
                    returnvalue[2] = "0";
                    returnvalue[3] = "";
                    return returnvalue;
                }
            }
            catch (Exception)
            {
                returnvalue[0] = "Invalid Query String";
                returnvalue[1] = "0";
                returnvalue[2] = "0";
                returnvalue[3] = "";
                return returnvalue;
            }

        }

        public string[] ConfirmResetPassword(string refid)
        {
            string[] returnvalue = new string[4];
            try
            {

                Guid ConfirmationID = Guid.Parse(Encryption.DecryptGuid(refid));
                var query = $@"SELECT tbl_forgot_password.* FROM tbl_forgot_password WHERE ForgotPasswordID= '{ConfirmationID}'";
                ForgotPassword link =  SharedManager.GetSingle<ForgotPassword>(query);

                if (link != null)
                {
                    if (link.LinkUsed == false)
                    {
                        TimeSpan varTime = DateTime.Now - link.ForgotDatetime;
                        double fractionalMinutes = varTime.TotalMinutes;

                        if (fractionalMinutes <= 30)
                        {
                            returnvalue[0] = "OK";
                            returnvalue[1] = link.ClientID.ToString();
                            returnvalue[2] = ConfirmationID.ToString();
                            //  returnvalue[3] = new ClientManager().GetClient().FirstName.ToString();
                            return returnvalue;
                        }
                        else

                        {
                            returnvalue[0] = "Link Expired";
                            returnvalue[1] = "0";
                            returnvalue[2] = "0";
                            returnvalue[3] = "";
                            return returnvalue;
                        }
                    }

                    else

                    {
                        returnvalue[0] = "Link Is Already Used";
                        returnvalue[1] = "0";
                        returnvalue[2] = "0";
                        returnvalue[3] = "";
                        return returnvalue;
                    }
                }

                else

                {
                    returnvalue[0] = "Invalid Query String";
                    returnvalue[1] = "0";
                    returnvalue[2] = "0";
                    returnvalue[3] = "";
                    return returnvalue;
                }
            }
            catch (Exception)
            {
                returnvalue[0] = "Invalid Query String";
                returnvalue[1] = "0";
                returnvalue[2] = "0";
                returnvalue[3] = "";
                return returnvalue;
            }

        }

        public string SendPasswordRestLink(string Email)
        {
            var query = $@"SELECT tbl_client.* FROM tbl_client WHERE Email = '{Email}'";
            Client Client  =  SharedManager.GetSingle<Client>(query);
            if (Client != null)
            {
                if (Client.IsActive == true)
                {
                    Guid Ref_id = Guid.NewGuid();
                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                    string ForgotPasswordURL = strUrl;
                    string EncryptedID = Encryption.EncryptGuid(Ref_id.ToString());
                    ForgotPasswordURL = ForgotPasswordURL + "/Register/ResetPassword?refid=" + EncryptedID;
                    var template = new Master().GetTemplate((int)TemplateType.PlatformForgotPassword);
                    string ForgotEmailTemplate = template.Body.ToString();//ForgotPasswordTemplate.Template["FP"];
                    ForgotEmailTemplate = ForgotEmailTemplate.Replace("@URL", ForgotPasswordURL).Replace("@NAME", Client.FirstName + " " + Client.LastName);
                    string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
                    //Finally Send Mail and save data Async
                    Thread email_sender_thread = new Thread(delegate ()
                    {
                        EmailSender emailobj = new EmailSender();
                        emailobj.SendMail(SenderEmail, Email, template.Subject.ToString(), ForgotEmailTemplate);
                    });

                    Thread SaveRestLink = new Thread(delegate ()
                     {
                        DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
                        List<ParametersCollection> parametersConfirmation = new List<ParametersCollection>() {
                    new ParametersCollection { ParamterName = "@ForgotPasswordID", ParamterValue = Ref_id, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                    new ParametersCollection { ParamterName = "@ClientID", ParamterValue = Client.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },

                   };
                        obj.Execute(CommandType.StoredProcedure, "sp_add_password_reset", parametersConfirmation);


                    });
                    email_sender_thread.IsBackground = true;
                    email_sender_thread.Start();
                    SaveRestLink.Start();
                    return "OK";
                }
                else
                {
                    return "Account is temporary Inactive";
                }
            }
            else
            {
                return "User does not exist";
            }
        }    

        public string DeleteSurvey(Guid SurveyID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@SurveyID", ParamterValue = SurveyID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_delete_client_survey", parameters);
            return result > 0 ? "OK" : "Error in Delete";
        }

      

    }
}