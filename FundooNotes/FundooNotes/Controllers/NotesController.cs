using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public readonly INotesBL notesBL;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        [Authorize]
        [HttpPost("CreateNote")]
        public IActionResult CreateNote(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = notesBL.CreateNote(notesModel, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Created Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Notes Creation UnSuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote(NotesModel notesModel, long noteId)
        {
            try
            {
                var notes = this.notesBL.UpdateNote(notesModel, noteId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " Note Updated successfully ", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to update note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                var notes = this.notesBL.DeleteNote(noteId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " Note is Deleted successfully ", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("GetNoteByUserId")]
        public IActionResult GetNotesByUserId(long userId)
        {
            try
            {
                var notes = this.notesBL.GetNotesByUserId(userId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = "Your Notes Are Displaying", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to Display Your noteS" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("GetAllNotes")]
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                var result = this.notesBL.GetAllNotes();
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
