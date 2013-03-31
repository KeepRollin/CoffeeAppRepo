using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using CoffeeApp.Domain.Abstract;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Concrete.ADO
{
    public class ADOConfigurationRepository : IConfigurationRepository
    {
        private DbConnection conn;
        private string connectionName;
        private ADODbContext context = new ADODbContext();
        private List<Configuration> result = new List<Configuration>();

        public ADOConfigurationRepository()
        {
            this.connectionName = "ADODbContext";
        }

        public IQueryable<Configuration> Configuration
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
                        command.CommandText = "SELECT * FROM Configurations";
                        DbDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Configuration temp = new Configuration() { ConfigurationId = (int)reader[0], Name = reader[1].ToString(), Value = (int)reader[2] };
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