using BuildMyUnicorn.Business_Layer;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Syncfusion.Drawing;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BuildMyUnicorn.Controllers
{
    public class LoginController : Controller
    {
        

       
        string url = string.Format("http://sandbox-merchant.revolut.com/api/1.0/orders");
        string bearerToken = string.Format("sk_N7UvtRt463xES8v_leQ9ofC9KF0yOLsxU89MvPCF4WmIc0ZRf9zPFnKnHNlRg7sv");
        public ActionResult  Index()
        {

            //Task task = new Task(GetData);
            //task.Start();
            //task.Wait();
            //HttpClient client = new HttpClient();
            //var url = "http://sandbox-merchant.revolut.com/api/1.0/orders";
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "sk_N7UvtRt463xES8v_leQ9ofC9KF0yOLsxU89MvPCF4WmIc0ZRf9zPFnKnHNlRg7sv");
            //var response =  await client.GetStringAsync(url);

            //client = new HttpClient();
            //client.BaseAddress = new Uri(url);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            //var responseMessage = client.GetAsync(url);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
            //    var jsonResponse = JsonConvert.DeserializeObject<List<string>>(responseData);
            //    return View(jsonResponse);
            //}

            return View();
        }

        public static async void GetData()
        {
            HttpClient client = new HttpClient();
            var url = "https://sandbox-merchant.revolut.com/api/1.0/orders";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "sk_N7UvtRt463xES8v_leQ9ofC9KF0yOLsxU89MvPCF4WmIc0ZRf9zPFnKnHNlRg7sv");
            var response = await client.GetAsync(url);
            if (response.Content != null)
            {
              var  responseContent = await response.Content.ReadAsStringAsync();
            }
            if (response.IsSuccessStatusCode)
            {
               


               // return JsonConvert.DeserializeObject<T>(responseContent, _jsonSerializerSettings);
            }
            else
            {
                //_logger.Error(response.StatusCode + "Error: " + responseContent);
            }

          //  client.DefaultRequestHeaders.Add("Authorization", "Bearer" + "");
           // var response = await client.GetStringAsync(url);
        }

        public string ValidateUser(Client Model)
        {
            
            return new ClientManager().ValidateCustomerLogin(Model);

        }

        public string ChangePassword(ChangePassword Model)
        {

            return new ClientManager().ChangePassword(Model);

        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}