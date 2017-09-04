using LoginPoC.DAL;
using LoginPoC.Model.ProcessType;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LoginPoC.Model.User;
using LoginPoC.Core.ProcessType;

namespace LoginPoC.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ApplicationUserRoles.Agent + ", " + ApplicationUserRoles.Administrator)]
    public class ProcessTypeController : Controller
    {
        // GET DbContext from container
        private IProcessTypeService ProcessTypeService { get; set; }

        public ProcessTypeController(IProcessTypeService processTypeService)
        {
            this.ProcessTypeService = processTypeService;
        }

        // GET: ProcessType
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Index()
        {
            return View(this.ProcessTypeService.GetAll());
        }

        // GET: ProcessType/Details/5
        [OverrideAuthorization]
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcessType processType = this.ProcessTypeService.GetById(id.Value);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // GET: ProcessType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcessType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] ProcessType processType)
        {
            if (ModelState.IsValid)
            {
                this.ProcessTypeService.Add(processType);
                return RedirectToAction("Index");
            }

            return View(processType);
        }

        // GET: ProcessType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcessType processType = this.ProcessTypeService.GetById(id.Value);
            if (processType == null)
            {
                return HttpNotFound();
            }
            return View(processType);
        }

        // POST: ProcessType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] ProcessType processType)
        {
            if (ModelState.IsValid)
            {
                this.ProcessTypeService.Update(processType);
                return RedirectToAction("Index");
            }
            return View(processType);
        }

        // GET: ProcessType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcessType processType = this.ProcessTypeService.GetById(id.Value);
            if (processType == null)
            {
                return HttpNotFound();
            }

            return View(processType);
        }

        // POST: ProcessType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.ProcessTypeService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
