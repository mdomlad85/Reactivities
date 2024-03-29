﻿using System;
using System.Collections.Generic;

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
    public bool IsCancelled { get; set; }
    public ICollection<ActivityAttendee> Attendees { get; set; } = new List<ActivityAttendee>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
