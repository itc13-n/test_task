using System;
using test_task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using test_task.Classes;
using test_task.DB;
using System.Collections.Generic;
using System.Configuration;


namespace test_task_tests
{
    [TestClass]
    public class Test_Class
    {
        #region ConnectionString
        private readonly string ConnectionString = @"Data Source=(localdb)\ProjectsV13; Initial Catalog=test_task_db; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False;";
        #endregion

        #region Client
        [TestMethod]
        public void ClientEdited_true()
        {
            Client client = new Client(0, "GGG", false, null);

            var result = client.Edited();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ClientEdited_false()
        {
            List<Client> clients = new List<Client>
            {
                new Client(),
                new Client() { Name = "", Comment = "" },
                new Client() { Name = "-", Comment = "dummy" },
                new Client() { Name = null, Comment = null }
            };

            for (int i = 0; i < clients.Count; i++)
            {
                var res = clients[i].Edited();
                if (res)
                {
                   Assert.IsTrue(false);
                }
            }
            Assert.IsTrue(true);
        }
        #endregion

        #region Manager
        [TestMethod]
        public void ManagerEdited_true()
        {
            Manager manager = new Manager(0, "man", null);

            var result = manager.Edited();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ManagerEdited_false()
        {
            List<Manager> managers = new List<Manager>
            {
                new Manager(),
                new Manager() { Name = "", Comment = "" },
                new Manager() { Name = "-", Comment = "dummy" },
                new Manager() { Name = null, Comment = null }
            };

            for (int i = 0; i < managers.Count; i++)
            {
                var res = managers[i].Edited();
                if (res)
                {
                   Assert.IsTrue(false);
                }
            }
            Assert.IsTrue(true);
        }
        #endregion

        #region Product
        [TestMethod]
        public void ProductEdited_true()
        {
            Product product = new Product(0, "pr", 10m, false, null);

            var result = product.Edited();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProductEdited_false()
        {
            List<Product> products = new List<Product>
            {
                new Product(),
                new Product() { Name = "", Subscribtion = false },
                new Product() { Name = "-", Subscribtion = true, Price = -1},
                new Product() { Name = null, Subscribtion = false, Price = 1 }
            };

            for (int i = 0; i < products.Count; i++)
            {
                var res = products[i].Edited();
                if (res)
                {
                   Assert.IsTrue(false);
                }
            }
            Assert.IsTrue(true);
        }
        #endregion

        #region DBOperator
        #region Insert
        [TestMethod]
        public void InsertData_Valid()
        {
            DBOperator.ConnectionString = ConnectionString;

            #region Manager
            List<Manager> managers = new List<Manager>()
            {
                new Manager() { Name = "Susan", Comment = "test_test1"},
                new Manager() { Name = "Pol", Comment = "test_test1" },
                new Manager() { Name = "Someone", Comment = "test_test1"}
            };

            try
            {
                DBOperator.InsertData(managers);
            }
            catch (Exception)
            {
               Assert.IsTrue(false);

            }

            List<string> checkListManager = DBOperator.DirectQueryR("select * from MANAGERS where COMMENT = 'test_test1'", false);

            if (checkListManager.Count == managers.Count)
            {
                for (int i = 0; i < managers.Count; i++)
                {
                    bool nameFound = false;
                    for (int k = 0; k < checkListManager.Count; k++)
                    {
                        if (checkListManager[k].Contains(managers[i].Name))
                        {
                            nameFound = true;
                            continue;
                        }
                    }
                    if (!nameFound)
                    {
                        Assert.IsTrue(false);

                    }
                }
            }
            else
            {
               Assert.IsTrue(false);
            }
            Assert.IsTrue(true);

            DBOperator.DirectQuery("delete from MANAGERS where COMMENT = 'test_test1'", false);
            #endregion

            #region Client
            List<Client> Clients = new List<Client>()
            {
                new Client() { Name = "Susan", Comment = "test_test1"},
                new Client() { Name = "Pol", Comment = "test_test1" },
                new Client() { Name = "Someone", Comment = "test_test1"}
            };

            try
            {
                DBOperator.InsertData(Clients);
            }
            catch (Exception)
            {
               Assert.IsTrue(false);
                
            }

            List<string> checkListClient = DBOperator.DirectQueryR("select * from CLIENTS where COMMENT = 'test_test1'", false);

            if (checkListClient.Count == Clients.Count)
            {
                for (int i = 0; i < Clients.Count; i++)
                {
                    bool nameFound = false;
                    for (int k = 0; k < checkListClient.Count; k++)
                    {
                        if (checkListClient[k].Contains(Clients[i].Name))
                        {
                            nameFound = true;
                            continue;
                        }
                    }
                    if (!nameFound)
                    {
                       Assert.IsTrue(false);
                        
                    }
                }
            }
            else
            {
               Assert.IsTrue(false);
                
            }
            Assert.IsTrue(true);

            DBOperator.DirectQuery("delete from CLIENTS where COMMENT = 'test_test1'", false);
            #endregion

            #region Product
            List<Product> Products = new List<Product>()
            {
                new Product() { Name = "test_test1", Subscribtion = true, Price = 10, Period = 1},
                new Product() { Name = "test_test1", Subscribtion = false, Price = 11, Period = null},
                new Product() { Name = "test_test1", Subscribtion = false,Price = 12, Period = null}
            };

            try
            {
                DBOperator.InsertData(Products);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);

            }

            List<string> checkListProduct = DBOperator.DirectQueryR("select * from PRODUCTS where NAME = 'test_test1'", false);

            if (checkListProduct.Count == Products.Count)
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    bool nameFound = false;
                    for (int k = 0; k < checkListProduct.Count; k++)
                    {
                        if (checkListProduct[k].Contains(Products[i].Name))
                        {
                            nameFound = true;
                            continue;
                        }
                    }
                    if (!nameFound)
                    {
                       Assert.IsTrue(false);

                    }
                }
            }
            else
            {
               Assert.IsTrue(false);

            }
            Assert.IsTrue(true);

            DBOperator.DirectQuery("delete from PRODUCTS where NAME = 'test_test1'", false);
            #endregion

        }

