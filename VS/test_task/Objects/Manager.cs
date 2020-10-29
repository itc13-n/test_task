namespace test_task.Classes
{
    class Manager
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }

        public Manager()
        {
            this.ID = -2;
            this.Name = "-";
            this.Comment = "dummy";
        }

        public Manager(int id, string name, string comment)
        {
            this.ID = id;
            this.Name = name;
            this.Comment = comment;
        }


    }
}
