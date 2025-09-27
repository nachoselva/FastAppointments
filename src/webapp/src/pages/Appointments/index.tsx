import { format, getDay, parse, startOfWeek, endOfWeek, addDays, addWeeks, addMonths } from "date-fns";
import { enUS } from "date-fns/locale/en-US";
import { useState } from "react";
import { Calendar, dateFnsLocalizer, type Event as RBCEvent, Views, type SlotInfo } from "react-big-calendar";
import "react-big-calendar/lib/css/react-big-calendar.css";

const locales = {
    "en-US": enUS,
};
const localizer = dateFnsLocalizer({
    format,
    parse,
    startOfWeek: () => startOfWeek(new Date(), { weekStartsOn: 0 }),
    getDay,
    locales,
});

interface AppointmentEvent extends RBCEvent {
    id: number;
    title: string;
    start: Date;
    end: Date;
}

const initialEvents: AppointmentEvent[] = [
    { id: 1, title: "Doctor Appointment", start: new Date(2025, 8, 26, 10, 0), end: new Date(2025, 8, 26, 11, 0) },
    { id: 2, title: "Team Meeting", start: new Date(2025, 8, 27, 14, 0), end: new Date(2025, 8, 27, 15, 30) },
    { id: 3, title: "Coffee with Alex", start: new Date(2025, 8, 28, 9, 30), end: new Date(2025, 8, 28, 10, 30) },
];

const viewValues = [Views.DAY, Views.WEEK, Views.MONTH, Views.AGENDA] as const;

const AppointmentsPage: React.FC = () => {
    const [events, setEvents] = useState<AppointmentEvent[]>(initialEvents);
    const [currentView, setCurrentView] = useState<typeof viewValues[number]>(Views.WEEK);
    const [currentDate, setCurrentDate] = useState<Date>(new Date());

    const handleSelectSlot = (slotInfo: SlotInfo) => {
        const title = prompt("Enter a new event name:");
        if (title) {
            const newEvent: AppointmentEvent = {
                id: events.length + 1,
                title,
                start: new Date(slotInfo.start),
                end: new Date(slotInfo.end),
            };
            setEvents(prev => [...prev, newEvent]);
        }
    };

    const handleSelectEvent = (event: AppointmentEvent) => {
        const action = window.confirm(`Do you want to delete "${event.title}"?`);
        if (action) setEvents(prev => prev.filter(e => e.id !== event.id));
    };

    const goToToday = () => setCurrentDate(new Date());
    const goToPrev = () => {
        switch (currentView) {
            case Views.DAY: setCurrentDate(d => addDays(d, -1)); break;
            case Views.WEEK: setCurrentDate(d => addWeeks(d, -1)); break;
            case Views.MONTH: setCurrentDate(d => addMonths(d, -1)); break;
            case Views.AGENDA: setCurrentDate(d => addDays(d, -1)); break;
        }
    };
    const goToNext = () => {
        switch (currentView) {
            case Views.DAY: setCurrentDate(d => addDays(d, 1)); break;
            case Views.WEEK: setCurrentDate(d => addWeeks(d, 1)); break;
            case Views.MONTH: setCurrentDate(d => addMonths(d, 1)); break;
            case Views.AGENDA: setCurrentDate(d => addDays(d, 1)); break;
        }
    };

    const getViewDescription = () => {
        switch (currentView) {
            case Views.DAY:
            case Views.AGENDA:
                return format(currentDate, "PPP");
            case Views.WEEK: {
                const start = startOfWeek(currentDate, { weekStartsOn: 0 });
                const end = endOfWeek(currentDate, { weekStartsOn: 0 });
                return `${format(start, "PPP")} - ${format(end, "PPP")}`;
            }
            case Views.MONTH:
                return format(currentDate, "MMMM yyyy");
        }
    };

    return (
        <div className="h-screen flex flex-col">
            <header className="p-4 flex-shrink-0 bg-white shadow">
                <h2 className="text-2xl font-bold">Appointments Page</h2>
            </header>

            <div className="flex gap-2 px-4 mt-4">
                {viewValues.map(view => (
                    <button
                        key={view}
                        onClick={() => setCurrentView(view)}
                        className={`px-4 py-2 rounded-full transition-colors ${currentView === view ? "bg-blue-500 text-white" : "bg-gray-200 text-gray-700 hover:bg-gray-300"
                            }`}
                    >
                        {view.charAt(0).toUpperCase() + view.slice(1)}
                    </button>
                ))}
            </div>

            <div className="px-4 mt-2 text-center text-xl font-semibold text-gray-700">
                {getViewDescription()}
            </div>

            <main className="flex-1 overflow-auto p-4">
                <div className="h-full">
                    <Calendar
                        localizer={localizer}
                        events={events}
                        startAccessor="start"
                        endAccessor="end"
                        selectable
                        onSelectSlot={handleSelectSlot}
                        onSelectEvent={handleSelectEvent}
                        view={currentView}
                        date={currentDate}
                        onView={view => setCurrentView(view as typeof viewValues[number])}
                        onNavigate={date => setCurrentDate(date)}
                        style={{ minHeight: "500px", height: "100%" }}
                        toolbar={false}
                    />
                </div>
            </main>

            <footer className="p-4 flex justify-center gap-4 bg-gray-50">
                <button onClick={goToPrev} className="p-2 rounded bg-gray-200 hover:bg-gray-300">◀</button>
                <button onClick={goToToday} className="px-3 py-2 rounded bg-gray-200 hover:bg-gray-300">Today</button>
                <button onClick={goToNext} className="p-2 rounded bg-gray-200 hover:bg-gray-300">▶</button>
            </footer>
        </div>
    );
};

export default AppointmentsPage;
