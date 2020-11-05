using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // Sincrona
        //public IActionResult Index()
        //{
        //    var list = _sellerService.FindAll();
        //    return View(list);
        //}
        // Assincrona
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        // Sincrona
        //public IActionResult Create()
        //{
        //    var departments = _departmentService.FindAll();
        //    var viewModel = new SellerFormViewModel { Departments = departments };
        //    return View(viewModel);
        //}
        // Assincrona
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Sincrona
        //public IActionResult Create(Seller seller)
        //{
        //    if ( !ModelState.IsValid )
        //    {
        //        var departments = _departmentService.FindAll();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }
        //    _sellerService.Insert(seller);
        //    return RedirectToAction(nameof(Index));
        //}
        // Assincrona
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // Sincrona
        //public IActionResult Delete(int? id)
        //{
        //  if ( id == null )
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }
        //    var obj = _sellerService.FindById(id.Value);
        //    if ( obj == null )
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }
        //
        //    return View(obj);
        //}
        // Assincrona
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Sincrona
        //public IActionResult Delete(int id)
        //{
        //  try
        //  {
        //    _sellerService.Remove(id);
        //    return RedirectToAction(nameof(Index));
        //  }
        //  catch (IntegrityException e )
        //  {
        //    return RedirectToAction(nameof(Error), new { message = e.Message});
        //  }
        //}
        // Assincrona
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch ( IntegrityException e )
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // Sincrona
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }
        //    var obj = _sellerService.FindById(id.Value);
        //    if (obj == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }
        //    return View(obj);
        //}
        // Assincrona
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }


        // Sincrona
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        //    }
        //    var obj = _sellerService.FindById(id.Value);
        //    if ( obj == null)
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id not found" });
        //    }
        //    List<Department> departments = _departmentService.FindAll();
        //    SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
        //    return View(viewModel);
        //}
        // Assincrona
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Sincrona
        //public IActionResult Edit(int id, Seller seller)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var departments = _departmentService.FindAll();
        //        var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
        //        return View(viewModel);
        //    }
        //  if ( id != seller.Id )
        //    {
        //        return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
        //    }
        //    try
        //    {
        //        _sellerService.Update(seller);
        //        return RedirectToAction(nameof(Index));
        //    } 
        //    catch ( ApplicationException e )
        //    {
        //        return RedirectToAction(nameof(Error), new { message = e.Message });
        //    } 
        //}
        // Assincrona
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
