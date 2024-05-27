using HotelManagmentAPI.Data;
using HotelManagmentAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagmentAPI.Repository
{
    public class ClientRepository : IClientsRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public Client GetClient(int clientID)
        {
            return _context.Clients.FirstOrDefault(c => c.ClientID == clientID);
        }

        public Client GetClientByName(string clientName)
        {
            return _context.Clients.FirstOrDefault(c => c.FirstName == clientName || c.LastName == clientName);
        }

        public ICollection<Reservation> GetReservationsByClient(int clientID)
        {
            return _context.Reservations.Where(r => r.ClientID == clientID).ToList();
        }

        public bool ClientExists(int clientID)
        {
            return _context.Clients.Any(c => c.ClientID == clientID);
        }

        public bool CreateClient(Client client)
        {
            _context.Clients.Add(client);

            return Save();
        }

        public bool UpdateClient(Client client)
        {
           _context.Clients.Update(client);
            return Save();
        }

        public bool DeleteClient(int clientID)
        {
           _context.Clients.Remove(GetClient(clientID));
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
