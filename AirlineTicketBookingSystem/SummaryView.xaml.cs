using AirlineTicketBookingSystemModel;
using System;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    public partial class SummaryView : Window
    {
        private FlightResponse ticket;

        public SummaryView()
        {
            InitializeComponent();
        }

        public SummaryView(FlightResponse ticket)
        {
            InitializeComponent();
            InitFields(ticket);
            this.ticket = ticket;
        }

        private void InitFields(FlightResponse ticket)
        {
            try
            {
                this.code.Content = ticket.code.ToString();
                this.date.Content = ticket.flight.date;
                this.price.Content = ticket.flight.price;
                this.passenger.Content = $"{ticket.userDTO.firstName} {ticket.userDTO.lastName}";
                this.email.Content = ticket.userDTO.email;
                this.fromAirPort.Content = ticket.flight.airPortFrom.airPortName;
                this.fromAddress.Content = $"{ticket.flight.airPortFrom.city}, {ticket.flight.airPortFrom.country}";
                this.toAirPort.Content = ticket.flight.airPortTo.airPortName;
                this.toAddress.Content = $"{ticket.flight.airPortTo.city}, {ticket.flight.airPortTo.country}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
