using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using OkayAssignment.Data;
using OkayAssignment.Models;
using OkayAssignment.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OkayAssignment.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly ILogger _logger;
        private readonly AppDBContext _context;
        private readonly UserManager<OkayAssignmentUser> _userManager;
        private OkayAssignmentUser _user;


        public SaleController(AppDBContext context, UserManager<OkayAssignmentUser> userManager, ILogger<PropertyController> logger)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (_user == null)
                {
                    _user = await _userManager.GetUserAsync(User);
                }
                IQueryable<TransactionVM> _transactionvm;
                if (_user != null)
                {
                    if (_user.IsAdmin)
                    {
                        _transactionvm = _context.Transaction.Include(x => x.property).Select(y => new TransactionVM { transaction = y, propertyid = y.property.PropertyId });

                    }
                    else
                    {
                        _transactionvm = _context.Transaction.Include(x => x.property).Where(z => z.UserId == _user.UserName).Select(y => new TransactionVM { transaction = y, propertyid = y.property.PropertyId });

                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(await _transactionvm.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var transaction = await _context.Transaction
                    .FirstOrDefaultAsync(m => m.TransactionId == id);
                if (transaction == null)
                {
                    return NotFound();
                }

                if (_user == null)
                {
                    _user = await _userManager.GetUserAsync(User);
                }

                var model = new TransactionVM()
                {
                    transaction = new Transaction()
                    {
                        UserId = transaction.UserId,
                        PropertyId=transaction.PropertyId,
                        property = transaction.property,
                        TransactionDate =transaction.TransactionDate,
                        TransactionId = transaction.TransactionId
                    }
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        }

        public async Task<IActionResult> Create()
        {
            try
            {
                if (_user == null)
                {
                    _user = await _userManager.GetUserAsync(User);
                }

                var model = new TransactionVM()
                {
                    transaction = new Transaction()
                    {
                        UserId = _user.UserName,
                        property = new Property(),
                        TransactionDate = System.DateTime.Now,
                        TransactionId = 0
                    }
                };
                List<Property> listofProperty = new List<Property>();
                listofProperty = (_context.Property.Where(x => x.IsAvaliable == true && ((x.UserId == _user.UserName) || _user.IsAdmin == true)).ToList());

                listofProperty.Insert(0, new Property { PropertyId = 0, PropertyName = "select" });
                ViewBag.ListofavailableProduct = listofProperty;

                ViewBag.TransactionDatetime = System.DateTime.Now.ToString();


                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,PropertyId,UserId,TransactionDate,transaction,property")] TransactionVM _transactionVM)
        {
            try
            {
                if (_user == null)
                {
                    _user = await _userManager.GetUserAsync(User);
                }
                if (_transactionVM.transaction.PropertyId == 0)
                {
                    ModelState.AddModelError("", "Please select Property");
                }

                if (ModelState.IsValid)
                {
                    if (_transactionVM != null)
                    {

                        var _transaction = new Transaction
                        {
                            PropertyId = _transactionVM.transaction.PropertyId,
                            TransactionDate = _transactionVM.transaction.TransactionDate,
                            TransactionId = _transactionVM.transaction.TransactionId,
                            UserId = _transactionVM.transaction.UserId
                        };
                        _context.Add(_transaction);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    List<Property> listofProperty = new List<Property>();
                    listofProperty = (_context.Property.Where(x => x.IsAvaliable == true && ((x.UserId == _user.UserName) || _user.IsAdmin == true)).ToList());

                    listofProperty.Insert(0, new Property { PropertyId = 0, PropertyName = "select" });
                    ViewBag.ListofavailableProduct = listofProperty;
                }
                return View(_transactionVM);
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

                var transaction = await _context.Transaction.FindAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }

                if (_user == null)
                {
                    _user = await _userManager.GetUserAsync(User);
                }

                List<Property> listofProperty = new List<Property>();
                listofProperty = (_context.Property.Where(x => x.IsAvaliable == true && ((x.UserId == _user.UserName) || _user.IsAdmin == true)).ToList());

                listofProperty.Insert(0, new Property { PropertyId = 0, PropertyName = "select" });

                ViewBag.ListofavailableProduct = listofProperty;
                ViewBag.SelectedProperty = transaction.PropertyId;

                var model = new TransactionVM()
                {
                    transaction = new Transaction()
                    {
                        UserId = transaction.UserId,
                        property = new Property() { PropertyId = transaction.PropertyId },
                        PropertyId = transaction.PropertyId,
                        TransactionDate = transaction.TransactionDate,
                        TransactionId = transaction.TransactionId
                    }
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // POST: Sale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,PropertyId,UserId,TransactionDate,transaction,property")] TransactionVM transactionvm)
        {
            try
            {
                if (id != transactionvm.transaction.TransactionId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var tranResult = new Transaction
                        {
                            PropertyId = transactionvm.transaction.PropertyId,
                            TransactionDate = transactionvm.transaction.TransactionDate,
                            TransactionId = transactionvm.transaction.TransactionId,
                            UserId = transactionvm.transaction.UserId
                        };

                        _context.Update(tranResult);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionExists(transactionvm.transaction.TransactionId))
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
                return View(transactionvm.transaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // GET: Sale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var transaction = await _context.Transaction
                    .FirstOrDefaultAsync(m => m.TransactionId == id);
                if (transaction == null)
                {
                    return NotFound();
                }

                var _transactionVM = new TransactionVM()
                {
                    transaction = new Transaction()
                    {
                        UserId = transaction.UserId,
                        property = new Property() { PropertyId = transaction.PropertyId },
                        PropertyId = transaction.PropertyId,
                        TransactionDate = transaction.TransactionDate,
                        TransactionId = transaction.TransactionId
                    }

                };
                return View(_transactionVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // POST: Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var transaction = await _context.Transaction.FindAsync(id);
                _context.Transaction.Remove(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.TransactionId == id);
        }
    }
}
