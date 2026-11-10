using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;


namespace HospitalApp.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.Migrate();

        if (!context.Departments.Any())
        {
            var departments = new List<Department>
            {
                new Department { Name = "Кардиология", Description = "Диагностика и лечение на сърдечносъдови заболявания." },
                new Department { Name = "Неврология", Description = "Лечение на заболявания на нервната система." },
                new Department { Name = "Ортопедия", Description = "Лечение на костите и ставите." },
                new Department { Name = "Образна диагностика", Description = "Рентген, ултразвук и ЯМР изследвания." },
                new Department { Name = "Вътрешни болести", Description = "Консултативна дейност." }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();
        }

        if (!context.Doctors.Any())
        {
            context.Doctors.AddRange(
                new Doctor { FullName = "д-р Мария Петрова", Specialty = "Кардиолог", Email = "m.petrova@hospital.bg", Phone = "0888 123 456", DepartmentId = 1 },
                new Doctor { FullName = "д-р Елена Стоянова", Specialty = "Невролог", Email = "e.stoyanova@hospital.bg", Phone = "0888 345 678", DepartmentId = 2 },
                new Doctor { FullName = "д-р Георги Иванов", Specialty = "Ортопед", Email = "g.ivanov@hospital.bg", Phone = "0888 234 567", DepartmentId = 3 },
                new Doctor { FullName = "д-р Иван Димитров", Specialty = "Изображителна диагностика", Email = "i.dimitrov@hospital.bg", Phone = "0888 456 789", DepartmentId = 4 }
            );
            context.SaveChanges();
        }
    }
}