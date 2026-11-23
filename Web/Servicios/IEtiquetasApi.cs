using Web.Models;

namespace Web.Servicios
{
    public interface IEtiquetasApi
    {
        Task<List<EtiquetaVm>> ListarAsync(CancellationToken ct = default);
        Task<EtiquetaVm?> ObtenerAsync(int id, CancellationToken ct = default);
        Task<EtiquetaVm> CrearAsync(EtiquetaCrearVm datos, CancellationToken ct = default);
    }
}
