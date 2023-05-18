using EnjoyEat.DTOs;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using EnjoyEat.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class MemberRegisterController : Controller
    {
        private readonly MemberLoginService  _memberLoginService;
        private readonly MembersService _membersService;
        public MemberRegisterController(MemberLoginService memberLoginService, MembersService membersService)
        {
            _memberLoginService = memberLoginService;
            _membersService = membersService;
        }
        [HttpPost]
        public IActionResult Register(MemberRegisterDTO memberRegisterDTO)
        {
            //空白驗證還未寫
            //驗證DB是否重複
            List<string> errorMessage = new List<string>();
            var memberLoginList =  _memberLoginService.GetAllByAccount(memberRegisterDTO.Account);
           var membersList=_membersService.GetAllByEmail(memberRegisterDTO.Email);
            if(memberLoginList!=null)
            {
                errorMessage.Add("帳號已重複");
            }
            if(membersList!=null)
            {
                errorMessage.Add("Email重複");
            }
            if (errorMessage.Any())
            {
                //回MemberRegister的頁面
            }
            return View();
           
        }
       
    }
}
