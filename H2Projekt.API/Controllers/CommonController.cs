using H2Projekt.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace H2Projekt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Rule>>> GetRules(CancellationToken cancellationToken)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "rules.json");

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Rules file not found.");
                }

                var jsonContent = await System.IO.File.ReadAllTextAsync(filePath, cancellationToken);

                var rules = JsonSerializer.Deserialize<List<Rule>>(jsonContent) ?? new List<Rule>();

                return Ok(rules);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
