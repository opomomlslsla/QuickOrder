using Microsoft.AspNetCore.Mvc;
using QuickOrder.Domain.Interfaces;
using QuickOrder.Domain.Models;
using QuickOrder.Services;
using System.Diagnostics;

namespace QuickOrder.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _service;
        public HomeController(IOrderService orderService)
        {
            _service = orderService;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> Orders = (List<Order>) await _service.GetAllOrdersasync();
            return View(Orders);
        }

        public IActionResult AddOrderForm()
        {
            return View();
        }
      
        public async Task<IActionResult> OrderView(Guid id)
        {
            var order1 = await _service.GetOrderByIdAsync(id);
            return View(order1);
            
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            _service.AddOrderAsync(order);
            
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
