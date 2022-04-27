namespace hotel_booking.DAL.ViewModels
{
    public class HotelVMCreate
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public int EmployeesNumber { get; set; }
        public string Image { get; set; }

    }

    public class HotelVMUpdate: HotelVMCreate
    {
        public int Id { get; set; }
    }
}
