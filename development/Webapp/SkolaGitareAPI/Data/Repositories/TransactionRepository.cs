using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SkolaGitareAPI.Data.DTOs;
using SkolaGitareAPI.Data.Entities;
using SkolaGitareAPI.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Data.Repositories
{
    public class TransactionRepository : GeneralRepository<Transaction>, ITransactionRepository
    {
        private readonly IMapper mapper;
        public TransactionRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<TransactionDTO> GetTransactionByStudentAndDate(string studentId, DateTime date)
        {
            return await context.Transactions.
                Include(x => x.Membership).
                ThenInclude(x => x.Member).
                Where(x => x.Membership.Member.Id == studentId && x.Date == date).
                ProjectTo<TransactionDTO>(mapper.ConfigurationProvider).
                AsNoTracking().
                FirstOrDefaultAsync();
        }

        public async Task<bool> CreateMonthlyTransactions()
        {
            var memberships = await context.Memberships.Include(x => x.Member).ToListAsync();
            foreach (var membership in memberships)
            {
                Transaction transaction = new Transaction
                {
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Membership = membership,
                    Paid = false
                };

                context.Add(transaction);
            }

            context.SaveChanges();

            return true;
        }

        public async Task<List<TransactionDTO>> GetTransactionsByStudent(string studentId)
        {
            return await context.Transactions.
                Include(x => x.Membership).
                ThenInclude(x => x.Member).
                Where(x => x.Membership.Member.Id == studentId).
                ProjectTo<TransactionDTO>(mapper.ConfigurationProvider).
                AsNoTracking().
                ToListAsync();
        }

        public async Task<List<TransactionDTO>> GetUnpaidTransactions()
        {
            return await context.Transactions.
                Include(x => x.Membership).
                ThenInclude(x => x.Member).
                Where(x => x.Paid == false).
                ProjectTo<TransactionDTO>(mapper.ConfigurationProvider).
                AsNoTracking().
                ToListAsync();
        }

        public async Task<List<TransactionDTO>> GetAllTransactions()
        {
            return await context.Transactions.
                Include(x => x.Membership).
                ThenInclude(x => x.Member).
                ProjectTo<TransactionDTO>(mapper.ConfigurationProvider).
                AsNoTracking().
                ToListAsync();
        }

        public async Task<TransactionDTO> GetTransaction(Guid id)
        {
            return await context.Transactions.
                Include(x => x.Membership).
                ThenInclude(x => x.Member).
                Where(x => x.Id == id).
                ProjectTo<TransactionDTO>(mapper.ConfigurationProvider).
                AsNoTracking().
                FirstOrDefaultAsync();
        }
    }
}
