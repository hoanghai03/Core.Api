using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Postgre.Providers
{
    public class PostgreProvider : IDatabaseProvider
    {
        public NpgsqlConnection GetConnection(string cnnString)
        {
            return new NpgsqlConnection(cnnString);
        }
    }
}
