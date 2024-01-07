using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Managers;

public static class LoanManager
{

    public static async Task MakeLoan(BookModel[] books, BorrowerModel borrower)
    {
        borrower.Books.AddRange(books);
        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        foreach (var bookModel in books)
        {
            bookModel.Borrower = borrower;
            bookModel.BorrowerId = borrower.Id;
            await DbAccess.BookRepo.UpdateBook(bookModel);
        }
    }

    public static async Task ReturnLoan(BookModel book, BorrowerModel borrower)
    {
        var borrowersBook = borrower.Books.FirstOrDefault(b => b.Id == book.Id);
        borrower.Books.Remove(borrowersBook);

        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        book.Borrower = new BorrowerModel();
        book.BorrowerId = 0;

        await DbAccess.BookRepo.UpdateBook(book);

    }
}