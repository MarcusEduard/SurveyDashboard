namespace WebAPI.Models
{
    public class AIInsights
    {
        public string[] KeyTrends { get; set; } = Array.Empty<string>();
        public string[] CriticalIssues { get; set; } = Array.Empty<string>();
        public string[] StrongPoints { get; set; } = Array.Empty<string>();
        public string OverallSentiment { get; set; } = "neutral";
        public double ConfidenceScore { get; set; }
        public string Summary { get; set; } = "";
    }

    public class AIRecommendations
    {
        public string[] ImmediateActions { get; set; } = Array.Empty<string>();
        public string[] LongTermStrategies { get; set; } = Array.Empty<string>();
        public string[] SectorsToFocus { get; set; } = Array.Empty<string>();
        public string PriorityLevel { get; set; } = "medium";
        public string ExpectedImpact { get; set; } = "";
    }

    public class AIPredictions
    {
        public PredictionData NextQuarterPrediction { get; set; } = new();
        public string[] RiskFactors { get; set; } = Array.Empty<string>();
        public string[] Opportunities { get; set; } = Array.Empty<string>();
        public double ClientRetentionForecast { get; set; }
    }

    public class PredictionData
    {
        public double ExpectedSatisfaction { get; set; }
        public string TrendDirection { get; set; } = "stable";
        public double Confidence { get; set; }
    }

    public class ChatQuery
    {
        public string Query { get; set; } = "";
    }

    public class ChatResponse
    {
        public string Response { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}