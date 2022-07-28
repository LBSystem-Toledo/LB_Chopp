namespace LB_ChoppAPI.Models
{
    public class BarrisTipo
    {
        public int Id_volume { get; set; }
        public int Id_tipo { get; set; }
        public int Volume { get; set; }
        public string Ds_tipo { get; set; } = string.Empty;
        public int Cheio { get; set; }
        public decimal Preco { get; set; } = decimal.Zero;
    }
}
