using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public LabelEntity AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                return this.labelRL.AddLabelName(labelName, noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<LabelEntity> GetByLabeId(long noteId)
        {
            try
            {
                return this.labelRL.GetByLabeId(noteId);
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
                return this.labelRL.RemoveLabel(labelId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<LabelEntity> UpdateLabel(long userID, string oldLabelName, string labelName)
        {
            try
            {
                return this.labelRL.UpdateLabel(userID, oldLabelName, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
