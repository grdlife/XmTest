namespace XmTest.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class X_Role
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
