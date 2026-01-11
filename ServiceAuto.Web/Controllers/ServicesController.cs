using Microsoft.AspNetCore.Mvc;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Web.Services;

namespace ServiceAuto.Web.Controllers;

public class ServicesController : Controller
{
    private readonly ApiClient _api;

    public ServicesController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var servicii = await _api.GetListAsync<ServiciuDto>(ApiRoutes.Servicii);
        return View(servicii);
    }

    public IActionResult Create()
    {
        return View(new ServiciuDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiciuDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var created = await _api.PostAsync<ServiciuDto>(ApiRoutes.Servicii, dto);
        if (created == null)
        {
            ModelState.AddModelError("", "Eroare la crearea serviciului.");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var serviciu = await _api.GetAsync<ServiciuDto>($"{ApiRoutes.Servicii}/{id}");
        if (serviciu == null) return NotFound();
        return View(serviciu);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ServiciuDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var ok = await _api.PutAsync($"{ApiRoutes.Servicii}/{id}", dto);
        if (!ok)
        {
            ModelState.AddModelError("", "Eroare la salvarea modificărilor.");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteAsync($"{ApiRoutes.Servicii}/{id}");
        return RedirectToAction(nameof(Index));
    }
}
