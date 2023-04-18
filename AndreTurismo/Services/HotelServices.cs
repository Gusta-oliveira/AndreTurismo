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
    public class HotelServices
    {
        readonly string strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\DataBase\fly.mdf;";
        readonly SqlConnection conn;
        public HotelServices()
        {
            conn = new SqlConnection(strConn);
            conn.Open();
        }
        public bool InsertHotel(Hotel hotel)
        {
            bool status = false;
            try
            {
                string strInsert = @"insert into Hotel (Name, Value) values (@Name, @Value)";
                SqlCommand commandInsert = new SqlCommand(strInsert, conn);

                commandInsert.Parameters.Add(new SqlParameter("@Name", hotel.Name));
                commandInsert.Parameters.Add(new SqlParameter("@Value", hotel.Value));
                commandInsert.Parameters.Add(new SqlParameter("@IdAddress", InsertAddress(hotel)));

            }catch(Exception)
            {
                status = false;
                throw;
            }
            return status;
        }

        private int InsertAddress(Hotel hotel)
        {
            string strInsert = "insert into Address (Street, Number, Neighborhood, CEP, Complement) " +
                " values (@Street, @Number, @Neighborhood, @CEP, @Complement); select cast(scope_identity() as int)";
            SqlCommand commandInsert = new SqlCommand(strInsert, conn);
            commandInsert.Parameters.Add(new SqlParameter("@Street", hotel.Address.Street));
            commandInsert.Parameters.Add(new SqlParameter("@Number", hotel.Address.Number));
            commandInsert.Parameters.Add(new SqlParameter("@Neighborhood", hotel.Address.Neighborhood));
            commandInsert.Parameters.Add(new SqlParameter("@CEP", hotel.Address.CEP));
            commandInsert.Parameters.Add(new SqlParameter("@Complement", hotel.Address.Complement));

            return (int) commandInsert.ExecuteScalar();
        }
        public List<Hotel> FindAllHotel()
        {
            List<Hotel> hotels = new();

            StringBuilder sb = new StringBuilder();
            sb.Append("   select h.Id,");
            sb.Append("   h.Name,");
            sb.Append("   h.Street Address ");
            sb.Append("   h.Number Address ");
            sb.Append("   h.Neighborhood Address ");
            sb.Append("   h.CEP Address ");
            sb.Append("   h.Complement Address ");
            sb.Append("   h.Checking");
            sb.Append("   h.Value");
            sb.Append(" from Client c, ");
            sb.Append("      Address a ");
            sb.Append(" where c.IdAddress = a.Id");

            SqlCommand commandSelect = new(sb.ToString(), conn);
            SqlDataReader dr = commandSelect.ExecuteReader();

            while (dr.Read())
            {
                Hotel hotel = new();

                hotel.Id = (int)dr["Id"];
                hotel.Name = (string)dr["Name"];
                hotel.Address = new Address()
                {
                    Street = (string)dr["Street"],
                    Number = (int)dr["Number"],
                    Neighborhood = (string)dr["Neighborhood"],
                    CEP = (string)dr["CEP"],
                    Complement = (string)dr["Complement"]
                };
                hotel.Checking = (DateTime)dr["DataCadastre"];
                hotel.Value = (decimal)dr["Value"];
                hotels.Add(hotel);
            }
            return hotels;
        }
    }
}
