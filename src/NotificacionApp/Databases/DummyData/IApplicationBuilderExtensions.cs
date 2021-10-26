using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NotificacionApp.Common;
using NotificacionApp.Domain;
using NotificacionApp.Repositories;
using System;

namespace NotificacionApp.Databases.DummyData
{
    public static class IApplicationBuilderExtensions
    {


        public static void UseDummyData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;
                DropAndCreateDatabase(provider);
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;
                AddDummyTeacherData(provider);
                AddDummyStudentData(provider);
            }
        }

        private static void DropAndCreateDatabase(IServiceProvider provider)
        {
            using var context = provider.GetRequiredService<NotificacionDbContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static void AddDummyTeacherData(IServiceProvider provider)
        {
            var repository = provider.GetRequiredService<ITeacherRepository>();

            var teacher1 = new Teacher("Teacher 1", "Teacher01");
            repository.SaveAsync(teacher1).Wait();

            var teacher2 = new Teacher("Teacher 2", "Teacher02");
            repository.SaveAsync(teacher2).Wait();

            var teacher3 = new Teacher("Teacher 3", "Teacher03");
            repository.SaveAsync(teacher3).Wait();

            var teachers = repository.FindAsync(null).GetAwaiter().GetResult();
            foreach (var teacher in teachers)
            {
                repository.SetPasswordAsync(teacher.Id, "Teacher").Wait();
            }
        }

        private static void AddDummyStudentData(IServiceProvider provider)
        {
            var repository = provider.GetRequiredService<IStudentRepository>();

            var student1 = new Student("Student 1", "Student01");
            repository.SaveAsync(student1).Wait();

            var student2 = new Student("Student 2", "Student02");
            repository.SaveAsync(student2).Wait();

            var student3 = new Student("Student 3", "Student03");
            repository.SaveAsync(student3).Wait();

            var students = repository.FindAsync(null).GetAwaiter().GetResult();
            foreach (var student in students)
            {
                repository.SetPasswordAsync(student.Id, "Student").Wait();
            }
        }


    }
}
