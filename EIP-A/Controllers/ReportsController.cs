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
    public class ReportsController : Controller
    {

        private readonly ApplicationDbContext applicationDbContext;
        public ReportsController(ApplicationDbContext applicationDbContext)
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
        [HttpPost]
        public async Task<ActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            var reports = await applicationDbContext.SellDb
                 .Where(s => startDate != null && endDate != null ? s.CreatedAt >= startDate && s.CreatedAt <= endDate.Value.Date.AddDays(1) : true)
                 .ToListAsync();

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View(reports);
        }
    }
}
