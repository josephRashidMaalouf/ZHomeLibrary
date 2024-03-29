﻿using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ZHomeLibraryShellApp.Models;

[Table("books")]
public class BookModel : ObservableObject
{
    private string _title;
    private string _authorName;
    private BorrowerModel _borrower = new();
    private DateTime _returnByDate = new DateTime(1993, 05, 30);

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull, Unique]
    public string Title
    {
        get => _title;
        set
        {
            if (value == _title) return;
            _title = value;
            OnPropertyChanged();
        }
    }

    public string AuthorName
    {
        get => _authorName;
        set
        {
            if (value == _authorName) return;
            _authorName = value;
            OnPropertyChanged();
        }
    }

    public DateTime ReturnByDate
    {
        get => _returnByDate;
        set
        {
            if (value.Equals(_returnByDate)) return;
            _returnByDate = value;
            OnPropertyChanged();
        }
    }

    [ForeignKey(typeof(BorrowerModel))]
    public int BorrowerId { get; set; }

    [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
    public BorrowerModel Borrower
    {
        get => _borrower;
        set
        {
            if (Equals(value, _borrower)) return;
            _borrower = value;
            OnPropertyChanged();
        }
    } 
}