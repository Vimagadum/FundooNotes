namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// COLLAB Model
    /// </summary>
    public class CollabModel
    {
        /// <summary>Gets or sets the notes identifier.</summary>
        /// <value>The notes identifier.</value>
        public long NotesId { get; set; }

        /// <summary>Gets or sets the COLLAB email.</summary>
        /// <value>The COLLAB email.</value>
        public string CollabEmail { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }
    }
}
