namespace XmTest.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class X_Classify
    {
        public int Id { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public int? UserID { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Count { get; set; }
    }
}
