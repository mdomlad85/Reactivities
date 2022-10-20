using System;

namespace Domain;

public class Activity
{
    private DateTime _date;
    public Guid Id { get; set; }
    public string Title { get; set; }

    public DateTime Date
    {
        get => _date; 
        set => _date = value.ToUniversalTime();
    }
    public string Description { get; set; }
    public string Category { get; set; }
    public string City { get; set; }
    public string Venue { get; set; }
}
