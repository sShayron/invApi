using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace invApi.Models
{
    public class ModelContext : IdentityDbContext<ApplicationUser>
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }
        public new DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .UseNpgsqlSerialColumn();
        }
    }

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLengthAttribute(10)]
        public string Name { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
     public class ApplicationUser : IdentityUser
    {
        public List<User> Users { get; set; }
    }
}