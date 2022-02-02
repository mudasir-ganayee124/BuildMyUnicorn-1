using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using ALMS_DAL;
using Business_Model.Helper;
using Business_Model.Model;

namespace BuildMyUnicornAccelerator.Business_Layer
{
    public class AccountManager
    {
        public string AddNewStartupAccelerator(StartupAccelerator Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@CompanyName", ParamterValue = Model.CompanyName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Website", ParamterValue = Model.Website, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ContactEmail", ParamterValue = Model.ContactEmail, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ContactName", ParamterValue = Model.ContactName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MainContact", ParamterValue = Model.MainContact, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Address", ParamterValue = Model.Address, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Username", ParamterValue = Model.ContactEmail, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Keygen.Random()), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_add_startup_accelerator", parameters);
            if (result > 0)
            {

                List<ParametersCollection> Customerparameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ContactEmail", ParamterValue = Model.ContactEmail, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
                StartupAccelerator StartupAccelerator = obj.GetSingle<StartupAccelerator>(CommandType.StoredProcedure, "sp_get_startup_accelerator_by_email", Customerparameters);
                Guid Ref_id = Guid.NewGuid();
                String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                string ForgotPasswordURL = strUrl;
                string EncryptedID = Encryption.EncryptGuid(Ref_id.ToString());
                ForgotPasswordURL = ForgotPasswordURL + "/Register/EmailVerification?refid=" + EncryptedID;
                string ForgotEmailTemplate = EmailTemplates.Templates["FP"];
                ForgotEmailTemplate = ForgotEmailTemplate.Replace("@URL", ForgotPasswordURL).Replace("@NAME", StartupAccelerator.CompanyName);
                string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
                //Finally Send Mail and save data Async
                Thread email_sender_thread = new Thread(delegate ()
                {
                    EmailSender emailobj = new EmailSender();
                    emailobj.SendMail(SenderEmail, Model.ContactEmail, "Build my Unicorn Startup Accelerator Account", ForgotEmailTemplate);
                });

                Thread SaveRestLink = new Thread(delegate ()
                {
                    List<ParametersCollection> parametersConfirmation = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@ID", ParamterValue = Ref_id, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@EntityID", ParamterValue = StartupAccelerator.StartupAcceleratorID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },

            };
                    obj.Execute(CommandType.StoredProcedure, "sp_add_email_confirmation", parametersConfirmation);


                });
                //int NetworkType = 1;
                //if (user_AffiliateLinkID != Guid.Empty)
                //{
                //    NetworkType = 2;
                //}
                //Thread UpdateNetwork = new Thread(delegate ()
                //{
                //    List<ParametersCollection> parametersNetwork = new List<ParametersCollection>() {
                //    new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Customer.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                //    new ParametersCollection { ParamterName = "@EntityType", ParamterValue = _EntityType.Client, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                //    new ParametersCollection { ParamterName = "@AffiliateLinkID", ParamterValue = user_AffiliateLinkID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                //    new ParametersCollection { ParamterName = "@NetworkType", ParamterValue = NetworkType, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },
                //    new ParametersCollection { ParamterName = "@Status", ParamterValue = 1, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

