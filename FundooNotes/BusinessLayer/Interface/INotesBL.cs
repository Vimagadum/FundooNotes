using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNote(NotesModel notesModel, long UserId);
        public NotesEntity UpdateNote(NotesModel notesModel, long noteId);
        public bool DeleteNote(long noteId);
        public List<NotesEntity> GetNotesByUserId(long useId);

        public List<NotesEntity> GetAllNotes();
        public List<NotesEntity> GetNotesByNotesId(long noteId);
        public NotesEntity IsArchieveOrNot(long noteId, long userId);
        public NotesEntity IsTrashOrNot(long noteId, long userId);
        public NotesEntity IsPinOrNot(long noteId, long userId);

    }
}
