using DataAccess.Tables;
using SQLite;

namespace DataAccess.Repositories;

public class BorrowerRepository
{
    private readonly string _dbPath;
    private SQLiteConnection _conn;


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

    public void DeleteBorrower(int id)
    {
        Init();
        _conn.Delete<BorrowerTable>(id);
    }

    //public void UpdateBorrower(BorrowerTable borrower)
    //{
    //    Init();
    //    _conn.Update(borrower);
    //}

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