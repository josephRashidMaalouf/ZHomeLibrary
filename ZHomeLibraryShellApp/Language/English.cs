namespace ZHomeLibraryShellApp.Language;


public class English : ILanguage
{
    public string YourBooks { get; set; } = "Your books";
    public string SearchTitlePlaceHolder { get; set; } = "Search for a title";
    public string BookDeleted { get; set; } = "Book deleted";
    public string YourBookShelf { get; set; } = "Your bookshelf";
    public string SearchBorrowerPlaceHolder { get; set; } = "Search for a borrower";
    public string AddNewBook { get; set; } = "Add new book";
    public string SortBy { get; set; } = "Sort by";
    public string Title { get; set; } = "Title";
    public string TitleAsc { get; set; } = "Title ascending \u2191";
    public string TitleDesc { get; set; } = "Title descending \u2193";
    public string Author { get; set; } = "Author(s)";
    public string AuthorAsc { get; set; } = "Author name ascending \u2191";
    public string AuthorDesc { get; set; } = "Author name descending \u2193";
    public string NameAsc { get; set; } = "Name ascending \u2191";
    public string NameDesc { get; set; } = "Name descending \u2193";
    public string ActiveLoansAsc { get; set; } = "Active loans ascending \u2191";
    public string ActiveLoansDesc { get; set; } = "Active loans descending \u2193";
    public string ActiveLoans { get; set; } = "Active loans: ";
    public string Borrowed { get; set; } = "Borrowed";
    public string NotBorrowed { get; set; } = "Not borrowed";
    public string ShowAll { get; set; } = "Show all";
    public string Filter { get; set; } = "Filter";
    public string Home { get; set; } = "Home";
    public string BookShelf { get; set; } = "Bookshelf";
    public string Borrowers { get; set; } = "Borrowers";
    public string BorrowedBy { get; set; } = "Borrowed by: ";
    public string PickABorrower { get; set; } = "Pick a borrower";
    public string LendOut { get; set; } = "Lend out";
    public string LendOutTo { get; set; } = "Lend out to:";
    public string YourHomeLibrary { get; set; } = "Your home library";
    public string SwedishLanguage { get; set; } = "Swedish";
    public string EnglishLanguage { get; set; } = "English";
    public string Update { get; set; } = "Update";
    public string Delete { get; set; } = "Delete";
    public string Return { get; set; } = "Return";
    public string BookDetails { get; set; } = "Book details";
    public string UpdateTitlePlaceHolder { get; set; } = "Update title";
    public string UpdateAuthorPlaceHolder { get; set; } = "Update author name(s)";
    public string YouHaveBookSameTitle { get; set; } = "You have a book with the same title in your library. Pick a title that helps you distinguish between your books.";
    public string CouldNotChangeTitle { get; set; } = "Could not change title";
    public string AddBorrower { get; set; } = "Add new borrower";
    public string Name { get; set; } = "Name";
    public string Mobile { get; set; } = "Mobile (optional)";
    public string Email { get; set; } = "Email (optional)";
    public string Optional { get; set; } = "Optional";
    public string UpdateName { get; set; } = "Update name";
    public string UpdatePhoneNo { get; set; } = "Update phone number";
    public string UpdateEmail { get; set; } = "Update email";
    public string BooksBorrowed { get; set; } = "Books borrowed";
    public string SelectBooksToReturn { get; set; } = "Select books to return";
    public string BorrowerDetails { get; set; } = "Borrower details";
    public string PickBooksToLendOut { get; set; } = "Pick books to lend out";
    public string ReturnBy { get; set; } = "Return by";
    public string LoanSuccessful { get; set; } = "Loan successful";
    public string DeleteBorrower { get; set; } = "Delete borrower";
    public string BookReturned { get; set; } = "Book returned";
    public string DeleteBook { get; set; } = " Book deleted";
    public string Yes { get; set; } = "Yes";
    public string No { get; set; } = "No";
    public string Ok { get; set; } = "OK";
    public string CouldNotChangeName { get; set; } = "Could not change name";
    public string ChooseAnotherName { get; set; } = "That name is occupied by another borrower. Choose another name.";

    public string GetLoanSuccessFullMessage(string bookTitle, string borrowerName) =>
        $"You lended out {bookTitle} to {borrowerName}";

    public string GetLoanSuccessFullMessage(int noOfBooks, string borrowerName)
    {
        if (noOfBooks == 1)
        {
            return $"You lended out {noOfBooks} book to {borrowerName}";
        }
        else
        {
            return $"You lended out {noOfBooks} books to {borrowerName}";
        }
    }

    public string GetDeleteBorrowerFailMessage(int noOfBooks, string borrowerName)
    {
        if (noOfBooks == 1)
        {
            return $"{borrowerName} has {noOfBooks} active loan. Make sure the book is returned before deleting the borrower";
        }
        else
        {
            return $"{borrowerName} has {noOfBooks} active loans. Make sure the books are returned before deleting the borrower";
        }
    }

    public string GetDeleteBorrowerAreYouSureMessage(string borrowerName) =>
        $"Are you sure you want to delete {borrowerName} from your borrowers list?";

    public string GetDeleteBorrowerSuccessMessage(string borrowerName) =>
        $"{borrowerName} has been successfully deleted from your borrowers list";

    public string GetDeleteBookFailMessage(string borrowerName) =>
        $"This book is currently borrowed by {borrowerName}. Make sure {borrowerName}" +
        $"returns the book before deleting it.";

    public string GetDeleteBookAreYouSureMessage(string bookTitle) =>
        $"Are you sure you want to delete {bookTitle} from your library?";

    public string GetDeleteBookSuccessMessage(string bookTitle) =>
        $"{bookTitle} has been successfully deleted from your library";

    public string GetBookReturnedMessage(string bookTitle, string borrowerName) =>
        $"{borrowerName} returned {bookTitle}.";

    public string GetBookReturnedMessage(int noOfBooks, string borrowerName)
    {

        if (noOfBooks == 1)
        {
            return $"{borrowerName} returned {noOfBooks} book"; ;
        }
        else
        {
            return $"{borrowerName} returned {noOfBooks} books";
        }


    }



}