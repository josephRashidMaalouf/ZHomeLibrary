using Plugin.LocalNotification;
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

    public static async Task MakeLoan(BookModel[] books, BorrowerModel borrower, DateTime returnDate)
    {
        borrower.Books.AddRange(books);
        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        foreach (var bookModel in books)
        {
            bookModel.ReturnByDate = returnDate;
            bookModel.Borrower = borrower;
            bookModel.BorrowerId = borrower.Id;
            await DbAccess.BookRepo.UpdateBook(bookModel);
        }

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
            return;
        var request = new NotificationRequest
        {
            NotificationId = borrower.Id,
            Title = $"Loan expired",
            Description = $"{borrower.Name}'s loans expire today",
            BadgeNumber = 42,
            CategoryType = NotificationCategoryType.Reminder,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = returnDate
            }
        };
        LocalNotificationCenter.Current.Show(request);
    }
    public static async Task MakeLoan(BookModel book, BorrowerModel borrower, DateTime returnDate)
    {
        borrower.Books.Add(book);
        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        book.ReturnByDate = returnDate;
        book.Borrower = borrower;
        book.BorrowerId = borrower.Id;
        await DbAccess.BookRepo.UpdateBook(book);

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
            return;
        var request = new NotificationRequest
        {
            NotificationId = 1000,
            Title = $"Loan expired",
            Description = $"{borrower.Name}'s loans expire today",
            BadgeNumber = 42,
            CategoryType = NotificationCategoryType.Reminder,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = returnDate.AddSeconds(10)
            }
        };
        
        LocalNotificationCenter.Current.Show(request);
    }

    public static async Task ReturnLoan(BookModel book, BorrowerModel borrower)
    {
        var borrowersBook = borrower.Books.FirstOrDefault(b => b.Id == book.Id);
        borrower.Books.Remove(borrowersBook);

        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        book.ReturnByDate = new DateTime(1993, 05, 30);
        book.Borrower = new BorrowerModel();
        book.BorrowerId = 0;

        await DbAccess.BookRepo.UpdateBook(book);

        await OnBookReturned(book);
        if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
            return;
        LocalNotificationCenter.Current.Cancel(borrower.Id);
        
    }

    public static async Task ReturnLoan(BookModel[] books, BorrowerModel borrower)
    {
        var borrowersBooks = new List<BookModel>();

        foreach (var book in books)
        {
            var bookToReturn = borrower.Books.FirstOrDefault(b => b.Id == book.Id);
            borrower.Books.Remove(bookToReturn);

            book.ReturnByDate = new DateTime(1993, 05, 30);
            book.Borrower = new BorrowerModel();
            book.BorrowerId = 0;

            await DbAccess.BookRepo.UpdateBook(book);
        }

        await DbAccess.BorrowerRepo.UpdateBorrower(borrower);

        await OnBooksReturned(books);

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
            return;
        LocalNotificationCenter.Current.Cancel(borrower.Id);
        
    }
}