namespace XmTest.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class X_Diary
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Content { get; set; }


        public DateTime CreateTime { get; set; }
    }
}
