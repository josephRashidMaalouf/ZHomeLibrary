using DataAccess.Tables;
using SQLite;

namespace ZHomeLibraryShellApp.Models;

public class BorrowerModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PhoneNo { get; set; }

    public string Email { get; set; }

    public List<BookTable> Books { get; set; }
}