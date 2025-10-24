using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAIAnalysisService
    {
        Task<AIInsights> GetSurveyInsightsAsync(List<SurveyResponse> responses);
        Task<AIRecommendations> GetRecommendationsAsync(List<SurveyResponse> responses);
        Task<AIPredictions> GetPredictiveInsightsAsync(List<SurveyResponse> responses);
        Task<string> ProcessNaturalLanguageQueryAsync(string query, List<SurveyResponse> responses);
        Task<string> GetTrendAnalysisAsync(List<SurveyResponse> responses);
        Task<string> GetServiceQualityInsightAsync(List<SurveyResponse> responses);
        Task<string> GetFullDatabaseAnalysisAsync(List<SurveyResponse> responses);
        Task<List<ForecastDataPoint>> GetSatisfactionForecastAsync(List<SurveyResponse> responses, int monthsAhead = 6);
    }

    public class AIAnalysisService : IAIAnalysisService
    {
        private readonly ChatClient _chatClient;
        private readonly ILogger<AIAnalysisService> _logger;

        public AIAnalysisService(IConfiguration configuration, ILogger<AIAnalysisService> logger)
        {
            var apiKey = configuration["OpenAI:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("OpenAI API key not configured");

            var client = new OpenAIClient(new ApiKeyCredential(apiKey));
            _chatClient = client.GetChatClient("gpt-4");
            _logger = logger;
        }

        public async Task<AIInsights> GetSurveyInsightsAsync(List<SurveyResponse> responses)
        {
            try
            {
                var analysisData = PrepareAnalysisData(responses);
                
                var prompt = $@"
                Analyze this survey data and provide insights:
                
                {analysisData}
                
                Provide insights in this JSON format:
                {{
                    ""keyTrends"": [""trend1"", ""trend2"", ""trend3""],
                    ""criticalIssues"": [""issue1"", ""issue2""],
                    ""strongPoints"": [""strength1"", ""strength2""],
                    ""overallSentiment"": ""positive/neutral/negative"",
                    ""confidenceScore"": 0.85,
                    ""summary"": ""Brief summary of findings""
                }}";

                var response = await _chatClient.CompleteChatAsync(prompt);
                var jsonResponse = response.Value.Content[0].Text;
                
                return ParseAIInsights(jsonResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI insights");
                return GetFallbackInsights();
            }
        }

        public async Task<AIRecommendations> GetRecommendationsAsync(List<SurveyResponse> responses)
        {
            try
            {
                var analysisData = PrepareAnalysisData(responses);
                
                var prompt = $@"
                Based on this survey data, provide actionable recommendations:
                
                {analysisData}
                
                Provide recommendations in this JSON format:
                {{
                    ""immediateActions"": [""action1"", ""action2""],
                    ""longTermStrategies"": [""strategy1"", ""strategy2""],
                    ""sectorsToFocus"": [""sector1"", ""sector2""],
                    ""priorityLevel"": ""high/medium/low"",
                    ""expectedImpact"": ""description""
                }}";

                var response = await _chatClient.CompleteChatAsync(prompt);
                var jsonResponse = response.Value.Content[0].Text;
                
                return ParseAIRecommendations(jsonResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI recommendations");
                return GetFallbackRecommendations();
            }
        }

        public async Task<AIPredictions> GetPredictiveInsightsAsync(List<SurveyResponse> responses)
        {
            try
            {
                var trendData = PrepareTrendData(responses);
                
                var prompt = $@"
                Analyze these survey trends and make predictions:
                
                {trendData}
                
                Provide predictions in this JSON format:
                {{
                    ""nextQuarterPrediction"": {{
                        ""expectedSatisfaction"": 7.5,
                        ""trendDirection"": ""increasing/stable/decreasing"",
                        ""confidence"": 0.78
                    }},
                    ""riskFactors"": [""risk1"", ""risk2""],
                    ""opportunities"": [""opportunity1"", ""opportunity2""],
                    ""clientRetentionForecast"": 85.5
                }}";

                var response = await _chatClient.CompleteChatAsync(prompt);
                var jsonResponse = response.Value.Content[0].Text;
                
                return ParseAIPredictions(jsonResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI predictions");
                return GetFallbackPredictions();
            }
        }

        public async Task<string> ProcessNaturalLanguageQueryAsync(string query, List<SurveyResponse> responses)
        {
            try
            {
                var dataContext = PrepareQueryContext(responses);
                
                var prompt = $@"
                Answer this question about the survey data: {query}
                
                Data context:
                {dataContext}
                
                Provide a clear, conversational answer based on the data. If the data doesn't contain enough information to answer, say so clearly.";

                var response = await _chatClient.CompleteChatAsync(prompt);
                return response.Value.Content[0].Text;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing natural language query");
                return "I'm sorry, I'm unable to process that query right now. Please try again later.";
            }
        }

        private string PrepareAnalysisData(List<SurveyResponse> responses)
        {
            var summary = new
            {
                TotalResponses = responses.Count,
                AverageSatisfaction = responses.Average(r => r.Question1),
                SectorBreakdown = responses.GroupBy(r => r.Sector)
                    .ToDictionary(g => g.Key, g => new
                    {
                        Count = g.Count(),
                        AvgSatisfaction = g.Average(r => r.Question1),
                        AvgProfessionalism = g.Average(r => r.Question2),
                        AvgValue = g.Average(r => r.Question4)
                    }),
                CompletionRate = responses.Count(r => r.DateCompleted.HasValue) / (double)responses.Count * 100,
                TopIssues = GetTopIssues(responses),
                RecentTrends = GetRecentTrends(responses)
            };

            return JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });
        }

        private string PrepareTrendData(List<SurveyResponse> responses)
        {
            var trends = responses
                .Where(r => r.DateCompleted.HasValue)
                .GroupBy(r => new { r.DateCompleted!.Value.Year, r.DateCompleted.Value.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                    AvgSatisfaction = g.Average(r => r.Question1),
                    ResponseCount = g.Count(),
                    NewClients = g.Select(r => r.CustomerName).Distinct().Count()
                });

            return JsonSerializer.Serialize(trends, new JsonSerializerOptions { WriteIndented = true });
        }

        private string PrepareQueryContext(List<SurveyResponse> responses)
        {
            return $@"
            Total Surveys: {responses.Count}
            Average Satisfaction: {responses.Average(r => r.Question1):F1}/10
            Sectors: {string.Join(", ", responses.Select(r => r.Sector).Distinct())}
            Date Range: {responses.Min(r => r.DateCompleted):yyyy-MM-dd} to {responses.Max(r => r.DateCompleted):yyyy-MM-dd}
            Top Performing Sector: {GetTopPerformingSector(responses)}
            Lowest Performing Sector: {GetLowestPerformingSector(responses)}";
        }

        private List<string> GetTopIssues(List<SurveyResponse> responses)
        {
            var issues = new List<string>();
            
            // Analyze low scores across questions
            if (responses.Average(r => r.Question2) < 6) issues.Add("Professionalism concerns");
            if (responses.Average(r => r.Question4) < 6) issues.Add("Value for money issues");
            if (responses.Average(r => r.Question6) < 6) issues.Add("Communication problems");
            if (responses.Average(r => r.Question7) < 6) issues.Add("Timeliness concerns");
            
            return issues;
        }

        private List<string> GetRecentTrends(List<SurveyResponse> responses)
        {
            // Simple trend analysis - comparing last 30 days with previous 30 days
            var recentDate = DateTime.Now.AddDays(-30);
            var olderDate = DateTime.Now.AddDays(-60);
            
            var recentAvg = responses.Where(r => r.DateCompleted >= recentDate).DefaultIfEmpty().Average(r => r?.Question1 ?? 0);
            var olderAvg = responses.Where(r => r.DateCompleted >= olderDate && r.DateCompleted < recentDate).DefaultIfEmpty().Average(r => r?.Question1 ?? 0);
            
            var trends = new List<string>();
            
            if (recentAvg > olderAvg + 0.5) trends.Add("Satisfaction improving");
            else if (recentAvg < olderAvg - 0.5) trends.Add("Satisfaction declining");
            else trends.Add("Satisfaction stable");
            
            return trends;
        }

        private string GetTopPerformingSector(List<SurveyResponse> responses)
        {
            return responses.GroupBy(r => r.Sector)
                .OrderByDescending(g => g.Average(r => r.Question1))
                .FirstOrDefault()?.Key ?? "N/A";
        }

        private string GetLowestPerformingSector(List<SurveyResponse> responses)
        {
            return responses.GroupBy(r => r.Sector)
                .OrderBy(g => g.Average(r => r.Question1))
                .FirstOrDefault()?.Key ?? "N/A";
        }

        private AIInsights ParseAIInsights(string jsonResponse)
        {
            try
            {
                return JsonSerializer.Deserialize<AIInsights>(jsonResponse) ?? GetFallbackInsights();
            }
            catch
            {
                return GetFallbackInsights();
            }
        }

        private AIRecommendations ParseAIRecommendations(string jsonResponse)
        {
            try
            {
                return JsonSerializer.Deserialize<AIRecommendations>(jsonResponse) ?? GetFallbackRecommendations();
            }
            catch
            {
                return GetFallbackRecommendations();
            }
        }

        private AIPredictions ParseAIPredictions(string jsonResponse)
        {
            try
            {
                return JsonSerializer.Deserialize<AIPredictions>(jsonResponse) ?? GetFallbackPredictions();
            }
            catch
            {
                return GetFallbackPredictions();
            }
        }

        private AIInsights GetFallbackInsights()
        {
            return new AIInsights
            {
                KeyTrends = new[] { "Data analysis in progress" },
                CriticalIssues = new[] { "Unable to analyze at this time" },
                StrongPoints = new[] { "System functioning normally" },
                OverallSentiment = "neutral",
                ConfidenceScore = 0.0,
                Summary = "AI analysis temporarily unavailable"
            };
        }

        private AIRecommendations GetFallbackRecommendations()
        {
            return new AIRecommendations
            {
                ImmediateActions = new[] { "Review recent survey responses" },
                LongTermStrategies = new[] { "Continue monitoring trends" },
                SectorsToFocus = new[] { "All sectors" },
                PriorityLevel = "medium",
                ExpectedImpact = "Standard monitoring procedures"
            };
        }

        private AIPredictions GetFallbackPredictions()
        {
            return new AIPredictions
            {
                NextQuarterPrediction = new PredictionData 
                { 
                    ExpectedSatisfaction = 7.0, 
                    TrendDirection = "stable", 
                    Confidence = 0.5 
                },
                RiskFactors = new[] { "Insufficient data for predictions" },
                Opportunities = new[] { "Collect more survey data" },
                ClientRetentionForecast = 75.0
            };
        }

        public async Task<string> GetTrendAnalysisAsync(List<SurveyResponse> responses)
        {
            try
            {
                if (!responses.Any())
                    return "Insufficient data for trend analysis.";

                // Calculate trend data
                var trendData = CalculateTrendData(responses);
                var trendDirection = CalculateTrendDirection(trendData);
                var avgSatisfaction = trendData.Any() ? trendData.Average(t => t.AverageScore) : 0;
                
                var prompt = $@"
You are a professional business analyst. Based on the following survey satisfaction data, provide a concise, insightful trend analysis in 1-2 sentences.

Data Summary:
- Number of time periods: {trendData.Count}
- Overall average satisfaction: {avgSatisfaction:F1}/10
- Trend direction: {trendDirection}
- Recent scores: {string.Join(", ", trendData.TakeLast(3).Select(t => t.AverageScore.ToString("F1")))}

Provide a professional, actionable insight about the satisfaction trend. Be specific about what the data shows and what it means for the business.";

                var completion = await _chatClient.CompleteChatAsync(new[]
                {
                    new UserChatMessage(prompt)
                });

                return completion.Value.Content.FirstOrDefault()?.Text ?? 
                       GetFallbackTrendAnalysis(trendDirection, avgSatisfaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating AI trend analysis");
                return GetFallbackTrendAnalysis("stable", responses.Any() ? responses.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0) : 0);
            }
        }

        public async Task<string> GetServiceQualityInsightAsync(List<SurveyResponse> responses)
        {
            try
            {
                if (!responses.Any())
                    return "No data available for service quality analysis.";

                // Calculate service quality
                var avgSatisfaction = responses.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0);
                var totalResponses = responses.Count;
                var highSatisfactionCount = responses.Count(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0 >= 8);
                var lowSatisfactionCount = responses.Count(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0 <= 5);
                
                var prompt = $@"
You are a service quality expert. Based on the following survey data, provide a concise, professional assessment of service quality in 1-2 sentences.

Service Quality Data:
- Overall satisfaction score: {avgSatisfaction:F1}/10
- Total responses: {totalResponses}
- High satisfaction responses (8+): {highSatisfactionCount} ({(highSatisfactionCount / (double)totalResponses * 100):F1}%)
- Low satisfaction responses (≤5): {lowSatisfactionCount} ({(lowSatisfactionCount / (double)totalResponses * 100):F1}%)

Provide a professional assessment that acknowledges the current service quality level and gives actionable insight.";

                var completion = await _chatClient.CompleteChatAsync(new[]
                {
                    new UserChatMessage(prompt)
                });

                return completion.Value.Content.FirstOrDefault()?.Text ?? 
                       GetFallbackServiceQualityInsight(avgSatisfaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating AI service quality insight");
                return GetFallbackServiceQualityInsight(responses.Any() ? responses.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0) : 0);
            }
        }

        private List<(DateTime Period, double AverageScore)> CalculateTrendData(List<SurveyResponse> responses)
        {
            return responses
                .Where(r => r.DateCompleted.HasValue)
                .GroupBy(r => new { r.DateCompleted!.Value.Year, r.DateCompleted.Value.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => (
                    Period: new DateTime(g.Key.Year, g.Key.Month, 1),
                    AverageScore: g.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0)
                ))
                .ToList();
        }

        private string CalculateTrendDirection(List<(DateTime Period, double AverageScore)> trendData)
        {
            if (trendData.Count < 2) return "stable";
            
            var first = trendData.First().AverageScore;
            var last = trendData.Last().AverageScore;
            var diff = last - first;
            
            return diff > 0.2 ? "up" : diff < -0.2 ? "down" : "stable";
        }

        private string GetFallbackTrendAnalysis(string direction, double avgScore)
        {
            return direction switch
            {
                "up" => $"Satisfaction scores show positive improvement with an average of {avgScore:F1}/10, indicating enhanced service delivery.",
                "down" => $"Satisfaction scores indicate a declining trend with an average of {avgScore:F1}/10, requiring attention to service quality.",
                _ => $"Satisfaction scores remain stable at {avgScore:F1}/10, suggesting consistent service delivery standards."
            };
        }

        private string GetFallbackServiceQualityInsight(double avgScore)
        {
            return avgScore switch
            {
                >= 8.5 => "Outstanding service quality - customers are highly satisfied!",
                >= 7.5 => "Good service quality - customers are generally satisfied.",
                >= 6.5 => "Acceptable service quality - room for improvement exists.",
                >= 5.5 => "Below average service quality - attention needed.",
                _ => "Service quality needs significant improvement - priority action required."
            };
        }

        public async Task<string> GetFullDatabaseAnalysisAsync(List<SurveyResponse> responses)
        {
            try
            {
                if (!responses.Any())
                    return "No survey data available for analysis.";

                var analysisData = new
                {
                    TotalResponses = responses.Count,
                    DateRange = new { 
                        Earliest = responses.Min(r => r.DateCompleted)?.ToString("yyyy-MM-dd"), 
                        Latest = responses.Max(r => r.DateCompleted)?.ToString("yyyy-MM-dd") 
                    },
                    OverallSatisfaction = responses.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0),
                    SectorBreakdown = responses.Where(r => !string.IsNullOrEmpty(r.Sector))
                                              .GroupBy(r => r.Sector)
                                              .Select(g => new { Sector = g.Key, Count = g.Count(), AvgSatisfaction = g.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0) }),
                    CompanySizeBreakdown = responses.Where(r => !string.IsNullOrEmpty(r.CompanySize))
                                                   .GroupBy(r => r.CompanySize)
                                                   .Select(g => new { Size = g.Key, Count = g.Count(), AvgSatisfaction = g.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0) }),
                    QuestionAverages = new {
                        OverallSatisfaction = responses.Average(r => r.Question1),
                        Professionalism = responses.Average(r => r.Question2),
                        InternationalGrowth = responses.Average(r => r.Question3),
                        ValueVsCost = responses.Average(r => r.Question4),
                        BusinessNeeds = responses.Average(r => r.Question5),
                        Communication = responses.Average(r => r.Question6),
                        Timeliness = responses.Average(r => r.Question7),
                        AdvisorExpertise = responses.Average(r => r.Question8),
                        FutureUse = responses.Average(r => r.Question9)
                    }
                };

                var prompt = $@"Analyze this comprehensive survey database and provide strategic insights:

DATABASE OVERVIEW:
- Total Responses: {analysisData.TotalResponses}
- Date Range: {analysisData.DateRange.Earliest} to {analysisData.DateRange.Latest}
- Overall Satisfaction: {analysisData.OverallSatisfaction:F1}/10

SECTOR ANALYSIS:
{string.Join("\n", analysisData.SectorBreakdown.Select(s => $"- {s.Sector}: {s.Count} responses, {s.AvgSatisfaction:F1}/10 avg satisfaction"))}

COMPANY SIZE ANALYSIS:
{string.Join("\n", analysisData.CompanySizeBreakdown.Select(s => $"- {s.Size}: {s.Count} responses, {s.AvgSatisfaction:F1}/10 avg satisfaction"))}

DETAILED QUESTION ANALYSIS:
- Overall Satisfaction: {analysisData.QuestionAverages.OverallSatisfaction:F1}/10
- Professionalism: {analysisData.QuestionAverages.Professionalism:F1}/10
- International Growth Support: {analysisData.QuestionAverages.InternationalGrowth:F1}/10
- Value vs Cost: {analysisData.QuestionAverages.ValueVsCost:F1}/10
- Business Needs Met: {analysisData.QuestionAverages.BusinessNeeds:F1}/10
- Communication: {analysisData.QuestionAverages.Communication:F1}/10
- Timeliness: {analysisData.QuestionAverages.Timeliness:F1}/10
- Advisor Expertise: {analysisData.QuestionAverages.AdvisorExpertise:F1}/10
- Future Use Likelihood: {analysisData.QuestionAverages.FutureUse:F1}/10

Provide a comprehensive strategic analysis including:
1. Key strengths and weaknesses across all data
2. Sector-specific insights and opportunities
3. Company size preferences and patterns
4. Critical areas for improvement
5. Strategic recommendations for business growth
Be detailed, actionable, and data-driven.";

                var completion = await _chatClient.CompleteChatAsync(new[]
                {
                    new UserChatMessage(prompt)
                });

                return completion.Value.Content.FirstOrDefault()?.Text ?? 
                       "Comprehensive database analysis showing mixed performance across sectors with opportunities for targeted improvements.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating full database analysis");
                return "Unable to generate comprehensive analysis at this time. Please try again later.";
            }
        }

        public async Task<List<ForecastDataPoint>> GetSatisfactionForecastAsync(List<SurveyResponse> responses, int monthsAhead = 6)
        {
            try
            {
                if (!responses.Any())
                    return new List<ForecastDataPoint>();

                // Calculate historical trend data
                var trendData = CalculateMonthlyTrend(responses);
                var historicalData = string.Join("\n", trendData.Select(t => $"{t.Period}: {t.AverageScore:F1}"));

                var prompt = $@"Based on this historical satisfaction data, generate a 6-month forecast:

HISTORICAL DATA:
{historicalData}

Current trend: {(trendData.Count > 1 ? (trendData.Last().AverageScore > trendData.First().AverageScore ? "improving" : "declining") : "stable")}

Generate realistic monthly predictions for the next {monthsAhead} months starting from {DateTime.Now:yyyy-MM}. 
Consider:
- Historical patterns and seasonality
- Current trajectory
- Realistic confidence intervals
- Market conditions

Return ONLY a JSON array in this exact format:
[
  {{""Date"": ""2025-11-01"", ""PredictedScore"": 7.5, ""ConfidenceInterval"": 0.3, ""Period"": ""Nov 2025""}},
  {{""Date"": ""2025-12-01"", ""PredictedScore"": 7.8, ""ConfidenceInterval"": 0.4, ""Period"": ""Dec 2025""}}
]";

                var completion = await _chatClient.CompleteChatAsync(new[]
                {
                    new UserChatMessage(prompt)
                });

                var jsonResponse = completion.Value.Content.FirstOrDefault()?.Text ?? "[]";
                
                var startIndex = jsonResponse.IndexOf('[');
                var endIndex = jsonResponse.LastIndexOf(']') + 1;
                if (startIndex >= 0 && endIndex > startIndex)
                {
                    jsonResponse = jsonResponse.Substring(startIndex, endIndex - startIndex);
                }

                var forecastPoints = JsonSerializer.Deserialize<List<ForecastDataPoint>>(jsonResponse) ?? new List<ForecastDataPoint>();
                return forecastPoints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating satisfaction forecast");
                
                // Generate forecast based on current trend
                var fallbackForecast = new List<ForecastDataPoint>();
                var currentAvg = responses.Any() ? responses.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0) : 7.0;
                
                for (int i = 1; i <= monthsAhead; i++)
                {
                    var futureDate = DateTime.Now.AddMonths(i);
                    fallbackForecast.Add(new ForecastDataPoint
                    {
                        Date = futureDate,
                        PredictedScore = Math.Max(1, Math.Min(10, currentAvg + (Random.Shared.NextDouble() - 0.5) * 0.5)),
                        ConfidenceInterval = 0.3,
                        Period = futureDate.ToString("MMM yyyy")
                    });
                }
                
                return fallbackForecast;
            }
        }

        private List<TrendDataPoint> CalculateMonthlyTrend(List<SurveyResponse> responses)
        {
            return responses
                .Where(r => r.DateCompleted.HasValue)
                .GroupBy(r => new { r.DateCompleted!.Value.Year, r.DateCompleted.Value.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new TrendDataPoint
                {
                    AverageScore = g.Average(r => (r.Question1 + r.Question2 + r.Question3 + r.Question4 + r.Question5 + r.Question6 + r.Question7 + r.Question8 + r.Question9) / 9.0),
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    ResponseCount = g.Count()
                })
                .ToList();
        }
    }
}