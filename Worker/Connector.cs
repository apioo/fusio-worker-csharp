
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Web;

namespace FusioWorker
{
    public class Connector
    {
        private FusioWorker.Connector.Connections connections;
        private Dictionary<string, object> instances = new Dictionary<string, object>();

        public Connector(FusioWorker.Connector.Connections connections)
        {
            this.connections = connections;
        }

        public object getConnection(string name)
        {
            if (this.instances.ContainsKey(name))
            {
                return this.instances[name];
            }

            if (!this.connections.containsKey(name))
            {
                throw new Exception("Provided connection is not configured");
            }

            Connection connection = this.connections[name];

            if (connection.Type.Equals("Fusio.Adapter.Sql.Connection.Sql"))
            {
                if (connection.Config["type"].Equals("pdo_mysql"))
                {
                    DbConnection con = this.NewSqlConnection("mysql", "server=" + connection.Config["host"] + ":3306;database=" + connection.Config["database"] + ";uid=" + connection.Config["username"] + ";pwd=" + connection.Config["password"]);

                    this.instances.Add(name, con);

                    return con;
                }
                else
                {
                    throw new Exception("SQL type is not supported");
                }
            }
            else if (connection.Type.Equals("Fusio.Adapter.Sql.Connection.SqlAdvanced"))
            {
                Uri url = new Uri(connection.Config["url"]);
                string connectionString = "server=" + url.Host;
                connectionString += ";database=" + url.AbsolutePath;

                var queryParams = HttpUtility.ParseQueryString(url.Query);
                connectionString += ";uid=" + queryParams.Get("");
                connectionString += ";pwd=" + queryParams.Get("");

                DbConnection con = this.NewSqlConnection(url.Scheme, connectionString);

                this.instances.Add(name, con);

                return con;
            }
            else if (connection.Type.Equals("Fusio.Adapter.Http.Connection.Http"))
            {
                HttpClient client = new HttpClient();

                // @TODO configure a base url so that the action can only make requests against this base url
                //connection.Config["url"];

                // @TODO configure proxy for http client
                //connection.Config["username"];
                //connection.Config["password"];
                //connection.Config["proxy"];

                this.instances.Add(name, client);

                return client;
            }
            else
            {
                throw new Exception("Provided a not supported connection type");
            }
        }

        private DbConnection NewSqlConnection(string type, string connectionString)
        {
            DbConnection con;

            if (type.Equals("mysql"))
            {
                con = new MySql.Data.MySqlClient.MySqlConnection();
                con.ConnectionString = connectionString;
                con.Open();
            }
            else
            {
                throw new Exception("Provided connection type is not supported");
            }

            return con;
        }
    }
}
