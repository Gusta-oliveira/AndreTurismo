using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismo.Models;

namespace AndreTurismo.Services
{
    public class ClientServices
    {
        readonly string strconn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\DataBase\fly.mdf;";
        readonly SqlConnection conn;
        public ClientServices()
        {
            conn = new SqlConnection(strconn);
            conn.Open();
        }
        public bool InsertClient(Client client)
        {
            bool status = false;
            try
            {
                string strInsert = "insert into Client (Name, Phone, Date_Cad) values (@Name, @Phone, @Date_Cad)";
                SqlCommand commandInsert = new SqlCommand(strInsert, conn);
                commandInsert.Parameters.Add(new SqlParameter("@Name", client.Name));
                commandInsert.Parameters.Add(new SqlParameter("@Phone", client.Phone));
                commandInsert.Parameters.Add(new SqlParameter("@Date_Cad", client.DateCadastre));
                commandInsert.Parameters.Add(new SqlParameter("@IdAddress", InsertAddress(client)));
            }
            catch (Exception)
            {
                status = false;
            }

            return status;
        }
        private int InsertAddress(Client client)
        {
            string strInsert = "insert into Address (Street, Number, Neighborhood, CEP, Complement) " +
                " values (@Street, @Number, @Neighborhood, @CEP, @Complement); select cast(scope_identity() as int)";
            SqlCommand commandInsert = new SqlCommand(strInsert, conn);
            commandInsert.Parameters.Add(new SqlParameter("@Street", client.Address.Street));
            commandInsert.Parameters.Add(new SqlParameter("@Number", client.Address.Number));
            commandInsert.Parameters.Add(new SqlParameter("@Neighborhood", client.Address.Neighborhood));
            commandInsert.Parameters.Add(new SqlParameter("@CEP", client.Address.CEP));
            commandInsert.Parameters.Add(new SqlParameter("@Complement", client.Address.Complement));
           
            return (int) commandInsert.ExecuteScalar();
        }
        public bool UpdateClient(Client client, string update)
        {
            string strUpdate = "update Client set " + update + " where ";
            return true;
        }
        public int DeleteClient(int id)
        {
            string strdelete = "delete from Client where id = @id";
            SqlCommand commandDelete = new SqlCommand(strdelete, conn);
            commandDelete.Parameters.Add(new SqlParameter("@id", id));

            return commandDelete.ExecuteNonQuery();
        }
        public List<Client> FindAllClient()
        {
            List<Client> clients = new();

            StringBuilder sb = new StringBuilder();
            sb.Append("   select c.Id,");
            sb.Append("   c.Name,");
            sb.Append("   c.Phone,");
            sb.Append("   a.Street Address ");
            sb.Append("   a.Number Address ");
            sb.Append("   a.Neighborhood Address ");
            sb.Append("   a.CEP Address ");
            sb.Append("   a.Complement Address ");
            sb.Append(" from Client c, ");
            sb.Append("      Address a ");
            sb.Append(" where c.IdAddress = a.Id");

            SqlCommand commandSelect = new(sb.ToString(), conn);
            SqlDataReader dr = commandSelect.ExecuteReader();

            while (dr.Read())
            {
                Client client = new();

                client.Id = (int)dr["Id"];
                client.Name = (string)dr["Name"];
                client.Phone = (string)dr["Phone"];
                client.Address = new Address() { Street = (string)dr["Street"],
                                                 Number = (int)dr["Number"],
                                                 Neighborhood = (string)dr["Neighborhood"],
                                                 CEP = (string)dr["CEP"],
                                                 Complement = (string)dr["Complement"]
                };
                clients.Add(client);
            }
            return clients;
        }
    }
}
