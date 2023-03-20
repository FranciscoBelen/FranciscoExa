using Ardalis.ApiEndpoints;
using Factura_Bici.Server.Context;
using Factura_Bici.Shared.Records;
using Factura_Bici.Shared.Routes;
using Factura_Bici.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factura_Bici.Server.Endpoints.Clientes;
using Respuesta = ResultList<ClienteRecord>;

public class Get : EndpointBaseAsync.WithoutRequest.WithActionResult<Respuesta>
{
    private readonly IMyDbContext dbContext;

    public Get(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet(ClienteRouteManager.BASE)]
    public override async Task<ActionResult<Respuesta>> HandleAsync(CancellationToken cancellationToken = default)
    {
        try{

        var roles = await dbContext.clientes
        .Select(rol=>rol.ToRecord())
        .ToListAsync(cancellationToken);

        return Respuesta.Success(roles);
        }
        catch(Exception ex){
            return Respuesta.Fail(ex.Message);
        }
        
    }
}
