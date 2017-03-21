using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PartyUp.Models;

namespace PartyUp.Controllers
{
    public class InvitedsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Inviteds
        public IQueryable<Invited> GetInviteds()
        {
            return db.Invited;
        }

        // GET: api/Inviteds/5
        [ResponseType(typeof(Invited))]
        public async Task<IHttpActionResult> GetInvited(string id)
        {
            Invited invited = await db.Invited.FindAsync(id);
            if (invited == null)
            {
                return NotFound();
            }

            return Ok(invited);
        }

        // PUT: api/Inviteds/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvited(string id, Invited invited)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invited.InvitedUserId)
            {
                return BadRequest();
            }

            db.Entry(invited).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvitedExists(id))
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

        // POST: api/Inviteds
        [ResponseType(typeof(Invited))]
        public async Task<IHttpActionResult> PostInvited(Invited invited)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invited.Add(invited);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvitedExists(invited.InvitedUserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = invited.InvitedUserId }, invited);
        }

        // DELETE: api/Inviteds/5
        [ResponseType(typeof(Invited))]
        public async Task<IHttpActionResult> DeleteInvited(string id)
        {
            Invited invited = await db.Invited.FindAsync(id);
            if (invited == null)
            {
                return NotFound();
            }

            db.Invited.Remove(invited);
            await db.SaveChangesAsync();

            return Ok(invited);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvitedExists(string id)
        {
            return db.Invited.Count(e => e.InvitedUserId == id) > 0;
        }
    }
}