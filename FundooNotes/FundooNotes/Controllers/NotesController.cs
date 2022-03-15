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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public readonly INotesBL notesBL;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
       
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
       
        [HttpGet("{Id}/Get")]
        public IActionResult GetNotesByUserId(long Id)
        {
            try
            {
                var notes = this.notesBL.GetNotesByUserId(Id);
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
        
        [HttpGet("GetNotesByNotesId")]
        public List<NotesEntity> GetNotesByNotesId(long noteId)
        {
            try
            {
                var result = this.notesBL.GetNotesByNotesId(noteId);
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
        
        [HttpGet("Archive")]
        public IActionResult IsArchiveOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsArchieveOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "  Is Archive Successfull ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Is Archive Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("Trash")]
        public IActionResult IsTrashOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsTrashOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "  Is Trash Successfull ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Is Trash Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("Pin")]
        public IActionResult IsPinOrNot(long noteId)
        {
            try
            {
                // Take id of  Logged User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.IsPinOrNot(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "  Is Pin Successfull ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Is Pin Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
