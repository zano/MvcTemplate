using System.Web.Mvc;
using Common;
using RiderAspNetMvc.Services;
using RiderAspNetMvc.ViewModels;

namespace RiderAspNetMvc.Controllers {
    public class StudentController : BaseController {
        private readonly StudentService service; 
        
        public StudentController() : this(new StudentService()) { }
        public StudentController(StudentService service) => this.service = service;

        // Generic default view
        public ActionResult Index() => View();

        public ActionResult List(int? id = null, string orderBy = null, string filter = null) {
            var model = service.ListAll();
            if (id != null) ViewBag.Created = id;
            return View(model);
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

    public class BaseController : Controller
    {
        private bool IsAjaxOrPartial => Request.IsAjaxRequest() || ControllerContext.IsChildAction;

        protected new ActionResult View(string viewName, object model) 
            => IsAjaxOrPartial ? (ActionResult)base.PartialView(viewName, model) : base.View(viewName, model);  
        protected new ActionResult View(string viewName, string masterName) 
            => IsAjaxOrPartial ? (ActionResult)base.PartialView(viewName, masterName) : base.View(viewName, masterName);
        protected new ActionResult View(IView view) 
            => IsAjaxOrPartial ? (ActionResult)base.PartialView(view) : base.View(view);
        protected new ActionResult View(object model) 
            => IsAjaxOrPartial ? (ActionResult)base.PartialView(model) : base.View(model);
        protected new ActionResult View() 
            => IsAjaxOrPartial ? (ActionResult)base.PartialView() : base.View();
    }
}
