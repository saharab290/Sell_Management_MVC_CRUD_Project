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
    public class SellsController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;
        public SellsController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        public ApplicationDbContext AppDbContext { get; }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sells = await applicationDbContext.SellDb.ToListAsync();
            return View(sells);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSell addSell)
        {
            var sell = new Sell()
            {
                ProductName = addSell.ProductName,
                ClinetId = User.Identity.Name,
                Quantity = addSell.Quantity,
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name
            };

            await applicationDbContext.SellDb.AddAsync(sell);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(int Id)
        {
            var sell = await applicationDbContext.SellDb.FirstOrDefaultAsync(c => c.Id == Id);
            if (sell != null)
            {
                var viewModel = new UpdateSell()
                {
                    Id = sell.Id,
                    ProductName = sell.ProductName,
                    Quantity = sell.Quantity,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = sell.ModifiedBy
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateSell model)
        {
            var sell = await applicationDbContext.SellDb.FindAsync(model.Id);

            if (sell != null)
            {
                sell.ProductName = model.ProductName;
                sell.Quantity = model.Quantity;
                sell.ModifiedAt = DateTime.Now;
                sell.ModifiedBy = User.Identity.Name;

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(UpdateSell model)
        {
            var sell = await applicationDbContext.SellDb.FindAsync(model.Id);

            if (sell != null)
            {
                var deletedSell = new DeletedSell
                {
                    ProductName = sell.ProductName,
                    Quantity = sell.Quantity,
                    CreatedAt = sell.CreatedAt,
                    CreatedBy = sell.CreatedBy,
                    ModifiedAt = sell.ModifiedAt,
                    ModifiedBy = sell.ModifiedBy
                };

                applicationDbContext.DSellDb.Add(deletedSell);
                await applicationDbContext.SaveChangesAsync();

                applicationDbContext.SellDb.Remove(sell);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index1()
        {
            var sells = await applicationDbContext.DSellDb.ToListAsync();
            return View(sells);
        }

    }
}
