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
        private SQLiteAsyncConnection _conn;


        private async Task Init()
        {
            if (_conn != null)
                return;

            _conn = new SQLiteAsyncConnection(_dbPath);
            await _conn.CreateTableAsync<BorrowerModel>();
        }

        public BorrowerRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewBorrower(string name, string phoneNo, string email)
        {
            await Init();
            await _conn.InsertAsync(new BorrowerModel() { Name = name, Email = email, PhoneNo = phoneNo });
        }

        public async Task DeleteBorrower(BorrowerModel borrower)
        {
            await Init();
            await _conn.DeleteAsync<BorrowerModel>(borrower.Id);
        }

        public async Task UpdateBorrower(BorrowerModel borrower)
        {
            await Init();
            await _conn.UpdateAsync(borrower);
        }

        public async Task<List<BorrowerModel>>  GetAllBorrowers()
        {
            await Init();
            var borrowers = await _conn.Table<BorrowerModel>().ToListAsync();

            if (borrowers == null)
                return new List<BorrowerModel>();
            else
                return borrowers;
        }
    }
}
