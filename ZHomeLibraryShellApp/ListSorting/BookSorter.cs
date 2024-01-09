using System.Collections.ObjectModel;
using ZHomeLibraryShellApp.DataAccess.Services;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.ListSorting;

public static class BookSorter
{
    
    public static List<BookModel> Sort(int promptIndex, List<BookModel> books)
    {
        switch (promptIndex)
        {
            case 0:
                return books.OrderBy(b => b.Title).ToList();
            case 1: 
                return books.OrderByDescending(b => b.Title).ToList();
            case 2:
                return books.OrderBy(b => b.AuthorName).ToList();
            case 3:
                return books.OrderByDescending(b => b.AuthorName).ToList();
            default:
                return books;
        }
    }
    public static List<BookModel> Filter(int promptIndex, List<BookModel> books)
    {
        
        switch (promptIndex)
        {
            case 0:
                return books.Where(b => b.BorrowerId > 0).ToList();
            case 1:
                return books.Where(b => b.BorrowerId == 0).ToList();
            case 2:
                return books;
            default:
                return books;
        }
    }

    

}




