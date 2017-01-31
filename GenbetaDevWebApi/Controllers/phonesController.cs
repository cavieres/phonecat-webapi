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
using GenbetaDevWebApi;

namespace GenbetaDevWebApi.Controllers
{
    public class phonesController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/phones
        public IQueryable<phone> Getphone()
        {
            return db.phone;
        }

        // GET: api/phones/5
        [ResponseType(typeof(phone))]
        public IHttpActionResult Getphone(string id)
        {
            phone phone = db.phone.Find(id);
            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        // PUT: api/phones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putphone(string id, phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phone.Id)
            {
                return BadRequest();
            }

            db.Entry(phone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!phoneExists(id))
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

        // POST: api/phones
        [ResponseType(typeof(phone))]
        public IHttpActionResult Postphone(phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.phone.Add(phone);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (phoneExists(phone.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = phone.Id }, phone);
        }

        // DELETE: api/phones/5
        [ResponseType(typeof(phone))]
        public IHttpActionResult Deletephone(string id)
        {
            phone phone = db.phone.Find(id);
            if (phone == null)
            {
                return NotFound();
            }

            db.phone.Remove(phone);
            db.SaveChanges();

            return Ok(phone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool phoneExists(string id)
        {
            return db.phone.Count(e => e.Id == id) > 0;
        }
    }
}