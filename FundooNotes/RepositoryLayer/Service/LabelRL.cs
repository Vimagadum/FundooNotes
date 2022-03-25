namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// LABEL RL Class
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.ILabelRL" />
    public class LabelRL : ILabelRL
    {
        /// <summary>
        /// The FUNDOO context
        /// </summary>
        private readonly FundooContext fundooContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The FUNDOO context.</param>
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Add Label Name
        /// </returns>
        public LabelEntity AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity
                {
                    LabelName = labelName,
                    Id = userId,
                    NotesId = noteId
                };
                this.fundooContext.Label.Add(labelEntity);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
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

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Remove Label
        /// </returns>
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                var labelDetails = this.fundooContext.Label.FirstOrDefault(l => l.LabelId == labelId && l.Id == userId);
                if (labelDetails != null)
                {
                    this.fundooContext.Label.Remove(labelDetails);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="oldLabelName">Old name of the label.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        /// Update Label
        /// </returns>
        public IEnumerable<LabelEntity> UpdateLabel(long userID, string oldLabelName, string labelName, long noteId)
        {
            IEnumerable<LabelEntity> labels;
            labels = this.fundooContext.Label.Where(e => e.Id == userID && e.LabelName == oldLabelName && e.NotesId == noteId).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    label.LabelName = labelName;
                }
                this.fundooContext.SaveChanges();
                return labels;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the by labe identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Get By LabeId
        /// </returns>
        public List<LabelEntity> GetByLabeId(long noteId)
        {
            try
            {
                // Fetch All the details with the given noteid.
                var data = this.fundooContext.Label.Where(d => d.NotesId == noteId).ToList();
                if (data != null)
                {
                    return data;
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

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>
        /// Get All Labels
        /// </returns>
        public IEnumerable<LabelEntity> GetAllLabels()
        {
            try
            {
                // Fetch All the details from Labels Table
                var labels = this.fundooContext.Label.ToList();
                if (labels != null)
                {
                    return labels;
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
