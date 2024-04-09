namespace lab13.Logic;

public class AttendanceManager
{
    public AttendanceManager()
    {
    }

    private List<Attendee> attendees = [];
    public event Action AttendeeAdded;
    public event Action AttendeeCheckedIn;

    public void AddAttendee(string name)
    {
        AttendeeAdded?.Invoke();
        if (name == null)
            throw new ArgumentNullException();
            
        Attendee person = new Attendee(name);
        attendees.Add(person);
    }

    public List<Attendee> GetAllAttendees()
    {
        return attendees;
    }
}

public class Attendee
{
    public string Name { get; set; }
    public Attendee(string name)
    {
        Name = name;
    }
}
