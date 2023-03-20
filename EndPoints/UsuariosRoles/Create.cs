using Ardalis.ApiEndpoints;
using Factura_Bici.Server.Context;
using Factura_Bici.Server.Models;
using Factura_Bici.Shared.Request;
using Factura_Bici.Shared.Routes;
using Factura_Bici.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factura_Bici.Server.Endpoints.UsuariosRoles;

using Request = UsuarioRolCreateRequest;
using Respuesta = Result<int>;

public class Create : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
{
    private readonly IMyDbContext dbContext;

    public Create(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpPost(UsuarioRolRouteManager.BASE)]
    public override async Task<ActionResult<Respuesta>> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        try
        {
            #region Validaciones
            var rol = await dbContext.UsuariosRoles.FirstOrDefaultAsync(r => r.Nombre.ToLower() == request.Nombre.ToLower(),cancellationToken);
            if (rol != null)
              return Respuesta.Fail($"Ya existe un rol con el nombre dado'({request.Nombre})'");
            #endregion
            rol = UsuarioRol.Crear(request);
            dbContext.UsuariosRoles.Add(rol);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Respuesta.Success(rol.Id);
        }
        catch(Exception e){
            return Respuesta.Fail(e.Message);
        }
}
}