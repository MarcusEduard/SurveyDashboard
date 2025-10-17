using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.Controllers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IAIAnalysisService _aiService;
        private readonly DNDContext _context;
        private readonly ILogger<AIController> _logger;

        public AIController(
            IAIAnalysisService aiService, 
            DNDContext context,
            ILogger<AIController> logger)
        {
            _aiService = aiService;
            _context = context;
            _logger = logger;
        }

        [HttpGet("insights")]
        public async Task<ActionResult<AIInsights>> GetInsights()
        {
            try
            {
                var responses = await GetSurveyResponses();
                var insights = await _aiService.GetSurveyInsightsAsync(responses);
                return Ok(insights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI insights");
                return StatusCode(500, "Error generating insights");
            }
        }

        [HttpGet("recommendations")]
        public async Task<ActionResult<AIRecommendations>> GetRecommendations()
        {
            try
            {
                var responses = await GetSurveyResponses();
                var recommendations = await _aiService.GetRecommendationsAsync(responses);
                return Ok(recommendations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI recommendations");
                return StatusCode(500, "Error generating recommendations");
            }
        }

        [HttpGet("predictions")]
        public async Task<ActionResult<AIPredictions>> GetPredictions()
        {
            try
            {
                var responses = await GetSurveyResponses();
                var predictions = await _aiService.GetPredictiveInsightsAsync(responses);
                return Ok(predictions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI predictions");
                return StatusCode(500, "Error generating predictions");
            }
        }

        [HttpPost("chat")]
        public async Task<ActionResult<ChatResponse>> Chat([FromBody] ChatQuery query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query.Query))
                {
                    return BadRequest("Query cannot be empty");
                }

                var responses = await GetSurveyResponses();
                var answer = await _aiService.ProcessNaturalLanguageQueryAsync(query.Query, responses);
                
                return Ok(new ChatResponse { Response = answer });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing chat query");
                return StatusCode(500, "Error processing your question");
            }
        }

        [HttpGet("summary")]
        public async Task<ActionResult<object>> GetAISummary()
        {
            try
            {
                var responses = await GetSurveyResponses();
                
                // Get all AI analysis in parallel for better performance
                var insightsTask = _aiService.GetSurveyInsightsAsync(responses);
                var recommendationsTask = _aiService.GetRecommendationsAsync(responses);
                var predictionsTask = _aiService.GetPredictiveInsightsAsync(responses);

                await Task.WhenAll(insightsTask, recommendationsTask, predictionsTask);

                var summary = new
                {
                    Insights = await insightsTask,
                    Recommendations = await recommendationsTask,
                    Predictions = await predictionsTask,
                    LastUpdated = DateTime.Now
                };

                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI summary");
                return StatusCode(500, "Error generating AI summary");
            }
        }

        [HttpGet("trend-analysis")]
        public async Task<ActionResult<string>> GetTrendAnalysis()
        {
            try
            {
                var responses = await GetSurveyResponses();
                var analysis = await _aiService.GetTrendAnalysisAsync(responses);
                return Ok(analysis);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI trend analysis");
                return StatusCode(500, "Error generating trend analysis");
            }
        }

        [HttpGet("service-quality")]
        public async Task<ActionResult<string>> GetServiceQualityInsight()
        {
            try
            {
                var responses = await GetSurveyResponses();
                var insight = await _aiService.GetServiceQualityInsightAsync(responses);
                return Ok(insight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting AI service quality insight");
                return StatusCode(500, "Error generating service quality insight");
            }
        }

        [HttpGet("database-analysis")]
        public async Task<ActionResult<string>> GetFullDatabaseAnalysis()
        {
            try
            {
                var responses = await GetSurveyResponses();
                var analysis = await _aiService.GetFullDatabaseAnalysisAsync(responses);
                return Ok(analysis);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting full database analysis");
                return StatusCode(500, "Error generating comprehensive database analysis");
            }
        }

        [HttpGet("forecast")]
        public async Task<ActionResult<List<ForecastDataPoint>>> GetSatisfactionForecast([FromQuery] int monthsAhead = 6)
        {
            try
            {
                var responses = await GetSurveyResponses();
                var forecast = await _aiService.GetSatisfactionForecastAsync(responses, monthsAhead);
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting satisfaction forecast");
                return StatusCode(500, "Error generating satisfaction forecast");
            }
        }

        private async Task<List<SurveyResponse>> GetSurveyResponses()
        {
            // Get survey responses directly from the database
            return await _context.SurveyResponses.ToListAsync();
        }
    }
}