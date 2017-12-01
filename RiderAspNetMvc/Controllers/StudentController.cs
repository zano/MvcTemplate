//using System.Collections.Generic;

using System.Web.Mvc;
using Common;
using Mapster;
using RiderAspNetMvc.Services;
using RiderAspNetMvc.ViewModels;
using Common;

namespace RiderAspNetMvc.Controllers {
    public class StudentController : Controller {
        private readonly StudentService service; // = new StudentService();
        private bool IsAjaxOrPartial() => Request.IsAjaxRequest() || ControllerContext.IsChildAction;

        public StudentController() : this(new StudentService()) { }
        public StudentController(StudentService service) => this.service = service;

        // Generic default view
        public ActionResult Index() => View();

        public ActionResult List(int? id = null, string orderBy = null, string filter = null) {
            var model = service.ListAll(orderBy);
            if (id != null) ViewBag.Created = id;
            return IsAjaxOrPartial() ? (ActionResult) PartialView(model) : View(model);
        }

        public ActionResult Details(int? id = null) {
            if (id == null) return Redirect(Url.Action("List"));
            var student = service.GetDetailsById(id.Value);
            if (student == null) return Redirect(Url.Action("List"));
            return View(student);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentCreate student) {
            if (!ModelState.IsValid) return View(student);
            var result = service.Create(student);
            if (result.Succeded) return RedirectToAction("List", new { student.Id });
            foreach (var error in result.Errors) ModelState.AddModelError("", error);
            return View(student);
        }

        public ActionResult Edit(int? id = null) {
            if (id == null) return Redirect(Url.Action("List"));
            return View(service.GetEditById(id.Value));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentEdit student) {
            if (!ModelState.IsValid) return View(student);
            var result = service.Update(student);
            if (result.Succeded) return RedirectToAction("Details", new { student.Id });
            ModelState.AddModelErrors("", result.Errors);
            return View(student);
        }
    }
}
