using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNote(NotesModel notesModel, long UserId);
        public NotesEntity UpdateNote(UpdateModel updateModel, long noteId, long userId);
        public bool DeleteNote(long noteId, long userId);
        public List<NotesEntity> GetNotesByUserId(long useId);

        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId);
        public NotesEntity IsArchieveOrNot(long noteId, long userId);
        public NotesEntity IsTrashOrNot(long noteId, long userId);
        public NotesEntity IsPinOrNot(long noteId, long userId);
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);

    }
}
