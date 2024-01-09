using ZHomeLibraryShellApp.Pages;
namespace ZHomeLibraryShellApp.Language;

public interface ILanguage
{
    public string YourBooks { get; set; }
    public string SearchTitlePlaceHolder { get; set; }
    public string BookDeleted { get; set; }
    public string YourBookShelf { get; set; }
    public string SearchBorrowerPlaceHolder { get; set; }
    public string AddNewBook { get; set; }
    public string SortBy { get; set; }
    public string Title { get; set; }
    public string TitleAsc { get; set; }
    public string TitleDesc { get; set; }
    public string Author { get; set; }
    public string AuthorAsc { get; set; }
    public string AuthorDesc { get; set; }
    public string NameAsc { get; set; }
    public string NameDesc { get; set; }
    public string ActiveLoansAsc { get; set; }
    public string ActiveLoansDesc { get; set; }
    public string ActiveLoans { get; set; }
    public string Borrowed { get; set; }
    public string NotBorrowed { get; set; }
    public string ShowAll { get; set; }
    public string Filter { get; set; }
    public string Home { get; set; }
    public string BookShelf { get; set; }
    public string Borrowers { get; set; }
    public string BorrowedBy { get; set; }
    public string PickABorrower { get; set; }
    public string LendOut { get; set; }
    public string LendOutTo { get; set; }
    public string YourHomeLibrary { get; set; }
    public string SwedishLanguage { get; set; }
    public string EnglishLanguage { get; set; }
    public string Update { get; set; }
    public string Delete { get; set; }
    public string Return { get; set; }
    public string BookDetails { get; set; }
    public string UpdateTitlePlaceHolder { get; set; }
    public string UpdateAuthorPlaceHolder { get; set; }
    public string YouHaveBookSameTitle { get; set; }
    public string CouldNotChangeTitle { get; set; }
    public string AddBorrower { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string Optional { get; set; }
    public string UpdateName { get; set; }
    public string UpdatePhoneNo { get; set; }
    public string UpdateEmail { get; set; }
    public string BooksBorrowed { get; set; }
    public string SelectBooksToReturn { get; set; }
    public string BorrowerDetails { get; set; }
    public string PickBooksToLendOut { get; set; }
    public string ReturnBy { get; set; }
    public string LoanSuccessful { get; set; }
    public string DeleteBorrower { get; set; }
    public string BookReturned { get; set; }
    public string DeleteBook { get; set; }
    public string Yes { get; set; }
    public string No { get; set; }
    public string Ok { get; set; }
    public string CouldNotChangeName { get; set; }
    public string ChooseAnotherName { get; set; }
    
    public string GetLoanSuccessFullMessage(string bookTitle, string borrowerName);
    public string GetLoanSuccessFullMessage(int noOfBooks, string borrowerName);
    public string GetDeleteBorrowerFailMessage(int noOfBooks, string borrowerName);
    public string GetDeleteBorrowerAreYouSureMessage(string borrowerName);
    public string GetDeleteBorrowerSuccessMessage(string borrowerName);
    public string GetDeleteBookFailMessage(string borrowerName);
    public string GetDeleteBookAreYouSureMessage(string bookTitle);
    public string GetDeleteBookSuccessMessage(string bookTitle);
    public string GetBookReturnedMessage(string bookTitle, string borrowerName);
    public string GetBookReturnedMessage(int noOfBooks, string borrowerName);
}