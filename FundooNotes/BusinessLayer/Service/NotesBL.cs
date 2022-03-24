namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Notes BL
    /// </summary>
    public class NotesBL : INotesBL
    {
        /// <summary>
        /// The notes RL
        /// </summary>
        private readonly INotesRL notesRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesBL"/> class.
        /// </summary>
        /// <param name="notesRL">The notes RL.</param>
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>
        /// Create Note
        /// </returns>
        public NotesEntity CreateNote(NotesModel notesModel, long UserId)
        {
            try
            {
                return this.notesRL.CreateNote(notesModel, UserId);
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
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Update Note
        /// </returns>
        public NotesEntity UpdateNote(UpdateModel updateModel, long noteId, long userId)
        {
            try
            {
                return this.notesRL.UpdateNote(updateModel, noteId, userId);
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
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Delete Note
        /// </returns>
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                return this.notesRL.DeleteNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Gets the notes by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///  Get Notes By UserId
        /// </returns>
        public List<NotesEntity> GetNotesByUserId(long userId)
        {
            try
            {
                return this.notesRL.GetNotesByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the notes by notes identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Get Notes By NotesId
        /// </returns>
        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId)
        {
            try
            {
                return this.notesRL.GetNotesByNotesId(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether [is archieve or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Is ARCHIEVE Or Not
        /// </returns>
        public NotesEntity IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsArchieveOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Determines whether [is trash or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Is Trash Or Not</returns>
        public NotesEntity IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsTrashOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Determines whether [is pin or not] [the specified note identifier].</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Is Pin Or Not</returns>
        public NotesEntity IsPinOrNot(long noteId, long userId)
        {
            try
            {
                return this.notesRL.IsPinOrNot(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Uploads the image.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Upload Image</returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                return this.notesRL.UploadImage(noteId, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Docolours the specified note identifier.</summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="colour">The COLOUR.</param>
        /// <returns>DO COLOUR</returns>
        public NotesEntity Docolour(long noteId, long userId, string colour)
        {
            try
            {
                return this.notesRL.Docolour(noteId, userId, colour);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
