﻿using SQLite;

namespace DataAccess.Tables;

[Table("borrowers")]
public class BorrowerTable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique, NotNull]
    public string Name { get; set; }

    public string PhoneNo { get; set; }

    public string Email { get; set; }

    public List<BookTable> Books { get; set; }
}