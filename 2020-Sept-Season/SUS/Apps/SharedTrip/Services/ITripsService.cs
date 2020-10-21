using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Create(TripInputModel input);

        TripInputModel GetById(string id);

        IEnumerable<TripInputModel> GetAll();

        void AddToTrip(string userId, string tripId);

        bool IsUserAddedToTrip(string userId, string tripId);

    }
}
