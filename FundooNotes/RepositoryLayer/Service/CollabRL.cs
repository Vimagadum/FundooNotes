namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// COLLAB RL Class
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.ICollabRL" />
    public class CollabRL : ICollabRL
    {
        /// <summary>
        /// The FUNDOO context
        /// </summary>
        private readonly FundooContext fundooContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The FUNDOO context.</param>
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabModel">The COLLAB model.</param>
        /// <returns>
        /// Add Collaborator
        /// </returns>
        public CollabEntity AddCollaborator(CollabModel collabModel)
        {
            try
            {
                CollabEntity collaboration = new CollabEntity();
                var user = this.fundooContext.User.Where(e => e.Email == collabModel.CollabEmail).FirstOrDefault();
                var notes = this.fundooContext.Notes.Where(e => e.NotesId == collabModel.NotesId && e.Id == collabModel.Id).FirstOrDefault();
                if (notes != null && user != null)
                {
                    collaboration.NotesId = collabModel.NotesId;
                    collaboration.CollabEmail = collabModel.CollabEmail;
                    collaboration.Id = collabModel.Id;
                    this.fundooContext.Collab.Add(collaboration);
                    var result = this.fundooContext.SaveChanges();
                    return collaboration;
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
        /// Removes the COLLAB.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The COLLAB identifier.</param>
        /// <returns>
        /// Remove COLLAB
        /// </returns>
        public CollabEntity RemoveCollab(long userId, long collabId)
        {
            try
            {
                var data = this.fundooContext.Collab.FirstOrDefault(d => d.Id == userId && d.CollabId == collabId);
                if (data != null)
                {
                    this.fundooContext.Collab.Remove(data);
                    this.fundooContext.SaveChanges();
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
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Get By NoteId
        /// </returns>
        public List<CollabEntity> GetByNoteId(long noteId, long userId)
        {
            try
            {
                var data = this.fundooContext.Collab.Where(c => c.NotesId == noteId && c.Id == userId).ToList();
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
        /// Gets all COLLAB.
        /// </summary>
        /// <returns>
        /// Get All COLLAB
        /// </returns>
        public List<CollabEntity> GetAllCollab()
        {
            try
            {
                var data = this.fundooContext.Collab.ToList();
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
    }
}
