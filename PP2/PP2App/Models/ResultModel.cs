namespace PP2App.Models
{
    // Modelo para almacenar los resultados de las operaciones binarias como un objeto (manejo más sencillo en la vista :D)
    public class ResultModel
    {
        public string Bin { get; set; } = string.Empty;
        public string Oct { get; set; } = string.Empty;
        public string Dec { get; set; } = string.Empty;
        public string Hex { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
