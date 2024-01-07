﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Managers;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("SelectedBookId", "SelectedBookId")]
public partial class BookDetailViewModel : ObservableObject
{
    private int _selectedBookId;

    [ObservableProperty]
    private BookModel book;

    [ObservableProperty]
    private BorrowerModel borrower = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBookInfoCommand))]
    private string editBookTitle;

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(UpdateBookInfoCommand))]
    private string editBookAuthor;

    public int SelectedBookId
    {
        get => _selectedBookId;
        set
        {
            if (Equals(value, _selectedBookId)) return;
            _selectedBookId = value;
            LoadBook();
            OnPropertyChanged();
        }
    }

    [RelayCommand]
    private async Task DeleteBook()
    {
        bool bookIsBorrowed = Book.BorrowerId > 0;
        if (bookIsBorrowed)
        {
            await Shell.Current.DisplayAlert("Delete book",
                $"This book is currently borrowed by {Book.Borrower.Name}. Make sure {Book.Borrower.Name} returns the book before deleting it.", "Ok");
            return;
        }

        var confirmation = await Shell.Current.DisplayAlert("Delete book",
            $"Are you sure you want to delete {Book.Title} from you library?", "Yes, delete it", "No, don't delete it");

        if (confirmation)
            await Shell.Current.GoToAsync($"..?SelectedBookToDeleteId={SelectedBookId}");
        else
            return;
    }

    [RelayCommand(CanExecute = nameof(UpdateBookInfoCanExecute))]
    private async Task UpdateBookInfo()
    {
        var books = await DbAccess.BookRepo.GetAllBooks();

        bool titleOccupied = books.Any(b => b.Title == EditBookTitle);

        if (titleOccupied)
        {
            await Shell.Current.DisplayAlert("Could not change title", "You have a book with the same title in your library.", "Ok");
            return;
        }

        if (!string.IsNullOrEmpty(EditBookTitle))
        {
            Book.Title = EditBookTitle;
            EditBookTitle = string.Empty;
        }

        if (!string.IsNullOrEmpty(EditBookAuthor))
        {
            Book.AuthorName = EditBookAuthor;
            EditBookAuthor = string.Empty;
        }

        await DbAccess.BookRepo.UpdateBook(Book);
        //await BookManager.OnBookUpdated(Book);
    }

    private bool UpdateBookInfoCanExecute()
    {
        bool entryFieldsNotEmpty = !string.IsNullOrEmpty(EditBookAuthor) || !string.IsNullOrEmpty(EditBookTitle);
        return entryFieldsNotEmpty;
    }

    [RelayCommand(CanExecute = nameof(ReturnBookCanExecute))]
    private async Task ReturnBook()
    {
        await LoanManager.ReturnLoan(Book, Borrower);
        Borrower = new BorrowerModel();
        ReturnBookCommand.NotifyCanExecuteChanged();
    }

    private bool ReturnBookCanExecute()
    {
        return Borrower.Id > 0;
    }

    private async Task LoadBook()
    {
        Book = await DbAccess.BookRepo.GetBookById(_selectedBookId);
        if (Book.BorrowerId > 0)
        {
            Borrower = await DbAccess.BorrowerRepo.GetBorrowerById(Book.BorrowerId);
            ReturnBookCommand.NotifyCanExecuteChanged();
        }
        
    }
}