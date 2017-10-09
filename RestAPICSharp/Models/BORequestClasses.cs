using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestAPICSharp.Models
{
    public class PboRequest
    {
        public PboEligibilityRequest Request { get; set; }
        public string ServiceCodes { get; set; }
        public APIResponse Response { get; set; }
    }
    public class PboEligibilityRequest 
    {
     
        public string PayerCode { set; get; }
        public string PayerName { set; get; }

        public PboProvider Provider { set; get; }                  

         public PboReqSubscriber Subscriber { set; get; }              

        public PboPatientDependent Dependent { set; get; }       

        public string IsSubscriberPatient { set; get; }

     
        public string DOS_StartDate { set; get; }

      
        public string DOS_EndDate { set; get; }

        public List<string> ServiceCodes { set; get; }
          
        public string RequestSource { get; set; }

    }

    public class NetworkSection
    {
        public string Identfier { get; set; }
        public string Label { get; set; }

        public string NetworkType { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    

    public class PboProvider
    {
     
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
     
        public string LastName { set; get; }
        public string NPI { set; get; }
        public string GroupNPI { set; get; }
        public string LegacyID { set; get; }
        public string PIN { set; get; }
        public string Taxonomy { set; get; }
        public string FederalTaxID { set; get; }
    

    }

    public class PboPatient
    {
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public string DOB { set; get; }
        public string Gender { set; get; }
 

     
       // public PboAddress Address { set; get; }
    }
    public class PboReqSubscriber :PboPatient
    {
      

        public string MemberID { set; get; }
        public string SSN { set; get; }
        public string EIN { set; get; }
        public string GroupNo { set; get; }
        public string IssueDate { set; get; }

       
    }

    public class PboPatientDependent
    {
        public PboPatient Patient { set; get; }
        public string RelationWithSubscriber { set; get; }
    }

    public class PboAddress
    {
        public string AddressLine1 { set; get; }
        public string AddressLine2 { set; get; }
        public string City { set; get; }
        public string PostalCode { set; get; }
        public string StateProvince { set; get; }
        public string Country { set; get; }
    }
}