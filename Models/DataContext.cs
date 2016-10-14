using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Stack> Stack { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>()
                .HasRequired(p => p.Stack)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.FK_Stack);

            /*modelBuilder.Entity<Comment>()
                .HasRequired(p => p.User);*/

            /*modelBuilder.Entity<Comment>()
                .HasRequired(p => p.FK_Stack)
                .WithOptional(p => p.FK_User)
                .HasForeignKey(p => p.FK_User);*/
        }
    }
}