using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ZHomeLibraryShellApp.Models;

namespace ZHomeLibraryShellApp.DataAccess.Services
{
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
            _conn.CreateTable<BorrowerModel>();
        }

        public BorrowerRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewBorrower(string name, string phoneNo, string email)
        {
            Init();
            _conn.Insert(new BorrowerModel() { Name = name, Email = email, PhoneNo = phoneNo });
        }

        public void DeleteBorrower(BorrowerModel borrower)
        {
            Init();
            _conn.Delete<BorrowerModel>(borrower.Id);
        }

        public void UpdateBorrower(BorrowerModel borrower)
        {
            Init();
            _conn.Update(borrower);
        }

        public List<BorrowerModel> GetAllBorrowers()
        {
            Init();
            var borrowers = _conn.Table<BorrowerModel>().ToList();

            if (borrowers == null)
                return new List<BorrowerModel>();
            else
                return borrowers;
        }
    }
}
