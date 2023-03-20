using System.ComponentModel.DataAnnotations;
using Factura_Bici.Server.Request;
using Factura_Bici.Shared.Records;

namespace Factura_Bici.Server.Models;

public class cliente
{
    public cliente()
    {
        
    }
    public cliente(string nombre, string apellido, int telefono, string correo)
    {
        Nombre = nombre;
        Apellido = apellido;
        Telefono = telefono;
        Correo = correo;
    }

    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public int Telefono { get; set; }
    public string Correo { get; set; } = null!;

    public static cliente Crear(clienteCreateRequest request)
    {
        return new cliente(request.Nombre, request.Apellido, request.Telefono, request.Correo);
    }

    public void Modificar(clienteUpdateRequest request)
    {
        if(Nombre!=request.Nombre) Nombre = request.Nombre;
        if(Apellido!=request.Apellido) Apellido = request.Apellido;
        if(Telefono!=request.Telefono) Telefono = request.Telefono;
        if(Correo!=request.Correo) Correo = request.Correo;
    }
    public ClienteRecord ToRecord()
    {
        return new ClienteRecord(Id, Nombre, Apellido, Telefono, Correo);
    }
}