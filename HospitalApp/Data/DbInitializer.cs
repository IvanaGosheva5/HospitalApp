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

        if (!context.Appointments.Any())
        {
            context.Appointments.AddRange(
                new Appointment { PatientName = "Георги Тодоров", PatientPhone = "0899 111 222", Date = DateTime.Now.AddDays(2).AddHours(10), Reason = "Преглед след болничен", DoctorId = 1 },
                new Appointment { PatientName = "Мария Атанасова", PatientPhone = "0899 333 444", Date = DateTime.Now.AddDays(3).AddHours(11).AddMinutes(30), Reason = "Консултация", DoctorId = 2 },
                new Appointment { PatientName = "Иван Стоилов", PatientPhone = "0899 555 666", Date = DateTime.Now.AddDays(5).AddHours(14), Reason = "Профилактичен преглед", DoctorId = 3 }
            );
            context.SaveChanges();
        }
    }
}