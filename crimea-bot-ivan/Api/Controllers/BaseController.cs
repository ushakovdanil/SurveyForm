using System;
using Api.Models.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected ActionResult ResponseFromServiceProcessingResult(
            ServiceResponse response,
            ResponseType type = ResponseType.Ok
        )
        {
            if (response.IsSuccess)
            {
                switch (type)
                {
                    case ResponseType.Created:
                        return Ok();
                    case ResponseType.NoContent:
                        return NoContent();
                    case ResponseType.Ok:
                    default:
                        return Ok();
                }
            }

            switch (type)
            {
                case ResponseType.NotFound:
                    return NotFound(response.Error);
                case ResponseType.BadRequest:
                default:
                    return BadRequest(response.Error);
            }
        }

        protected ActionResult ResponseFromServiceProcessingResult<T>(
            ServiceResponse<T> response,
            ResponseType type = ResponseType.Ok
        )
        {
            if (response.IsSuccess)
            {
                switch (type)
                {
                    case ResponseType.Created:
                        return Ok(response.Response);
                    case ResponseType.NoContent:
                        return NoContent();
                    case ResponseType.Ok:
                    default:
                        return Ok(response.Response);
                }
            }

            switch (type)
            {
                case ResponseType.NotFound:
                    return NotFound(response.Error);
                case ResponseType.BadRequest:
                default:
                    return BadRequest(response.Error);
            }
        }
    }
}

