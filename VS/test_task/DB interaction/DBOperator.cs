using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using test_task.Classes;
using test_task.Models;

namespace test_task.DB
{
    public static class DBOperator
    {
        public static string ConnectionString { get; set; }

        public static void Initialize()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }



        public static List<T> GetObjects<T>(T type,
                                            string select = "*",
                                            string where = null,
                                            string orderBy = null,
                                            string groupBy = null)
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
                MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
            }
            finally
            {
               cn.Close(); 
            }

        return outObjects;
        }

        public static List<Product> GetProductsByClient(int client_ID,
                                                        string select = "PL.*",
                                                        string where = null,
                                                        string orderBy = null,
                                                        string groupBy = null)
        {
            List<Product> outObjects = new List<Product>();
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader reader;

            if (!(orderBy is null))
            {
                orderBy = $"order by + {orderBy}";
            }
            if (!(groupBy is null))
            {
                groupBy = $"group by + {groupBy}";
            }

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"select {select} " +
                                $"from PRODUCTS PL " +
                                $"join CLIENTS_PRODUCTS_LINK CPL on PL.ID = CPL.PRODUCT_ID " +
                                $"where CLIENT_ID = {client_ID} {where}" +
                                $"{orderBy} " +
                                $"{groupBy}";

            try
            {
                cn.Open();
                reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int? n;
                        if (reader.IsDBNull(4))
                        {
                            n = null;
                        }
                        else
                        {
                            n = reader.GetInt32(4);
                        }


                        outObjects.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetBoolean(3), n));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
            }
            finally
            {
                cn.Close();
            }

            return outObjects;
        }

        public static List<Client> GetClientsByManager(int manager_ID,
                                                       string select = "CL.*",
                                                       string where = null,
                                                       string orderBy = null,
                                                       string groupBy = null)
        {
            List<Client> outObjects = new List<Client>();
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader reader;

            if (!(orderBy is null))
            {
                orderBy = $"order by + {orderBy}";
            }
            if (!(groupBy is null))
            {
                groupBy = $"group by + {groupBy}";
            }

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"select {select}" +
                                $" from CLIENTS CL " +
                                $"join CLIENTS_MANAGERS_LINK CML on CL.ID = CML.CLIENT_ID " +
                                $"where MANAGER_ID = {manager_ID} {where}" +
                                $"{orderBy} " +
                                $"{groupBy}";

            try
            {
                cn.Open();
                reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        outObjects.Add(new Client(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetString(3)));
                    }                             
                }                                 
                                                  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
            }
            finally
            {
                cn.Close();
            }

            return outObjects;
        }

        public static List<Client> GetClientsByProduct(int product_ID,
                                                       string select = "CL.*",
                                                       string where = null,
                                                       string orderBy = null,
                                                       string groupBy = null)
        {
            List<Client> outObjects = new List<Client>();
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader reader;

            if (!(orderBy is null))
            {
                orderBy = $"order by + {orderBy}";
            }
            if (!(groupBy is null))
            {
                groupBy = $"group by + {groupBy}";
            }

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"select {select}" +
                                $" from CLIENTS CL" +
                                $" join CLIENTS_PRODUCTS_LINK CPL on CL.ID = CPL.CLIENT_ID" +
                                $" where PRODUCT_ID = {product_ID} {where}" +
                                $" {orderBy}" +
                                $" {groupBy}";

            try
            {
                cn.Open();
                reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        outObjects.Add(new Client(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetString(3)));
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
            }
            finally
            {
                cn.Close();
            }

            return outObjects;
        }
        
        public static Manager GetManagerByClient(int client_ID)
        {
            Manager outObject = new Manager(); ;
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader reader;

            
            sqlCmd.Connection = cn;
            sqlCmd.CommandText = " select * " +
                                 " from MANAGERS MAN" +
                                 " join CLIENTS_MANAGERS_LINK CML on MAN.ID = CML.MANAGER_ID" +
                                $" where CLIENT_ID = {client_ID}";

            try
            {
                cn.Open();
                reader = sqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    outObject = new Manager(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
            }
            finally
            {
                cn.Close();
            }

            return outObject;
        }



        public static int InsertData<T>(List<T> data, bool showMessage = false)
        {
            if (data is null && (data.Count < 1))
            {
                throw new ArgumentNullException($"data {typeof(T)} was null");
            }
            else if (!(data[0] as IInsertNotNull).Edited())
            {
                throw new ArgumentNullException($"data {typeof(T)} was null!");
            }

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            string table, values;
            int rowsAffected = -2;

            switch (data[0])
            {
                case Manager m:
                    table = "MANAGERS";
                    values = "('" + (data[0] as Manager).Name + "', '" 
                        + (data[0] as Manager).Comment + "')";
                    if (data.Count > 0)
                    {
                        for (int i = 1; i < data.Count; i++)
                        {
                            values += ", ('"
                                + (data[i] as Manager).Name + "', '" 
                                + (data[i] as Manager).Comment + "')";
                        }
                    }
                    break;

                case Client c:
                    table = "CLIENTS";
                    values = "('"
                        + (data[0] as Client).Name + "', " 
                        + Convert.ToInt32((data[0] as Client).PriorClient) +", '" 
                        + (data[0] as Client).Comment + "')";
                    if (data.Count > 0)
                    {
                        for (int j = 1; j < data.Count; j++)
                        {
                            values += ", ('" 
                                + (data[j] as Client).Name + "', "
                                + Convert.ToInt32((data[j] as Client).PriorClient) + ", '" 
                                + (data[j] as Client).Comment + "')";
                        }
                    }
                    break;

                case Product p:
                    string per;
                    table = "PRODUCTS";
                    if (!(data[0] as Product).Subscribtion)
                    {
                        per = "NULL";
                        (data[0] as Product).Period = null;
                    }
                    else
                    {
                        per = Convert.ToString((data[0] as Product).Period);
                    }

                    values = "('"
                        + (data[0] as Product).Name + "', " 
                        + (data[0] as Product).Price + ", " 
                        + Convert.ToInt32((data[0] as Product).Subscribtion) + ", " 
                        + per + ")";
                    if (data.Count > 0)
                    {
                        for (int k = 1; k < data.Count; k++)
                        {
                            if (!(data[k] as Product).Subscribtion)
                            {
                                per = "NULL";
                                (data[k] as Product).Period = null;
                            }
                            else
                            {
                                per = Convert.ToString((data[0] as Product).Period);
                            }

                            values += ", ('"
                                + (data[k] as Product).Name + "', " 
                                + (data[k] as Product).Price + ", " 
                                + Convert.ToInt32((data[k] as Product).Subscribtion) + ", " 
                                + per + ")";
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
                if (showMessage)
                {
                    MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
                }
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
            catch (Exception)// ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;

        }

        public static int DeleteObject<T>(T objectToRemove, string where = null)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            string table;
            string reference;
            int rowsAffected = -2;

            switch (objectToRemove)
            {
                case Manager m:
                    table = "MANAGERS";
                    if (where is null)
                    {
                        where = "ID = " + (objectToRemove as Manager).ID;
                    }
                    reference = $"delete from CLIENTS_MANAGERS_LINK where MANAGER_ID = {(objectToRemove as Manager).ID};";
                    break;

                case Client c:
                    table = "CLIENTS";
                    if (where is null)
                    {
                        where = "ID = " + (objectToRemove as Client).ID;
                    }
                    reference = $"delete from CLIENTS_PRODUCTS_LINK where CLIENT_ID = {(objectToRemove as Client).ID};" +
                                $"delete from CLIENTS_MANAGERS_LINK where CLIENT_ID = {(objectToRemove as Client).ID};";
                    break;

                case Product p:
                    table = "PRODUCTS";
                    if (where is null)
                    {
                        where = "ID = " + (objectToRemove as Product).ID;
                    }
                    reference = $"delete from CLIENTS_PRODUCTS_LINK where PRODUCT_ID = {(objectToRemove as Product).ID}";
                    break;

                default:
                    throw new ArgumentException("Invalid object Type. DELETE query aborted.");
            }
            
            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $";{reference} DELETE from {table} where {where} ";

            try
            {
                cn.Open();

                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;
        }


        /// <summary>
        /// Add/Remove relation between two objects in CLIENTS and MANAGER/PRODUCTS
        /// </summary>
        /// <param name="T">Manager/Client/Product</param>
        /// <param name="client_ID">Client</param>
        /// <param name="relType">Dummy type for table recognition</param>
        /// <param name="remove">Remove query executed if true</param>
        /// <param name="both">Remove query executed against both link tables if remove=true and both=true</param>
        /// <returns>Rows affected by query (not valid for both = true)</returns>
        public static int Relation<T>(int client_ID, T relType, bool remove = false, bool both = false)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            string table, insertText, deleteText, identify;
            int rowsAffected = -2;
            if (!remove)
            {
                switch (relType)
                {
                    case Manager _:
                        table = "CLIENTS_MANAGERS_LINK";
                        deleteText = $"delete from {table} where CLIENT_ID = {client_ID};";
                        insertText = $" insert into {table} values({(relType as Manager).ID}, {client_ID})";

                        break;

                    case Product _:
                        table = "CLIENTS_PRODUCTS_LINK";
                        deleteText = $"delete from {table} where CLIENT_ID = {client_ID} and PRODUCT_ID = {(relType as Product).ID};";
                        insertText = $" insert into {table} values({client_ID}, {(relType as Product).ID})";
                        break;

                    default:
                        throw new ArgumentException("Invalid object Type. INSERT (relation) query aborted."); 
                }
                sqlCmd.CommandText = deleteText + insertText;
            }
            else
            {
                if (!both)
                {
                    switch (relType)
                    {
                        case Manager _:
                            table = "CLIENTS_MANAGERS_LINK";
                            identify = $"MANAGER_ID = {(relType as Manager).ID}";
                            break;

                        case Product _:
                            table = "CLIENTS_PRODUCTS_LINK";
                            identify = $"PRODUCT_ID = {(relType as Product).ID}";
                            break;

                        default:
                            throw new ArgumentException("Invalid object Type. DELETE (relation) query aborted.");
                    }
                    sqlCmd.CommandText = $"delete from {table} where CLIENT_ID = {client_ID} and {identify}"; 
                }
                else
                {
                    sqlCmd.CommandText = $"delete from CLIENTS_MANAGERS_LINK where CLIENT_ID = {client_ID};" +
                                         $" \n" + $"delete from CLIENTS_PRODUCTS_LINK where CLIENT_ID = {client_ID}";
                }
                
            }

            sqlCmd.Connection = cn;

            try
            {
                cn.Open();

                rowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;
        }    


        public static int InsertContact(Contact contact, int client_ID, bool showMessage = false)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlInsertCmd = new SqlCommand();
            SqlCommand sqlClearCmd = new SqlCommand();
            int rowsAffected = -2;
            
            sqlClearCmd.Connection = cn;
            sqlClearCmd.CommandText = $"delete from CONTACTS where CLIENT_ID = {client_ID};";

            string tel;
            if (contact.Tel is null)
            {
                tel = "NULL";
            }
            else
            {
                tel = contact.Tel.Value.ToString();
            }

            sqlInsertCmd.Connection = cn;
            sqlInsertCmd.CommandText = "insert into CONTACTS values (" +
                                        $" '{contact.FirstName}'," +
                                        $" '{contact.LastName}' ," +
                                        $"{tel}," +
                                        $" '{contact.Email}' ," +
                                        $" '{contact.Comment}' ," +
                                        $"{client_ID})";

            try
            {
                cn.Open();
                sqlClearCmd.ExecuteNonQuery();
                rowsAffected = sqlInsertCmd.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                if (showMessage)
                {
                    MessageBox.Show(ex.Message
                        + "\n"
                        + sqlClearCmd.CommandText
                        + "\n"
                        + sqlInsertCmd.CommandText);
                }
            }
            finally
            {
                cn.Close();
            }

            return rowsAffected;
        }

        public static Contact GetContact(int client_ID)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand
            {
                Connection = cn,
                CommandText = $"select * from CONTACTS where CLIENT_ID = {client_ID} order by ID offset 0 rows fetch next 1 row only"
            };

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
        public static int DirectQuery(string query, bool showMessage = false)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            
            sqlCmd.Connection = cn;
            sqlCmd.CommandText = query;

            int rowsAffected = - 2;

            DialogResult dialogResult;

            if (showMessage)
            {
                dialogResult = MessageBox.Show($"{query}", $"Execute?", MessageBoxButtons.YesNo);
            }
            else
            {
                dialogResult = DialogResult.Yes;
            }

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
                if (showMessage)
                {
                    MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
                }
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
        public static List<string> DirectQueryR(string query, bool showMessage = true)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader reader;

            List<string> outList = new List<string>();
            string s = "";

            sqlCmd.Connection = cn;
            sqlCmd.CommandText = $"{query}";

            DialogResult dialogResult;

            if (showMessage)
            {
                dialogResult = MessageBox.Show($"{query}", $"Execute?", MessageBoxButtons.YesNo);
            }
            else
            {
                dialogResult = DialogResult.Yes;
            }

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
                        s += "||" + reader[i].ToString() + "||";
                    }
                    outList.Add(s);
                    s = "";
                }

            }
            catch (Exception ex)
            {
                if (showMessage)
                {
                    MessageBox.Show(ex.Message + "\n" + sqlCmd.CommandText);
                }
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