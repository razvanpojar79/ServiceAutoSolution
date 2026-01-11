using Microsoft.AspNetCore.Mvc;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Web.Services;

namespace ServiceAuto.Web.Controllers;

public class ClientsController : Controller
{
    private readonly ApiClient _api;

    public ClientsController(ApiClient api)
    {
        _api = api;
    }

    private static string Ep(string endpoint)
        => endpoint.StartsWith("/") ? endpoint : "/" + endpoint;

    public async Task<IActionResult> Index()
    {
        var clienti = await _api.GetListAsync<ClientDto>(Ep(ApiRoutes.Clienti));
        return View(clienti);
    }

    public IActionResult Create()
    {
        return View(new ClientDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClientDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            var created = await _api.PostAsync<ClientDto>(Ep(ApiRoutes.Clienti), dto);

            if (created == null)
            {
                ModelState.AddModelError("", "Eroare la crearea clientului. Verifică Api:BaseUrl din appsettings.json și că API-ul rulează.");
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Eroare la crearea clientului: {ex.Message}");
            return View(dto);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var client = await _api.GetAsync<ClientDto>(Ep($"{ApiRoutes.Clienti}/{id}"));
        if (client == null) return NotFound();
        return View(client);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ClientDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            var ok = await _api.PutAsync(Ep($"{ApiRoutes.Clienti}/{id}"), dto);

            if (!ok)
            {
                ModelState.AddModelError("", "Eroare la salvarea modificărilor. Verifică Api:BaseUrl și API-ul.");
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Eroare la salvarea modificărilor: {ex.Message}");
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _api.DeleteAsync(Ep($"{ApiRoutes.Clienti}/{id}"));
        }
        catch
        {
            // ignore
        }

        return RedirectToAction(nameof(Index));
    }
}
