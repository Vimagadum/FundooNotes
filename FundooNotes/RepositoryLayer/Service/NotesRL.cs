using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL:INotesRL
    {
        public readonly FundooContext fundooContext;
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NotesEntity CreateNote(NotesModel notesModel, long UserId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity()
                {
                    Title = notesModel.Title,
                    Description = notesModel.Description,
                    Remainder = notesModel.Remainder,
                    Colour = notesModel.Colour,
                    Image = notesModel.Image,
                    IsTrash = notesModel.IsTrash,
                    IsArchive = notesModel.IsArchive,
                    IsPinn = notesModel.IsPinn,
                    CreatedAt = notesModel.CreatedAt,
                    ModifierAt = notesModel.ModifierAt,
                    Id = UserId
                };
                this.fundooContext.Notes.Add(notesEntity);

                // Save Changes Made in the database
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return notesEntity;
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
        public NotesEntity UpdateNote(NotesModel notesModel, long noteId)
        {
            try
            {
                // Fetch All the details with the given noteId.
                var note = this.fundooContext.Notes.Where(u => u.NotesId == noteId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = notesModel.Title;
                    note.Description = notesModel.Description;
                    note.Colour = notesModel.Colour;
                    note.Image = notesModel.Image;
                    note.ModifierAt = notesModel.ModifierAt;

                    // Update database for given NoteId.
                    this.fundooContext.Notes.Update(note);

                    // Save Changes Made in the database
                    this.fundooContext.SaveChanges();
                    return note;
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
        public bool DeleteNote(long noteId)
        {
            try
            {
                
                var notes = this.fundooContext.Notes.Where(n => n.NotesId == noteId).FirstOrDefault();
                if (notes != null)
                {
                    // Remove Note details from database
                    this.fundooContext.Notes.Remove(notes);

                    
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var notesResult = this.fundooContext.Notes.Where(n => n.Id == userId).ToList();
                if(notesResult!=null)
                {
                    return notesResult;
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
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                // Fetch All the details from Notes Table
                var allNotesResult = this.fundooContext.Notes.ToList();
                if (allNotesResult != null)
                {
                    return allNotesResult;
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
        public List<NotesEntity> GetNotesByNotesId(long noteId)
        {
            try
            {
                var notesResult = this.fundooContext.Notes.Where(n => n.NotesId == noteId).ToList();
                if (notesResult != null)
                {
                    return notesResult;
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
        public NotesEntity IsArchieveOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsArchive == false)
                    {
                        notes.IsArchive = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsArchive = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
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
        public NotesEntity IsTrashOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsTrash == false)
                    {
                        notes.IsTrash = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsTrash = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
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
        public NotesEntity IsPinOrNot(long noteId, long userId)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var notes = this.fundooContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    if (notes.IsPinn == false)
                    {
                        notes.IsPinn = true;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        notes.IsPinn = false;
                        this.fundooContext.SaveChanges();
                        return notes;
                    }
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
