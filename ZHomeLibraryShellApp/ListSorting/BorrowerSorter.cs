using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.ListSorting;

public class BorrowerSorter
{
    private static List<BorrowerModel> Borrowers { get; set; } = new();


    public static List<BorrowerModel> Sort(int promptIndex, List<BorrowerModel> borrowers)
    {
        switch (promptIndex)
        {
            case 0:
                return borrowers.OrderBy(b => b.Name).ToList();
            case 1:
                return borrowers.OrderByDescending(b => b.Name).ToList();
            case 2:
                return borrowers.OrderBy(b => b.Books.Count).ToList();
            case 3:
                return borrowers.OrderByDescending(b => b.Books.Count).ToList();
            default:
                return new List<BorrowerModel>();
        }
    }
    
}