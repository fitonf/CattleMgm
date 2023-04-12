using CattleMgm.Data;
using CattleMgm.Data.Entities;
using CattleMgm.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace CattleMgm.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ApplicationDbContext context, praktikadbContext db) : base(context, db)
        {
        }

        public IActionResult Index()
        {
            var users = _db.AspNetUsers.ToList();
            List<UserViewModel> model = new List<UserViewModel>();

            //foreach method
            //foreach (var item in users)
            //{
            //    model.Add(new UserViewModel
            //    {
            //        Id = item.Id,
            //        UserName = item.UserName,
            //        Email = item.Email,
            //        FirstName = item.FirstName,
            //        LastName = item.LastName,
            //        PhoneNumber = item.PhoneNumber,
            //        isRoleConfirmed = item.IsRoleConfirmed == null ?false : item.IsRoleConfirmed.Value,
            //    });
            //}

            //linq method same as foreach method
            model = (from item in users
                     select new UserViewModel
                     {
                         Id = item.Id,
                         UserName = item.UserName,
                         Email = item.Email,
                         FirstName = item.FirstName,
                         LastName = item.LastName,
                         PhoneNumber = item.PhoneNumber,
                         isRoleConfirmed = item.IsRoleConfirmed == null ? false : item.IsRoleConfirmed.Value,
                     }).ToList();

            return View(model);
        }
    }
}
