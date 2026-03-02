using FluentValidation;
using H2Projekt.Domain.Validators.Rooms;

namespace H2Projekt.Domain
{
    public class RoomType : EntityBase
    {
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public int MaxOccupancy { get; private set; }
        public bool PetsAllowed { get; private set; }
        public decimal PricePerNight { get; private set; }

        private readonly List<Booking> bookings = new List<Booking>();
        public IReadOnlyCollection<Booking> Bookings => bookings.AsReadOnly();

        private readonly List<Room> rooms = new List<Room>();
        public IReadOnlyCollection<Room> Rooms => rooms.AsReadOnly();

        private readonly List<RoomDiscount> roomDiscounts = new List<RoomDiscount>();
        public IReadOnlyCollection<RoomDiscount> RoomDiscounts => roomDiscounts.AsReadOnly();

        public RoomType() { }

        public RoomType(string name, string? description, int maxOccupancy, bool petsAllowed, decimal pricePerNight)
        {
            Name = name;
            Description = description;
            MaxOccupancy = maxOccupancy;
            PetsAllowed = petsAllowed;
            PricePerNight = pricePerNight;

            ThrowIfInvalid();
        }

        public void UpdateDetails(string name, string? description, int maxOccupancy, bool petsAllowed, decimal pricePerNight)
        {
            Name = name;
            Description = description;
            MaxOccupancy = maxOccupancy;
            PetsAllowed = petsAllowed;
            PricePerNight = pricePerNight;

            ThrowIfInvalid();
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);

            ThrowIfInvalid();
        }

        public void RemoveRoom(Room room)
        {
            rooms.Remove(room);

            ThrowIfInvalid();
        }

        public void AddRoomDiscount(RoomDiscount roomDiscount)
        {
            roomDiscounts.Add(roomDiscount);

            ThrowIfInvalid();
        }

        public void RemoveRoomDiscount(RoomDiscount roomDiscount)
        {
            roomDiscounts.Remove(roomDiscount);

            ThrowIfInvalid();
        }

        private void ThrowIfInvalid()
        {
            var validator = new RoomTypeValidator();

            var result = validator.Validate(this);

            if (result.IsValid)
            {
                return;
            }

            throw new ValidationException(result.Errors);
        }
    }
}
