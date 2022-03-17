using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabelName(string labelName, long noteId, long userId);
        public IEnumerable<LabelEntity> UpdateLabel(long userID, string oldLabelName, string labelName);
        public bool RemoveLabel(long labelId, long userId);
        public List<LabelEntity> GetByLabeId(long noteId);

    }
}
