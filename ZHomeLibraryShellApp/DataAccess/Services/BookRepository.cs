﻿using SQLite;
using ZHomeLibraryShellApp.Managers;
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

        var newBook = new BookModel() { Title = title, AuthorName = authorName };
        await _conn.InsertAsync(newBook);

        await BookManager.OnBookAdded(newBook);

        return await _conn.GetAsync<BookModel>(b => b.Title == title);
    }

    public async Task DeleteBook(int id)
    {
        await Init();
        await _conn.DeleteAsync<BookModel>(id);

        await BookManager.OnBookDeleted(id);
    }

    public async Task UpdateBook(BookModel book)
    {
        await Init();
        await _conn.UpdateAsync(book);

        await BookManager.OnBookUpdated(book);
    }

    public async Task<BookModel> GetBookById(int id)
    {
        await Init();
        var book = await _conn.FindAsync<BookModel>(id);
        book.Borrower =
            await _conn.Table<BorrowerModel>().Where(b => b.Id == book.BorrowerId).FirstOrDefaultAsync();
        return book;
    }

    public async Task<List<BookModel>> GetAllBooks()
    {
        await Init();
        var books = await _conn.Table<BookModel>().ToListAsync();

        foreach (var book in books)
        {
            book.Borrower =
                await _conn.Table<BorrowerModel>().Where(b => b.Id == book.BorrowerId).FirstOrDefaultAsync();
        }


        if (books == null)
            return new List<BookModel>();
        else
            return books;
    }
}
