using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ZHomeLibraryShellApp.Models;

[Table("borrowers")]
public class BorrowerModel
{
    [PrimaryKey,  AutoIncrement]
    public int Id { get; set; }

    [Unique, NotNull]
    public string Name { get; set; }

    public string PhoneNo { get; set; }

    public string Email { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<BookModel> Books { get; set; }
}