using StudentDiary.Models.Configurations;
using StudentDiary.Models.Domains;
using StudentDiary.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace StudentDiary
{
    public class ApplicationDbContext : DbContext
    {
        private static string _connectingData = $@"
            server = {Settings.Default.serverAdress}\{Settings.Default.serverName};
            Database = {Settings.Default.nameDataBase};
            User Id = {Settings.Default.userDataBase};
            Password = {Settings.Default.passwordDataBase};";


        public ApplicationDbContext()
            : base(_connectingData)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
    }
}