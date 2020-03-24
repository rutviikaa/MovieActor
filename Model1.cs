namespace MovieActor
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movy> Movies { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .Property(e => e.ActorName)
                .IsUnicode(false);

            modelBuilder.Entity<Actor>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Actors)
                .Map(m => m.ToTable("Movie_Actor").MapLeftKey("ActorId").MapRightKey("MovieId"));

            modelBuilder.Entity<Movy>()
                .Property(e => e.MovieName)
                .IsUnicode(false);
        }
    }
}
