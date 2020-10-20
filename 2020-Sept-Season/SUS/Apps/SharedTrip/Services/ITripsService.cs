using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Create(TripInputModel input);

        TripInputModel GetTripId(string id);

        IEnumerable<TripInputModel> GetAll();

    }
}
