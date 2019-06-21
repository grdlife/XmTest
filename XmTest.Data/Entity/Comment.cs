namespace XmTest.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        [Column(TypeName = "money")]
        public decimal? NoteID { get; set; }

        public int? CtiticID { get; set; }

        public int? ReplyID { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        public int? Sort { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
