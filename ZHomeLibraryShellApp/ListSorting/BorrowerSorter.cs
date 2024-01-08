using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.ListSorting;

public class BorrowerSorter
{
    private static List<BorrowerModel> Borrowers { get; set; } = new();


    public static List<BorrowerModel> Sort(string prompt, List<BorrowerModel> borrowers)
    {
        switch (prompt)
        {
            case "Name ascending \u2191":
                return borrowers.OrderBy(b => b.Name).ToList();
            case "Name descending \u2193":
                return borrowers.OrderByDescending(b => b.Name).ToList();
            case "Active loans ascending \u2191":
                return borrowers.OrderBy(b => b.Books.Count).ToList();
            case "Active loans descending \u2193":
                return borrowers.OrderByDescending(b => b.Books.Count).ToList();
            default:
                return new List<BorrowerModel>();
        }
    }
    
}