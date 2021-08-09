using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRMDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        /// <summary>
        /// Retrieves data from the sql database through stored prodcedures.
        /// </summary>
        /// <typeparam name="T"></typeparam> Generic Type.
        /// <typeparam name="U"></typeparam> Generic Type.
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }

        }


        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure);

                
            }

        }


    }
}
