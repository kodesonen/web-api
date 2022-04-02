using api.Models;
using System.Collections.Generic;

namespace api.Tests.Helpers
{
    public class Utilities
    {
        public static void InitializeDbForTests(DataContext db)
        {
            /*
            db.Messages.AddRange(GetSeedingMessages());
            db.SaveChanges();
            */

            db.courses.AddRange(GetSeedingCourses());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(DataContext db)
        {
            /*
            db.Messages.RemoveRange(db.Messages);
            InitializeDbForTests(db);
            */
            db.courses.RemoveRange(GetSeedingCourses());
            InitializeDbForTests(db);
        }

        public static List<Course> GetSeedingCourses()
        {
            return new List<Course>()
            {
                new Course() { Name = "Java Course", Description = "Course about java", Icon = "Icon about java" },
                new Course() { Name = ".NET Course", Description = "Course about .NET", Icon = "Icon about .NET" }
            };
        }
    }
}