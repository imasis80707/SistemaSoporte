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

        public async Task<IActionResult> Detalle(int id, CancellationToken ct)
        {
            // ObtenerAsync devuelve null cuando el API responde 404: aquí se vuelve un 404 del MVC.
            var etiqueta = await _api.ObtenerAsync(id, ct);
            return etiqueta is null ? NotFound() : View(etiqueta);
        }
    }
}