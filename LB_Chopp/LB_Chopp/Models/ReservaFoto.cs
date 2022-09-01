namespace LB_Chopp.Models
{
    public class ReservaFoto
    {
        public int ID { get; set; }
        public string Cd_empresa { get; set; } = string.Empty;
        public int Id_reserva { get; set; }
        public string Foto { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;
    }
}
