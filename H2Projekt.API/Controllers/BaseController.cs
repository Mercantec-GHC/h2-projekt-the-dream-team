using H2Projekt.API.Extensions;
using H2Projekt.Application.Dto.Guests;
using Microsoft.AspNetCore.Mvc;

namespace H2Projekt.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected WorkContext? WorkContext => User.GetWorkContextOrNull();
    }
}
