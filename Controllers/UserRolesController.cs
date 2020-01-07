using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(getUsers());
        }

        public ActionResult Promote(string Id)
        {
            var user = getUsers()
                .Where(m => m.Id == Id)
                .FirstOrDefault();

            var role = db.Roles.SingleOrDefault(m => m.Name == "Editor");
            user.Roles.Add(new IdentityUserRole { RoleId = role.Id });

            return View("Index");
        }

        public IQueryable<ApplicationUser> getUsers()
        {
            var userStore = new UserStore<ApplicationUser>(db);
            return userStore.Users;
        }
    }
}