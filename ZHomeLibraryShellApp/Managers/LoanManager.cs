using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Managers;

public static class LoanManager
{
    public static event Action<BookModel> BookReturned;

    public static async Task OnBookReturned(BookModel book)
    {
        BookReturned?.Invoke(book);
    }
    public static event Action<BookModel[]> BooksReturned;

    public static async Task OnBooksReturned(BookModel[] books)
    {
        BooksReturned?.Invoke(books);
    }

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
    public static async Task MakeLoan(BookModel book, BorrowerModel borrower)
    {
        borrower.Books.Add(book);
        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        book.Borrower = borrower;
        book.BorrowerId = borrower.Id;
        await DbAccess.BookRepo.UpdateBook(book);

        await Shell.Current.DisplayAlert("Loan successful", $"You lended out {book.Title} to {borrower.Name}",
            "Ok");
    }

    public static async Task ReturnLoan(BookModel book, BorrowerModel borrower)
    {
        var borrowersBook = borrower.Books.FirstOrDefault(b => b.Id == book.Id);
        borrower.Books.Remove(borrowersBook);

        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        book.Borrower = new BorrowerModel();
        book.BorrowerId = 0;

        await DbAccess.BookRepo.UpdateBook(book);

        await Shell.Current.DisplayAlert("Book returned", $"{borrower.Name} returned {book.Title}",
            "Ok");
    }

    public static async Task ReturnLoan(BookModel[] books, BorrowerModel borrower)
    {
        var borrowersBooks = new List<BookModel>();

        foreach (var book in books)
        {
            var bookToReturn = borrower.Books.FirstOrDefault(b => b.Id == book.Id);
            borrower.Books.Remove(bookToReturn);

            book.Borrower = new BorrowerModel();
            book.BorrowerId = 0;

            await DbAccess.BookRepo.UpdateBook(book);
        }

        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        await Shell.Current.DisplayAlert("Book returned", $"{borrower.Name} returned {books.Length} books",
            "Ok");
    }
}