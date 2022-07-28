namespace LB_ChoppAPI.Models
{
    public class ReservaChopeira
    {
        public string Cd_empresa { get; set; } = string.Empty;
        public int Id_reserva { get; set; }
        public int Id_item { get; set; }
        public int Id_chopeira { get; set; }
        public string Nr_chopeira { get; set; } = string.Empty;
        public string Voltagem { get; set; } = string.Empty;
        public string Qt_torneiras { get; set; } = string.Empty;
    }
}
