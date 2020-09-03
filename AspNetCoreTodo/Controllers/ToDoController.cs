using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Controllers
{
    /// <summary>
    /// getting todo items from the database
    /// </summary>
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

          public TodoController(ITodoItemService todoItemService)
         {
             _todoItemService = todoItemService;
         }
        public async Task<IActionResult> Index()
        {
            var items = await _todoItemService.GetIncompleteItemsAsync();

            var model = new TodoViewModel()
            {
                Items = items
            };

         return View(model);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
        if (id == Guid.Empty)
         {
         return RedirectToAction("Index");
         }
           var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
           {
             return BadRequest("Could not mark item as done.");
           }

         return RedirectToAction("Index");
        }
    }
}