        [TestMethod]
        public void InsertContact_Valid()
        {
            DBOperator.ConnectionString = ConnectionString;

            Contact contact = new Contact() { Comment = "test_test" };

            int client_ID = Convert.ToInt32(DBOperator.DirectQueryR("select top 1 ID from CLIENTS", false)[0].Split("||".ToCharArray())[2]);
            Assert.AreEqual(1, DBOperator.InsertContact(contact, client_ID,showMessage:true));

            DBOperator.DirectQuery("delete from CONTACTS where COMMENT = 'test_test'", false);
        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateObject_Valide()
        {
            DBOperator.ConnectionString = ConnectionString;

            #region Manager
            DBOperator.DirectQuery("insert into MANAGERS values('Someone','test_test')", false);
            int manager_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('MANAGERS');", false)[0].Split("||".ToCharArray())[2]);

            Manager manager = new Manager();
            manager.ID = manager_ID;
            manager.Name = "test_name";
            manager.Comment = "test_test";
            DBOperator.UpdateObject(manager);

            List<string> checkListManager = DBOperator.DirectQueryR($"Select NAME from MANAGERS where ID = {manager_ID}", false);

            if (checkListManager.Count == 1)
            {
                Assert.IsTrue(checkListManager[0].Contains("test_name"));
            }
            else
            {
                Assert.IsTrue(false);

            }

            DBOperator.DirectQuery("delete from MANAGERS where COMMENT = 'test_test'", false);
            #endregion

            #region Client
            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int Client_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);

            Client Client = new Client();
            Client.ID = Client_ID;
            Client.Name = "test_name";
            Client.Comment = "test_test";
            DBOperator.UpdateObject(Client);

            List<string> checkListClient = DBOperator.DirectQueryR($"Select NAME from CLIENTS where ID = {Client_ID}", false);

            if (checkListClient.Count == 1)
            {
                Assert.IsTrue(checkListClient[0].Contains("test_name"));
            }
            else
            {
               Assert.IsTrue(false);
                
            }

            DBOperator.DirectQuery("delete from CLIENTS where COMMENT = 'test_test'", false);
            #endregion

            #region Product
            DBOperator.DirectQuery("insert into PRODUCTS values('Something', 1, 0, NULL)", false);
            int Product_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('PRODUCTS');", false)[0].Split("||".ToCharArray())[2]);

            Product Product = new Product
            {
                ID = Product_ID,
                Subscribtion = false,
                Period = null,
                Price = 1,
                Name = "test_name"
            };

            DBOperator.UpdateObject(Product);

            List<string> checkListProduct = DBOperator.DirectQueryR($"Select NAME from PRODUCTS where ID = {Product_ID}", false);

            if (checkListProduct.Count == 1)
            {
                Assert.IsTrue(checkListProduct[0].Contains("test_name"));
            }
            else
            {
               Assert.IsTrue(false);
                
            }

            DBOperator.DirectQuery("delete from PRODUCTS where NAME = 'test_name'", false);
            #endregion
        }
        #endregion

