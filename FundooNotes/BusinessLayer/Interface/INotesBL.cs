/// <summary>
/// INOTES BL
/// </summary>
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;

    /// <summary>
    ///  notes interface class
    /// </summary>
    public interface INotesBL
    {
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>Create Note</returns>
        public NotesEntity CreateNote(NotesModel notesModel, long UserId);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="updateModel">The update model.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Update Note</returns>        
        public NotesEntity UpdateNote(UpdateModel updateModel, long noteId, long userId);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Delete Note</returns>
        public bool DeleteNote(long noteId, long userId);

        /// <summary>
        /// Gets the notes by user identifier.
        /// </summary>
        /// <param name="useId">The use identifier.</param>
        /// <returns>Get Notes By UserId</returns>
        public List<NotesEntity> GetNotesByUserId(long useId);

        /// <summary>
        /// Gets the notes by notes identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Get Notes By NotesId</returns>
        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId);

        /// <summary>
        /// Determines whether [is archieve or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Is ARCHIEVE Or Not</returns>
        public NotesEntity IsArchieveOrNot(long noteId, long userId);

        /// <summary>
        /// Determines whether [is trash or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Is Trash Or Not</returns>
        public NotesEntity IsTrashOrNot(long noteId, long userId);

        /// <summary>
        /// Determines whether [is pin or not] [the specified note identifier].
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Is Pin Or Not</returns>
        public NotesEntity IsPinOrNot(long noteId, long userId);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Upload Image</returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);

        /// <summary>
        /// DO COLOUR the specified note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="colour">The colour.</param>
        /// <returns>DO COLOUR</returns>
        public NotesEntity Docolour(long noteId, long userId, string colour);
    }
}
