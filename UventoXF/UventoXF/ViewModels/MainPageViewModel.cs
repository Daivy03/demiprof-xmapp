using System;
using System.Collections.ObjectModel;
using System.Linq;
using UventoXF.Helpers;
using UventoXF.Models;
using UventoXF.ViewModel;
using Xamarin.Forms;

namespace UventoXF.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string userName;

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }
        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            EventTypes = new ObservableCollection<EventType>();
            EventItems = new ObservableCollection<EventItem>();
            Dates = new ObservableCollection<DateItem>();
            SelectDateCommand = new Command<DateItem>((model) => ExecuteSelectDateCommand(model));
            SelectEventTypeCommand = new Command<EventType>((model) => ExecuteSelectEventTypeCommand(model));
            loadEventTypes();
            loadEventItems();
            loadDates();
            LoadUserName();
        }



        
        public string UserId
        {
            get
            {
                if (App.Current.Properties.ContainsKey("UserId"))
                {
                    return (string)App.Current.Properties["UserId"];
                }
                else
                {
                    return "Anonimo";
                }
            }
        }
        private async void LoadUserName()
        {
            var userId = App.Current.Properties["UserId"].ToString();
            var user = await FirebaseHelper.GetUserById(userId);
            if (user != null)
            {
                UserName = $"{user.FirstName} {user.LastName}";
            }
            else
            {
                userName = "Anonimo";
            }
        }

        public Command SelectDateCommand { get; }
        public Command SelectEventTypeCommand { get; }
        public ObservableCollection<EventType> EventTypes { get; }
        public ObservableCollection<EventItem> EventItems { get; }
        public ObservableCollection<DateItem> Dates { get; }

        private DateItem _selectedDate;
        public DateItem SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private EventType _selectedEventType;
        public EventType SelectedEventType
        {
            get => _selectedEventType;
            set => SetProperty(ref _selectedEventType, value);
        }

        private void loadEventTypes()
        {
            EventTypes.Add(new EventType()
            {
                name = "Matematica",
                image = "math.png",
                selected = true,
                backgroundColor = "#FFF",
                textColor = "#000000"
            });

            EventTypes.Add(new EventType()
            {
                name = "Chimica",
                image = "chimica.png",
                backgroundColor = "#29404E",
                textColor = "#FFFFFF"
            });

            EventTypes.Add(new EventType()
            {
                name = "Informatica",
                image = "code.png",
                backgroundColor = "#29404E",
                textColor = "#FFFFFF"
            });
        }

        private void loadEventItems()
        {
            EventItems.Add(new EventItem()
            {
                title = "Nicola Sottoferro",
 //               date = "Jan 12, 2021",
                classinfo = "5^ Informatica CFP",
                image = "nicola.png"
            });

            EventItems.Add(new EventItem()
            {
                title = "Nicotra Fabio",
//                date = "Jan 12, 2021",
                classinfo = "5^ Informatica",
                image = "fabio.png"
            });

            EventItems.Add(new EventItem()
            {
                title = "Davide Reale",
//                date = "Jan 12, 2021",
                classinfo = "5^ Informatica",
                image = "davide.png"
            });

            EventItems.Add(new EventItem()
            {
                title = "Alessandra Rossi",
 //               date = "Jan 12, 2021",
                classinfo = "4^ Meccanica",
                image = "avatar.png"
            });
            EventItems.Add(new EventItem()
            {
                title = "Mario Bianchi",
                //               date = "Jan 12, 2021",
                classinfo = "5^ Chimica",
                image = "avatar.png"
            });
        }

        private void loadDates()
        {
            var dateInit = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var dateEnd = new DateTime(dateInit.Year, dateInit.Month, DateTime.DaysInMonth(dateInit.Year, dateInit.Month));

            for (int i = 1; i <= dateEnd.Day; i++)
            {
                Dates.Add(new DateItem()
                {
                    day = string.Format("{0:00}", i),
                    month = dateInit.ToString("MMM").FirstLetterUpperCase(),
                    dayWeek = new DateTime(dateInit.Year, dateInit.Month, i).DayOfWeek.ToString().Substring(0, 3),
                    selected = i == DateTime.Today.Day,
                    backgroundColor = i == DateTime.Today.Day ? "#FF6D00" : "Transparent",
                    textColor = i == DateTime.Today.Day ? "#000000" : "#FFFFFF",
                });
            }
        }

        private void ExecuteSelectDateCommand(DateItem model)
        {
            Dates.ToList().ForEach((item) =>
            {
                item.selected = false;
                item.backgroundColor = "Transparent";
                item.textColor = "#FFFFFF";
            });

            var index = Dates.ToList().FindIndex(p => p.day == model.day && p.dayWeek == model.dayWeek);
            if (index > -1)
            {
                Dates[index].backgroundColor = "#FF6D00";
                Dates[index].textColor = "#000000";
                Dates[index].selected = true;
            }
        }

        private void ExecuteSelectEventTypeCommand(EventType model)
        {
            EventTypes.ToList().ForEach((item) =>
            {
                item.selected = false;
                item.backgroundColor = "#101010";
                item.textColor = "#FFFFFF";
            });

            var index = EventTypes.ToList().FindIndex(p => p.name == model.name);
            if (index > -1)
            {
                EventTypes[index].backgroundColor = "#FF6D00";
                EventTypes[index].textColor = "#FF6D00";
                EventTypes[index].selected = true;
            }
        }
    }
}
