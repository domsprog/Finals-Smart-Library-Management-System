using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using SmartLibrary.DTOs.FineDTOs;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Services.FineService
{
    public class FineService : IFineService
    {
        private readonly IFineRepository _fineRepo;
        private readonly decimal _perDay = 5m;

        public FineService(IFineRepository fineRepo) => _fineRepo = fineRepo;

       
        public async Task<FineResponseDTO> AddFine(CreateFineDTO dto)
        {
            return await CreateFineAsync(dto);
        }

        public async Task<FineResponseDTO> CreateFineAsync(CreateFineDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var fine = new Fine
            {
                FineId = Guid.NewGuid().ToString(),
                LoanId = dto.LoanId,
                UserId = dto.UserId,
                Amount = dto.Amount,
                IsPaid = false,
                CreatedDate = DateTime.UtcNow,
                DueDate = dto.DueDate,
                Reason = dto.Reason
            };

            var added = await _fineRepo.AddFine(fine);
            return new FineResponseDTO
            {
                FineId = added.FineId,
                LoanId = added.LoanId,
                UserId = added.UserId,
                Amount = added.Amount,
                IsPaid = added.IsPaid,
                CreatedDate = added.CreatedDate,
                PaidDate = added.PaidDate,
                DueDate = added.DueDate,
                Reason = added.Reason
            };
        }

        public async Task<FineResponseDTO> GetFineByIdAsync(string fineId)
        {
            if (string.IsNullOrWhiteSpace(fineId)) return null;
            var f = await _fineRepo.GetFineById(fineId);
            if (f == null) return null;
            return new FineResponseDTO
            {
                FineId = f.FineId,
                LoanId = f.LoanId,
                UserId = f.UserId,
                Amount = f.Amount,
                IsPaid = f.IsPaid,
                CreatedDate = f.CreatedDate,
                PaidDate = f.PaidDate,
                DueDate = f.DueDate,
                Reason = f.Reason
            };
        }

        public async Task<IEnumerable<FineResponseDTO>> GetAllFinesAsync()
        {
            var fines = await _fineRepo.GetAllFines();
            return fines.Select(f => new FineResponseDTO
            {
                FineId = f.FineId,
                LoanId = f.LoanId,
                UserId = f.UserId,
                Amount = f.Amount,
                IsPaid = f.IsPaid,
                CreatedDate = f.CreatedDate,
                PaidDate = f.PaidDate,
                DueDate = f.DueDate,
                Reason = f.Reason
            });
        }

        public async Task<IEnumerable<FineResponseDTO>> GetFinesByUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Enumerable.Empty<FineResponseDTO>();

            var fines = await _fineRepo.GetFinesByUser(userId);
            return fines.Select(f => new FineResponseDTO
            {
                FineId = f.FineId,
                LoanId = f.LoanId,
                UserId = f.UserId,
                Amount = f.Amount,
                IsPaid = f.IsPaid,
                CreatedDate = f.CreatedDate,
                PaidDate = f.PaidDate,
                DueDate = f.DueDate,
                Reason = f.Reason
            });
        }

        public async Task<FineResponseDTO> GetFineByLoanIdAsync(string loanId)
        {
            if (string.IsNullOrWhiteSpace(loanId)) return null;
            var f = await _fineRepo.GetFineByLoan(loanId);
            if (f == null) return null;

            return new FineResponseDTO
            {
                FineId = f.FineId,
                LoanId = f.LoanId,
                UserId = f.UserId,
                Amount = f.Amount,
                IsPaid = f.IsPaid,
                CreatedDate = f.CreatedDate,
                PaidDate = f.PaidDate,
                DueDate = f.DueDate,
                Reason = f.Reason
            };
        }

        public async Task<FineResponseDTO> UpdateFineAsync(string fineId, UpdateFineDTO dto)
        {
            if (string.IsNullOrWhiteSpace(fineId)) throw new ArgumentException("fineId required");
            var f = await _fineRepo.GetFineById(fineId);
            if (f == null) return null;

            f.Amount = dto.Amount;
            if (!string.IsNullOrWhiteSpace(dto.Reason))
                f.Reason = dto.Reason;

            await _fineRepo.UpdateFine(f);

            return new FineResponseDTO
            {
                FineId = f.FineId,
                LoanId = f.LoanId,
                UserId = f.UserId,
                Amount = f.Amount,
                IsPaid = f.IsPaid,
                CreatedDate = f.CreatedDate,
                PaidDate = f.PaidDate,
                DueDate = f.DueDate,
                Reason = f.Reason
            };
        }

        public async Task<bool> PayFineAsync(string fineId)
        {
            if (string.IsNullOrWhiteSpace(fineId)) return false;
            return await _fineRepo.PayFine(fineId);
        }

        // CS0815 FIX: Make it properly async and return decimal directly
        public async Task<decimal> CalculateFine(DateTime dueDate, DateTime returnDate)
        {
            // Use await Task.Yield() to make it truly async if needed
            await Task.Yield();

            if (returnDate <= dueDate) return 0m;
            var days = (returnDate.Date - dueDate.Date).Days;
            var amount = Math.Max(0, days) * _perDay;
            return amount;
        }
    }
}
