using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdentityInCore3.API.Controllers
{
    public class BaseController : Controller
    {
        public string UserId
        {
            get
            {

                try
                {
                    return HttpContext.User != null ? Convert.ToString(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type.ToLower() == "id")) : string.Empty;

                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
    }
}
