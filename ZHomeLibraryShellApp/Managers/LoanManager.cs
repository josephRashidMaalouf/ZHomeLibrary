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
}