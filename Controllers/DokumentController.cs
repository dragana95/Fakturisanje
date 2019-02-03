using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Fakturisanje.Models;

namespace Fakturisanje.Controllers
{
    public class DokumentController : Controller
    {
        // GET: Dokument Dokument/Index
        public ActionResult Index()
        {
            using (LocalDBEntities dbEntity = new LocalDBEntities())
            {
                return View(dbEntity.DokumentFaktures.ToList());
            }
        }

        // GET: Dokument/Details/5
        public ActionResult Details(string id)
        {
            using (LocalDBEntities dbmodel = new LocalDBEntities())
            {
                var rezultat = new Faktura
                {
                    
                    FK_DokumetFakture = dbmodel.DokumentFaktures.FirstOrDefault(x => x.RedniBroj.Equals(id)).RedniBroj,
                    DatumFakture = dbmodel.DokumentFaktures.FirstOrDefault(x => x.RedniBroj.Equals(id)).DatumFakture,
                    BrojFakture = dbmodel.DokumentFaktures.FirstOrDefault(x => x.RedniBroj.Equals(id)).RedniBroj,

                    S = (from s in dbmodel.StavkaFaktures join d in dbmodel.DokumentFaktures
                         on s.FK_DokumentFakture equals d.RedniBroj
                         where s.FK_DokumentFakture.Equals(id)
                         select new { s.RedniBroj, s.Kolicina, s.Cena, s.Ukupno })
                         .ToDictionary(k => k.RedniBroj, v => Tuple.Create(v.Kolicina, v.Cena, v.Ukupno))
                };

                return View(rezultat);

            }
        }

        // GET: Dokument/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dokument/Create
        [HttpPost]
        public ActionResult Create(DokumentFakture dokument)
        {
                // TODO: Add insert logic here
            try
            {
                    using (LocalDBEntities dBEntity = new LocalDBEntities())
                    {
                        dBEntity.DokumentFaktures.Add(dokument);
                        dBEntity.SaveChanges();
                    }
                    return RedirectToAction("Index");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Faktura nije dodata!");
                return View();
            }
        }

        // GET: Dokument/Edit
        public ActionResult Edit(string id)
        {
            using (LocalDBEntities dBEntity = new LocalDBEntities())
            {
                return View(dBEntity.DokumentFaktures.Where(x => x.RedniBroj == id).FirstOrDefault());
            }
        }

        // POST: Dokument/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DokumentFakture dokument)
        {
            try
            {
                // TODO: Add update logic here
                using(LocalDBEntities dbEntity = new LocalDBEntities())
                {
                    string idStr = id.ToString();
                    dbEntity.Entry(dokument).State = System.Data.EntityState.Modified;
                    dbEntity.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dokument/Delete/5
        public ActionResult Delete(string id)
        {
            using (LocalDBEntities dBEntity = new LocalDBEntities())
            {
                return View(dBEntity.DokumentFaktures.Where(x => x.RedniBroj == id).FirstOrDefault());
            }
        }

        // POST: Dokument/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using(LocalDBEntities dbEntity = new LocalDBEntities())
                {
                    DokumentFakture dokument = dbEntity.DokumentFaktures.Where(x => x.RedniBroj == id).FirstOrDefault();
                    dbEntity.DokumentFaktures.Remove(dokument);
                    dbEntity.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
