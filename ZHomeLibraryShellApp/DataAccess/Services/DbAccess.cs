namespace ZHomeLibraryShellApp.DataAccess.Services;

public static class DbAccess
{
    static readonly string dbPath = FileAccessHelper.GetLocalFilePath("ZHomeLib.db");
    public static BookRepository BookRepo { get; private set; } = new BookRepository(dbPath);
    public static BorrowerRepository BorrowerRepo { get; private set; } = new BorrowerRepository(dbPath);
}