using DOTNETCORE.RestApi.Db;
using DOTNETCORE.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace DOTNETCORE.RestApi.Controllers
{
    //https://localhost:3000/api/blog
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _dbContext = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _dbContext.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blogDataModel)
        {
            _dbContext.Blogs.Add(blogDataModel);
            var result = _dbContext.SaveChanges();
            string message = result > 0 ? "Creating Successfull.." : "Creating Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog Titlte is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog Author is required");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog Content is required");
            }
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successfull.." : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id , BlogDataModel blog)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("No Data Found");
            }
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }

            var result = _dbContext.SaveChanges();
            string message = result > 0 ? "Updating Successfull.." : "Updating Failed";
            return Ok(message);
            
        }

        [HttpDelete]
        public IActionResult DeleteBlog()
        {
            return Ok("delete");
        }
    }
}
