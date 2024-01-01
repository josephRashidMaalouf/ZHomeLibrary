using DataAccess.Tables;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.Mappers;

public static class BorrowerMapper
{
    public static BorrowerModel MapToModel(BorrowerTable borrower)
    {
        var bm = new BorrowerModel()
        {
            Id = borrower.Id,
            Name = borrower.Name,
            PhoneNo = borrower.PhoneNo,
            Email = borrower.Email,
            Books = borrower.Books,
        };

        return bm;
    }

    public static BorrowerTable MapToTable(BorrowerModel borrower)
    {
        var bm = new BorrowerTable()
        {
            Id = borrower.Id,
            Name = borrower.Name,
            PhoneNo = borrower.PhoneNo,
            Email = borrower.Email,
            Books = borrower.Books,
        };

        return bm;
    }
}