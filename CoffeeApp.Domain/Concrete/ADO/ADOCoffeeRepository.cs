using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Concrete.ADO
{
    public class ADOCoffeeRepository : ICoffeeRepository
    {
        private DbConnection conn;
        private ADODbContext context = new ADODbContext();
        private string connectionName;
        private List<Coffee> result = new List<Coffee>();

        public ADOCoffeeRepository()
        {
            this.connectionName = "ADODbContext";
        }

        public IQueryable<Coffee> Coffee
        {
            get
            {
                using (conn = context.GetConnection(connectionName))
                {
                    try
                    {
                        // Open the connection.
                        conn.Open();

                        // Create and execute the DbCommand.
                        DbCommand command = conn.CreateCommand();
                        command.CommandText = "SELECT * FROM Coffees";
                        DbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Coffee temp = new Coffee() { CoffeeID = (int)reader["CoffeeID"], Name = reader["Name"].ToString(), Description=reader["Description"].ToString(), Price = (decimal)reader["Price"] };
                            result.Add(temp);
                        }
                        return result.AsQueryable();

                        // Handle all other exceptions.
                    }
                    catch (DbException ex)
                    {
                        throw new Exception("Something went wrong with ADO " + ex.ErrorCode);
                    }
                }
            }
        }
    }
}