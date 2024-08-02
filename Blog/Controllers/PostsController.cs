using BlogApp.Data;
using BlogApp.Entities;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly BlogContext _context;

    public PostsController(BlogContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var posts = _context.Posts.Include(p => p.Author).ToList();
        return Ok(posts);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] PostEntity model)
    {
        var userId = int.Parse(User.Identity.Name);
        var post = new Post
        {
            Title = model.Title,
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
            AuthorId = userId
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = post.PostId }, post);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var post = _context.Posts.Include(p => p.Author).SingleOrDefault(p => p.PostId == id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] PostEntity model)
    {
        var post = _context.Posts.SingleOrDefault(p => p.PostId == id);
        if (post == null)
        {
            return NotFound();
        }

        if (post.AuthorId != int.Parse(User.Identity.Name))
        {
            return Forbid();
        }

        post.Title = model.Title;
        post.Content = model.Content;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var post = _context.Posts.SingleOrDefault(p => p.PostId == id);
        if (post == null)
        {
            return NotFound();
        }

        if (post.AuthorId != int.Parse(User.Identity.Name))
        {
            return Forbid();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}