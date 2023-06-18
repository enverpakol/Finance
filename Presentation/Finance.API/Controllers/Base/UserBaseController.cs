using Finance.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Finance.API.Controllers
{
    [Authorize(Roles = "user")]
    [Authorize(AuthenticationSchemes = "App")]
    public class UserBaseController : CustomBaseController
    {

       
    }
}
