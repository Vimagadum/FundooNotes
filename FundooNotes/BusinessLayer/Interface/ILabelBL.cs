/// <summary>
/// ILABEL BL
/// </summary>
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RepositoryLayer.Entity;
    
    /// <summary>
    /// label interface class
    /// </summary>
    public interface ILabelBL
    {
        /// <summary>Adds the name of the label.</summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///  Add Label Name
        /// </returns>
        public LabelEntity AddLabelName(string labelName, long noteId, long userId);

        /// <summary>Updates the label.</summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="oldLabelName">Old name of the label.</param>
        /// <param name="labelName">Name of the label.</param>
        /// <returns>
        ///  Update Label
        /// </returns>
        public IEnumerable<LabelEntity> UpdateLabel(long userID, string oldLabelName, string labelName);

        /// <summary>Removes the label.</summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///  Remove Label
        /// </returns>
        public bool RemoveLabel(long labelId, long userId);

        /// <summary>Gets the by labe identifier.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///  Get By LabeId
        /// </returns>
        public List<LabelEntity> GetByLabeId(long noteId);

        /// <summary>Gets all labels.</summary>
        /// <returns>
        /// Get All Labels
        /// </returns>
        public IEnumerable<LabelEntity> GetAllLabels();
    }
}
