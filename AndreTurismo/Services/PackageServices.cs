using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismo.Models;

namespace AndreTurismo.Services
{
    public class PackageServices
    {
        readonly string strofconn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\DataBase\reservation.mdf;";
        readonly SqlConnection conn;

        public PackageServices()
        {
            conn = new SqlConnection(strofconn);
            conn.Open();
        }
        public bool InsertPack(Package pack)
        {
            bool status;
            try
            {
                string strInsert = "insert into Package (Value) values (@Value)";
                SqlCommand commandInsert = new(strInsert, conn);
                commandInsert.Parameters.Add(new SqlParameter("@IdHotel", InsertH(pack)));
                commandInsert.Parameters.Add(new SqlParameter("@Value", pack.Value));
            }catch (Exception) 
            {
                status = false;
                throw;
            }
            return true;
        }

        private int InsertH(Package pack)
        {
            string strInsert = @"insert into Hotel (Name, Value) values (@Name, @Value)";
            SqlCommand commandInsert = new SqlCommand(strInsert, conn);
            commandInsert.Parameters.Add(new SqlParameter("@Name", pack.Hotel.Name));
            commandInsert.Parameters.Add(new SqlParameter("@Value", pack.Hotel.Value));

            return (int) commandInsert.ExecuteScalar();
        }
    }
}

