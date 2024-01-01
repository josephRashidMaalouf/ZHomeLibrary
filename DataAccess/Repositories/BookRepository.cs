using DataAccess.Tables;
using SQLite;

namespace DataAccess.Repositories;

public class BookRepository
{
    private readonly string _dbPath;
    private SQLiteConnection _conn;

    public string StatusMessage { get; set; }

    private void Init()
    {
        if (_conn != null)
            return;

        _conn = new SQLiteConnection(_dbPath);
        _conn.CreateTable<BookTable>();
    }

    public BookRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewBook(string title, string authorname)
    {
        Init();
        _conn.Insert(new BookTable() { Title = title, AuthorName = authorname});
    }

    public void DeleteBook(BookTable book)
    {
        Init();
        _conn.Delete<BookTable>(book.Id);
    }

    public void UpdateBook(BookTable book)
    {
        Init();
        _conn.Update(book);
    }

    public List<BookTable> GetAllBooks()
    {
        Init();
        var books = _conn.Table<BookTable>().ToList();

        if (books == null)
            return new List<BookTable>();
        else
            return books;
    }
}