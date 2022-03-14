using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity CreateNote(NotesModel notesModel, long UserId);
        public NotesEntity UpdateNote(NotesModel notesModel, long noteId);
        public bool DeleteNote(long noteId);
        public List<NotesEntity> GetNotesByUserId(long useId);
        public List<NotesEntity> GetAllNotes();
        public List<NotesEntity> GetNotesByNotesId(long noteId);





    }
}
