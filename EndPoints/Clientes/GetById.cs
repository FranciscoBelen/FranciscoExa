using Ardalis.ApiEndpoints;
using Factura_Bici.Server.Context;
using Factura_Bici.Shared.Records;
using Factura_Bici.Shared.Routes;
using Factura_Bici.Shared.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factura_Bici.Server.Endpoints.Cliente;
using Respuesta = Result<ClienteRecord>;
using Request = ClienteRouteManager;

public class GetById : EndpointBaseAsync.WithRequest<Request>.WithActionResult<Respuesta>
{
    private readonly IMyDbContext dbContext;

    public GetById(IMyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet(ClienteRouteManager.GetById)]
    public override async Task<ActionResult<Respuesta>> HandleAsync([FromRoute] Request request,CancellationToken cancellationToken = default)
    {
        try{

        var rol = await dbContext.clientes
        .Where(r=>r.Id == request.Id)
        .Select(rol=>rol.ToRecord())
        .FirstOrDefaultAsync(cancellationToken);

        if(rol==null)
        return Respuesta.Fail($"No fue posible encontrar el rol'{request.Id}'");

        return Respuesta.Success(rol);
        }
        catch(Exception ex){
            return Respuesta.Fail(ex.Message);
        }
        
    }
}
