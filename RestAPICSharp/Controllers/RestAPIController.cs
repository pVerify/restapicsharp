using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestAPICSharp.Models;
using RestSharp;
using System.Configuration;

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
    }
}