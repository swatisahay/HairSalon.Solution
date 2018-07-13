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
        int ClientStylistId = rdr.GetInt32(4);

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
  }
}
