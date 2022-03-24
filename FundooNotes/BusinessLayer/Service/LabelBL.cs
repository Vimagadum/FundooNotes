/// <summary>
/// LABEL BL
/// </summary>
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Label BL
    /// </summary>
    public class LabelBL : ILabelBL
    {
        /// <summary>
        /// The LABEL RL
        /// </summary>
        private readonly ILabelRL labelRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBL"/> class.
        /// </summary>
        /// <param name="labelRL">The LABEL RL.</param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Add LABEL Name
        /// </returns>
        public LabelEntity AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                return this.labelRL.AddLabelName(labelName, noteId, userId);
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
                return this.labelRL.GetAllLabels();
            }
            catch (Exception)
            {
                throw;
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
                return this.labelRL.GetByLabeId(noteId);
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
        /// Remove LABEL
        /// </returns>
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                return this.labelRL.RemoveLabel(labelId, userId);
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
        /// Update LABEL
        /// </returns>
        public IEnumerable<LabelEntity> UpdateLabel(long userID, string oldLabelName, string labelName)
        {
            try
            {
                return this.labelRL.UpdateLabel(userID, oldLabelName, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
