using HotelManagmentAPI.Data;
using HotelManagmentAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagmentAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Reservation> GetReservations()
        {
            return _context.Reservations.ToList();
        }

        public Reservation GetReservation(int reservationID)
        {
            return _context.Reservations.FirstOrDefault(r => r.ReservationID == reservationID);
        }

        public ICollection<Reservation> GetReservationsByClient(int clientID)
        {
            return _context.Reservations.Where(r => r.ClientID == clientID).ToList();
        }

        public ICollection<Reservation> GetReservationsByClientName(string clientName)
        {
            return _context.Reservations.Where(r => r.Client.FirstName == clientName).ToList();
        }

        public bool ReservationExists(int reservationID)
        {
            return _context.Reservations.Any(r => r.ReservationID == reservationID);
        }

        public bool CreateReservation(Reservation reservation)
        {
           _context.Reservations.Add(reservation);
            return Save();
        }

        public bool UpdateReservation(Reservation reservation)
        {
           _context.Reservations.Update(reservation);
            return Save();
        }

        public bool DeleteReservation(int reservationID)
        {
           _context.Reservations.Remove(GetReservation(reservationID));
            return Save();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
