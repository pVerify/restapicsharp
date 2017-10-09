using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestAPICSharp.Models;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;

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
         
            return View(model);
        }

        #endregion

        #region Payers


        public ActionResult Payers()
        {
            PayerModel model = new PayerModel();
            return View(model);
        }

        [HttpPost]
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


    }
}