using System.Runtime.Serialization;

namespace lab13.Logic;

public class AttendanceManager
{
    public AttendanceManager()
    {
    }

    private List<Attendee> attendees = new();
    public event Action? AttendeeAdded;
    public event Action? AttendeeCheckedIn;
    public static AttendanceManager Instance = new();
    public List<Attendee> GetAllAttendees()
    {
        return attendees;
    }

    public void AddAttendee(string? name)
    {
        if (name == null || name == "")
            throw new ArgumentNullException();

        AttendeeAdded?.Invoke();
        Attendee person = new Attendee(name);
        attendees.Add(person);
    }

    public void CheckInAttendee(Attendee? attendee)
    {
        if (attendee == null)
            throw new ArgumentNullException();

        if (attendee.CheckedInAt != null)
            throw new AlreadyCheckedInException();
            
        AttendeeCheckedIn?.Invoke();
        attendee.CheckedInAt = DateTime.UtcNow;
    }
}

[Serializable]
public class AlreadyCheckedInException : Exception
{
    public AlreadyCheckedInException()
    {
    }

    public AlreadyCheckedInException(string? message) : base(message)
    {
    }

    public AlreadyCheckedInException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected AlreadyCheckedInException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

public class Attendee
{
    public string? Name { get; set; }
    public DateTime? CheckedInAt{ get; set; }
    public Attendee(string name)
    {
        Name = name;
    }
}
