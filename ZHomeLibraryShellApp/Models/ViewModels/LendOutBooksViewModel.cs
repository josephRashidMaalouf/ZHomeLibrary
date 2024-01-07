﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Managers;

namespace ZHomeLibraryShellApp.Models.ViewModels;

public partial class LendOutBooksViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<BorrowerModel> borrowers = new();

    [ObservableProperty]
    private ObservableCollection<BookModel> books = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(LendOutBooksCommand))]
    private BorrowerModel selectedBorrower = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(LendOutBooksCommand))]
    private ObservableCollection<Object> selectedBooks = new();

    [ObservableProperty] private DateTime returnByDate; //Figure this out

    [ObservableProperty]
    private string searchBookQuery = string.Empty;



    public LendOutBooksViewModel()
    {
        LoadBooksAsync();
        LoadBorrowersAsync();

        BookManager.BookUpdated += BookManager_UpdateBook;
        BookManager.BookAdded += BookManager_AddBook;
        BookManager.BookDeleted += BookManager_DeleteBook;

        BorrowerManager.BorrowerUpdated += BorrowerManager_UpdateBorrower;
        BorrowerManager.BorrowerAdded += BorrowerManager_AddBorrower;
        BorrowerManager.BorrowerDeleted += BorrowerManager_DeleteBorrower;
    }


    [RelayCommand(CanExecute = nameof(LendOutBooksCanExecute))]
    private async Task LendOutBooks() 
    {
        List<BookModel> books = new();
        foreach (var selectedBook in SelectedBooks)
        {
            if (selectedBook is BookModel book)
            {
                books.Add(book);
                
            }
        }
        foreach (var book in books)
        {
            Books.Remove(book);
        }


        await LoanManager.MakeLoan(books.ToArray(), SelectedBorrower);



        await Shell.Current.DisplayAlert("Loan successful", $"You lended out {books.Count} books to {SelectedBorrower.Name}",
            "Ok");

        

        SelectedBorrower = new();
        SelectedBooks.Clear();

        
    }

    private bool LendOutBooksCanExecute()
    {

        return SelectedBorrower != null && SelectedBorrower.Id != 0 && SelectedBooks.Count > 0;
    }

    [RelayCommand]
    private async Task SelectedBookChanged()
    {
        LendOutBooksCommand.NotifyCanExecuteChanged();
    }

    #region Events

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
    private void BorrowerManager_DeleteBorrower(int id)
    {
        var borrowerToRemove = Borrowers.FirstOrDefault(b => b.Id == id);

        if (borrowerToRemove != null)
        {
            Borrowers.Remove(borrowerToRemove);
        }
    }

    private void BorrowerManager_AddBorrower(BorrowerModel obj)
    {
        Borrowers.Add(obj);
    }

    private void BookManager_DeleteBook(int id)
    {
        var bookToDelete = Books.FirstOrDefault(b => id == b.Id);

        if (bookToDelete != null)
        {
            Books.Remove(bookToDelete);
        }
    }

    private void BookManager_AddBook(BookModel obj)
    {
        Books.Add(obj);
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

    #endregion


    public async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new ObservableCollection<BookModel>(booksList.Where(b => b.BorrowerId == 0));
    }

    public async Task LoadBorrowersAsync()
    {
        var borrowersList = await DbAccess.BorrowerRepo.GetAllBorrowers();
        Borrowers = new ObservableCollection<BorrowerModel>(borrowersList);
    }
}