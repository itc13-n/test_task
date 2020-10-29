namespace test_task.Classes
{
    class Contact
    {

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Tel { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int ClientID { get; set; }

        public Contact()
        {
            this.ID = -2;
            this.FirstName = "-";
            this.LastName = "-";
            this.Tel = -2;
            this.Email = "@";
            this.Comment = "-";
            this.ClientID = -2;
        }

        public Contact(int id, string firstName, string lastName, int tel, string email, string comment, int clientID)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Tel = tel;
            this.Email = email;
            this.Comment = comment;
            this.ClientID = clientID;
        }

    }
}
