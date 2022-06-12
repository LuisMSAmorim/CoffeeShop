using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeShop.Domain.Model.Entities;
using CoffeeShop.Domain.Model.Interfaces.Services.Domain;

namespace CoffeeShop.Web.Controllers;

public class CoffeesController : Controller
{
    private readonly ICoffeeService _coffeeService;

    public CoffeesController
    (
        ICoffeeService coffeeService
    )
    {
        _coffeeService = coffeeService;
    }

    // GET: Coffees
    public async Task<IActionResult> Index()
    {
        var coffees = await _coffeeService.GetAllAsync();

        return View(coffees);
    }

    // GET: Coffees/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var coffee = await _coffeeService.GetByIdAsync(id);

        if (coffee == null)
            return NotFound();

        return View(coffee);
    }

    // GET: Coffees/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Coffees/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BrandName,ProductorName,Altitude,Location,ImageUrl,Id")] Coffee coffee)
    {
        if (!ModelState.IsValid)
            return View(coffee);

        await _coffeeService.CreateAsync(coffee);

        return RedirectToAction(nameof(Index));
    }

    // GET: Coffees/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var coffee = await _coffeeService.GetByIdAsync(id);

        if (coffee == null)
            return NotFound();

        return View(coffee);
    }

    // POST: Coffees/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BrandName,ProductorName,Altitude,Location,ImageUrl,Id")] Coffee coffee)
    {
        if (id != coffee.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(coffee);

        try
        {
            await _coffeeService.UpdateAsync(id, coffee);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (! await CoffeeExists(coffee.Id))
                return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Coffees/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var coffee = await _coffeeService.GetByIdAsync(id);

        if (coffee == null)
            return NotFound();

        return View(coffee);
    }

    // POST: Coffees/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var coffee = await _coffeeService.GetByIdAsync(id);

        if (coffee == null)
            return NotFound();

        await _coffeeService.DeleteAsync(coffee);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CoffeeExists(int id)
    {
        if (await _coffeeService.GetByIdAsync(id) != null) return true;

        return false;
    }
}
