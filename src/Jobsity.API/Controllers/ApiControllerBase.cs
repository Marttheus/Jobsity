using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Jobsity.API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult CreateResponse(List<string> errors, string message = null, object? result = null)
        {
            if (errors is null || !errors.Any())
            {
                if (result is null && string.IsNullOrEmpty(message))
                    return NoContent();

                return Ok(new Response(StatusCodes.Status200OK, message, null, result));
            }

            return BadRequest(new Response(StatusCodes.Status400BadRequest, message, errors, null));
        }

        protected IActionResult CreateResponse(ModelStateDictionary modelState, string message = null)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors).Select(x => x.ErrorMessage).ToList();

            return CreateResponse(errors, message);
        }

        private class Response
        {
            public int Status { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string Message { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public List<string> Errors { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public object? Data { get; set; }

            public Response(int status, string message, List<string> errors, object? data)
            {
                Status = status;
                Message = message;
                Errors = errors;
                Data = data;
            }
        }
    }
}
