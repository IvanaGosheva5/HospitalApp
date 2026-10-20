using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models;

public class Doctor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на лекаря е задължително")]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Три имена")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Специалността е задължителна")]
    [StringLength(100)]
    [Display(Name = "Специалност")]
    public string Specialty { get; set; } = null!;

    [Required(ErrorMessage = "Имейлът е задължителен")]
    [EmailAddress(ErrorMessage = "Невалиден имейл")]
    [Display(Name = "Имейл")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Телефонът е задължителен")]
    [Phone(ErrorMessage = "Невалиден телефонен номер")]
    [Display(Name = "Телефон")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Отделение")]
    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