                // }; obj.Execute(CommandType.StoredProcedure, "sp_update_account_network", parametersNetwork);
                //});
                email_sender_thread.IsBackground = true;
                email_sender_thread.Start();
                SaveRestLink.Start();
                //UpdateNetwork.Start();
                return "OK";
            }
            else
            {
                return "Email is already registered";
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

        public string UpdatePassword(Client Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Model.Password), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = _EntityType.Accelerator, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_password", parameters);
            if (result > 0) return "OK"; else return "Password update Failed, Please Try again";

        }

        public StartupAccelerator GetAccelerator(Guid StartupAcceleratorID)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@StartupAcceleratorID", ParamterValue = StartupAcceleratorID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input } };
            return obj.GetSingle<StartupAccelerator>(CommandType.StoredProcedure, "sp_get_startup_accelerator_by_id", parameters);
        }

        public void UpdateEmailConfirmation(Client Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ID", ParamterValue = Model.ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            obj.Execute(CommandType.StoredProcedure, "sp_update_email_confirmation", parameters);

        }

        public string ValidateCustomerLogin(StartupAccelerator Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@ContactEmail", ParamterValue = Model.ContactEmail, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            StartupAccelerator Customer = obj.GetSingle<StartupAccelerator>(CommandType.StoredProcedure, "sp_get_startup_accelerator_by_email", parameters);
            if (Customer != null)
            {


                if (!Customer.IsDeleted)
                {
                    if (Customer.IsActive)
                    {

                        if (Encryption.Encrypt(Model.Password) == Customer.Password)
                        {

                            FormsAuthentication.SetAuthCookie(Customer.StartupAcceleratorID.ToString(), true); return "OK";
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

        public string[] ConfirmForgotPassword(string refid)
        {
            string[] returnvalue = new string[4];
            try
            {

                Guid ConfirmationID = Guid.Parse(Encryption.DecryptGuid(refid));
                var query = $@"SELECT tbl_forgot_password.* FROM tbl_forgot_password WHERE ForgotPasswordID= '{ConfirmationID}'";
                ForgotPassword link = SharedManager.GetSingle<ForgotPassword>(query);

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

        public string ChangePassword(ChangePassword Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() { new ParametersCollection { ParamterName = "@StartupAcceleratorID", ParamterValue = Model.ClientID, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input } };
            StartupAccelerator startupAccelertor = obj.GetSingle<StartupAccelerator>(CommandType.StoredProcedure, "sp_get_startup_accelerator_by_id", parameters);
            if (startupAccelertor != null && Model.ClientID == startupAccelertor.StartupAcceleratorID)
            {
                if (Encryption.Encrypt(Model.CurrentPassword) == startupAccelertor.Password)
                {

                    List<ParametersCollection> parameterPassword = new List<ParametersCollection>() {
                         new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Model.ClientID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                         new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Model.NewPassword), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                         new ParametersCollection { ParamterName = "@Type", ParamterValue = _EntityType.Accelerator, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

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

        public string SendPasswordResetLink(string Email)
        {
            var query = $@"SELECT tbl_startup_accelerator.* FROM tbl_startup_accelerator WHERE ContactEmail = '{Email}'";
            StartupAccelerator acceleratorObj = SharedManager.GetSingle<StartupAccelerator>(query);
            if (acceleratorObj != null)
            {
                if (acceleratorObj.IsActive == true)
                {
                    Guid Ref_id = Guid.NewGuid();
                    String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, VirtualPathUtility.ToAbsolute("~/"));
                    string ForgotPasswordURL = strUrl;
                    string EncryptedID = Encryption.EncryptGuid(Ref_id.ToString());
                    ForgotPasswordURL = ForgotPasswordURL + "/Account/ResetPassword?refid=" + EncryptedID;
                    var template = new Master().GetEmailTemplate(TemplateType.PFP.ToString());
                    string ForgotEmailTemplate = template.EmailTemplateBody.ToString();//ForgotPasswordTemplate.Template["FP"];
                    ForgotEmailTemplate = ForgotEmailTemplate.Replace("@URL", ForgotPasswordURL).Replace("@NAME", acceleratorObj.ContactName);
                    string SenderEmail = ConfigurationManager.AppSettings["SmtpServerUsername"];
                    //Finally Send Mail and save data Async
                    Thread email_sender_thread = new Thread(delegate ()
                    {
                        EmailSender emailobj = new EmailSender();
                        emailobj.SendMail(SenderEmail, Email, template.EmailTemplateSubject.ToString(), ForgotEmailTemplate);
                    });

                    Thread SaveRestLink = new Thread(delegate ()
                    {
                        DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
                        List<ParametersCollection> parametersConfirmation = new List<ParametersCollection>() {
                    new ParametersCollection { ParamterName = "@ForgotPasswordID", ParamterValue = Ref_id, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                    new ParametersCollection { ParamterName = "@ClientID", ParamterValue = acceleratorObj.StartupAcceleratorID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },

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

        public void UpdateForgotPassword(StartupAccelerator Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
            new ParametersCollection { ParamterName = "@ForgotPasswordID", ParamterValue = Model.ConfirmationID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input }};
            obj.Execute(CommandType.StoredProcedure, "sp_update_forgot_password", parameters);

        }

        public string UpdatePassword(StartupAccelerator Model)
        {
            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
             new ParametersCollection { ParamterName = "@EntityID", ParamterValue = Model.StartupAcceleratorID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Password", ParamterValue = Encryption.Encrypt(Model.Password), ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
             new ParametersCollection { ParamterName = "@Type", ParamterValue = _EntityType.Accelerator, ParamterType = DbType.Int16, ParameterDirection = ParameterDirection.Input },

            };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_password", parameters);
            if (result > 0) return "OK"; else return "Password update Failed, Please Try again";

        }

        public string UpdateProfile(StartupAccelerator Model)
        {

            DataLayer obj = new DataLayer(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString, Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]));
            List<ParametersCollection> parameters = new List<ParametersCollection>() {
                new ParametersCollection { ParamterName = "@StartupAcceleratorID", ParamterValue = Model.StartupAcceleratorID, ParamterType = DbType.Guid, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@CompanyName", ParamterValue = Model.CompanyName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Website", ParamterValue = Model.Website, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ContactEmail", ParamterValue = Model.ContactEmail, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@ContactName", ParamterValue = Model.ContactName, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@MainContact", ParamterValue = Model.MainContact, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input },
                new ParametersCollection { ParamterName = "@Address", ParamterValue = Model.Address, ParamterType = DbType.String, ParameterDirection = ParameterDirection.Input }

        };
            int result = obj.ExecuteWithReturnValue(CommandType.StoredProcedure, "sp_update_startup_accelerator", parameters);
            if (result > 0) return "OK"; else return "Profile update Failed, Please Try again";
        }

    }
}