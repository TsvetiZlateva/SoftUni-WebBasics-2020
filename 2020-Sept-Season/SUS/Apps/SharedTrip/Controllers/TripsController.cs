using SharedTrip.Models;
using SharedTrip.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController: Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View("");
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel trip)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (String.IsNullOrWhiteSpace(trip.StartPoint))
            {
                return this.Error("Start point is required.");
            }

            if (String.IsNullOrWhiteSpace(trip.EndPoint))
            {
                return this.Error("End point is required.");
            }

            if (trip.Seats < 2 || trip.Seats > 6)
            {
                return this.Error("Seats must be between 2 and 6.");
            }

            if (String.IsNullOrWhiteSpace(trip.Description))
            {
                return this.Error("Description is required.");
            }

            if (trip.Description.Length > 80)
            {
                return this.Error("Description must be most 80 characters.");
            }

            this.tripsService.Create(trip);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            IEnumerable<TripInputModel> trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var tripdetails = this.tripsService.GetById(tripId);
            return this.View(tripdetails);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            if (this.tripsService.IsUserAddedToTrip(userId, tripId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.tripsService.AddToTrip(userId, tripId);
            return this.Redirect("/");
        }
    }
}
