using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ZHomeLibraryShellApp.DataAccess.Services;

namespace ZHomeLibraryShellApp.Models.ViewModels;

[QueryProperty("SelectedBookId", "SelectedBookId")]
public partial class BookDetailViewModel : ObservableObject
{
    private int _selectedBookId;

    [ObservableProperty]
    private BookModel book;

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

    public async Task LoadBook()
    {
        Book = await DbAccess.BookRepo.GetBookById(_selectedBookId);
    }
}