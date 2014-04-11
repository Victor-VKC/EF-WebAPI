using System.Collections.Generic;
using System.Web.Http;
using Data.Models;
using Server.Services;

namespace Server.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly CategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        public IEnumerable<Category> GetCategories(int? parentCategoryId)
        {
            return _categoryService.GetCategories(parentCategoryId);
        }
    }
}
