using Ardalis.ApiEndpoints;
using Factura_Bici.Server.Context;
using Factura_Bici.Shared.Records;
using Factura_Bici.Shared.Routes;
using Factura_Bici.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factura_Bici.Server.Endpoints.UsuariosRoles;
using Respuesta = ResultList<UsuarioRolRecord>;

public class Get : EndpointBaseAsync.WithoutRequest.WithActionResult<Respuesta>
{
    private readonly IMyDbContext dbContext;

    public Get(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet(UsuarioRolRouteManager.BASE)]
    public override async Task<ActionResult<Respuesta>> HandleAsync(CancellationToken cancellationToken = default)
    {
        try{

        var roles = await dbContext.UsuariosRoles
        .Select(rol=>rol.ToRecord())
        .ToListAsync(cancellationToken);

        return Respuesta.Success(roles);
        }
        catch(Exception ex){
            return Respuesta.Fail(ex.Message);
        }
        
    }
}
