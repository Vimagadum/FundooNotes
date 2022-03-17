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

        public LabelEntity UpdateLabel(string labeName, long noteId, long userId)
        {
            try
            {
                return this.labelRL.UpdateLabel(labeName, noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