        #region Relation
        [TestMethod]
        public void Relation_Valide()
        {
            DBOperator.ConnectionString = ConnectionString;

            DBOperator.DirectQuery("insert into MANAGERS values('Someone','test_test')", false);
            int manager_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('MANAGERS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery("insert into PRODUCTS values('Something', 1, 0, NULL)", false);
            int product_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('PRODUCTS');", false)[0].Split("||".ToCharArray())[2]);

            Product product = new Product() { ID = product_ID };
            Manager manager = new Manager() { ID = manager_ID };

            DBOperator.Relation(client_ID, product);
            DBOperator.Relation(client_ID, manager);

            List<string> resManager = DBOperator.DirectQueryR($"select count(*) from CLIENTS_MANAGERS_LINK where CLIENT_ID = {client_ID} and MANAGER_ID = {manager_ID}", false);
            List<string> resProduct = DBOperator.DirectQueryR($"select count(*) from CLIENTS_PRODUCTS_LINK where CLIENT_ID = {client_ID} and PRODUCT_ID = {product_ID}", false);

            Assert.AreEqual(1, resManager.Count);
            Assert.AreEqual(1, resProduct.Count);

            DBOperator.DirectQuery($"delete from CLIENTS_MANAGERS_LINK where CLIENT_ID = {client_ID}; " +
                                   $"delete from CLIENTS_PRODUCTS_LINK where CLIENT_ID = {client_ID} and PRODUCT_ID = {product_ID};" +
                                   $"delete from MANAGERS where ID = {manager_ID}; " +
                                   $"delete from CLIENTS where ID = {client_ID}; " +
                                   $"delete from PRODUCTS where ID = {product_ID}; ", false);


        }
        #endregion

        #region Get
        [TestMethod]
        public void GetObjects_Valide()
        {
            DBOperator.ConnectionString = ConnectionString;

            #region Manager
            DBOperator.DirectQuery("insert into MANAGERS values ('Someone','test_test'), ('Someone','test_test')", false);

            List<Manager> managers = DBOperator.GetObjects(new Manager());

            int counterManager = 0;

            for (int i = 0; i < managers.Count; i++)
            {
                if (managers[i].Name.Contains("Someone"))
                {
                    counterManager++;
                }
            }

            DBOperator.DirectQuery("Delete from MANAGERS where COMMENT = 'test_test'", false);

            Assert.IsTrue(counterManager >= 2);
            #endregion

            #region Client
            DBOperator.DirectQuery("insert into CLIENTS values ('Someone', 0, 'test_test'), ('Someone', 0, 'test_test')", false);

            List<Client> Clients = DBOperator.GetObjects(new Client());

            int counterClient = 0;

            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].Name.Contains("Someone") & (Clients[i].Comment.Contains("test_test")))
                {
                    counterClient++;
                }
            }

            DBOperator.DirectQuery("Delete from CLIENTS where COMMENT = 'test_test'", false);

            Assert.IsTrue(counterClient >= 2);
            #endregion

            #region Product
            DBOperator.DirectQuery("insert into PRODUCTS values ('Something_test', 1, 0, NULL), ('Something_test', 1, 0, NULL)", false);

            List<Product> Products = DBOperator.GetObjects(new Product());

