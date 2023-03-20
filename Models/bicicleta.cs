using System.ComponentModel.DataAnnotations;
using Factura_Bici.Server.Request;
using Factura_Bici.Shared.Records;

namespace Factura_Bici.Server.Models;

public class bicicleta
{
    public bicicleta()
    {
        
    }
    public bicicleta(string modelo, string llanta, int precio, int suplidor)
    {
        Modelo = modelo;
        this.Llanta = llanta;
        this.Precio = precio;
        this.Suplidor = suplidor;
    }

    [Key]
    public int Id { get; set;}
    public string  Modelo { get; set;} =null!;
    public string Llanta { get; set;} = null!;
    public int Precio { get; set;}
    public int Suplidor { get; set;}

    public static bicicleta Crear(bicicletaCreateRequest request)
    {
        return new bicicleta(request.Modelo, request.Llanta, request.Precio, request.Suplidor);
    }

    public void Modificar(bicicletaUpdateRequest request)
    {
        if(Modelo!=request.Modelo) Modelo = request.Modelo;
        if(Llanta!=request.Llanta) Llanta = request.Llanta;
        if(Precio!=request.Precio) Precio = request.Precio;
        if(Suplidor!=request.Suplidor) Suplidor = request.Suplidor;
    }
    public bicicletaRecord ToRecord()
    {
        return new bicicletaRecord(Id, Modelo, Llanta, Precio, Suplidor);
    }

}