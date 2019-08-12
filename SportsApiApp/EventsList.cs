using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public class EventsList
    {
        private List<Event> events;

        public List<Event> Events { get => events; set => events = value; }
    }
}
