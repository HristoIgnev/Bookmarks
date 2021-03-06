﻿namespace Bookmarks.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Common.Contracts;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;

    public class Bookmark : IDeletableEntity
    {
        private ICollection<Tag> tags;

        public Bookmark()
        {
            this.tags = new HashSet<Tag>();
        }

        [Key]
        public int Id { get; set; }

        [Index]
        [Required]
        [MaxLength(ValidationConstants.MaxBookmarkDescriptionLength)]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public Website WebSite { get; set; }

        [MaxLength(ValidationConstants.MaxBookmarkDescriptionLength)]
        public string Description { get; set; }

        public string SnapshotBase64String { get; set; }

        public string UserId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }

    }
}
