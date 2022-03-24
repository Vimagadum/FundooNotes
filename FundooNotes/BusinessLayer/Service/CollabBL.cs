/// <summary>
/// COLLAB BL CLASS
/// </summary>
namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// COLLAB BL  
    /// </summary>
    public class CollabBL : ICollabBL
    {
        /// <summary>
        /// The COLLAB RL
        /// </summary>
        private readonly ICollabRL collabRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabBL"/> class.
        /// </summary>
        /// <param name="collabRL">The COLLAB RL.</param>
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabModel">The COLLAB model.</param>
        /// <returns>Add Collaborator</returns>
        public CollabEntity AddCollaborator(CollabModel collabModel)
        {
            try
            {
                return this.collabRL.AddCollaborator(collabModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all COLLAB.
        /// </summary>
        /// <returns>Get All COLLAB</returns>
        public List<CollabEntity> GetAllCollab()
        {
            try
            {
                return this.collabRL.GetAllCollab();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Get By NoteId</returns>
        public List<CollabEntity> GetByNoteId(long noteId, long userId)
        {
            try
            {
                return this.collabRL.GetByNoteId(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the COLLAB.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The COLLAB identifier.</param>
        /// <returns>Remove COLLAB</returns>
        public CollabEntity RemoveCollab(long userId, long collabId)
        {
            try
            {
                return this.collabRL.RemoveCollab(userId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
