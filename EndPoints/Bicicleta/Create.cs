using Ardalis.ApiEndpoints;
using Factura_Bici.Server.Context;
using Factura_Bici.Server.Models;
using Factura_Bici.Shared.Request;
using Factura_Bici.Shared.Routes;
using Factura_Bici.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factura_Bici.Server.Endpoints.Bicicleta;

using Request = bicicletaCreateRequest;
using Respuesta = Result<int>;

public class Create : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
{
    private readonly IMyDbContext dbContext;

    public Create(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpPost(BicicletaRouteManager.BASE)]
    public override async Task<ActionResult<Respuesta>> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        try
        {
            #region Validaciones
            var rol = await dbContext.bicicletas.FirstOrDefaultAsync(r => r.Modelo.ToLower() == request.Modelo.ToLower(),cancellationToken);
            if (rol != null)
              return Respuesta.Fail($"Ya existe un rol con el nombre dado'({request.Modelo})'");
            #endregion
            rol = bicicleta.Crear(request);
            dbContext.bicicletas.Add(rol);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Respuesta.Success(rol.Id);
        }
        catch(Exception e){
            return Respuesta.Fail(e.Message);
        }
}
}