namespace LB_ChoppAPI.Models
{
    public class ChopeiraDisponivel
    {
        public string Voltagem { get; set; } = string.Empty;
        public string Qt_torneiras { get; set; } = string.Empty;
        public int Qt_chopeira { get; set; }
        public int Qt_reservada { get; set; }
    }
}
