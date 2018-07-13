using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    private string _clientName;
    private int _id;
    private int _stylistId;

    public Client(string Name, int StylistId , int Id = 0)
    {
      _clientName = Name;
      _stylistId = StylistId;
      _id = Id;

    }
    public string GetName()
    {
      return _clientName;
    }
    public void SetName(string Name)
    {
      _clientName = Name;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public void SetStylistId(int StylistId)
    {
      _stylistId = StylistId;
    }
    public int GetId()
    {
      return _id;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (idEquality && nameEquality && stylistIdEquality);
      }
    }
    public static List<Client> GetAll()
    {
      // return _instances;
      List<Client> allClient = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int ClientStylistId = rdr.GetInt32(2);

        Client newClient = new Client(clientName, ClientStylistId, clientId);
        allClient.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClient;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (client_name, stylist_id) VALUES (@clientName, @stylistId);";

      cmd.Parameters.Add(new MySqlParameter("@clientName", _clientName));
      cmd.Parameters.Add(new MySqlParameter(" @stylistId", _stylistId));


      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;    // This line is new!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Client Find(int id)
   {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";
     MySqlParameter thisId = new MySqlParameter();
       thisId.ParameterName = "@thisId";
       thisId.Value = id;
       cmd.Parameters.Add(thisId);
       var rdr = cmd.ExecuteReader() as MySqlDataReader;
       int clientId = 0;
       string clientName = "";
       int clientStylistId = 0;

       while (rdr.Read())
       {
         clientId = rdr.GetInt32(0);
         clientName = rdr.GetString(1);
         clientStylistId = rdr.GetInt32(2);
       }
       Client foundClient = new Client(clientName, clientStylistId, clientId);

     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return foundClient;
  }
  }
}
