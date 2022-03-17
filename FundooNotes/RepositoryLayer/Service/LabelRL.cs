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
        public LabelEntity UpdateLabel(string labeName,long noteId,long userId)
        {
            try
            {
                var label = this.fundooContext.Label.FirstOrDefault(a => a.NotesId == noteId && a.Id == userId);
                if(label!=null)
                {
                    label.LabelName = labeName;
                    this.fundooContext.Label.Update(label);
                    this.fundooContext.SaveChanges();
                    return label;

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
