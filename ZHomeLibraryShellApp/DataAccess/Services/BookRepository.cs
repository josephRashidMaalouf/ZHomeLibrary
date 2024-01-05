using SQLite;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.DataAccess.Services;

public class BookRepository
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection _conn;


    private async Task Init()
    {
        if (_conn != null)
            return;

        _conn = new SQLiteAsyncConnection(_dbPath);
        await _conn.CreateTableAsync<BookModel>();
    }

    public BookRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public async Task<BookModel> AddNewBook(string title, string authorName)
    {
        await Init();
        await _conn.InsertAsync(new BookModel() { Title = title, AuthorName = authorName});

        return await _conn.GetAsync<BookModel>(b => b.Title == title);
    }

    public async Task DeleteBook(int id)
    {
        await Init();
        await _conn.DeleteAsync<BookModel>(id);
    }

    public async Task UpdateBook(BookModel book)
    {
        await Init();
        await _conn.UpdateAsync(book);
    }

    public async Task<BookModel> GetBookById(int id)
    {
        await Init();
        return await _conn.FindAsync<BookModel>(id);
    }

    public async Task<List<BookModel>> GetAllBooks()
    {
        await Init();
        var books = await _conn.Table<BookModel>().ToListAsync();

        if (books == null)
            return new List<BookModel>();
        else
            return books;
    }
}
