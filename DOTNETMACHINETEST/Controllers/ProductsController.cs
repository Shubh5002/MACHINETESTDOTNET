using DOTNETMACHINETEST.Data;
using DOTNETMACHINETEST.Models;
using DOTNETMACHINETEST.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOTNETMACHINETEST.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public ProductsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var products = await mvcDemoDbContext.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddProductViewModel addProductRequest)
        {
            var product = new Product()
            {
                ProductId = Guid.NewGuid(),
                ProductName = addProductRequest.ProductName,
                CategoryId = addProductRequest.CategoryId,
                CategoryName = addProductRequest.CategoryName
            };

            await mvcDemoDbContext.Products.AddAsync(product);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {

            var product = await mvcDemoDbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if(product != null)
            {
                var viewModel = new UpdateProductViewModel()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    CategoryName = product.CategoryName
                };
                return await Task.Run(() => View("View",viewModel));
            }

           
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateProductViewModel model)
        {
            var product = await mvcDemoDbContext.Products.FindAsync(model.ProductId);

            if(product != null)
            {
                product.ProductName = model.ProductName;
                product.CategoryId = model.CategoryId;
                product.CategoryName = model.CategoryName;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductViewModel model)
        {
            var product = await mvcDemoDbContext.Products.FindAsync(model.ProductId);
            if(product != null)
            {
                mvcDemoDbContext.Products.Remove(product);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
