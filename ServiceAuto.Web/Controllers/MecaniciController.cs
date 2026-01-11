using Microsoft.AspNetCore.Mvc;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Web.Services;

namespace ServiceAuto.Web.Controllers;

public class MecaniciController : Controller
{
    private readonly ApiClient _api;

    public MecaniciController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var list = await _api.GetListAsync<MecanicDto>(ApiRoutes.Mecanici);
        return View(list);
    }

    public IActionResult Create() => View(new MecanicDto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MecanicDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var created = await _api.PostAsync<MecanicDto>(ApiRoutes.Mecanici, dto);
        if (created == null)
        {
            ModelState.AddModelError("", "Eroare la crearea mecanicului.");
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _api.GetAsync<MecanicDto>($"{ApiRoutes.Mecanici}/{id}");
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MecanicDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var ok = await _api.PutAsync($"{ApiRoutes.Mecanici}/{id}", dto);
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
        await _api.DeleteAsync($"{ApiRoutes.Mecanici}/{id}");
        return RedirectToAction(nameof(Index));
    }
}
