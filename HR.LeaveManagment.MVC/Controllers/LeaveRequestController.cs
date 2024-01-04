using AutoMapper;
using HR.LeaveManagment.MVC.Contracts;
using HR.LeaveManagment.MVC.Models;
using HR.LeaveManagment.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Win32;

namespace HR.LeaveManagment.MVC.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IMapper _mapper;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, ILeaveTypeService leaveTypeService, IMapper mapper)
        {
            _leaveRequestService = leaveRequestService;
            _leaveTypeService = leaveTypeService;
            _mapper = mapper;
        }

        // GET: LeaveRequestController
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var model = await _leaveRequestService.GetAdminLeaveRequestList();
            return View(model);
        }

        // GET: LeaveRequestController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
            return View(model);
        }

        // GET: LeaveRequestController/Create
        public async Task<ActionResult> Create()
        {
            var leaveTypes = await _leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            var model = new CreateLeaveRequestVM { LeaveTypes = leaveTypeItems };
            return View(model);
        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM leaveRequest)
        {
            var leaveTypes = await _leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            leaveRequest.LeaveTypes = leaveTypeItems;
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestService.CreateLeaveRequest(leaveRequest);
                if (response.Sucess)
                {
                    return RedirectToAction(nameof(Details),response.Data);
                }
                ModelState.AddModelError("", response.ValidationErros);
            }
             leaveTypes = await _leaveTypeService.GetLeaveTypes();
             leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
             leaveRequest.LeaveTypes = leaveTypeItems;
             return View(leaveRequest);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await _leaveRequestService.ApproveLeaveRequest(id, approved);
                return Redirect(nameof(Index));
            }
            catch (Exception ex)
            {
                return Redirect(nameof(Index));
            }

            // GET: LeaveRequestController/Edit/5
            //public ActionResult Edit(int id)
            //{
            //    return View();
            //}

            //// POST: LeaveRequestController/Edit/5
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Edit(int id, IFormCollection collection)
            //{
            //    try
            //    {
            //        return RedirectToAction(nameof(Index));
            //    }
            //    catch
            //    {
            //        return View();
            //    }
            //}

            //// GET: LeaveRequestController/Delete/5
            //public ActionResult Delete(int id)
            //{
            //    return View();
            //}

            //// POST: LeaveRequestController/Delete/5
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Delete(int id, IFormCollection collection)
            //{
            //    try
            //    {
            //        return RedirectToAction(nameof(Index));
            //    }
            //    catch
            //    {
            //        return View();
            //    }
            //}
        }
    }
}
