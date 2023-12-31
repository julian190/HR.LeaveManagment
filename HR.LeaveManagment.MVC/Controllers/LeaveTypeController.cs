using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services;
using HR.LeaveManagment.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HR.LeaveManagment.MVC.Controllers
{
    [Authorize(Roles = "Administrator")]

    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveAllocationService _leaveAllocationService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService, ILeaveAllocationService leaveAllocationService)
        {
            _leaveTypeService = leaveTypeService;
            _leaveAllocationService = leaveAllocationService;

        }
        // GET: LeaveTypeController
        public async Task<ActionResult> Index()
        {
            var model = await _leaveTypeService.GetLeaveTypes();
            return View(model);
        }

        // GET: LeaveTypeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _leaveTypeService.GetLeaveTypeDetails(id);
            return View(model);
        }

        // GET: LeaveTypeController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = await _leaveTypeService.CreateLeaveType(leaveType);
                if(response.Sucess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErros);
                
            }
            catch (Exception ex )
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        // GET: LeaveTypeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _leaveTypeService.GetLeaveTypeDetails(id);
            return View(model);
        }

        // POST: LeaveTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LeaveTypeVM LeaveType)
        {
            try
            {
                var response = await _leaveTypeService.UpdateLeaveType(id, LeaveType);
                if( response.Sucess)
                {
                    return RedirectToAction(nameof(Index));

                }
                ModelState.AddModelError("", response.ValidationErros);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            return View(LeaveType);
        }

        // GET: LeaveTypeController/Delete/5
       

        // POST: LeaveTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _leaveTypeService.DeleteLeaveType(id);
                if (response.Sucess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErros);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Allocate(int id)
        {
            try
            {
                var response = await _leaveAllocationService.CreateLeaveAllocations(id);
                if (response.Sucess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return BadRequest();
        }
    }
}
