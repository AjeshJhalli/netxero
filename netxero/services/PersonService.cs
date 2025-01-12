using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Sqlite;
using netxero.Components.Pages;

static class PersonService
{
  public class Person
  {
    public int id { get; set; } = 0;
    public string idString
    {
      get { return id.ToString(); }
      set { id = int.Parse(value); }
    }
    [Required(ErrorMessage = "First Name is required")]
    public string firstName { get; set; }
    [Required(ErrorMessage = "Last Name is required")]
    public string lastName { get; set; }
    [Required(ErrorMessage = "Email Address is required")]
    public string emailAddress { get; set; }
    public string phoneNumber { get; set; }
    public Person(int i, string fn, string ln, string ea, string pn)
    {
      id = i;
      firstName = fn;
      lastName = ln;
      emailAddress = ea;
      phoneNumber = pn;
    }
  };

  public static void Create(Person person)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"INSERT INTO person (first_name, last_name, email_address, phone_number) VALUES ($first_name, $last_name, $email_address, $phone_number)";
      command.Parameters.AddWithValue("$first_name", person.firstName);
      command.Parameters.AddWithValue("$last_name", person.lastName);
      command.Parameters.AddWithValue("$email_address", person.emailAddress);
      command.Parameters.AddWithValue("$phone_number", person.phoneNumber);

      var result = command.ExecuteNonQuery();
    }
  }

  public static Person Get(int id)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"SELECT first_name, last_name, email_address, phone_number FROM person WHERE id = $id";
      command.Parameters.AddWithValue("$id", id);

      var reader = command.ExecuteReader();

      if (reader.Read())
      {
        PersonService.Person person = new(id, reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
        return person;
      }
      else
      {
        // If no person is found, return new one
        return new Person(0, "", "", "", "");
      }
    }
  }

  public static void Update(Person person)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = @"UPDATE person SET first_name = $first_name, last_name = $last_name, email_address = $email_address, phone_number = $phone_number WHERE id = $id";
      command.Parameters.AddWithValue("$id", person.id);
      command.Parameters.AddWithValue("$first_name", person.firstName);
      command.Parameters.AddWithValue("$last_name", person.lastName);
      command.Parameters.AddWithValue("$email_address", person.emailAddress);
      command.Parameters.AddWithValue("$phone_number", person.phoneNumber);
      var result = command.ExecuteNonQuery();
    }
  }

  public static void Delete(int id)
  {
    using (var connection = new SqliteConnection("Data Source=databases/HotelRoomAllocator.sqlite"))
    {
      connection.Open();
      var command = connection.CreateCommand();
      command.CommandText = @"DELETE FROM person WHERE id = $id";
      command.Parameters.AddWithValue("$id", id);
      var result = command.ExecuteNonQuery();
    }
  }


}
