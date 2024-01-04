using System.Collections.ObjectModel;
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
        var confirmation = await Shell.Current.DisplayAlert("Delete book",
            $"Are you sure you want to delete {book.Title} from you library?", "Yes, delete it", "No, don't delete it");

        if(confirmation)
            await Shell.Current.GoToAsync($"..?SelectedBookToDeleteId={SelectedBookId}");
        else
            return;
    }

    [RelayCommand(CanExecute = nameof(UpdateBookInfoCanExecute))]
    private async Task UpdateBookInfo()
    {
        if (!string.IsNullOrEmpty(editBookTitle))
        {
            book.Title = EditBookTitle;
            EditBookTitle = string.Empty;
        }

        if (!string.IsNullOrEmpty(editBookAuthor))
        {
            book.AuthorName = EditBookAuthor;
            EditBookAuthor = string.Empty;
        }
        await DbAccess.BookRepo.UpdateBook(book);
        await BookManager.OnBookUpdated(book);
    }

    private bool UpdateBookInfoCanExecute()
    {
        return !string.IsNullOrEmpty(editBookAuthor) || !string.IsNullOrEmpty(editBookTitle);
    }

    public async Task LoadBook()
    {
        Book = await DbAccess.BookRepo.GetBookById(_selectedBookId);
    }
}