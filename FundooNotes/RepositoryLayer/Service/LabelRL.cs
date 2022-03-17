using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity
                {
                    LabelName = labelName,
                    Id = userId,
                    NotesId = noteId
                };
                this.fundooContext.Label.Add(labelEntity);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
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
        
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                var labelDetails = this.fundooContext.Label.FirstOrDefault(l => l.LabelId == labelId && l.Id == userId);
                if (labelDetails != null)
                {
                    this.fundooContext.Label.Remove(labelDetails);

                    // Save Changes Made in the database
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
        public IEnumerable<LabelEntity> UpdateLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<LabelEntity> labels;
            labels = fundooContext.Label.Where(e => e.Id == userID && e.LabelName == oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    label.LabelName = labelName;
                }
                fundooContext.SaveChanges();
                return labels;
            }
            else
            {
                return null;
            }

        }
        public List<LabelEntity> GetByLabeId(long noteId)
        {
            try
            {
                // Fetch All the details with the given noteid.
                var data = this.fundooContext.Label.Where(d => d.NotesId == noteId).ToList();
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
