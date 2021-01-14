using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories.Interfaces
{
    public interface ITransactionRepository : IGeneralRepository<Transaction>
    {
        Task<List<TransactionDTO>> GetAllTransactions();

        Task<TransactionDTO> GetTransaction(Guid id);

        Task<bool> CreateMonthlyTransactions();

        Task<TransactionDTO> GetTransactionByStudentAndDate(string studentId, DateTime date);

        Task<List<TransactionDTO>> GetTransactionsByStudent(string studentId);

        Task<List<TransactionDTO>> GetUnpaidTransactions();
    }
}
