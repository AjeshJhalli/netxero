using Microsoft.Data.Sqlite;

class Hotel
{
  public Hotel(int id)
  {
    if (id == 0)
    {
      using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
      {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO person (first_name, last_name, email_address, phone_number) VALUES ($first_name, $last_name, $email_address, $phone_number)";
        command.Parameters.AddWithValue("$first_name", "");
        command.Parameters.AddWithValue("$last_name", "");
        command.Parameters.AddWithValue("$email_address", "");
        command.Parameters.AddWithValue("$phone_number", "");

        var result = command.ExecuteNonQuery();
      }
    }
    else
    {
      // Find existing one
    }
  }
}