using HotelManagmentAPI.Data;
using System.Collections.Generic;

namespace HotelManagmentAPI.Interfaces
{
    public interface IClientsRepository
    {
        ICollection<Client> GetClients();
        Client GetClient(int clientID);
        Client GetClientByName(string clientName);
        ICollection<Reservation> GetReservationsByClient(int clientID);
        bool ClientExists(int clientID);

        bool CreateClient(Client client);
        bool Save();
        bool UpdateClient(Client client);
        bool DeleteClient(int clientID);
    }
}
