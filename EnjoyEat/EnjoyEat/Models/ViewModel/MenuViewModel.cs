namespace EnjoyEat.Models.ViewModel
{
    public class MenuViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int UnitPrice { get; set; }
        public byte CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}