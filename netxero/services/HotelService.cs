using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;

static class HotelService
{
  public class Hotel
  {
    public int id { get; set; } = 0;
    public string idString
    {
      get { return id.ToString(); }
      set { id = int.Parse(value); }
    }
    [Required(ErrorMessage = "Hotel Name is required")]
    public string name { get; set; }
    public string addressLine1 { get; set; }
    public string addressLine2 { get; set; }
    public string addressCity { get; set; }
    public string addressCounty { get; set; }
    public string addressCountry { get; set; }
    public string addressPostcode { get; set; }
    
    public Hotel(int _id, string _name, string _addressLine1, string _addressLine2, string _addressCity, string _addressCounty, string _addressCountry, string _addressPostcode)
    {
      id = _id;
      name = _name;
      addressLine1 = _addressLine1;
      addressLine2 = _addressLine2;
      addressCity = _addressCity;
      addressCounty = _addressCounty;
      addressCountry = _addressCountry;
      addressPostcode = _addressPostcode;
    }
  };

  public static void Create(Hotel hotel)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"INSERT INTO hotel (name, address_line1, address_line2, address_city, address_county, address_country, address_postcode) VALUES ($name, $address_line1, $address_line2, $address_city, $address_county, $address_country, $address_postcode)";
      command.Parameters.AddWithValue("$name", hotel.name);
      command.Parameters.AddWithValue("$address_line1", hotel.addressLine1);
      command.Parameters.AddWithValue("$address_line2", hotel.addressLine2);
      command.Parameters.AddWithValue("$address_city", hotel.addressCity);
      command.Parameters.AddWithValue("$address_county", hotel.addressCounty);
      command.Parameters.AddWithValue("$address_country", hotel.addressCountry);
      command.Parameters.AddWithValue("$address_postcode", hotel.addressPostcode);

      var result = command.ExecuteNonQuery();
    }
  }

  public static Hotel Get(int id)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"SELECT name, address_line1, address_line2, address_city, address_county, address_country, address_postcode FROM hotel WHERE id = $id";
      command.Parameters.AddWithValue("$id", id);

      var reader = command.ExecuteReader();

      if (reader.Read())
      {
        Hotel hotel = new(id, reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
        return hotel;
      }
      else
      {
        return new Hotel(0, "", "", "", "", "", "", "");
      }
    }
  }

  public static void Update(Hotel hotel)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = @"UPDATE hotel SET name = $name, address_line1 = $address_line1, address_line2 = $address_line2, address_city = $address_city, address_county = $address_county, address_country = $address_country, address_postcode = $address_postcode WHERE id = $id";
      command.Parameters.AddWithValue("$id", hotel.id);
      command.Parameters.AddWithValue("$name", hotel.name);
      command.Parameters.AddWithValue("$address_line1", hotel.addressLine1);
      command.Parameters.AddWithValue("$address_line2", hotel.addressLine2);
      command.Parameters.AddWithValue("$address_city", hotel.addressCity);
      command.Parameters.AddWithValue("$address_county", hotel.addressCounty);
      command.Parameters.AddWithValue("$address_country", hotel.addressCountry);
      command.Parameters.AddWithValue("$address_postcode", hotel.addressPostcode);
      var result = command.ExecuteNonQuery();
    }
  }

  public static void Delete(int id)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = @"DELETE FROM hotel WHERE id = $id";
      command.Parameters.AddWithValue("$id", id);
      var result = command.ExecuteNonQuery();
    }
  }

}