            int counterProduct = 0;

            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Name.Contains("Something_test"))
                {
                    counterProduct++;
                }
            }

            DBOperator.DirectQuery("Delete from PRODUCTS where NAME = 'Something_test'", false);

            Assert.IsTrue(counterProduct >= 2);
            #endregion
        }

        [TestMethod]
        public void GetProductsByClient_Valide()
        {
            DBOperator.ConnectionString = ConnectionString;

            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery("insert into PRODUCTS values('Something', 1, 0, NULL)", false);
            int product_ID1 = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('PRODUCTS');", false)[0].Split("||".ToCharArray())[2]);
            DBOperator.DirectQuery("insert into PRODUCTS values('Something', 1, 0, NULL)", false);
            int product_ID2 = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('PRODUCTS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery($"insert into CLIENTS_PRODUCTS_LINK values ({client_ID},{product_ID1}), ({client_ID},{product_ID2})", false);

            List<Product> checkListProducts = DBOperator.GetProductsByClient(client_ID);

            DBOperator.DirectQuery($"delete from CLIENTS_PRODUCTS_LINK where CLIENT_ID = {client_ID} and PRODUCT_ID in ({product_ID1}, {product_ID2}); " +
                                                                                         $"delete from CLIENTS where ID = {client_ID}; " +
                                                                                         $"delete from PRODUCTS where ID in ({product_ID1}, {product_ID2}); ", 
                                                                                         false);

            Assert.AreEqual(2, checkListProducts.Count);
        }

        [TestMethod]
        public void GetClientsByManager_Valide()
        {
            DBOperator.ConnectionString = ConnectionString;

            DBOperator.DirectQuery("insert into MANAGERS values('Something', 'test_test')", false);
            int manager_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('ManagerS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID1 = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);
            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID2 = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);


            DBOperator.DirectQuery($"insert into CLIENTS_MANAGERS_LINK values ({manager_ID},{client_ID1}), ({manager_ID},{client_ID2})", false);

            List<Client> checkListManagers = DBOperator.GetClientsByManager(manager_ID);

            DBOperator.DirectQuery($"delete from CLIENTS_MANAGERS_LINK where MANAGER_ID = {manager_ID} and CLIENT_ID in ({client_ID1}, {client_ID2}); " +
                                                                                         $"delete from MANAGERS where ID = {manager_ID}; " +
                                                                                         $"delete from CLIENTS where ID in ({client_ID1}, {client_ID2}); ",
                                                                                         false);

            Assert.AreEqual(2, checkListManagers.Count);
        }

        [TestMethod]
        public void GetClientsByProduct_Valide()
        {
            DBOperator.ConnectionString = ConnectionString;

            DBOperator.DirectQuery("insert into PRODUCTS values('Something_test', 1, 0, NULL)", false);
            int Product_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('PRODUCTS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID1 = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);
            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID2 = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);


            DBOperator.DirectQuery($"insert into CLIENTS_PRODUCTS_LINK values ({client_ID1},{Product_ID}), ({client_ID2},{Product_ID})", false);

            List<Client> checkListProducts = DBOperator.GetClientsByProduct(Product_ID);

            DBOperator.DirectQuery($"delete from CLIENTS_PRODUCTS_LINK where PRODUCT_ID = {Product_ID} and CLIENT_ID in ({client_ID1}, {client_ID2}); " +
                                                                                         $"delete from PRODUCTS where ID = {Product_ID}; " +
                                                                                         $"delete from CLIENTS where ID in ({client_ID1}, {client_ID2}); ",
                                                                                         false);

            Assert.AreEqual(2, checkListProducts.Count);
        }

        [TestMethod]
        public void GetManagerByClient_Validate()
        {
            DBOperator.ConnectionString = ConnectionString;

            DBOperator.DirectQuery("insert into MANAGERS values('Something', 'test_test')", false);
            int manager_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('ManagerS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery($"insert into CLIENTS_MANAGERS_LINK values ({manager_ID}, {client_ID})", false);

            Manager manager = DBOperator.GetManagerByClient(client_ID);


            DBOperator.DirectQuery($"delete from CLIENTS_MANAGERS_LINK where MANAGER_ID = {manager_ID} and CLIENT_ID = {client_ID}; " +
                                                                                         $"delete from MANAGERS where ID = {manager_ID}; " +
                                                                                         $"delete from CLIENTS where ID = {client_ID}; ",
                                                                                         false);
            Assert.IsTrue(manager.ID == manager_ID);

        }

        [TestMethod]
        public void GetContact_Validate()
        {
            DBOperator.ConnectionString = ConnectionString;

            DBOperator.DirectQuery("insert into CLIENTS values('Someone',0,'test_test')", false);
            int client_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CLIENTS');", false)[0].Split("||".ToCharArray())[2]);

            DBOperator.DirectQuery($"insert into CONTACTS values('Someone','Someone', 9998883322 ,'@','test_test', {client_ID})", false);
            int contact_ID = Convert.ToInt32(DBOperator.DirectQueryR("SELECT IDENT_CURRENT('CONTACTS');", false)[0].Split("||".ToCharArray())[2]);

            Contact contact = DBOperator.GetContact(client_ID);
            
            DBOperator.DirectQuery($"delete from CONTACTS where ID = {contact_ID};" +
                                   $"delete from CLIENTS where ID = {client_ID}",
                                   false);

            Assert.IsTrue(contact.ID == contact_ID);

        }
        #endregion
        #endregion
    }
}
