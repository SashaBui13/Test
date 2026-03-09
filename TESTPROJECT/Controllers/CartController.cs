using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTPROJECT.Data;
using TESTPROJECT.Models;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var items = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return View(new Cart { Items = items });
    }

    public async Task<IActionResult> AddToCart(int productId)
    {
        var userId = _userManager.GetUserId(User);
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            _context.CartItems.Add(new CartItem
            {
                ProductId = productId,
                UserId = userId,
                Quantity = 1
            });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}