namespace LB_ChoppAPI.Models
{
    public class ReservaBarril
    {
        public string Cd_empresa { get; set; } = string.Empty;
        public int Id_reserva { get; set; }
        public int Id_item { get; set; }
        public string Cd_produto { get; set; } = string.Empty;
        public string Ds_produto { get; set; } = string.Empty;
        public int Id_barril { get; set; }
        public string Nr_barril { get; set; } = string.Empty;
        public int Id_tipo { get; set; }
        public string Ds_tipo { get; set; } = string.Empty;
        public int Volume { get; set; }
        public decimal Valor { get; set; } = decimal.Zero;
        public decimal Vl_desconto { get; set; } = decimal.Zero;
        public decimal Vl_frete { get; set; } = decimal.Zero;
        public bool RetornouCheio { get; set; } = false;
    }
}
