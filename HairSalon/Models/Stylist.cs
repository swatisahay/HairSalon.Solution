using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _stylistName;
    private int _id;
    private string _stylistDetail;

    public Stylist(string Name, string StylistDetail , int Id = 0)
    {
      _stylistName = Name;
      _stylistDetail = StylistDetail;
      _id = Id;

    }
    public string GetStylistName()
    {
      return _stylistName;
    }
    public void SetStylistName(string Name)
    {
      _stylistName = Name;
    }
    public string GetStylistDetail()
    {
      return _stylistDetail;
    }
    public void SetStylistDetail(string StylistDetail)
    {
      _stylistDetail = StylistDetail;
    }
    public int GetId()
    {
      return _id;
    }
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetStylistName() == newStylist.GetStylistName());
        bool stylistDetailEquality = (this.GetStylistDetail() == newStylist.GetStylistDetail());
        return (idEquality && nameEquality && stylistDetailEquality);
      }
    }
    public static List<Stylist> GetAll()
    {
      // return _instances;
      List<Stylist> allStylist = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistDetail = rdr.GetString(2);

        Stylist newStylist = new Stylist(stylistName, stylistDetail, stylistId);
        allStylist.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylist;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";

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
      cmd.CommandText = @"INSERT INTO stylists (stylist_name, stylist_detail) VALUES (@stylistName, @stylistDetail);";

      cmd.Parameters.Add(new MySqlParameter("@stylistName", _stylistName));
      cmd.Parameters.Add(new MySqlParameter(" @stylistDetail", _stylistDetail));


      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;    // This line is new!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int stylistId = 0;
      string stylistName = "";
      string stylistDetail = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
        stylistDetail = rdr.GetString(2);
      }
      Stylist foundStylist = new Stylist(stylistName, stylistDetail, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }
    public  List<Client> GetClients()
    {
      List<Client> allClientDetails = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int ClientStylistId = rdr.GetInt32(2);

      Client newClient = new Client(clientName, ClientStylistId, clientId);
        allClientDetails.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClientDetails;

    }
    public void Edit(string newStylist)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"UPDATE stylists SET stylist_name = @newStylistName WHERE id = @searchId;";

    cmd.Parameters.Add(new MySqlParameter("@searchId", _id));
    cmd.Parameters.Add(new MySqlParameter("@newStylistName", newStylist));

    cmd.ExecuteNonQuery();
    _stylistName = newStylist;

    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
  }
  public void Delete()
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

    cmd.Parameters.Add(new MySqlParameter("@thisId", _id));

    cmd.ExecuteNonQuery();
    if (conn != null)
    {
      conn.Close();
    }
  }
  public void AddSpeciality(Speciality newSpeciality)
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO stylist_speciality (stylist_id, speciality_id) VALUES (@StylistId, @SpecialityId);";

    cmd.Parameters.Add(new MySqlParameter("@StylistId", _id));
    cmd.Parameters.Add(new MySqlParameter("@SpecialityId", newSpeciality.GetSpecialityId()));


    cmd.ExecuteNonQuery();
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
  }

  public List<Speciality> GetSpecialities()
  {
    MySqlConnection conn = DB.Connection();
    conn.Open();
    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT specialities.* FROM stylists
    JOIN stylist_speciality ON (stylists.id = stylist_speciality.stylist_id)
    JOIN specialities ON (stylist_speciality.speciality_id = specialities.id)
    WHERE stylists.id = @stylistId;";

    cmd.Parameters.Add(new MySqlParameter("@stylistId", _id));

    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    List<Speciality> specialities = new List<Speciality>{};

    while(rdr.Read())
    {
      int specialityId = rdr.GetInt32(0);
      string specialityName = rdr.GetString(1);
      Speciality newSpeciality = new Speciality(specialityName, specialityId);
      specialities.Add(newSpeciality);
    }
    conn.Close();
    if (conn != null)
    {
      conn.Dispose();
    }
    return specialities;
    // return new List<Student>{};
  }
  }
}
