namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepositoryLayer.Entity;

    /// <summary>
    /// NOTES CONTROLLER
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes BL
        /// </summary>
        public readonly INotesBL notesBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesBL">The notes BL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>Create Note</returns>
        [HttpPost("Create")]
        public IActionResult CreateNote(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = this.notesBL.CreateNote(notesModel, userId);
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

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="updateModel">The update model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Update Note</returns>
        [HttpPut("Update")]
        public IActionResult UpdateNote(UpdateModel updateModel, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.notesBL.UpdateNote(updateModel, noteId, userId);
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

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Delete Note</returns>
        [HttpDelete("Delete")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.notesBL.DeleteNote(noteId, userId);
                if (!notes)
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete note" });
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

        /// <summary>
        /// Gets the notes by user identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Get Notes By UserId</returns>
        [HttpGet("GetNote")]
        public IActionResult GetNotesByUserId()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                
                    var notes = this.notesBL.GetNotesByUserId(userId);
                    if (notes != null)
                    {
                        return this.Ok(new { Success = true, message = "Notes are displayed", data = notes });
                    }
                    else
                    {
                        return this.BadRequest(new { Success = false, message = "failed to Display the notes" });
                    }
                
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all notess using REDIS cache.
        /// </summary>
        /// <returns>Get All Notess Using REDIS Cache</returns>
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotessUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                NotesList = (List<NotesEntity>) this.notesBL.GetNotesByUserId(userId);
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return this.Ok(NotesList);
        }

        /// <summary>
        /// Gets the notes by notes identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Get Notes By Notes Id</returns>
        [Authorize]
        [HttpGet("{noteId}/GetNote")]
        public IActionResult GetNotesByNotesId(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.GetNotesByNotesId(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes are displayed", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to Display the notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether [is archive or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Is Archive Or Not</returns>
        [HttpGet("IsArchive")]
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

        /// <summary>
        /// Determines whether [is trash or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Is Trash Or Not</returns>
        [HttpGet("IsTrash")]
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

        /// <summary>
        /// Determines whether [is pin or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Is Pin Or Not</returns>
        [HttpGet("IsPin")]
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

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Upload Image</returns>
        [HttpPost("{noteId}/ImageUpload")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                // Take id of  Logged In User
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.notesBL.UploadImage(noteId, userId, image);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Image Uploaded Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Image Upload Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the COLOUR.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="colour">The COLOUR.</param>
        /// <returns>Update COLOUR</returns>
        [HttpPut("colour")]
        public IActionResult UpdateColour(long noteId, string colour)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.notesBL.Docolour(noteId, userId, colour);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " colour Added successfully ", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to add colour" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
