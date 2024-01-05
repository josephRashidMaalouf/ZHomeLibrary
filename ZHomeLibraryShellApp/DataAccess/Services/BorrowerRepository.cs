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

        public async Task<BorrowerModel> AddNewBorrower(string name, string phoneNo, string email)
        {
            await Init();
            await _conn.InsertAsync(new BorrowerModel() { Name = name, Email = email, PhoneNo = phoneNo });

            return await _conn.GetAsync<BorrowerModel>(b => b.Name == name);

        }

        public async Task DeleteBorrower(int id)
        {
            await Init();
            await _conn.DeleteAsync<BorrowerModel>(id);
        }

        public async Task<(bool success, string message)> UpdateBorrower(BorrowerModel borrower)
        {
            var borrowers = await GetAllBorrowers();

            if (borrowers.Any(b => b.Name == borrower.Name))
            {
                return (false, "That name is occupied by another borrower. Choose another name.");
            }
            await _conn.UpdateAsync(borrower);
            return (true, "");
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

            if (borrowers == null)
                return new List<BorrowerModel>();
            else
                return borrowers;
        }
    }
}
