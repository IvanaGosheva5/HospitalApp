using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;
using HospitalApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AppointmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

   
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Appointments.Include(a => a.Doctor)
            .OrderBy(a => a.Date);
        return View(await applicationDbContext.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

   
    public IActionResult Create()
    {
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
        return View();
    }

  
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,PatientName,PatientPhone,Date,Reason,DoctorId")] Appointment appointment)
    {
        if (appointment.Date <= DateTime.Now)
        {
            ModelState.AddModelError("Date", "Датата и часът трябва да са в бъдеще!");
        }

        if (ModelState.IsValid)
        {
            _context.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
        return View(appointment);
    }

   
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
        return View(appointment);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,PatientName,PatientPhone,Date,Reason,DoctorId")] Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return NotFound();
        }

        if (appointment.Date <= DateTime.Now)
        {
            ModelState.AddModelError("Date", "Датата и часът трябва да са в бъдеще!");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(appointment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(appointment.Id))
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
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName");
        return View(appointment);
    }

    
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

  
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AppointmentExists(int? id)
    {
        return _context.Appointments.Any(e => e.Id == id);
    }
}