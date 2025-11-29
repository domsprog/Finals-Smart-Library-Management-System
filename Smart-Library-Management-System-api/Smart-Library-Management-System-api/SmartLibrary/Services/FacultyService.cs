using Smart_Library_Management_System_api.SmartLibrary.Entities;
using Smart_Library_Management_System_api.SmartLibrary.Repository.Interface;
using SmartLibrary.DTOs.FacultyDTOs;
using SmartLibrary.Services.Interfaces;

namespace SmartLibrary.Services.FacultyService
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepo;
        public FacultyService(IFacultyRepository facultyRepo) => _facultyRepo = facultyRepo;

        public async Task<FacultyResponseDTO> CreateFacultyAsync(CreateFacultyDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var userId = string.IsNullOrWhiteSpace(dto.UserId) ? Guid.NewGuid().ToString() : dto.UserId;

            var faculty = new Faculty(userId, dto.Name, dto.Email, dto.FacultyId, dto.Department, dto.Position);
            await _facultyRepo.AddFaculty(faculty);

            return new FacultyResponseDTO
            {
                UserId = faculty.UserId,
                FacultyId = faculty.FacultyId,
                Name = faculty.Name,
                Email = faculty.Email,
                Department = faculty.Department,
                Position = faculty.Position,
            };
        }

        public async Task<bool> DeleteFacultyAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return false;
            var existing = await _facultyRepo.GetFacultyById(userId);
            if (existing == null) return false;
            await _facultyRepo.DeleteFaculty(userId);
            return true;
        }

        public async Task<IEnumerable<FacultyResponseDTO>> GetAllFacultyAsync()
        {
            var list = await _facultyRepo.GetAllFaculty();
            return list.Select(f => new FacultyResponseDTO
            {
                UserId = f.UserId,
                FacultyId = f.FacultyId,
                Name = f.Name,
                Email = f.Email,
                Department = f.Department,
                Position = f.Position,
            });
        }

        public async Task<FacultyResponseDTO> GetFacultyByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return null;
            var f = await _facultyRepo.GetFacultyById(userId);
            if (f == null) return null;
            return new FacultyResponseDTO
            {
                UserId = f.UserId,
                FacultyId = f.FacultyId,
                Name = f.Name,
                Email = f.Email,
                Department = f.Department,
                Position = f.Position,
            };
        }

        public async Task<FacultyResponseDTO> UpdateFacultyAsync(string userId, UpdateFacultyDTO dto)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("userId required");
            var f = await _facultyRepo.GetFacultyById(userId);
            if (f == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                f.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Email)) // ADDED
                f.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Department))
                f.Department = dto.Department;

            if (!string.IsNullOrWhiteSpace(dto.Position))
                f.Position = dto.Position;

            await _facultyRepo.UpdateFaculty(f);

            return new FacultyResponseDTO
            {
                UserId = f.UserId,
                FacultyId = f.FacultyId,
                Name = f.Name,
                Email = f.Email,
                Department = f.Department,
                Position = f.Position,
            };
        }
    }
}