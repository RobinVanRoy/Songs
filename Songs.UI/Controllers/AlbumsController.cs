using System.Linq;
using System.Net;
using System.Web.Mvc;
using Songs.Classes;
using Songs.DataLayer.Repositories;

namespace Songs.UI.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly AlbumRepository _repoAlbum = new AlbumRepository();
        private readonly GenreRepository _repoGenre = new GenreRepository();

        // GET: Albums
        public ActionResult Index() => View(_repoAlbum.All.ToList());

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Album album = _repoAlbum.Find(id);
            if (album == null) return HttpNotFound();
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_repoGenre.All, "GenreId", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,Title,ReleaseDate,GenreId")] Album album)
        {
            if (ModelState.IsValid)
            {
                _repoAlbum.InsertOrUpdate(album);
                _repoAlbum.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(_repoGenre.All, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Album album = _repoAlbum.Find(id);
            if (album == null) return HttpNotFound();
            ViewBag.GenreId = new SelectList(_repoGenre.All, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Title,ReleaseDate,GenreId")] Album album)
        {
            if (ModelState.IsValid)
            {
                _repoAlbum.InsertOrUpdate(album);
                _repoAlbum.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(_repoGenre.All, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Album album = _repoAlbum.Find(id);
            if (album == null) return HttpNotFound();
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repoAlbum.Delete(id);
            _repoAlbum.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repoAlbum.Dispose();
                _repoGenre.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
