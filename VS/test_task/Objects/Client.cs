namespace test_task.Classes
{
    class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool PriorClient { get; set; }
        public string Comment { get; set; }

        public Client()
        {
            this.ID = -2;
            this.Name = "-";
            this.PriorClient = false;
            this.Comment = "dummy";
        }

        public Client(int id, string name, bool prior, string comment)
        {
            this.ID = id;
            this.Name = name;
            this.PriorClient = prior;
            this.Comment = comment;
        }
    }
}
