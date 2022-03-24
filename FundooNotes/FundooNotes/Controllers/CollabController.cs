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
    ///  COLLAB Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        /// <summary>The COLLAB BL</summary>
        private readonly ICollabBL collabBL;

        /// <summary>The memory cache</summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>The distributed cache</summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>Initializes a new instance of the <see cref="CollabController" /> class.</summary>
        /// <param name="collabBL">The COLLAB BL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>Adds the COLLAB.</summary>
        /// <param name="email">The email.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///  Add COLLAB
        /// </returns>
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                CollabModel collaboratorModel = new CollabModel();
                collaboratorModel.Id = userId;
                collaboratorModel.NotesId = noteId;
                collaboratorModel.CollabEmail = email;
                var result = this.collabBL.AddCollaborator(collaboratorModel);
                if (result != null)
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

        /// <summary>Removes the COLLAB.</summary>
        /// <param name="collabId">The COLLAB identifier.</param>
        /// <returns>
        ///  Remove COLLAB
        /// </returns>
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

        /// <summary>Gets the by note identifier.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///  Get By Note Id
        /// </returns>
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

        /// <summary>Gets all COLLABS.</summary>
        /// <returns>
        ///   Get All COLLABS
        /// </returns>
        [Authorize]
        [HttpGet("Get")]
        public List<CollabEntity> GetAllCollabs()
        {
            try
            {
                var result = this.collabBL.GetAllCollab();
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

        /// <summary>Gets all COLLAB using REDIS cache.</summary>
        /// <returns>
        ///  Get All COLLAB Using REDIS Cache
        /// </returns>
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "CollabList";
            string serializedColabList;
            var collabList = new List<CollabEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedColabList = Encoding.UTF8.GetString(redisLabelList);
                collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedColabList);
            }
            else
            {
                collabList = (List<CollabEntity>)this.collabBL.GetAllCollab();
                serializedColabList = JsonConvert.SerializeObject(collabList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedColabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(collabList);
        }
    }
}
