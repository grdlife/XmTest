namespace XmTest.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class X_Album
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string FileUrl { get; set; }

        public int FileType { get; set; }

        public DateTime CreateTime { get; set; }

        [Column(TypeName = "text")]
        public string Describe { get; set; }
    }
}
