using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestAPICSharp.Models;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace RestAPICSharp.Controllers
{
    public class RestAPIController : Controller
    {
        // GET: RestAPI
        public ActionResult Index()
        {
            return View();
        }

        #region Token
        public ActionResult Token()
        {
            TokenModel token = new TokenModel();
            token.Token = "";
            token.ApiResponse = "";
            return View(token);
        }

        [HttpPost]
         public ActionResult Token(TokenModel model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/Token", Method.POST);
            //add headers
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            request.AddParameter("application/x-www-form-urlencoded", "username=" + model.UserName + "&password=" + model.Password + "&grant_type=password", ParameterType.RequestBody);
           
            request.RequestFormat = DataFormat.Json;

            RestClient client = new RestClient(apiBaseURL);
            // client.Timeout = 3 * 60 * 1000;//3 minutes

            IRestResponse response = client.Execute(request);
            model.StatusCode = response.StatusCode;
            model.ApiResponse = response.Content;
            if(response.StatusCode==HttpStatusCode.OK)
            {
                dynamic r = JsonConvert.DeserializeObject(response.Content);
                model.Token = r.access_token;
                Session["accessToken"] = model.Token;
                Session["userName"] = model.UserName;
            }
           
         
            return View(model);
        }

        #endregion

        #region Payers


        public ActionResult Payers()
        {
            PayerModel model = new PayerModel();
            model.Token = GetToken();
            model.ClientUserName = GetUserName();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payers(PayerModel model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/GetPayers", Method.GET);
            //add headers
             request.AddHeader("Authorization", "Bearer "+model.Token);
            request.AddHeader("Client-User-Name", model.ClientUserName);
            request.AddHeader("Client-Password",  model.ClientPassword);
            request.RequestFormat = DataFormat.Json;
         
            RestClient client = new RestClient(apiBaseURL);
            // client.Timeout = 3 * 60 * 1000;//3 minutes
            IRestResponse response = client.Execute(request);
            model.StatusCode = response.StatusCode;
            model.ApiResponse = response.Content;

            return View(model);
        }

     
        #endregion


        #region Inquiry
        public ActionResult Self()
        {
            PboRequest model = new PboRequest();
            PboEligibilityRequest request = new PboEligibilityRequest();
            request.Subscriber = new PboReqSubscriber();
            request.ServiceCodes = new List<string>();
            request.Provider = new PboProvider();
            model.Request = request;
            model.Response = new APIResponse();
            model.Response.Token = GetToken();
            model.Response. ClientUserName = GetUserName();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Self(PboRequest model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/EligibilityInquiry", Method.POST);
            //add headers
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + model.Response.Token);
            request.AddHeader("Client-User-Name", model.Response.ClientUserName);
            request.AddHeader("Client-Password", model.Response.ClientPassword);
            request.RequestFormat = DataFormat.Json;

            if( !string.IsNullOrEmpty(model.ServiceCodes))
            {
                model.Request.ServiceCodes = model.ServiceCodes.Split(',').ToList();
            }
            model.Request.IsSubscriberPatient = "True";
            model.Request.RequestSource = "API";
            request.AddBody(model.Request);

            model.Response.ApiRequest = JsonConvert.SerializeObject(model.Request);
            model.Response.ApiRequest = model.Response.ApiRequest.Trim();
       
            RestClient client = new RestClient(apiBaseURL);
          //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.Response.StatusCode = response.StatusCode;
            model.Response.ApiResponse = response.Content;

            return View(model);
        }

      
        public ActionResult Dependent()
        {
            PboRequest model = new PboRequest();
            PboEligibilityRequest request = new PboEligibilityRequest();
            request.Subscriber = new PboReqSubscriber();
            request.Dependent = new PboPatientDependent();
            request.Dependent.Patient = new PboPatient();
            request.ServiceCodes = new List<string>();
            request.Provider = new PboProvider();
            model.Request = request;
            model.Response = new APIResponse();
            model.Response.Token = GetToken();
            model.Response. ClientUserName = GetUserName();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dependent(PboRequest model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/EligibilityInquiry", Method.POST);
            //add headers
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + model.Response.Token);
            request.AddHeader("Client-User-Name", model.Response.ClientUserName);
            request.AddHeader("Client-Password", model.Response.ClientPassword);
            request.RequestFormat = DataFormat.Json;

            if (!string.IsNullOrEmpty(model.ServiceCodes))
            {
                model.Request.ServiceCodes = model.ServiceCodes.Split(',').ToList();
            }
            model.Request.IsSubscriberPatient = "False";
            model.Request.RequestSource = "API";
            request.AddBody(model.Request);

            model.Response.ApiRequest = JsonConvert.SerializeObject(model.Request);

            model.Response.ApiRequest = model.Response.ApiRequest.Trim();

            RestClient client = new RestClient(apiBaseURL);
            //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.Response.StatusCode = response.StatusCode;
            model.Response.ApiResponse = response.Content;

            return View(model);

        }

        #endregion



        #region Get Response API/GetEligibilityResponse/{id}

        public ActionResult GetResponse()
        {
            ElgResponse model = new ElgResponse();
            model.Token = GetToken();
            model.ClientUserName = GetUserName();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetResponse(ElgResponse model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/GetEligibilityResponse/"+model.ElgRequestId, Method.GET);
            //add headers
             request.AddHeader("Authorization", "Bearer " + model.Token);
            request.AddHeader("Client-User-Name", model.ClientUserName);
            request.AddHeader("Client-Password", model.ClientPassword);
        
            RestClient client = new RestClient(apiBaseURL);
            //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.StatusCode = response.StatusCode;
            model.ApiResponse = response.Content;

            return View(model);

        }

       


        #endregion

        #region Pending Transactions


        public ActionResult Pending()
        {
            PayerModel model = new PayerModel();
            model.Token = GetToken();
            model.ClientUserName = GetUserName();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pending(PayerModel model)
        {
            DateTime dt;
            if(!DateTime.TryParse(model.DOS,out dt))
            {
                ModelState.AddModelError("DOS", "Invalid date.");
                return View(model);
            }

            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/GetPendingInquiries?dos="+dt.ToString("MM-dd-yyyy"), Method.GET);
            //add headers
            request.AddHeader("Authorization", "Bearer " + model.Token);
            request.AddHeader("Client-User-Name", model.ClientUserName);
            request.AddHeader("Client-Password", model.ClientPassword);

            RestClient client = new RestClient(apiBaseURL);
            //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.StatusCode = response.StatusCode;
            model.ApiResponse = response.Content;

            return View(model);

        }
        #endregion

        #region Cancel Transactions


        public ActionResult CancelTransaction()
        {
            PayerModel model = new PayerModel();
            model.Token = GetToken();
            model.ClientUserName = GetUserName();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelTransaction(PayerModel model)
        {
           
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/CancelTransaction", Method.POST);
            //add headers
            request.AddHeader("Authorization", "Bearer " + model.Token);
            request.AddHeader("Client-User-Name", model.ClientUserName);
            request.AddHeader("Client-Password", model.ClientPassword);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { TransactionId = model.TransactionId });
            RestClient client = new RestClient(apiBaseURL);

            model.ApiRequest = JsonConvert.SerializeObject(new { TransactionId = model.TransactionId });
            model.ApiRequest = model.ApiRequest.Trim();

            //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.StatusCode = response.StatusCode;
            model.ApiResponse = response.Content;
            
            return View(model);
        }

        #endregion

        #region NON Actions
        [NonAction]
        private string GetToken()
        {
            if(Session["accessToken"] !=null)
            {
                return Session["accessToken"].ToString();
            }

            return "";
        }
        [NonAction]
        private string GetUserName()
        {
            if (Session["userName"] != null)
            {
                return Session["userName"].ToString();
            }

            return "";
        }
#endregion

    }
}