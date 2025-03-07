using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCartMvcUI.Models.DTOs;
using ShoppingCartMvcUI.Repositories;
using System.Data;

namespace ShoppingCartMvcUI.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminOperationsController : Controller
    {


        public IActionResult Dashboard()
        {
            return View();
        }

    }

}
