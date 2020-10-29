namespace test_task.Classes
{
    class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Subscribtion { get; set; }
        public int? Period { get; set; }
        public int? Owner { get; set; }

        public Product()
        {
            this.ID = -2;
            this.Name = "dummy";
            this.Price = -2;
            this.Subscribtion = false;
            this.Period = -2;
            this.Owner = null;
        }

        public Product(int id, string name, decimal price, bool subsc, int? period)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.Subscribtion = subsc;
            this.Period = period;
            this.Owner = null;
        }

        public Product(int id, string name, decimal price, bool subsc, int period, int owner)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.Subscribtion = subsc;
            this.Period = period;
            this.Owner = owner;
        } // careful with owner (! null accepting int)
    }


}
