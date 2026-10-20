using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace HospitalApp.Models;

public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името на отделението е задължително")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Името трябва да е между 2 и 100 символа")]
    [Display(Name = "Отделение")]
    public string Name { get; set; } = null!;

    [StringLength(500, ErrorMessage = "Описанието не може да е повече от 500 символа")]
    [Display(Name = "Описание")]
    public string? Description { get; set; }

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
