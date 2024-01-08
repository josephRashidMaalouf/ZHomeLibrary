using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ZHomeLibraryShellApp.Models;

[Table("borrowers")]
public class BorrowerModel : ObservableObject
{
    private string _name = "no one";
    private string _phoneNo;
    private string _email;
    private List<BookModel> _books = new();

    [PrimaryKey,  AutoIncrement]
    public int Id { get; set; }

    [Unique, NotNull]
    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public string PhoneNo
    {
        get => _phoneNo;
        set
        {
            if (value == _phoneNo) return;
            _phoneNo = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (value == _email) return;
            _email = value;
            OnPropertyChanged();
        }
    }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<BookModel> Books
    {
        get => _books;
        set
        {
            if (Equals(value, _books)) return;
            _books = value;
            OnPropertyChanged();
        }
    }

    public override string ToString() => Name;
}