﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Language;
using ZHomeLibraryShellApp.Managers;
using ZHomeLibraryShellApp.Pages;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("SelectedBookToDeleteId", "SelectedBookToDeleteId")]
public partial class BookShelfViewModel : ObservableObject
{

    private int _selectedBookToDeleteId;
    public int SelectedBookToDeleteId
    {
        get => _selectedBookToDeleteId;
        set
        {
            if (Equals(value, _selectedBookToDeleteId)) return;
            _selectedBookToDeleteId = value;

            if (value != 0)
                DeleteSelectedBookAsync();

            OnPropertyChanged();
        }
    }

    private int _filterPrompt = 2;
    public int FilterPrompt
    {
        get => _filterPrompt;
        set
        {
            //if (value == _filterPrompt) return;
            _filterPrompt = value;
            var filteredBooks = ListSorting.BookSorter.Filter(value, Books.ToList());
            FilteredBooks = new ObservableCollection<BookModel>(filteredBooks);
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> FilterPrompts { get; set; }

    private int _sortByPrompt = 0;
    public int SortByPrompt
    {
        get => _sortByPrompt;
        set
        {
            //if (value == _sortByPrompt) return;
            _sortByPrompt = value;
            FilteredBooks = new ObservableCollection<BookModel>(ListSorting.BookSorter.Sort(value, FilteredBooks.ToList()));
            OnPropertyChanged();
        }
    }
    public ObservableCollection<string> SortByPrompts { get; set; }

    [ObservableProperty]
    private ILanguage language;

    [ObservableProperty]
    private BookModel book = new();

    [ObservableProperty] private BookModel selectedBook = new();

    [ObservableProperty, NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
    private string bookTitle;

    [ObservableProperty] private ObservableCollection<BookModel> books = new();
    [ObservableProperty] private ObservableCollection<BookModel> filteredBooks = new();


    public BookShelfViewModel()
    {
        LoadBooksAsync();

        Language = LanguageManager.CurrentLanguage;

        SortByPrompts = new ObservableCollection<string>()
        {
            Language.TitleAsc,
            Language.TitleDesc,
            Language.AuthorAsc,
            Language.AuthorDesc
        };

        FilterPrompts = new ObservableCollection<string>()
        {
            Language.Borrowed,
            Language.NotBorrowed,
            Language.ShowAll
        };
        LanguageManager.LanguageChanged += LanguageManager_LanguageChanged;
        BookManager.BookUpdated += BookManager_UpdateBook;


    }

    private void LanguageManager_LanguageChanged(ILanguage obj)
    {
        Language = obj;

        SortByPrompts.Clear();
        SortByPrompts.Add(Language.TitleAsc);
        SortByPrompts.Add(Language.TitleDesc);
        SortByPrompts.Add(Language.AuthorAsc);
        SortByPrompts.Add(Language.AuthorDesc);

        FilterPrompts.Clear();
        FilterPrompts.Add(Language.Borrowed);
        FilterPrompts.Add(Language.NotBorrowed);
        FilterPrompts.Add(Language.ShowAll);
    }

    private void BookManager_UpdateBook(BookModel obj)
    {
        var bookToUpdate = Books.FirstOrDefault(b => obj.Id == b.Id);

        if (bookToUpdate != null)
        {
            bookToUpdate.Title = obj.Title;
            bookToUpdate.AuthorName = obj.AuthorName;
            bookToUpdate.Borrower = obj.Borrower;
            bookToUpdate.BorrowerId = obj.BorrowerId;
        }

        FilterPrompt = _filterPrompt;
        SortByPrompt = _sortByPrompt;

    }

    [RelayCommand(CanExecute = nameof(AddCommandCanExecute))]
    private async Task AddBook()
    {
        Book.Title = BookTitle;

        var addedBook = await DbAccess.BookRepo.AddNewBook(Book.Title, Book.AuthorName);

        Books.Add(addedBook);

        FilterPrompt = _filterPrompt;
        SortByPrompt = _sortByPrompt;


        BookTitle = string.Empty;
        Book = new();


    }

    private bool AddCommandCanExecute()
    {

        bool titleIsNotEmpty = !string.IsNullOrEmpty(BookTitle);
        bool titleIsUnique = Books.All(b => b.Title != BookTitle);

        return titleIsNotEmpty && titleIsUnique;
    }

    [RelayCommand]
    private async Task OpenBookDetailPage()
    {
        if (SelectedBook == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?SelectedBookId={SelectedBook.Id}");
    }

    private async Task DeleteSelectedBookAsync()
    {
        await DbAccess.BookRepo.DeleteBook(SelectedBookToDeleteId);

        var bookToDelete = Books.FirstOrDefault(b => b.Id == SelectedBookToDeleteId);
        var bookTitle = string.Empty;
        if (bookToDelete != null)
        {
            bookTitle = bookToDelete.Title;
            Books.Remove(bookToDelete);
        }
        FilterPrompt = _filterPrompt;
        SortByPrompt = _sortByPrompt;

        var bookDeleteMsg = Language.GetDeleteBookSuccessMessage(bookTitle);
        await Shell.Current.DisplayAlert(Language.BookDeleted, bookDeleteMsg, Language.Ok);
    }

    public async Task LoadBooksAsync()
    {
        var booksList = await DbAccess.BookRepo.GetAllBooks();
        Books = new ObservableCollection<BookModel>(booksList);
        FilteredBooks = new ObservableCollection<BookModel>(booksList);
    }
}