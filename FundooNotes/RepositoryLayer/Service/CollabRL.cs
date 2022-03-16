using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL:ICollabRL
    {
        private readonly FundooContext fundooContext;
        
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollabEntity AddCollaborator(CollabModel collabModel)
        {
            try
            {
                CollabEntity collaboration = new CollabEntity();
                var user = fundooContext.User.Where(e => e.Email == collabModel.CollabEmail).FirstOrDefault();

                var notes = fundooContext.Notes.Where(e => e.NotesId == collabModel.NotesId && e.Id == collabModel.Id).FirstOrDefault();
                if (notes != null && user != null)
                {
                    collaboration.NotesId = collabModel.NotesId;
                    collaboration.CollabEmail = collabModel.CollabEmail;
                    collaboration.Id = collabModel.Id;
                    fundooContext.Collab.Add(collaboration);
                    var result = fundooContext.SaveChanges();
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
        public List<CollabEntity> GetByNoteId(long noteId,long userId)
        {
            try
            {
                var data = this.fundooContext.Collab.Where(c => c.NotesId == noteId&&c.Id==userId).ToList();
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
