// -----------------------------------------------------------------------
// <copyright file="ICollabBL.cs" company="Vinayak">
//     Company copyright tag.
// </copyright>
//------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
 
    /// <summary>
    ///   COLLAB interface 
    /// </summary>
    public interface ICollabBL
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabModel">The COLLAB model.</param>
        /// <returns>Add Collaborator</returns>
        public CollabEntity AddCollaborator(CollabModel collabModel);

        /// <summary>
        /// Removes the COLLAB.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The COLLAB identifier.</param>
        /// <returns>Remove COLLAB</returns>
        public CollabEntity RemoveCollab(long userId, long collabId);

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Get By NoteId</returns>
        public List<CollabEntity> GetByNoteId(long noteId, long userId);

        /// <summary>
        /// Gets all COLLAB.
        /// </summary>
        /// <returns>Get All COLLAB</returns>
        public List<CollabEntity> GetAllCollab();
    }
}
