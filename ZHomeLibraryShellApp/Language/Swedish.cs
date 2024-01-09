namespace ZHomeLibraryShellApp.Language;


public class Swedish : ILanguage
{
    public string YourBooks { get; set; } = "Dina böcker";
    public string SearchTitlePlaceHolder { get; set; } = "Sök efter titel";
    public string BookDeleted { get; set; } = "Bok borttagen";
    public string YourBookShelf { get; set; } = "Din bokhylla";
    public string SearchBorrowerPlaceHolder { get; set; } = "Sök efter låntagare";
    public string AddNewBook { get; set; } = "Lägg till ny bok";
    public string SortBy { get; set; } = "Sortera efter";
    public string Title { get; set; } = "Titel";
    public string TitleAsc { get; set; } = "Titel stigande \u2191";
    public string TitleDesc { get; set; } = "Titel fallande \u2193";
    public string Author { get; set; } = "Författare";
    public string AuthorAsc { get; set; } = "Författarnamn stigande \u2191";
    public string AuthorDesc { get; set; } = "Författarnamn fallande \u2193";
    public string NameAsc { get; set; } = "Namn stigande \u2191";
    public string NameDesc { get; set; } = "Namn fallande \u2193";
    public string ActiveLoansAsc { get; set; } = "Antal aktiva lån stigande \u2191";
    public string ActiveLoansDesc { get; set; } = "Antal aktiva lån fallande \u2193";
    public string ActiveLoans { get; set; } = "Antal aktiva lån: ";
    public string Borrowed { get; set; } = "Utlånad";
    public string NotBorrowed { get; set; } = "Ej utlånad";
    public string ShowAll { get; set; } = "Visa alla";
    public string Filter { get; set; } = "Filtrera";
    public string Home { get; set; } = "Hem";
    public string BookShelf { get; set; } = "Bokhylla";
    public string Borrowers { get; set; } = "Låntagare";
    public string BorrowedBy { get; set; } = "Lånad av: ";
    public string PickABorrower { get; set; } = "Välj låntagare";
    public string LendOut { get; set; } = "Låna ut";
    public string LendOutTo { get; set; } = "Låna ut till:";
    public string YourHomeLibrary { get; set; } = "Ditt hembibliotek";
    public string SwedishLanguage { get; set; } = "Svenska";
    public string EnglishLanguage { get; set; } = "Engelska";
    public string Update { get; set; } = "Uppdatera";
    public string Delete { get; set; } = "Ta bort";
    public string Return { get; set; } = "Lämna tillbaka";
    public string BookDetails { get; set; } = "Detaljerad information om din bok";
    public string UpdateTitlePlaceHolder { get; set; } = "Uppdatera titel";
    public string UpdateAuthorPlaceHolder { get; set; } = "Uppdatera författarnamn";
    public string YouHaveBookSameTitle { get; set; } = "Du har redan en bok med den titeln i ditt bibliotek. Välj ett namn som hjälper dig särskilja på böckerna.";
    public string CouldNotChangeTitle { get; set; } = "Titeln gick inte att ändra";
    public string AddBorrower { get; set; } = "Lägg till ny låntagare";
    public string Name { get; set; } = "Namn";
    public string Mobile { get; set; } = "Mobil (ej obligatoriskt)";
    public string Email { get; set; } = "E-post (ej obligatoriskt)";
    public string Optional { get; set; } = "Valfritt";
    public string UpdateName { get; set; } = "Uppdatera namn";
    public string UpdatePhoneNo { get; set; } = "Uppdatera telefonnummer";
    public string UpdateEmail { get; set; } = "Uppdatera e-post";
    public string BooksBorrowed { get; set; } = "Böcker lånade";
    public string SelectBooksToReturn { get; set; } = "Välj böcker att lämna tillbaka";
    public string BorrowerDetails { get; set; } = "Information om låntagare";
    public string PickBooksToLendOut { get; set; } = "Välj böcker att låna ut";
    public string ReturnBy { get; set; } = "Lämnas tillbaka senast";
    public string LoanSuccessful { get; set; } = "Utlåning";
    public string DeleteBorrower { get; set; } = "Ta bort låntagare";
    public string BookReturned { get; set; } = "Boken har lämnats tillbaka";
    public string DeleteBook { get; set; } = "Boken är borttagen";
    public string Yes { get; set; } = "Ja";
    public string No { get; set; } = "Nej";
    public string Ok { get; set; } = "OK";
    public string CouldNotChangeName { get; set; } = "Namnbyte ej genomfört"; 
    public string ChooseAnotherName { get; set; } = "Namnet du valde är upptaget av en annan låntagare. Välj ett annat namn.";

    public string GetLoanSuccessFullMessage(string bookTitle, string borrowerName) =>
        $"Du har lånat ut {bookTitle} till {borrowerName}";

    public string GetLoanSuccessFullMessage(int noOfBooks, string borrowerName)
    {
        if (noOfBooks == 1)
        {
            return $"Du har lånat ut {noOfBooks} bok till {borrowerName}";
        }
        else
        {
            return $"Du har lånat ut {noOfBooks} böcker till {borrowerName}";
        }
    }

    public string GetDeleteBorrowerFailMessage(int noOfBooks, string borrowerName)
    {
        if (noOfBooks == 1)
        {
            return $"{borrowerName} har {noOfBooks} aktivt lån. Se till att boken lämnas tillbaka innan låntagaren tagits bort.";
        }
        else
        {
            return
                $"{borrowerName} har {noOfBooks} aktiva lån. Se till att böckerna lämnas tillbaka innan låntagaren tagits bort.";
        }
    }

    public string GetDeleteBorrowerAreYouSureMessage(string borrowerName) =>
        $"Är du säker på att du ta bort {borrowerName} som låntagare?";

    public string GetDeleteBorrowerSuccessMessage(string borrowerName) =>
        $"{borrowerName} har tagits bort från dina låntagare.";

    public string GetDeleteBookFailMessage(string borrowerName) =>
        $"Boken är för närvarande utlånad till {borrowerName}. Se till att {borrowerName} lämnar tillbaka boken innan den tas bort.";

    public string GetDeleteBookAreYouSureMessage(string bookTitle) =>
        $"Är du säker på att du vill ta bort {bookTitle} från ditt bibliotek?";

    public string GetDeleteBookSuccessMessage(string bookTitle) =>
        $"{bookTitle} har tagits bort från ditt bibliotek.";

    public string GetBookReturnedMessage(string bookTitle, string borrowerName) =>
        $"{borrowerName} har lämnat tillbaka {bookTitle}.";

    public string GetBookReturnedMessage(int noOfBooks, string borrowerName)
    {
        if (noOfBooks == 1)
        {
            return $"{borrowerName} lämnat tillbaka {noOfBooks} bok";
        }
        else
        {
            return $"{borrowerName} lämnat tillbaka {noOfBooks} böcker";
        }
        
    }
}
