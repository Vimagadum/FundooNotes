using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity AddCollaborator(CollabModel collabModel);
        public CollabEntity RemoveCollab(long userId, long collabId);
        public List<CollabEntity> GetByNoteId(long noteId, long userId);

    }
}
