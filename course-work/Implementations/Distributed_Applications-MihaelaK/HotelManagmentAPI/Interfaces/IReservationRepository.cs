namespace HotelManagmentAPI.Interfaces
{
    public interface IReservationRepository
    {
        ICollection<Reservation> GetReservations();
        Reservation GetReservation(int reservationID);
        ICollection<Reservation> GetReservationsByClient(int clientID);

        ICollection<Reservation> GetReservationsByClientName(string clientName);
        bool ReservationExists(int reservationID);

        bool CreateReservation(Reservation reservation);
        bool Save();
        bool DeleteReservation(int reservationID);
        bool UpdateReservation(Reservation reservation);

    }
}
