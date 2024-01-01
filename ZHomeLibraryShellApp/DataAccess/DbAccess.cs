using DataAccess;
using DataAccess.Repositories;

namespace ZHomeLibraryShellApp.DataAccess;

public static class DbAccess
{
    public static readonly BookRepository BookRepository = new BookRepository(FileAccessHelper.GetLocalFilePath("books.db3"));

    public static readonly BorrowerRepository BorrowerRepository = new BorrowerRepository(FileAccessHelper.GetLocalFilePath("borrowers.db3"));

}