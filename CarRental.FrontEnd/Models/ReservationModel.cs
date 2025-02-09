namespace CarRental.FrontEnd.Models
{
    public class ReservationModel
    {
        public int ReservationID { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
    }
}
