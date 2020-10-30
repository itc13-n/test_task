using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using test_task.Classes;

namespace test_task.DB
{
    internal static class DBOperator
    {
        public static string ConnectionString { get; set; }

        public static void Initialize()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }


        public static List<T> GetObjects<T>(T type, string select = "*", string where = null, string orderBy = null, string groupBy = null)
        {
            List<T> outObjects = new List<T>();
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader reader;
            string table;

            switch (type)
            {
                case Manager m:
                    table = "MANAGERS";
                    break;
                case Client c:
                    table = "CLIENTS";
                    break;
                case Product p:
                    table = "PRODUCTS";
                    break;
                default:
                    throw new ArgumentException("Invalid object Type. SELECT query aborted.");
            }

            if (!string.IsNullOrEmpty(where))
            {
                where = "where " + where;
            }

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"SELECT {select} from {table} {where} {orderBy} {groupBy}";

            try
            {
                cn.Open();
                reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        outObjects.Add(GetObjectFromReader(type, ref reader));
                    }
                }

        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               cn.Close(); 
            }

        return outObjects;
        }

        public static int InsertData <T>(List<T> data)
        {
            if (data is null)
            {
                throw new ArgumentNullException($"data {typeof(T)} was null");
            }

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            string table, values;
            int rowsAffected = -2;

            switch (data)
            {
                case List<Manager> m:
                    table = "MANAGERS";
                    values = "('" + (data[0] as Manager).Name + "', '" 
                        + (data[0] as Manager).Comment + "')";
                    if (data.Count > 0)
                    {
                        for (int i = 1; i < data.Count - 1; i++)
                        {
                            values = ", ('"
                                + (data[i] as Manager).Name + "', '" 
                                + (data[i] as Manager).Comment + "')";
                        }
                    }
                    break;

                case List<Client> c:
                    table = "CLIENTS";
                    values = "('"
                        + (data[0] as Client).Name + "', " 
                        + Convert.ToInt32((data[0] as Client).PriorClient) +", '" 
                        + (data[0] as Client).Comment + "')";
                    if (data.Count > 0)
                    {
                        for (int j = 1; j < data.Count - 1; j++)
                        {
                            values = ", ('" 
                                + (data[j] as Client).Name + "', " 
                                + (data[j] as Client).PriorClient + ", '" 
                                + (data[j] as Client).Comment + "')";
                        }
                    }
                    break;

                case List<Product> p:
                    table = "PRODUCTS";
                    values = "('"
                        + (data[0] as Product).Name + "', " 
                        + (data[0] as Product).Price + ", " 
                        + Convert.ToInt32((data[0] as Product).Subscribtion) + ", '" 
                        + (data[0] as Product).Period + "')";
                    if (data.Count > 0)
                    {
                        for (int k = 1; k < data.Count - 1; k++)
                        {
                            values = ", ('"
                                + (data[k] as Product).Name + "', " 
                                + (data[k] as Product).Price + ", " 
                                + (data[k] as Product).Subscribtion + ", " 
                                + (data[k] as Product).Period + ")";
                        }
                    }

                    if (!((data[0] as Product).Owner is null))
                    {
                        int client_ID = (data[0] as Product).Owner.Value;
                        Relation(client_ID, data[0] as Product);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid object Type. INSERT query aborted.");
            }

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"insert into {table} " +
                                 $"values {values}";

            try
            {
                cn.Open();

                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return rowsAffected;
        }

        public static int UpdateObject<T>(T objNew)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            string table, where, set;
            int rowsAffected = -2;

            switch (objNew)
            {
                case Manager _:
                    table = "MANAGERS";
                    set = $"NAME = '{(objNew as Manager).Name}' " +
                        $", COMMENT = '{(objNew as Manager).Comment}' ";
                    where = $"ID = {(objNew as Manager).ID}";
                    break;

                case Client _:
                    table = "CLIENTS";
                    set = $"NAME = '{(objNew as Client).Name}' " +
                          $", PRIOR_CLIENT = {Convert.ToInt32((objNew as Client).PriorClient)}" +
                          $", COMMENT = '{(objNew as Client).Comment}' ";
                    where = $"ID = {(objNew as Client).ID}";
                    break;

                case Product _:
                    string period;
                    if ((objNew as Product).Period is null)
                    {
                        period = "NULL";
                    }
                    else
                    {
                        period = ""+(objNew as Product).Period.Value.ToString()+ "";
                    }
                    table = "PRODUCTS";
                    set = $"NAME = '{(objNew as Product).Name}'" +
                          $", PRICE = {(objNew as Product).Price}" +
                          $", SUBSC = {Convert.ToInt32((objNew as Product).Subscribtion)}" +
                           $", PERIOD = " + period;
                    where = $"ID = {(objNew as Product).ID}";

                    if (!((objNew as Product).Owner is null))
                    {
                        int client_ID = (objNew as Product).Owner.Value;
                        Relation(client_ID, objNew as Product);
                    }
                    break;

                default:
                    throw new ArgumentException("Invalid object Type. UPDATE query aborted.");
            }

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"update {table} set {set} where {where}";

            try
            {
                cn.Open();

                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;

        }

        public static int DeleteObject<T>(T type, string where)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            string table;
            int rowsAffected = -2;

            switch (type)
            {
                case Manager m:
                    table = "MANAGERS";
                    break;

                case Client c:
                    table = "CLIENTS";
                    break;

                case Product p:
                    table = "PRODUCTS";
                    break;

                default:
                    throw new ArgumentException("Invalid object Type. DELETE query aborted.");
            }
            
            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"DELETE from {table} where {where}";

            try
            {
                cn.Open();

                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;
        }

        
        public static int Relation<T>(int client, T relType, bool remove = false)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            string table;
            int rowsAffected = -2;
            if (!(remove))
            {
                switch (relType)
                {
                    case Manager _:
                        table = "CLIENTS_MANAGERS_LINK";
                        sqlCmd.CommandText = $"delete from {table} where CLIENT_ID = {client}); insert into {table} values({(relType as Manager).ID}, {client})";
                        break;

                    case Product _:
                        table = "CLIENTS_PRODUCTS_LINK";
                        sqlCmd.CommandText = $"delete from {table} where CLIENT_ID = {client}); insert into {table} values({client}, {(relType as Product).ID})";
                        break;

                    default:
                        throw new ArgumentException("Invalid object Type. INSERT (relation) query aborted.");
                }
            }
            else
            {
                switch (relType)
                {
                    case Manager _:
                        table = "CLIENTS_MANAGERS_LINK";
                        break;

                    case Product _:
                        table = "CLIENTS_PRODUCTS_LINK";
                        break;

                    default:
                        throw new ArgumentException("Invalid object Type. DELETE (relation) query aborted.");
                }
                sqlCmd.CommandText = $"delete from {table} where CLIENT_ID = {client})";
            }

            sqlCmd.Connection = cn;

            try
            {
                cn.Open();

                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;
        }    


        public static int InsertContact(Contact contact, Client client = null)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlInsertCmd = new SqlCommand();
            SqlCommand sqlClearCmd = new SqlCommand();
            int rowsAffected = -2;
            int client_ID = client is null ? contact.ClientID : client.ID;
            
            sqlClearCmd.Connection = cn;
            sqlClearCmd.CommandText = $"delete from CONTACTS where CLIENT_ID = {client_ID};";
            
            sqlInsertCmd.Connection = cn;
            sqlInsertCmd.CommandText = "insert into CONTACTS values (" +
                                        $" '{contact.FirstName}'," +
                                        $" '{contact.LastName}' ," +
                                        $"{contact.Tel}," +
                                        $" '{contact.Email}' ," +
                                        $" '{contact.Comment}' ," +
                                        $"{client_ID})";

            //"insert into CONTACTS values ( 'Randal', 'Zayac' ,79990007766, 'RandalZ@uuu.mm' , '' ,6)"
            try
            {
                cn.Open();
                sqlClearCmd.ExecuteNonQuery();
                rowsAffected = sqlInsertCmd.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;
        }

        public static Contact GetContact(Client client)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            
            sqlCmd.Connection = cn;

            sqlCmd.CommandText = $"select * from CONTACTS where CLIENT_ID = {client.ID} order by ID offset 0 rows fetch next 1 row only";

            Contact contact;

            try
            {
                cn.Open();
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.Read())
                {
                    long? tel;
                    if (reader.IsDBNull(3))
                    {
                        tel = null;
                    }
                    else
                    {
                        tel = reader.GetInt64(3);
                    }
                    contact = new Contact(reader.GetInt32(0),
                                          reader.GetString(1),
                                          reader.GetString(2),
                                          tel,
                                          reader.GetString(4),
                                          reader.GetString(5),
                                          reader.GetInt32(6));
                }
                else
                {
                    contact = new Contact();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new Contact();
            }
            finally
            {
                cn.Close();
            }

            return contact;
        }


        ///<summary>
        ///Wipes ALL data from given table (!!!)
        ///</summary>
        ///<param name="tableName">table to clear</param>
        /// <returns>rows affected</returns>
        public static int ClearTable(string tableName)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete ALL data from table {tableName}?",
                                                        $"Wipe ALL data from {tableName}?",
                                                        MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("DELETE operation aborted");
                return -2;
            }

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"delete from {tableName}";

            int rowsAffected = -2;

            try
            {
                cn.Open();

                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Executes query directly against DB (!!!)
        /// </summary>
        /// <param name="query">Query itself</param>
        /// <returns>rows affected</returns>
        public static int DirectQuery(string query)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            
            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"{query}";

            int rowsAffected = - 2;

            DialogResult dialogResult = MessageBox.Show($"{query}",
                                                        $"Execute?",
                                                        MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Aborted");
                return -2;
            }

            try
            {
                cn.Open();
                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Executes query directly against DB (!!!)
        /// </summary>
        /// <param name="query">Query itself</param>
        /// <returns>List of rows</returns>
        public static List<string> DirectQueryR(string query)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader reader;

            List<string> outList = new List<string>();
            string s = "";

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"{query}";

            DialogResult dialogResult = MessageBox.Show($"{query}",
                                                        $"Execute?",
                                                        MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Aborted");
                return null;
            }

            try
            {
                cn.Open();
                reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        s += " || " + reader[i].ToString() + " || ";
                    }
                    outList.Add(s);
                    s = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return outList;
        }


        private static T GetObjectFromReader<T>(T type, ref SqlDataReader reader)
        {
            switch (type)
            {
                case Manager m:
                    return (T)Convert.ChangeType(new Manager(reader.GetInt32(0),
                                                             reader.GetString(1),
                                                             reader.GetString(2)), typeof(T));
                case Client c:
                    return (T)Convert.ChangeType(new Client(reader.GetInt32(0),
                                                            reader.GetString(1),
                                                            reader.GetBoolean(2),
                                                            reader.GetString(3)), typeof(T));
                case Product p:

                    int? n;
                    if(reader.IsDBNull(4))
                    {
                        n = null;
                    }
                    else
                    {
                        n = reader.GetInt32(4);
                    }

                    int statusIndex = reader.GetOrdinal("PERIOD");
 

                    return (T)Convert.ChangeType(new Product(reader.GetInt32(0),
                                                             reader.GetString(1),
                                                             reader.GetDecimal(2),
                                                             reader.GetBoolean(3),
                                                             n), typeof(T));
                default:
                    throw new ArgumentException("Invalid object Type. Cannot instantiate object.");
            }
        }
    }
}