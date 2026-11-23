using Microsoft.AspNetCore.Mvc;
using Web.Models;
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

        [HttpGet]
        public IActionResult Crear() => View(new EtiquetaCrearVm());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(EtiquetaCrearVm datos, CancellationToken ct)
        {
            if(!ModelState.IsValid) return View(datos);
            try
            {
                var creada = await _api.CrearAsync(datos, ct);
                TempData["Exito"] = $"Etiqueta {creada.Nombre} creada.";
                return RedirectToAction(nameof(Detalle), new { id = creada.Id });
            }           
            catch (ApiException ex) when (ex.Codigo is 400 or 409)
            {
                AgregarErroresDelApi(ex);
                return View(datos);
            }
                 
        }

        private void AgregarErroresDelApi(ApiException ex)
        {
            // Sin errores por campo (409 o 422): un mensaje general. Con errores por campo (400): uno por campo.
            if (ex.Errores.Count == 0) ModelState.AddModelError(string.Empty, ex.Message);
            foreach (var (campo, mensajes) in ex.Errores)
                foreach (var mensaje in mensajes)
                    ModelState.AddModelError(campo, mensaje);
        }

    }
}