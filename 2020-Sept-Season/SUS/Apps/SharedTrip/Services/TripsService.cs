using SharedTrip.Data;
using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(TripInputModel input)
        {
            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = input.DepartureTime,
                Seats = input.Seats,
                Description = input.Description,
                ImagePath = input.ImagePath
            };

            db.Trips.Add(trip);
            db.SaveChanges();
        }



        public TripInputModel GetById(string tripId)
        {
            return db.Trips.Where(t => t.Id == tripId)
                .Select(t => new TripInputModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Seats = t.Seats,
                    Description = t.Description,
                    ImagePath = t.ImagePath
                }).FirstOrDefault();
        }

        public IEnumerable<TripInputModel> GetAll()
        {
            return db.Trips.Select(t => new TripInputModel
            {
                Id = t.Id,
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime,
                Seats = t.Seats
            }).ToList();
        }

    }
}
