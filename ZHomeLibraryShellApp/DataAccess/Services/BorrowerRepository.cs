using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ZHomeLibraryShellApp.Managers;
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

        public async Task<BorrowerModel> AddNewBorrower(string name, string phoneNo, string email)
        {
            await Init();

            var newBorrower = new BorrowerModel() { Name = name, Email = email, PhoneNo = phoneNo };
            await _conn.InsertAsync(newBorrower);

            await BorrowerManager.OnBorrowerAdded(newBorrower);

            return await _conn.GetAsync<BorrowerModel>(b => b.Name == name);

        }

        public async Task DeleteBorrower(int id)
        {
            await Init();

            await _conn.DeleteAsync<BorrowerModel>(id);

            await BorrowerManager.OnBorrowerDeleted(id);
        }

        public async Task UpdateBorrower(BorrowerModel borrower)
        {
            await Init();
            await _conn.UpdateAsync(borrower);

            await BorrowerManager.OnBorrowerUpdated(borrower);
        }

        public async Task<BorrowerModel> GetBorrowerById(int id)
        {
            await Init();
            return await _conn.FindAsync<BorrowerModel>(id);

        }


        public async Task<List<BorrowerModel>>  GetAllBorrowers()
        {
            await Init();
            var borrowers = await _conn.Table<BorrowerModel>().ToListAsync();

            foreach (var borrower in borrowers)
            {
                borrower.Books = await _conn.Table<BookModel>()
                    .Where(b => b.BorrowerId == borrower.Id).ToListAsync();
            }

            if (borrowers == null)
                return new List<BorrowerModel>();
            else
                return borrowers;
        }
    }
}
