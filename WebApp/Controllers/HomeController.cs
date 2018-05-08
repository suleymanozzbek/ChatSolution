using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using DataAccess;
using WebApp.Models;
using System.Web.Routing;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        GateWay _gateWayService = new GateWay();
        public ActionResult Chat()
        {
            return View(_gateWayService.UserList());
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                MvcUsers newUser = new MvcUsers()
                {
                    UserName = model.username,
                    Password = model.password
                };

                _gateWayService.insertUser(newUser);
            }
            return RedirectToAction("Chat");
        }
        [HttpGet]
        public ActionResult EditUser(int id)
        {

            var model = new EditUserModel();
            var user = _gateWayService.Find(id);
            model.username = user.UserName;
            model.password = user.Password;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditUser(EditUserModel model)
        {
            var user = _gateWayService.Find(model.Id);
            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = model.password;
                    user.UserName = model.username;
                    _gateWayService.updateUser(user);
                }
                catch
                {


                }
            }
            return RedirectToAction("Chat");
        }
        public ActionResult DeleteUser(int id)
        {
            if (id == Convert.ToInt32(Session["pkID"]))
            {
                return View();

            }

            //önceki sayfaya gider
            string url = Request.UrlReferrer.AbsoluteUri.ToString();
            _gateWayService.deleteUser(id);
            return RedirectToAction("Chat");

            //          return RedirectToAction("Chat", new RouteValueDictionary(
            //new { controller = "Home", action = "Chat", ProcessType = "CRUD" }));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                MvcUsers newUser = new MvcUsers()
                {
                    UserName = model.username,
                    Password = model.password

                };
                int userId = 0;
                Session["userName"] = model.username;
                userId = GateWay.userLogin(newUser);
                if (userId > 0)
                {
                    Session["pkID"] = userId;
                    return RedirectToAction("Chat", new RouteValueDictionary(
            new { controller = "Home", action = "Chat", pkID = userId.ToString() }));
                }
                else
                {
                    return RedirectToAction("Login", new RouteValueDictionary(
             new { controller = "Home", action = "Login", Error = "PasswordError" }));
                }

            }
            else
            {
                return RedirectToAction("Login", new RouteValueDictionary(
               new { controller = "Home", action = "Login", Error = "Invalid" }));

            }
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}
