namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Notes Model
    /// </summary>
    public class NotesModel
    {
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the remainder.</summary>
        /// <value>The remainder.</value>
        public DateTime Remainder { get; set; }

        /// <summary>Gets or sets the COLOUR.</summary>
        /// <value>The COLOUR.</value>
        public string Colour { get; set; }

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        public string Image { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is trash.</summary>
        /// <value>
        ///    <c>true</c> if this instance is trash; otherwise, <c>false</c>.</value>
        public bool IsTrash { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is archive.</summary>
        /// <value>
        ///   <c>true</c> if this instance is archive; otherwise, <c>false</c>.</value>
        public bool IsArchive { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is PINN.</summary>
        /// <value>
        ///    <c>true</c> if this instance is PINN; otherwise, <c>false</c>.</value>
        public bool IsPinn { get; set; }

        /// <summary>Gets or sets the created at.</summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; set; }

        /// <summary>Gets or sets the modifier at.</summary>
        /// <value>The modifier at.</value>
        public DateTime? ModifierAt { get; set; }
    }
}
