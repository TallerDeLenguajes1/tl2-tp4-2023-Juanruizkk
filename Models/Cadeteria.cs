using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.IO;
namespace EspacioCadeteria;
public class Cadeteria
{
    private string nombre;
    private double telefono;
    private List<Cadete> listadoCadetes;

    private List<Pedido> listadoPedidos;
    private static Cadeteria instance;

    private Cadeteria()
    {

        nombre = "Cadeteria Juan";
        telefono = 3814452417;
        listadoCadetes = new List<Cadete>();
        listadoPedidos = new List<Pedido>();
        CrearPedido(1, "Queso", Estado.EnPreparacion, "Juan", "Bolivia", 323, "Entregar en puera");
        CrearPedido(2, "Mila", Estado.EnPreparacion, "Pedro", "Mendoza", 323, "Entregar en puera");
        CrearPedido(3, "Bacon", Estado.EnPreparacion, "Ricardito", "Peru", 323, "Entregar en puera");
        listadoCadetes.Add(new Cadete(1, "Ramiro", "Parque", 38445262));
    }

    public static Cadeteria Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Cadeteria();
            }
            return instance;
        }
    }
    public Cadeteria(string nombre, double telefono, List<Cadete> listadoCadetes = null)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listadoCadetes = listadoCadetes;
        this.listadoPedidos = new List<Pedido>();
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public double Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    public Cliente CrearCliente(string nombre, string direccion, double telefono, string observacion)
    {
        Cliente nuevo = new Cliente(nombre, direccion, telefono, observacion);
        return nuevo;
    }
    public Cadete EncontrarCadete(int idCad)
    {
        Cadete cad = listadoCadetes.Find(x => x.Id == idCad);
        return cad;
    }
    public Pedido EncontrarPedido(int numPedido)
    {
        Pedido ped = listadoPedidos.Find(x => x.Numero == numPedido);
        return ped;
    }
    public List<Cadete> getListadoCadete()
    {
        return listadoCadetes;
    }
    public List<Pedido> getListadoPedido()
    {
        return listadoPedidos;
    }
    public double JornalACobrar(int idCad)
    {
        int cont = 0;

        foreach (var pedido in listadoPedidos)
        {
            if (pedido.Idcadete == idCad)
            {
                cont++;
            }
        }

        return cont * 200;
    }
    public void CrearPedido(int numero, string obs, Estado estado, string nombreCliente, string direccionCliente, double telefonoCliente, string observacionCliente)
    {
        var nuevoCliente = CrearCliente(nombreCliente, direccionCliente, telefonoCliente, observacionCliente);
        Pedido nuevoPedido = new Pedido(numero, obs, nuevoCliente, estado);
        listadoPedidos.Add(nuevoPedido);
    }

    public void AsignarCadeteAPedido(int idCadete, int numPedido)
    {
        Cadete cadeteAAsignar = EncontrarCadete(idCadete);
        Pedido pedido = EncontrarPedido(numPedido);
        pedido.Idcadete = cadeteAAsignar.getIdCadete();
    }
    public void CambiarEstadoPedido(int numPedido, Estado nuevoEstado)
    {
        Pedido pedido = EncontrarPedido(numPedido);
        pedido.SetEstadoPedido(nuevoEstado);
    }


    public void reasignarPedido(int numPedido, int idCadAAsignar)
    {
        Pedido pedidoAReadignar = EncontrarPedido(numPedido);
        Cadete cadeteAReasignarPedido = EncontrarCadete(idCadAAsignar);
        pedidoAReadignar.Idcadete = cadeteAReasignarPedido.getIdCadete();
    }

    public Informe GenerarInforme()
    {
        Informe nuevoInforme = new Informe();
        InformeCadete CadIndependiente;

        foreach (var cadete in listadoCadetes)
        {
            int cantEnvios = 0;
            double montoGanado = JornalACobrar(cadete.getIdCadete());

            foreach (var pedido in listadoPedidos)
            {
                if (pedido.Idcadete == cadete.getIdCadete())
                {
                    cantEnvios++;
                }
            }
            CadIndependiente = new InformeCadete(cadete.Nombre, montoGanado, cantEnvios);
            nuevoInforme.InformeCadetes.Add(CadIndependiente);
        }
        return nuevoInforme;


    }

}