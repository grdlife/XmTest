namespace XmTest.Data.DBEntity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using XmTest.Data.Entity;

    public partial class XmDBConetext : DbContext
    {
        public XmDBConetext()
            : base("name=XmDBConetext")
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<X_Classify> X_Classify { get; set; }
        public virtual DbSet<X_Role> X_Role { get; set; }
        public virtual DbSet<X_User> X_User { get; set; }
        public virtual DbSet<X_User_Role> X_User_Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .Property(e => e.NoteID)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Content)
                .IsUnicode(false);
        }
    }
}
