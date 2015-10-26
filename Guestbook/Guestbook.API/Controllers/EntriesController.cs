using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Guestbook.API;
using Guestbook.API.Models;

namespace Guestbook.API.Controllers
{
    public class EntriesController : ApiController
    {
        private Guestbook_DevEntities db = new Guestbook_DevEntities();

        // GET: api/Entries
        public IQueryable<EntriesModel> GetEntries()
        {
            return db.Entries.Select(e => new EntriesModel
            {
                CreatedDate = e.CreatedDate,
                Name = e.Name,
                EntryContent = e.EntryContent
            });
        }

        // GET: api/Entries/5
        [ResponseType(typeof(EntriesModel))]
        public IHttpActionResult GetEntry(int id)
        {
            Entry dbEntry = db.Entries.Find(id);
            if (dbEntry == null)
            {
                return NotFound();
            }

            EntriesModel modelEntry = new EntriesModel
            {
                CreatedDate = dbEntry.CreatedDate,
                Name = dbEntry.Name,
                EntryContent = dbEntry.EntryContent
            };

            return Ok(modelEntry);
        }

        // PUT: api/Entries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntry(int id, EntriesModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entry.EntryId)
            {
                return BadRequest();
            }
            var dbPutEntry = db.Entries.Find(entry.EntryId);

            dbPutEntry.Update(entry);

            db.Entry(dbPutEntry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Entries
        [ResponseType(typeof(EntriesModel))]
        public IHttpActionResult PostEntry(EntriesModel entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbPostEntry = new Entry();

            dbPostEntry.Update(entry);

            db.Entries.Add(dbPostEntry);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dbPostEntry.EntryId }, entry);
        }

        // DELETE: api/Entries/5
        [ResponseType(typeof(Entry))]
        public IHttpActionResult DeleteEntry(int id)
        {
            Entry entry = db.Entries.Find(id);
            if (entry == null)
            {
                return NotFound();
            }

            db.Entries.Remove(entry);
            db.SaveChanges();

            return Ok(entry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntryExists(int id)
        {
            return db.Entries.Count(e => e.EntryId == id) > 0;
        }
    }
}