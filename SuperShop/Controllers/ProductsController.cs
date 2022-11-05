using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Ententies;
using SuperShop.Helpers;
using SuperShop.Models;

namespace SuperShop.Controllers
{
    //[Authorize]
    public class ProductsController : Controller
    {
        
        private readonly IProductsRepository _productsRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        //private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public ProductsController(IProductsRepository productsRepository, IUserHelper userHelper, IBlobHelper blobHelper, IConverterHelper converterHelper)
        {
            
            _productsRepository = productsRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            //_imageHelper = imageHelper;
           _converterHelper = converterHelper;
        }

        
        // GET: Products
        public IActionResult Index()
        {
            return View(_productsRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _productsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")]*/ ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {


                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");

                }


                var product = _converterHelper.toProduct(model, imageId, true);
            
        

                //TODO: Modificar para o que tiver logado
                product.User = await _userHelper.GetUserbyEmailAsync(this.User.Identity.Name);
                await _productsRepository.CreateAsync(product);
                //No generic repository grava automaticamente
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _productsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }
            var model = _converterHelper.toProductViewModel(product);
            return View(model);
        }
        
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvailable,Stock")]*/ProductViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = Guid.Empty;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {


                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");

                    }


                    var product = _converterHelper.toProduct(model, imageId, false);

                    
                    product.User = await _userHelper.GetUserbyEmailAsync(this.User.Identity.Name);
                    await _productsRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _productsRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("ProductNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _productsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productsRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _productsRepository.DeleteAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if(ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{product.Name} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{product.Name} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                       $"Exprimente primeiro apagar todas as encomendas que o estão a usar," +
                       $"e torne novamente a apagá-lo";
                }


                

                return View("Error");
                
            }
            
            
            
        }

        public IActionResult ProductNotFound()
        {
            return View();
        }

    }
}
