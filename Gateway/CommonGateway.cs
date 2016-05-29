using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class CommonGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UniversityManagementSystem"].ConnectionString;
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public string Query { get; set; }

        public CommonGateway()
        {
            Connection = new SqlConnection(connectionString);
        }
    }
}