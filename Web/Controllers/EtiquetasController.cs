using Microsoft.AspNetCore.Mvc;
using Web.Servicios;

namespace Web.Controllers
{
    public class EtiquetasController : Controller
    {
        private readonly IEtiquetasApi _api;

        public EtiquetasController(IEtiquetasApi api) => _api = api;
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var etiquetas = await _api.ListarAsync(ct);
            return View(etiquetas);
        }
    }
}
