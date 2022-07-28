namespace LB_ChoppAPI.Models
{
    public class ReservaCilindro
    {
        public string Cd_empresa { get; set; } = string.Empty;
        public int Id_reserva { get; set; }
        public int Id_item { get; set; }
        public int Id_cilindro { get; set; }
        public string Nr_cilindro { get; set; } = string.Empty;
    }
}
