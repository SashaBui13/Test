using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
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
        var cart = await GetCartViewModel();
        return View(cart);
    }

    public async Task<IActionResult> Checkout()
    {
        var cart = await GetCartViewModel();
        if (!cart.Items.Any()) return RedirectToAction("Index");
        return View(new Order());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmOrder(Order model)
    {
        var userId = _userManager.GetUserId(User);
        var cartItems = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        if (!cartItems.Any()) return RedirectToAction("Index");

        if (!ModelState.IsValid)
        {
            return View("Checkout", model);
        }

        model.UserId = userId;
        model.OrderDate = DateTime.Now;
        model.Status = "Pending";
        model.TotalAmount = cartItems.Sum(i => i.Product.Price * i.Quantity);

        foreach (var item in cartItems)
        {
            model.OrderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Product.Price
            });
        }

        _context.Orders.Add(model);
        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();

        var jsonParams = new
        {
            public_key = "sandbox_i83332356563",
            version = 3,
            action = "pay",
            amount = model.TotalAmount,
            currency = "UAH",
            description = $"Замовлення №{model.Id} для {model.FirstName} {model.LastName}",
            order_id = model.Id.ToString(),
            result_url = "https://localhost:7165/Cart/Success"
        };

        string data = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(jsonParams)));
        string signature = CreateSignature(data, "sandbox_pv83332356563_test");

        ViewBag.Data = data;
        ViewBag.Signature = signature;
        ViewBag.OrderId = model.Id;
        ViewBag.Amount = model.TotalAmount;

        return View("Pay");
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

    private async Task<Cart> GetCartViewModel()
    {
        var userId = _userManager.GetUserId(User);
        var items = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return new Cart { Items = items };
    }

    private string CreateSignature(string data, string privateKey)
    {
        var str = privateKey + data + privateKey;
        using (var sha1 = SHA1.Create())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(hash);
        }
    }
}