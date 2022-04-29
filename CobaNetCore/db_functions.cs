using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace CobaNetCore
{
    public class db_functions
    {
        private IConfiguration configuration;
        private MySqlConnection db;

        public db_functions(IConfiguration _configuration)
        {
            configuration = _configuration;
            string constr = configuration.GetConnectionString("dev_sd7");
            db = new MySqlConnection(constr);
        }

        public string GetEmployee()
        {
            string sqlQuery = "";
            string temp = "";
            DataTable dt = new DataTable();

            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                sqlQuery = "SELECT id, name, address, salary FROM employees";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, db);
                MySqlDataAdapter mda = new MySqlDataAdapter(sqlCommand);
                mda.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    temp += dt.Rows[i]["id"].ToString() + "|" + dt.Rows[i]["name"].ToString() + "|" + dt.Rows[i]["address"].ToString() + "|" + dt.Rows[i]["salary"].ToString();
                    if (i < dt.Rows.Count - 1) temp += '\n';
                }

                return temp;
            }
            catch(Exception ex)
            {
                return "";
            }
            finally
            {
                db.Close();
            }
        }

        public bool InsertEmployee(string name, string address, int salary)
        {
            string sqlQuery = "";

            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                sqlQuery = "INSERT INTO employees(name, address, salary) ";
                sqlQuery += "VALUES(@name,@address,@salary);";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, db);
                sqlCommand.Parameters.AddWithValue("@name", name);
                sqlCommand.Parameters.AddWithValue("@address", address);
                sqlCommand.Parameters.AddWithValue("@salary", salary);
                if(sqlCommand.ExecuteNonQuery() > 0) return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        public bool UpdateEmployee(string id, string name, string address, int salary)
        {
            string sqlQuery = "";

            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                sqlQuery = "UPDATE employees ";
                sqlQuery += "SET name = @name, address = @address, salary = @salary ";
                sqlQuery += "WHERE id = @id;";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, db);
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@name", name);
                sqlCommand.Parameters.AddWithValue("@address", address);
                sqlCommand.Parameters.AddWithValue("@salary", salary);
                if (sqlCommand.ExecuteNonQuery() > 0) return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        public bool DeleteEmployee(string id)
        {
            string sqlQuery = "";

            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                sqlQuery = "DELETE FROM employees ";
                sqlQuery += "WHERE id = @id;";
                MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, db);
                sqlCommand.Parameters.AddWithValue("@id", id);
                if (sqlCommand.ExecuteNonQuery() > 0) return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
