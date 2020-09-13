using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OkayAssignment.Data;
using OkayAssignment.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OkayAssignment.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private readonly ILogger _logger;
        private readonly AppDBContext _context;
        private readonly UserManager<OkayAssignmentUser> _userManager;
        public OkayAssignmentUser _user;

        public PropertyController(AppDBContext context, UserManager<OkayAssignmentUser> userManager, ILogger<PropertyController> logger)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager; 
        }

        // only dispaly user his/her data
        // admin can see all data 
        public async Task<IActionResult> Index()
        {
            try
            {
                if (_user == null)
                {
                    _user = await _userManager.GetUserAsync(User);
                }
                if (_user != null)
                {
                    if (_user.IsAdmin)
                    {
                        return View(await _context.Property.ToListAsync());
                    }
                    else
                    {
                        return View(await _context.Property.Where(x => x.UserId == _user.UserName).ToListAsync());
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Property/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var @property = await _context.Property
                    .FirstOrDefaultAsync(m => m.PropertyId == id);
                if (@property == null)
                { 
                   return NotFound();
                }
                return View(@property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyId,PropertyName,Bedroom,IsAvaliable,SalePrice,LeasePrice")] Property @property)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_user == null)
                    {
                        _user = await _userManager.GetUserAsync(User);
                    }

                    @property.UserId = _user.UserName;

                    _context.Add(@property);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(@property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var @property = await _context.Property.FindAsync(id);
                if (@property == null)
                {
                    return NotFound();
                }
                return View(@property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropertyId,PropertyName,Bedroom,IsAvaliable,SalePrice,LeasePrice,UserId")] Property @property)
        {
            try
            {
                if (id != @property.PropertyId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(@property);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (!PropertyExists(@property.PropertyId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            _logger.LogError(ex.ToString());
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(@property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var @property = await _context.Property
                    .FirstOrDefaultAsync(m => m.PropertyId == id);
                if (@property == null)
                {
                    return NotFound();
                }

                return View(@property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            { 
                var @property = await _context.Property.FindAsync(id);
                _context.Property.Remove(@property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        private bool PropertyExists(int id)
        {
            return _context.Property.Any(e => e.PropertyId == id);
        }
    }
}
