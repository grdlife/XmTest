namespace XmTest.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notes
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int ClassifyID { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(150)]
        public string iCon { get; set; }

        public int? Viewed { get; set; }

        public int? ThumbUpCount { get; set; }

        public int? DownCount { get; set; }

        public int? ReplyCount { get; set; }

        [StringLength(150)]
        public string Motto { get; set; }


        public DateTime CreateTime { get; set; }


        public DateTime UpdateTime { get; set; }

    }
}
