using SQLite;

namespace DataAccess.Tables;

[Table("books")]
public class BookTable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Title { get; set; }

    public string AuthorName { get; set; }

}