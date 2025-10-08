using System;

namespace WebAPI.Models
{
    public class SurveyResponse
    {
        public int SurveyResponseId { get; set; }       // Database primary key
        public string SurveyId { get; set; }            // Unique survey ID (GUID)
        public string CustomerName { get; set; }        // Previously CompanyName
        public string Sector { get; set; }
        public string CompanySize { get; set; }         // Micro, SME, Large
        public string ProjectName { get; set; }
        public string ProjectDuration { get; set; }
        public DateTime? DateSent { get; set; }         // When survey was sent
        public DateTime? DateCompleted { get; set; }    // When survey was completed
        public int Question1 { get; set; }  // Overall satisfaction
        public int Question2 { get; set; }  // Professionalism
        public int Question3 { get; set; }  // International growth
        public int Question4 { get; set; }  // Value vs cost
        public int Question5 { get; set; }  // Business needs
        public int Question6 { get; set; }  // Communication
        public int Question7 { get; set; }  // Timeliness
        public int Question8 { get; set; }  // Advisor expertise
        public int Question9 { get; set; }  // Future use
        public DateTime SubmittedAt { get; set; }       // Legacy field - same as DateCompleted
        
        // Constructor to auto-generate SurveyId
        public SurveyResponse()
        {
            SurveyId = Guid.NewGuid().ToString();
        }
    }
}