using System;

namespace WebAPI.Models
{
    public class SurveyResponse
    {
        public int SurveyResponseId { get; set; }       // Primary key
        public string SurveyId { get; set; }            // Unique survey ID
        public string CustomerName { get; set; }        // CustomerName
        public string Sector { get; set; }              // Sector
        public string CompanySize { get; set; }         // CompanySize  
        public string ProjectName { get; set; }         // ProjectName
        public string ProjectDuration { get; set; }     // ProjectDuration
        public DateTime? DateSent { get; set; }         // When survey was sent by admin
        public DateTime? DateCompleted { get; set; }    // When survey was completed by the customer
        public int Question1 { get; set; }  // Overall satisfaction
        public int Question2 { get; set; }  // Professionalism
        public int Question3 { get; set; }  // International growth
        public int Question4 { get; set; }  // Value vs cost
        public int Question5 { get; set; }  // Business needs
        public int Question6 { get; set; }  // Communication
        public int Question7 { get; set; }  // Timeliness
        public int Question8 { get; set; }  // Advisor expertise
        public int Question9 { get; set; }  // Future use of TCH
        public DateTime SubmittedAt { get; set; }       // DateCompleted
        
        // Auto-generating SurveyId
        public SurveyResponse()
        {
            SurveyId = Guid.NewGuid().ToString();
        }
    }

    public class ForecastDataPoint
    {
        public DateTime Date { get; set; }
        public double PredictedScore { get; set; }
        public double ConfidenceInterval { get; set; }
        public string Period { get; set; } = string.Empty;
    }

    public class TrendDataPoint
    {
        public double AverageScore { get; set; }
        public string Period { get; set; } = "";
        public DateTime Date { get; set; }
        public int ResponseCount { get; set; }
    }
}