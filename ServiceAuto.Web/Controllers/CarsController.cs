using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Web.Services;

namespace ServiceAuto.Web.Controllers;

public class CarsController : Controller
{
    private readonly ApiClient _api;

    public CarsController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var masini = await _api.GetListAsync<MasinaDto>(ApiRoutes.Masini);
        return View(masini);
    }

    public async Task<IActionResult> Create()
    {
        await LoadClientiSelectList();
        return View(new MasinaDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MasinaDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadClientiSelectList(dto.ClientId);
            return View(dto);
        }

        var created = await _api.PostAsync<MasinaDto>(ApiRoutes.Masini, dto);
        if (created == null)
        {
            ModelState.AddModelError("", "Eroare la crearea mașinii.");
            await LoadClientiSelectList(dto.ClientId);
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var masina = await _api.GetAsync<MasinaDto>($"{ApiRoutes.Masini}/{id}");
        if (masina == null) return NotFound();

        await LoadClientiSelectList(masina.ClientId);
        return View(masina);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, MasinaDto dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadClientiSelectList(dto.ClientId);
            return View(dto);
        }

        var ok = await _api.PutAsync($"{ApiRoutes.Masini}/{id}", dto);
        if (!ok)
        {
            ModelState.AddModelError("", "Eroare la salvarea modificărilor.");
            await LoadClientiSelectList(dto.ClientId);
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteAsync($"{ApiRoutes.Masini}/{id}");
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadClientiSelectList(int? selectedId = null)
    {
        var clienti = await _api.GetListAsync<ClientDto>(ApiRoutes.Clienti);
        ViewBag.Clienti = new SelectList(clienti, "Id", "Nume", selectedId);
    }
}
