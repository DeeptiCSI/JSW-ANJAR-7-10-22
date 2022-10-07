using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.Controllers
{
    public class AccountsController : Controller
    {
        private readonly DateTime date = new(2022, 09, 30);
        private readonly IUnitOfWork _unitOfWork;

        public AccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var res = await _unitOfWork.AccountServices.Login(model);
            if (res == null)
            {
                ViewBag.messege = "Invalid Credentials";
                return View("Login");
            }
            return RedirectToAction("Index", "Plate", res);
        }

        public async Task<APIResponse> CreateUser(CreateUserModel model)
        {
            var res = await _unitOfWork.AccountServices.CreateUser(model);
            return res;
        }

        public async Task<List<UserListModel>> UserListByModuleId(long moduleId)
        {
            return await _unitOfWork.AccountServices.UserListByModuleId(moduleId);
        }

        public void DeleteUser(long UserId)
        {
            _unitOfWork.AccountServices.DeleteUser(UserId);
        }

        public bool ValidateUser(long userId, string password)
        {
            return _unitOfWork.AccountServices.ValidateUser(userId, password);
        }
    }
}
