namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepositoryLayer.Entity;

    /// <summary>
    ///  Label Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        /// <summary>The label BL</summary>
        private readonly ILabelBL labelBL;

        /// <summary>The memory cache</summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>The distributed cache</summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>Initializes a new instance of the <see cref="LabelController" /> class.</summary>
        /// <param name="labelBL">The label BL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>Adds the label.</summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddLabel(string labelName, long noteId)
        {
            try
            {

                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                var label = this.labelBL.AddLabelName(labelName, noteId, userId);
                if(label!=null)
                {
                    return this.Ok(new { success = true, message = "Label Added Successfully", data = label });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Label adding UnSuccessfull" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>Renames the label.</summary>
        /// <param name="lableName">Name of the lable.</param>
        /// <param name="newLabelName">New name of the label.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult RenameLabel(long noteId, string lableName, string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.UpdateLabel(userID, lableName, newLabelName, noteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label renamed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>Removes the label.</summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveLabel(long labelId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.labelBL.RemoveLabel(labelId, userId))
                {
                    return this.Ok(new { Success = true, message = " Label Removed  successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label Remove Failed " });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Gets the by label identifier.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Authorize]
        [HttpGet("{noteId}/Get")]
        public IActionResult GetByLabelId(long noteId)
        {
            try
            {
                var result = this.labelBL.GetByLabeId(noteId);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Displaying successfully", response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Gets all labels.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet("GetAll")]
        public IEnumerable<LabelEntity> GetAllLabels()
        {
            try
            {
                var result = this.labelBL.GetAllLabels();
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

        /// <summary>Gets all label using redis cache.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = (List<LabelEntity>)this.labelBL.GetAllLabels();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
