using HotelApplication.Contexts;
using HotelApplication.Interfaces;
using HotelApplication.Models;
using HotelApplication.Contexts;
using HotelApplication.Interfaces;
using HotelApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApplication.Repositories
{
    public class RoomAmenityRepository : IRepository<int, RoomAmenity>
    {
        private readonly HotelsDbContext _context;

        public RoomAmenityRepository(HotelsDbContext context)
        {
            _context = context;
        }
        public RoomAmenity Add(RoomAmenity entity)
        {
            _context.RoomAmenities.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public RoomAmenity Delete(int key)
        {
            var amenity = GetById(key);
            if (amenity != null)
            {
                _context.RoomAmenities.Remove(amenity);
                _context.SaveChanges();
                return amenity;
            }
            return null;
        }

        public IList<RoomAmenity> GetAll()
        {
            if (_context.RoomAmenities.Count() == 0)
                return null;
            return _context.RoomAmenities.ToList();
        }

        public RoomAmenity GetById(int key)
        {
            var amenity = _context.RoomAmenities.SingleOrDefault(u => u.RoomAmenityId == key);
            return amenity;
        }

        public RoomAmenity Update(RoomAmenity entity)
        {
            var amenity = GetById(entity.RoomAmenityId);
            if (amenity != null)
            {
                _context.Entry<RoomAmenity>(amenity).State = EntityState.Modified;
                _context.SaveChanges();
                return amenity;
            }
            return null;
        }
    }
}