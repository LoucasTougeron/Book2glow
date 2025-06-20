using Book2Glow.Infrastructure.Data;
using Book2Glow.Infrastructure.Data.Model;
using Book2Glow.Service.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Service
{
    public class ServiceService : IServiceService
    {
        private readonly DataModelContext _context;

        public ServiceService(DataModelContext context)
        {
            _context = context;
        }
        public async Task<ServiceModel> Create(ServiceModel service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<bool> Delete(Guid id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(b => b.Id == id);
            if (service == null)
            {
                return false; 
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ServiceModel>> GetAll()
        {
            return await _context.Services.ToListAsync();
        }


        public async Task<ServiceModel> GetById(Guid id)
        {
            return await _context.Services.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<ServiceDto>> GetServicesByCategoryAndCity(Guid categoryId, string city)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId);
            if (!categoryExists)
            {
                throw new KeyNotFoundException("Category not found");
            }

            var services = await _context.Services
                .Include(s => s.BusinessCategory)
                .ThenInclude(bc => bc.Business)
                    .Where(s =>
                        s.BusinessCategory.CategoryId == categoryId &&
                        s.BusinessCategory.Business.City.ToLower() == city.ToLower())
                    .Select(s => new ServiceDto
                    {
                        Id = s.Id,
                        Name = s.name,
                        Amount = s.amount,
                        Duration = s.duration,
                        BusinessName = s.BusinessCategory.Business.Name,
                        City = s.BusinessCategory.Business.City
                    })
                    .ToListAsync();

            return services;
        }

        public async Task<ServiceModel> Update(Guid id, ServiceModel service)
        {
            var existingService = await _context.Services.FirstOrDefaultAsync(b => b.Id == id);
            if (existingService == null)
            {
                throw new KeyNotFoundException("Business not found");
            }

            _context.Entry(existingService).CurrentValues.SetValues(service);
            await _context.SaveChangesAsync();

            return existingService;
        }

        public async Task<List<string>> GetAvailableSlots(Guid serviceId, int duration, DateOnly date)
        {
            var business = await _context.Services
                .Where(s => s.Id == serviceId)
                .Select(s => s.BusinessCategory.Business)
                .FirstOrDefaultAsync();

            if (business == null)
                return new List<string>();

            int openingTime = business.OpeningTime;
            int closingTime = business.ClosingTime;

            var bookings = await _context.Bookings
                .Where(b => b.ServiceId == serviceId && b.StartDate == date)
                .Include(b => b.Service)
                .ToListAsync();

            var reservedSlots = bookings
                .Select(b => (Start: b.StartTime, End: b.StartTime + duration))
                .ToList();

            var availableSlots = new List<string>();

            for (int current = openingTime; current + duration <= closingTime; current += duration)
            {
                double start = current;
                double end = current + duration;

                bool isOverlapping = reservedSlots.Any(r =>
                    !(r.End <= start || r.Start >= end)
                );

                if (!isOverlapping)
                {
                    var time = TimeSpan.FromMinutes(current);
                    availableSlots.Add(time.ToString(@"hh\:mm"));
                }
            }

            return availableSlots;
        }

        public async Task<string> BookAppointment(DateOnly date, int startTime, Guid serviceId, string userId)
        {
            bool alreadyBooked = await _context.Bookings
            .AnyAsync(b => b.ServiceId == serviceId && b.StartDate == date && b.StartTime == startTime);

            if (alreadyBooked)
                return "The slot is already booked";

            var service = await _context.Services
                .Include(s => s.BusinessCategory)
                .ThenInclude(bc => bc.Business)
                .FirstOrDefaultAsync(s => s.Id == serviceId);

            if (service == null)
                return "No Service found.";

            var businessId = service.BusinessCategory.BusinessId;

            var booking = new BookingModel
            {
                Id = Guid.NewGuid(),
                StartDate = date,
                StartTime = startTime,
                ServiceId = serviceId
            };

            await _context.Bookings.AddAsync(booking);

            var book = new BookModel
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                BusinessId = businessId,
                BookingId = booking.Id
            };

            await _context.BookingsBooks.AddAsync(book);
            await _context.SaveChangesAsync();

            return "Successfully book";
        }

        public async Task<List<BookingDto>> GetAllBookingAsync(string userId)
        {
            var reservations = await _context.BookingsBooks
            .Include(bb => bb.Booking)
            .ThenInclude(b => b.Service)
                .ThenInclude(s => s.BusinessCategory)
                    .ThenInclude(bc => bc.Business)
            .Select(bb => new BookingDto
            {
                StartDate = bb.Booking.StartDate,
                StartTime = TimeSpan.FromMinutes(bb.Booking.StartTime).ToString(@"hh\:mm"),
                Service = bb.Booking.Service.name,
                Business = bb.Business.Name,
                BusinessId=bb.Business.Id,
                ServiceId = bb.Booking.ServiceId,
            })
            .ToListAsync();

            return reservations;
        }
    }
}
