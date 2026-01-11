using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceAuto.Shared;
using ServiceAuto.Shared.AppDtos;
using ServiceAuto.Web.Services;

namespace ServiceAuto.Web.Controllers;

public class ProgramariController : Controller
{
    private readonly ApiClient _api;

    public ProgramariController(ApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var list = await _api.GetListAsync<ProgramareDto>(ApiRoutes.Programari);
        return View(list);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateSelectLists();
        return View(new ProgramareDto { DataOra = DateTime.Now });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProgramareDto dto)
    {
        if (!ModelState.IsValid)
        {
            await PopulateSelectLists(dto.MasinaId, dto.ServiciuId, dto.MecanicId, dto.Status);
            return View(dto);
        }

        dto.NumeClient ??= "";
        dto.MasinaInfo ??= "";
        dto.DenumireServiciu ??= "";

        var created = await _api.PostAsync<ProgramareDto>(ApiRoutes.Programari, dto);
        if (created == null)
        {
            ModelState.AddModelError("", "Eroare la crearea programării.");
            await PopulateSelectLists(dto.MasinaId, dto.ServiciuId, dto.MecanicId, dto.Status);
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _api.GetAsync<ProgramareDto>($"{ApiRoutes.Programari}/{id}");
        if (item == null) return NotFound();

        await PopulateSelectLists(item.MasinaId, item.ServiciuId, item.MecanicId, item.Status);
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProgramareDto dto)
    {
        if (!ModelState.IsValid)
        {
            await PopulateSelectLists(dto.MasinaId, dto.ServiciuId, dto.MecanicId, dto.Status);
            return View(dto);
        }

        dto.NumeClient ??= "";
        dto.MasinaInfo ??= "";
        dto.DenumireServiciu ??= "";

        var ok = await _api.PutAsync($"{ApiRoutes.Programari}/{id}", dto);
        if (!ok)
        {
            ModelState.AddModelError("", "Eroare la salvarea modificărilor.");
            await PopulateSelectLists(dto.MasinaId, dto.ServiciuId, dto.MecanicId, dto.Status);
            return View(dto);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _api.DeleteAsync($"{ApiRoutes.Programari}/{id}");
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectLists(
        int? selectedMasinaId = null,
        int? selectedServiciuId = null,
        int? selectedMecanicId = null,
        StatusProgramare? selectedStatus = null)
    {
        var masini = await _api.GetListAsync<MasinaDto>(ApiRoutes.Masini);
        var servicii = await _api.GetListAsync<ServiciuDto>(ApiRoutes.Servicii);
        var mecanici = await _api.GetListAsync<MecanicDto>(ApiRoutes.Mecanici);

        ViewBag.Masini = masini
            .Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"{m.Marca} {m.Model} ({m.NrInmatriculare})",
                Selected = selectedMasinaId.HasValue && m.Id == selectedMasinaId.Value
            })
            .ToList();

        ViewBag.Servicii = servicii
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Denumire,
                Selected = selectedServiciuId.HasValue && s.Id == selectedServiciuId.Value
            })
            .ToList();

        var mecaniciSelect = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "",
                Text = "— Fără mecanic —",
                Selected = !selectedMecanicId.HasValue
            }
        };

        mecaniciSelect.AddRange(
            mecanici.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = $"{m.Nume} ({m.Specializare})",
                Selected = selectedMecanicId.HasValue && m.Id == selectedMecanicId.Value
            })
        );

        ViewBag.Mecanici = mecaniciSelect;

        var statuses = Enum.GetValues(typeof(StatusProgramare))
            .Cast<StatusProgramare>()
            .Select(st => new SelectListItem
            {
                Value = ((int)st).ToString(),
                Text = st.ToString(),
                Selected = selectedStatus.HasValue && st == selectedStatus.Value
            })
            .ToList();

        ViewBag.Statusuri = statuses;
    }
}
