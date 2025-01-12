import sqlite3

conn = sqlite3.connect("./databases/HotelRoomAllocator.sqlite")
cur = conn.cursor();

cur.execute("DROP TABLE IF EXISTS person");

cur.execute("CREATE TABLE person (id INTEGER PRIMARY KEY AUTOINCREMENT, first_name TEXT NOT NULL, last_name TEXT NOT NULL, email_address TEXT, phone_number TEXT)");

cur.execute("INSERT INTO person (first_name, last_name, email_address, phone_number) VALUES ('Ajesh', 'Jhalli', 'ajeshjhalli@gmail.com', '+44 7488 290928')");

cur.close();
conn.commit();
conn.close();
