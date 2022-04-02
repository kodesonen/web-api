using api.Models;
using System.Collections.Generic;
using System;

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

        public static List<Module> GetSeedingModules()
        {
            return new List<Module>()
            {
                new Module() 
                {
                    CourseId = 1, 
                    Chapter = 1, 
                    SubChapter = 1, 
                    Author = "Magnus Bredeli", 
                    Published = DateTimeOffset.Now, 
                    Updated = DateTimeOffset.Now,
                    LastUpdatedBy = "MAggiebbbb",
                    ModuleName = "Module about Java Basics",
                    Html = "Htmllll"
                },
                new Module() {
                    CourseId = 2, 
                    Chapter = 1, 
                    SubChapter = 1, 
                    Author = "Magnus Bredeli", 
                    Published = DateTimeOffset.Now, 
                    Updated = DateTimeOffset.Now,
                    LastUpdatedBy = "MAggiebbbb",
                    ModuleName = "Module about .NET Basics",
                    Html = "Htmllll"
                }
            };
        }

        public static List<Challenge> GetSeedingChallenges()
        {
            return new List<Challenge>()
            {
                new Challenge()
            };
        }
    }
}