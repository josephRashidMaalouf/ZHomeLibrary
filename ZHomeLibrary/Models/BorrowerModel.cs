using SQLite;

namespace ZHomeLibrary.Models;

[Table("borrowers")]
public class BorrowerModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique, NotNull]
    public string Name { get; set; }

    public string PhoneNo { get; set; }

    public string Email { get; set; }
}