using EIP_A.Data;
using EIP_A.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace EIP_A.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;
        public ProductsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        public ApplicationDbContext AppDbContext { get; }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await applicationDbContext.ProductDb.ToListAsync();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProduct addProduct)
        {
            var product = new Product()
            {
                ProductName = addProduct.ProductName,
                Quantity = addProduct.Quantity,
                CostPrice = addProduct.CostPrice,
                SellingPrice = addProduct.SellingPrice,
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name
            };

            await applicationDbContext.ProductDb.AddAsync(product);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(int Id)
        {
            var product = await applicationDbContext.ProductDb.FirstOrDefaultAsync(c => c.Id == Id);
            if (product != null)
            {
                var viewModel = new UpdateProduct()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    CostPrice = product.CostPrice,
                    SellingPrice = product.SellingPrice,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = product.ModifiedBy
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateProduct model)
        {
            var product = await applicationDbContext.ProductDb.FindAsync(model.Id);

            if (product != null)
            {
                product.ProductName = model.ProductName;
                product.Quantity = model.Quantity;
                product.CostPrice = model.CostPrice;
                product.SellingPrice = model.SellingPrice;
                product.ModifiedAt = DateTime.Now;
                product.ModifiedBy = User.Identity.Name;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProduct model)
        {
            var product = await applicationDbContext.ProductDb.FindAsync(model.Id);

            if (product != null)
            {
                applicationDbContext.ProductDb.Remove(product);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
