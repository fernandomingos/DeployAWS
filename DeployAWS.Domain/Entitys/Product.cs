namespace DeployAWS.Domain.Entitys
{
    public class Product : Base
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public bool IsDisponivel { get; set; }
    }
}
