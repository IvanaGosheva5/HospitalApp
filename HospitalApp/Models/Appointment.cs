using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на пациента е задължително")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Пациент")]
    public string PatientName { get; set; } = null!;

    [Required(ErrorMessage = "Телефонът на пациента е задължителен")]
    [Phone(ErrorMessage = "Невалиден телефонен номер")]
    [Display(Name = "Телефон на пациента")]
    public string PatientPhone { get; set; } = null!;

    [Required(ErrorMessage = "Датата и часът са задължителни")]
    [Display(Name = "Дата и час")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Причината за посещението е задължителна")]
    [StringLength(300)]
    [Display(Name = "Причина за посещението")]
    public string Reason { get; set; } = null!;

    [Display(Name = "Лекар")]
    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;
}
