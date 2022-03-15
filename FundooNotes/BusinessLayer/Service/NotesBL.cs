using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        public NotesEntity CreateNote(NotesModel notesModel, long UserId)
        {
            try
            {
                return notesRL.CreateNote(notesModel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity UpdateNote(NotesModel notesModel, long noteId)
        {
            try
            {
                return notesRL.UpdateNote(notesModel, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteNote(long noteId)
        {
            try
            {
                return notesRL.DeleteNote(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                return notesRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotesEntity> GetNotesByUserId(long userId)
        {
            try
            {
                return notesRL.GetNotesByUserId(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotesEntity> GetNotesByNotesId(long noteId)
        {
            try
            {
                return notesRL.GetNotesByNotesId(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsArchieveOrNot(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsTrashOrNot(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity IsPinOrNot(long noteId, long userId)
        {
            try
            {
                return notesRL.IsPinOrNot(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                return notesRL.UploadImage(noteId, userId, image);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
