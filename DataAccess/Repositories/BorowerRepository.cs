using DataAccess.Tables;
using SQLite;

namespace DataAccess.Repositories;

public class BorrowerRepository
{
    private readonly string _dbPath;
    private SQLiteConnection _conn;

    public string StatusMessage { get; set; }

    private void Init()
    {
        if (_conn != null)
            return;

        _conn = new SQLiteConnection(_dbPath);
        _conn.CreateTable<BorrowerTable>();
    }

    public BorrowerRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewBorrower(string name, string phoneNo, string email)
    {
        Init();
        _conn.Insert(new BorrowerTable() { Name = name, Email = email, PhoneNo = phoneNo });
    }

    public void DeleteBorrower(BorrowerTable borrower)
    {
        Init();
        _conn.Delete<BorrowerTable>(borrower.Id);
    }

    public void UpdateBorrower(BorrowerTable borrower)
    {
        Init();
        _conn.Update(borrower);
    }

    public List<BorrowerTable> GetAllBorrowers()
    {
        Init();
        var borrowers = _conn.Table<BorrowerTable>().ToList();

        if (borrowers == null)
            return new List<BorrowerTable>();
        else
            return borrowers;
    }
}