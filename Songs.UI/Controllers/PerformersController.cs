using System.Linq;
using System.Net;
using System.Web.Mvc;
using Songs.Classes;
using Songs.DataLayer.Repositories;

namespace Songs.UI.Controllers
{
    public class PerformersController : Controller
    {
        private readonly PerformerRepository _repo = new PerformerRepository();

        // GET: Performers
        public ActionResult Index() => View(_repo.All.ToList());

        // GET: Performers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Performer performer = _repo.Find((int) id);
            if (performer == null) return HttpNotFound();
            return View(performer);
        }

        // GET: Performers/Create
        public ActionResult Create() => View();

        // POST: Performers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PerformerId,FirstName,LastName,NickName,Birthday,BirthPlace,Country")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertOrUpdate(performer);
                _repo.Save();
                return RedirectToAction("Index");
            }

            return View(performer);
        }

        // GET: Performers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Performer performer = _repo.Find((int) id);
            if (performer == null) return HttpNotFound();
            return View(performer);
        }

        // POST: Performers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PerformerId,FirstName,LastName,NickName,Birthday,BirthPlace,Country")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertOrUpdate(performer);
                _repo.Save();
                return RedirectToAction("Index");
            }
            return View(performer);
        }

        // GET: Performers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Performer performer = _repo.Find((int) id);
            if (performer == null) return HttpNotFound();
            return View(performer);
        }

        // POST: Performers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
