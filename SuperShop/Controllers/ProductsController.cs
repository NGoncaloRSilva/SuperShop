using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data;
using SuperShop.Data.Ententies;
using SuperShop.Helpers;
using SuperShop.Models;

namespace SuperShop.Controllers
{
    public class ProductsController : Controller
    {
        
        private readonly IProductsRepository _productsRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public ProductsController(IProductsRepository productsRepository, IUserHelper userHelper, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            
            _productsRepository = productsRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
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
                return NotFound();
            }

            var product = await _productsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
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
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {

                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "products");

                }


                var product = _converterHelper.toProduct(model, path, true);
            
        

                //TODO: Modificar para o que tiver logado
                product.User = await _userHelper.GetUserbyEmailAsync("ngoncalorsilva@gmail.com");
                await _productsRepository.CreateAsync(product);
                //No generic repository grava automaticamente
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

       

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
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
                    var path = model.ImageUrl;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "products");

                    }


                    var product = _converterHelper.toProduct(model, path,false);

                    //TODO: Modificar para o que tiver logado
                    product.User = await _userHelper.GetUserbyEmailAsync("ngoncalorsilva@gmail.com");
                    await _productsRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _productsRepository.ExistAsync(model.Id))
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
            return View(model);
        }


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productsRepository.GetByIdAsync(id);
            await _productsRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
