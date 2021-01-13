using AirlineTicketBookingSystemModel;
using System;
using System.Windows;

namespace AirlineTicketBookingSystem
{
    /// <summary>
    /// Interaction logic for DetailsView.xaml
    /// </summary>
    public partial class DetailsView : Window
    {
        public DetailsView(Flights ticket)
        {
            InitializeComponent();
            InitFields(ticket);
        }

        public DetailsView(Ticket ticket)
        {
            InitializeComponent();
            InitFields(ticket);
        }

        private void InitFields(Ticket ticket)
        {
            try
            {
                this.labelName.Content = "Kod biletu:";
                this.id.Content = ticket.code;
                this.price.Content = ticket.flight.price;
                this.date.Content = ticket.flight.date;
                this.time.Content = ticket.flight.time;
                this.fromAirPort.Content = ticket.flight.airPortFrom.airPortName;
                this.fromAddress.Content = $"{ticket.flight.airPortFrom.city}, {ticket.flight.airPortFrom.country}";
                this.toAirPort.Content = ticket.flight.airPortTo.airPortName;
                this.toAddress.Content = $"{ticket.flight.airPortTo.city}, {ticket.flight.airPortTo.country}";
            }
            catch (Exception)
            {

            }
        }

        private void InitFields(Flights ticket)
        {
            try
            {
                this.id.Content = ticket._id;
                this.price.Content = ticket.price;
                this.date.Content = ticket.date;
                this.time.Content = ticket.time;
                this.fromAirPort.Content = ticket.airPortFrom.airPortName;
                this.fromAddress.Content = $"{ticket.airPortFrom.city}, {ticket.airPortFrom.country}";
                this.toAirPort.Content = ticket.airPortTo.airPortName;
                this.toAddress.Content = $"{ticket.airPortTo.city}, {ticket.airPortTo.country}";
            }
            catch (Exception)
            {

            }
        }
    }
}
