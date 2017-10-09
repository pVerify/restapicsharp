using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RestAPICSharp.Models
{
    public class PboEligibilityResponse
    {
        public int ElgRequestID { get; set; }
        public string EDIErrorMessage { get; set; }
        public string VerificationStatus { get; set; }
        public string VerificationMessage { get; set; }
        public string IsPayerBackOffice { get; set; }
        public string Status { get; set; }
        public string PayerName { get; set; }
        public string VerificationType { get; set; }
     
      
        public string DOS { get; set; }
        public string Plan { get; set; }
        public string ExceptionNotes { get; set; }
        public string AdditionalInformation { get; set; }
     
        public string OtherMessage { get; set; }
        public string ReportURL { get; set; }
        public PboEligibilityPeriod EligibilityPeriod { get; set; }
        public PboDemographicInfo DemographicInfo { get; set; }
        public List<PboNetworkSection> NetworkSections { get; set; }
        public PboServiceType HealthBenefitPlanCoverageServiceType { get; set; }
        public List<PboServiceType> ServicesTypes { get; set; }
        public List<PboElement> CustomFields { get; set; }

        //Extended Properties
        public PboElgResponseExtensionProperties ExtensionProperties { set; get; }

     
    }

    public class PboElgResponseExtensionProperties
    {
        public string PayerCode { set; get; }
        public int PayerID { get; set; }
        public string ResultReport { get; set; }
        public string VerifiedOn { get; set; }
        public string VerifiedBy { get; set; }
    }

    public class PboEligibilityPeriod
    {
        public string Label { get; set; }
        public string EffectiveFromDate { get; set; }
        public string ExpiredOnDate { get; set; }
    }

    public class PboDemographicInfo
    {
        public PboSubscriber Subscriber { get; set; }
        public PboDependent Dependent { get; set; }
    }

    public class PboNetworkSection
    {
        public string Identifier { get; set; }
        public string Label { get; set; }
        public List<PboElement> InNetworkParameters { get; set; }
        public List<PboElement> OutNetworkParameters { get; set; }
    }

    public class PboServiceType
    {
        public string ServiceTypeName { get; set; }
        public List<PboServiceSection> ServiceTypeSections { get; set; }
    }

    public class PboServiceSection
    {
        public string Label { get; set; }
        public List<PboElement> ServiceParameters { get; set; }
    }

    public class PboElement
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public List<string> Message { get; set; }
    }

    public class PboSubscriber
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public List<PboCommunicationType> CommunicationNumber { get; set; }
        public List<PboDateType> Date { get; set; }
        public string DOB_R { get; set; }
        public string Firstname { get; set; }
        public string Gender_R { get; set; }
        public List<PboIdentificationType> Identification { get; set; }
        public string Lastname_R { get; set; }
        public string Middlename { get; set; }
        public string State { get; set; }
        public string Suffix { get; set; }
        public string Zip { get; set; }
    }

    public class PboDependent
    {
        public PboSubscriber DependentInfo { get; set; }
        public string Relationship { get; set; }
    }

    public class PboIdentificationType
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class PboCommunicationType
    {
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class PboDateType
    {
        public string Date { get; set; }
        public string Type { get; set; }
    }
}