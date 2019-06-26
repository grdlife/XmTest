namespace XmTest.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class X_AlbumType
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int AlbumType { get; set; }

        [Required]
        [StringLength(100)]
        public string AlbumName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
