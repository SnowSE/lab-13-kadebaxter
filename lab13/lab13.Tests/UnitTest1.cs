using FluentAssertions;
using lab13.Logic;
namespace lab13.Tests;

public class UnitTest1
{
    [Fact]
    public void TestEvents_Manually()
    {
        //start off with a bool that stores whether or not the event was ever raised
        bool eventRaised = false;
        var o = new AttendanceManager();

        //subscribe to the event you want to test, and in your event handler, update the bool
        o.AttendeeAdded += () => eventRaised = true;

        //call code that should raise the event
        o.AddAttendee("Chuck Norris");

        //ensure that the bool shows the event was indeed raised
        eventRaised.Should().BeTrue();
    }

    [Fact]
    public void TestAddAttendee_AddsPersonToList()
    {
        var o = new AttendanceManager();

        o.AddAttendee("Somebody");

        o.GetAllAttendees().Should().HaveCount(1);
    }

    [Fact]
    public void TestAddAttendee_NameNull()
    {
        var o = new AttendanceManager();
        o.Invoking((o) => o.AddAttendee(null))
         .Should()
         .Throw<ArgumentNullException>();
    }

    [Fact]
    public void TestCheckInAttendee_RaisesAttendeeCheckedIn()
    {
        var o = new AttendanceManager();
        Attendee person = new Attendee("Some Name");
        var eventMonitor = o.Monitor();

        o.CheckInAttendee(person);

        eventMonitor.Should().Raise("AttendeeCheckedIn");
    }

    [Fact]
    public void TestCheckInAttendee_SetsTimestamp()
    {
        Attendee person = new Attendee("Somebody");
        var o = new AttendanceManager();

        o.CheckInAttendee(person);

        person.CheckedInAt.Should().BeAfter(DateTime.Now.Date);
    }

    [Fact]
    public void TestCheckInAttendee_ArgumentNullException()
    {
        var o = new AttendanceManager();
        o.Invoking((o) => o.CheckInAttendee(null))
         .Should()
         .Throw<ArgumentNullException>();
    }

    [Fact]
    public void TestCheckInAttendee_AlreadyCheckedInException()
    {
        Attendee person = new Attendee("Somebody");
        var o = new AttendanceManager();

        o.CheckInAttendee(person);

        o.Invoking((o) => o.CheckInAttendee(person))
         .Should()
         .Throw<AlreadyCheckedInException>();
    }

    [Fact]
    public void TestGetAllAttendees_GetsList()
    {
        var o = new AttendanceManager();
        o.AddAttendee("Somebody");
        var list = o.GetAllAttendees();
        list.Should().HaveCount(1);
    }
}