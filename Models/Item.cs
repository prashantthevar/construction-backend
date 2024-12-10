namespace MyApiApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        // Ensure that Name is initialized
        public string Name { get; set; } = string.Empty; // Default to an empty string

        public decimal Price { get; set; }

        // Optionally, add a constructor to set values
        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
