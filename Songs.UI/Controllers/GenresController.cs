using System.Net;
using System.Web.Mvc;
using Songs.Classes;
using Songs.DataLayer.Repositories;

namespace Songs.UI.Controllers
{
    public class GenresController : Controller
    {
        private readonly GenreRepository _repo = new GenreRepository();

        // GET: Genres
        public ActionResult Index() => View(_repo.All);

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Genre genre = _repo.Find((int) id);
            if (genre == null) return HttpNotFound();
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create() => View();

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreId,Name")] Genre genre)
        {
            if (!ModelState.IsValid) return View(genre);
            _repo.InsertOrUpdate(genre);
            _repo.Save();
            return RedirectToAction("Index");
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Genre genre = _repo.Find((int) id);
            if (genre == null) return HttpNotFound();
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreId,Name")] Genre genre)
        {
            if (!ModelState.IsValid) return View(genre);
            _repo.InsertOrUpdate(genre);
            _repo.Save();
            return RedirectToAction("Index");
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Genre genre = _repo.Find((int) id);
            if (genre == null) return HttpNotFound();
            return View(genre);
        }

        // POST: Genres/Delete/5
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
