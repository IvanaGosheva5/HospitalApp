using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;
using HospitalApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

public class DoctorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DoctorsController(ApplicationDbContext context)
    {
        _context = context;
    }

 
    public async Task<IActionResult> Index()
    {
        return View(await _context.Doctors.Include(d => d.Department).ToListAsync());
    }

    
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors
    .Include(d => d.Department)
    .Include(d => d.Appointments)
    .FirstOrDefaultAsync(m => m.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    
    public IActionResult Create()
    {
        Console.WriteLine("=== GET CREATE ИЗВИКАН (нова версия на кода) ===");

        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FullName,Specialty,Email,Phone,DepartmentId")] Doctor doctor)
    {
      

        if (ModelState.IsValid)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
            Console.WriteLine("=== ЗАПИСАНО В БАЗАТА УСПЕШНО ===");
            return RedirectToAction(nameof(Index));
        }
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
        return View(doctor);
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
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
        return View(doctor);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,FullName,Specialty,Email,Phone,DepartmentId")] Doctor doctor)
    {
        if (id != doctor.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
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
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
        return View(doctor);
    }

  
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        return View(doctor);
    }

    // POST: Doctors/Delete/
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var doctor = await _context.Doctors
    .Include(d => d.Department)
    .FirstOrDefaultAsync(m => m.Id == id);

        if (doctor != null)
        {
            _context.Doctors.Remove(doctor);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DoctorExists(int? id)
    {
        return _context.Doctors.Any(e => e.Id == id);
    }
}