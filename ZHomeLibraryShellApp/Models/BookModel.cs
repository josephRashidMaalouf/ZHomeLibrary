using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ZHomeLibraryShellApp.Models;

[Table("books")]
public class BookModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull, Unique]
    public string Title { get; set; }

    public string AuthorName { get; set; }

    public int BorrowerId { get; set; }

    [ManyToOne]
    public BorrowerModel Borrower { get;  set; }
}