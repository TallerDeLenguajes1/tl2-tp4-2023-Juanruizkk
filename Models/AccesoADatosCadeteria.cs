/* namespace EspacioCadeteria;

using System.Text.Json;

public class AccesoADatosCadeteria
{
    Cadeteria Obtener(string path){
        string json = JsonSerializer.Deserialize<Cadeteria>(path);
        Cadeteria nuevaCad = Json

    }
}

string json = File.ReadAllText(Path.Combine(path, "Cadeteria.json"));
        Cadeteria nuevaCadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
        nuevaCadeteria.ListadoCadetes = CargarCadetes(path);
        return nuevaCadeteria; */