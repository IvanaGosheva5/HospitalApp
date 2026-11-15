using HospitalApp.Data;
using HospitalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Controllers;

public class DepartmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _context.Departments
            .OrderBy(d => d.Name)
            .ToListAsync();

        return View(departments);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await GetWithDoctorsAsync(id.Value);
        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description")] Department department)
    {
        await ValidateNameAsync(department);

        if (!ModelState.IsValid)
        {
            return View(department);
        }

        _context.Add(department);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Department department)
    {
        if (id != department.Id)
        {
            return NotFound();
        }

        await ValidateNameAsync(department);

        if (!ModelState.IsValid)
        {
            return View(department);
        }

        try
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(department.Id))
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

        var department = await GetWithDoctorsAsync(id.Value);
        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var department = await GetWithDoctorsAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        if (department.Doctors.Any())
        {
            return View("Delete", department);
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task ValidateNameAsync(Department department)
    {
        bool nameTaken = await _context.Departments
            .AnyAsync(d => d.Name == department.Name && d.Id != department.Id);

        if (nameTaken)
        {
            ModelState.AddModelError(nameof(Department.Name), "Вече има отделение с това име.");
        }
    }

    private Task<Department?> GetWithDoctorsAsync(int id)
    {
        return _context.Departments
            .Include(d => d.Doctors)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    private bool DepartmentExists(int id)
    {
        return _context.Departments.Any(e => e.Id == id);
    }
}