import sqlite3

conn = sqlite3.connect("./databases/HotelRoomAllocator.sqlite")
cur = conn.cursor();

cur.execute("drop table booking")
cur.execute("drop table room")
cur.execute("drop table allocation")

cur.execute("""CREATE TABLE IF NOT EXISTS person (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  creation_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
  first_name TEXT NOT NULL,
  last_name TEXT NOT NULL,
  email_address TEXT,
  phone_number TEXT
)""");

cur.execute("""CREATE TABLE IF NOT EXISTS hotel (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  creation_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
  name TEXT NOT NULL,
  address_line1 TEXT,
  address_line2 TEXT,
  address_city TEXT,
  address_county TEXT,
  address_country TEXT,
  address_postcode TEXT
)""");

cur.execute("""CREATE TABLE IF NOT EXISTS booking (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  creation_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
  hotel_id INTEGER,
  status TEXT
)""");

cur.execute("""CREATE TABLE IF NOT EXISTS room (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  creation_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
  booking_id INTEGER,
  type TEXT,
  date_start DATETIME,
  capacity INTEGER NOT NULL,
  nights INTEGER NOT NULL
)""");

cur.execute("""CREATE TABLE IF NOT EXISTS allocation (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  creation_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
  room_id INTEGER,
  person_id INTEGER
)""");

cur.close();
conn.commit();
conn.close();
