using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email,long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                CollabModel collaboratorModel = new CollabModel();
                collaboratorModel.Id = userId;
                collaboratorModel.NotesId = noteId;
                collaboratorModel.CollabEmail = email;
                var result = this.collabBL.AddCollaborator(collaboratorModel);
                if(result!=null)
                {
                    return this.Ok(new { Success = true, message = " Collab Added  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Add Failed ! Try Again" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.RemoveCollab(userId, collabId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Removed  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Remove Failed ! Try Again" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("{noteId}/Get")]
        public List<CollabEntity> GetByNoteId(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.GetByNoteId(noteId, userId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
