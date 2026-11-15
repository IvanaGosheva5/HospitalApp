using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Controllers;

public class DoctorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DoctorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var doctors = await _context.Doctors
            .Include(d => d.Department)
            .OrderBy(d => d.FullName)
            .ToListAsync();

        return View(doctors);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await GetFullDoctorAsync(id.Value);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    public IActionResult Create()
    {
        LoadDepartments();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FullName,Specialty,Email,Phone,DepartmentId")] Doctor doctor)
    {
        await ValidateDoctorAsync(doctor);

        if (!ModelState.IsValid)
        {
            LoadDepartments();
            return View(doctor);
        }

        _context.Add(doctor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }

        LoadDepartments();
        return View(doctor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Specialty,Email,Phone,DepartmentId")] Doctor doctor)
    {
        if (id != doctor.Id)
        {
            return NotFound();
        }

        await ValidateDoctorAsync(doctor);

        if (!ModelState.IsValid)
        {
            LoadDepartments();
            return View(doctor);
        }

        try
        {
            _context.Update(doctor);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DoctorExists(doctor.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await GetFullDoctorAsync(id.Value);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var doctor = await GetFullDoctorAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }

        if (doctor.Appointments.Any())
        {
            return View("Delete", doctor);
        }

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private void LoadDepartments()
    {
        ViewData["DepartmentId"] = new SelectList(_context.Departments.OrderBy(d => d.Name), "Id", "Name");
    }

    private async Task ValidateDoctorAsync(Doctor doctor)
    {
        bool departmentExists = await _context.Departments.AnyAsync(d => d.Id == doctor.DepartmentId);
        if (!departmentExists)
        {
            ModelState.AddModelError(nameof(Doctor.DepartmentId), "Изберете съществуващо отделение.");
        }

        bool emailTaken = await _context.Doctors.AnyAsync(d => d.Email == doctor.Email && d.Id != doctor.Id);
        if (emailTaken)
        {
            ModelState.AddModelError(nameof(Doctor.Email), "Вече има лекар с този имейл.");
        }
    }

    private Task<Doctor?> GetFullDoctorAsync(int id)
    {
        return _context.Doctors
            .Include(d => d.Department)
            .Include(d => d.Appointments)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    private bool DoctorExists(int id)
    {
        return _context.Doctors.Any(e => e.Id == id);
    }
}