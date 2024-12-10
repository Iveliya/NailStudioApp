using Microsoft.AspNetCore.Mvc;

namespace NailStudioApp.WebAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    [ApiController]
    [Authorize]
    [Route("[controller]/[action]")]
    public class StaffMemberApiController : ControllerBase
    {
        
    }
}
