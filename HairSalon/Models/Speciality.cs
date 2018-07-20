using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Speciality
  {
    private int _specialityId;
    private string _specialityName;

    public Speciality(string SpecialityName, int SpecialityId = 0)
    {
      _specialityId = SpecialityId;
      _specialityName = SpecialityName;
    }
    public int GetSpecialityId()
    {
      return _specialityId;
    }
    public string GetSpecialityName()
    {
      return _specialityName;
    }

    public override bool Equals(System.Object otherSpeciality)
    {
      if(!(otherSpeciality is Speciality))
      {
        return false;
      }
      else
      {
        Speciality newSpeciality = (Speciality) otherSpeciality;
        bool idEquality = this.GetSpecialityId().Equals(newSpeciality.GetSpecialityId());
        bool nameEquality = this.GetSpecialityName().Equals(newSpeciality.GetSpecialityName());
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetSpecialityId().GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialities (speciality) VALUES (@specialityName);";

      cmd.Parameters.Add(new MySqlParameter("@specialityName", _specialityName));

      cmd.ExecuteNonQuery();
      _specialityId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Speciality> GetAllSpecialitys()
    {
      List<Speciality> allSpecialitys = new List<Speciality> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int SpecialityId = rdr.GetInt32(0);
        string SpecialityName = rdr.GetString(1);
        Speciality newSpeciality = new Speciality(SpecialityName, SpecialityId);
        allSpecialitys.Add(newSpeciality);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialitys;
      // return new List<Speciality>{};
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialities; DELETE FROM stylist_speciality;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Speciality Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int SpecialityId = 0;
      string SpecialityName = "";

      while(rdr.Read())
      {
        SpecialityId = rdr.GetInt32(0);
        SpecialityName = rdr.GetString(1);
      }
      Speciality newSpeciality = new Speciality(SpecialityName, SpecialityId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      // return new Speciality("", "", 0);
      return newSpeciality;
    }
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylist_speciality (stylist_id, speciality_id) VALUES (@StylistId, @SpecialityId);";

      cmd.Parameters.Add(new MySqlParameter("@StylistId", newStylist.GetId()));
      cmd.Parameters.Add(new MySqlParameter("@SpecialityId", _specialityId));


      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialities
      JOIN stylist_speciality ON (specialities.id = stylist_speciality.speciality_id)
      JOIN stylists ON (stylist_speciality.stylist_id = stylists.id)
      WHERE specialities.id = @SpecialityId;";

      cmd.Parameters.Add(new MySqlParameter("@SpecialityId", _specialityId));

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistDetails = rdr.GetString(2);
        Stylist newStylist = new Stylist(stylistName, stylistDetails, stylistId);
        stylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
      // return new List<Stylist>{};
    }
    public void Edit(string newSpecialityName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialities SET speciality = @newSpecialityName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _specialityId));
      cmd.Parameters.Add(new MySqlParameter("@newSpecialityName", newSpecialityName));

      cmd.ExecuteNonQuery();
      _specialityName = newSpecialityName;

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
      cmd.CommandText = @"DELETE FROM specialities WHERE id = @SpecialityId; DELETE FROM stylist_speciality WHERE speciality_id = @SpecialityId;";

      cmd.Parameters.Add(new MySqlParameter("@SpecialityId", this.GetSpecialityId()));

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
