using ClickServ2022.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClickServ2022.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IRepositoryDAL endereco;

        public EnderecoController(IRepositoryDAL _endereco)
        {
            endereco = _endereco;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: EnderecoController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: EnderecoController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnderecoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EnderecoController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnderecoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EnderecoController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnderecoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
