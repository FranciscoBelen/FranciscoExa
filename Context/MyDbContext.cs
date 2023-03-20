using Factura_Bici.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Factura_Bici.Server.Context;

public interface IMyDbContext
{
    DbSet<UsuarioRol> UsuariosRoles { get; set; }
    DbSet<Usuario> Usuarios { get; set; }
    DbSet<cliente> clientes { get; set; }
    DbSet<bicicleta> bicicletas { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class MyDbContext : DbContext, IMyDbContext
{
    private readonly IConfiguration config;

    public MyDbContext(IConfiguration _config)
    {
        config = _config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("CRUD"));
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    #region Tablas de mi base de datos
    public DbSet<UsuarioRol> UsuariosRoles { set; get; } = null!;
    public DbSet<Usuario> Usuarios { set; get; } = null!;
    public DbSet<cliente> clientes { set; get; } = null!;
    public DbSet<bicicleta> bicicletas { set; get; } = null!;
    #endregion
}