using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollaborator(CollabModel collabModel);
        public CollabEntity RemoveCollab(long userId, long collabId);
        public List<CollabEntity> GetByNoteId(long noteId, long userId);
    }
}
