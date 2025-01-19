using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;

static class BookingService
{
  public class Booking
  {
    public int id { get; set; } = 0;
    public string idString
    {
      get { return id.ToString(); }
      set { id = int.Parse(value); }
    }
    [Required(ErrorMessage = "Hotel is required")]
    public int hotelId { get; set; }
    public string hotelName { get; set; }
    public string status { get; set; }
    
    public Booking(int _id, int _hotelId, string _hotelName, string _status)
    {
      id = _id;
      hotelId = _hotelId;
      hotelName = _hotelName;
      status = _status;
    }
  };

  public class Room
  {
    public int id { get; set; } = 0;
    public string idString
    {
      get { return id.ToString(); }
      set { id = int.Parse(value); }
    }
    public int bookingId { get; set; } = 0;
    public string type { get; set; } = "";
    public DateOnly dateStart { get; set; } = new DateOnly();
    public int capacity { get; set; } = 1;
    public int nights { get; set; } = 1;

    public Room(int _id, int _bookingId, string _type, DateOnly _dateStart, int _capacity, int _nights)
    {
      id = _id;
      bookingId = _bookingId;
      type = _type;
      dateStart = _dateStart;
      capacity = _capacity;
      nights = _nights;
    }
  }

  public static void Create(Booking booking)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"INSERT INTO booking (hotel_id, status) VALUES ($hotel_id, $status)";
      command.Parameters.AddWithValue("$hotel_id", booking.hotelId);
      command.Parameters.AddWithValue("$status", booking.status);

      var result = command.ExecuteNonQuery();
    }
  }

  public static Booking Get(int id)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"SELECT hotel_id, hotel.name AS hotel_name, status FROM booking LEFT JOIN hotel ON booking.hotel_id = hotel.id WHERE booking.id = $id";
      command.Parameters.AddWithValue("$id", id);

      var reader = command.ExecuteReader();

      if (reader.Read())
      {
        Booking booking = new(id, reader.GetInt32(0), reader.GetString(1), reader.GetString(1));
        return booking;
      }
      else
      {
        return new Booking(0, 0, "", "Provisional");
      }
    }
  }

  public static void Update(Booking booking)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = @"UPDATE booking SET hotel_id = $hotel_id, status = $status WHERE id = $id";
      command.Parameters.AddWithValue("$id", booking.id);
      command.Parameters.AddWithValue("$hotel_id", booking.hotelId);
      command.Parameters.AddWithValue("$status", booking.status);
      var result = command.ExecuteNonQuery();
    }
  }

  public static void Delete(int id)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = @"DELETE FROM booking WHERE id = $id";
      command.Parameters.AddWithValue("$id", id);
      var result = command.ExecuteNonQuery();
    }
  }

}
