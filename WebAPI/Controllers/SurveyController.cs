using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly ESGContext _context;

        public SurveyController(ESGContext context)
        {
            _context = context;
        }

        [HttpGet("responses")]
        public async Task<ActionResult<IEnumerable<SurveyResponse>>> GetSurveyResponses()
        {
            return await _context.SurveyResponses.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyResponse>>> GetAllSurveys()
        {
            return await _context.SurveyResponses.ToListAsync();
        }

        [HttpGet("by-id/{surveyId}")]
        public async Task<ActionResult<SurveyResponse>> GetSurveyById(string surveyId)
        {
            var survey = await _context.SurveyResponses
                .FirstOrDefaultAsync(s => s.SurveyId == surveyId);
            
            if (survey == null)
                return NotFound();
                
            return survey;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSurvey(int id, [FromBody] SurveyResponse response)
        {
            if (id != response.SurveyResponseId)
                return BadRequest();

            var existingSurvey = await _context.SurveyResponses.FindAsync(id);
            if (existingSurvey == null)
                return NotFound();

            // Update all editable fields
            existingSurvey.CustomerName = response.CustomerName;
            existingSurvey.ProjectName = response.ProjectName;
            existingSurvey.ProjectDuration = response.ProjectDuration;
            existingSurvey.Sector = response.Sector;
            existingSurvey.CompanySize = response.CompanySize;
            existingSurvey.Question1 = response.Question1;
            existingSurvey.Question2 = response.Question2;
            existingSurvey.Question3 = response.Question3;
            existingSurvey.Question4 = response.Question4;
            existingSurvey.Question5 = response.Question5;
            existingSurvey.Question6 = response.Question6;
            existingSurvey.Question7 = response.Question7;
            existingSurvey.Question8 = response.Question8;
            existingSurvey.Question9 = response.Question9;
            existingSurvey.DateCompleted = response.DateCompleted;
            existingSurvey.SubmittedAt = response.SubmittedAt;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SurveyResponse response)
        {
            response.SubmittedAt = DateTime.Now;
            _context.SurveyResponses.Add(response);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.SurveyResponses.FindAsync(id);
            if (item == null) return NotFound();
            _context.SurveyResponses.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}