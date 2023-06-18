using Finance.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Finance.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)

                return new ObjectResult(null)
                {
                    StatusCode = (int)response.StatusCode,
                };


            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode,
            };
        }


        [NonAction]
        public IActionResult CreateActionResult<T>(PagerResponseDto<T> response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)

                return new ObjectResult(null)
                {
                    StatusCode = (int)response.StatusCode,
                };

            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode,
            };
        }
    }
}
