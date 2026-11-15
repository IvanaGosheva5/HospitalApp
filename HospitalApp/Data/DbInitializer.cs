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
            context.Departments.AddRange(
                new Department { Name = "Кардиология", Description = "Диагностика и лечение на сърдечносъдови заболявания." },
                new Department { Name = "Неврология", Description = "Лечение на заболявания на нервната система." },
                new Department { Name = "Ортопедия", Description = "Лечение на костите и ставите." },
                new Department { Name = "Образна диагностика", Description = "Рентген, ултразвук и ЯМР изследвания." },
                new Department { Name = "Вътрешни болести", Description = "Консултативна дейност." });
            context.SaveChanges();
        }

        if (!context.Doctors.Any())
        {
            var departmentIds = context.Departments.OrderBy(d => d.Id).Select(d => d.Id).ToList();

            context.Doctors.AddRange(
                new Doctor { FullName = "Петър Иванов Петров", Specialty = "Кардиолог", Email = "p.petrov@hospital.bg", Phone = "0888 100 200", DepartmentId = departmentIds[0] },
                new Doctor { FullName = "Елена Димитрова Колева", Specialty = "Невролог", Email = "e.koleva@hospital.bg", Phone = "0888 300 400", DepartmentId = departmentIds[1 % departmentIds.Count] },
                new Doctor { FullName = "Николай Георгиев Стоянов", Specialty = "Ортопед", Email = "n.stoyanov@hospital.bg", Phone = "0888 500 600", DepartmentId = departmentIds[2 % departmentIds.Count] });
            context.SaveChanges();
        }

        if (!context.Appointments.Any())
        {
            var doctorIds = context.Doctors.OrderBy(d => d.Id).Select(d => d.Id).ToList();

            context.Appointments.AddRange(
                new Appointment { PatientName = "Георги Тодоров", PatientPhone = "0899 111 222", Date = DateTime.Today.AddDays(2).AddHours(10), Reason = "Преглед след болничен", DoctorId = doctorIds[0] },
                new Appointment { PatientName = "Мария Атанасова", PatientPhone = "0899 333 444", Date = DateTime.Today.AddDays(3).AddHours(11).AddMinutes(30), Reason = "Консултация", DoctorId = doctorIds[1 % doctorIds.Count] },
                new Appointment { PatientName = "Иван Стоилов", PatientPhone = "0899 555 666", Date = DateTime.Today.AddDays(5).AddHours(14), Reason = "Профилактичен преглед", DoctorId = doctorIds[2 % doctorIds.Count] });
            context.SaveChanges();
        }
    }
}