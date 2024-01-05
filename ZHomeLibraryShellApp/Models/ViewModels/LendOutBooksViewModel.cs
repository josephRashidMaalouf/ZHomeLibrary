using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ZHomeLibraryShellApp.Managers;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class LendOutBooksViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<BorrowerModel> borrowers = new();

    [ObservableProperty]
    private ObservableCollection<BookModel> books = new();

    [ObservableProperty]
    private BorrowerModel selectedBorrower = new();

    [ObservableProperty]
    private ObservableCollection<BookModel> selectedBooks = new();

    [ObservableProperty]
    private DateOnly returnByDate = new();

    [ObservableProperty]
    private string searchBookQuery = string.Empty;

    public LendOutBooksViewModel()
    {
        BookManager.LoadBooksAsync(Books);
        BorrowerManager.LoadBorrowersAsync(Borrowers);

        BookManager.BookUpdated += BookManager_UpdateBook;
        BookManager.BookAdded += BookManager_AddBook;
        BookManager.BookDeleted += BookManager_DeleteBook;

        BorrowerManager.BorrowerUpdated += BorrowerManager_UpdateBorrower;
        BorrowerManager.BorrowerAdded += BorrowerManager_AddBorrower;
        BorrowerManager.BorrowerDeleted += BorrowerManager_DeleteBorrower;
    }

    private void BorrowerManager_UpdateBorrower(BorrowerModel obj)
    {
        var borrowerToUpdate = Borrowers.FirstOrDefault(b => obj.Id == b.Id);

        if (borrowerToUpdate != null)
        {
            borrowerToUpdate.Name = obj.Name;
            borrowerToUpdate.PhoneNo = obj.PhoneNo;
            borrowerToUpdate.Email = obj.Email;
        }
    }
    private void BorrowerManager_DeleteBorrower(BorrowerModel obj)
    {
        throw new NotImplementedException();
    }

    private void BorrowerManager_AddBorrower(BorrowerModel obj)
    {
        throw new NotImplementedException();
    }

    private void BookManager_DeleteBook(BookModel obj)
    {
        throw new NotImplementedException();
    }

    private void BookManager_AddBook(BookModel obj)
    {
        throw new NotImplementedException();
    }



    private void BookManager_UpdateBook(BookModel obj)
    {
        var bookToUpdate = Books.FirstOrDefault(b => obj.Id == b.Id);

        if (bookToUpdate != null)
        {
            bookToUpdate.Title = obj.Title;
            bookToUpdate.AuthorName = obj.AuthorName;
        }
    }
}