using System;

namespace Domain;

public class Comment
{
    private DateTime _date = DateTime.UtcNow;
    
    public int Id { get; set; }
    public string Body { get; set; }
    public AppUser Author { get; set; }
    public Activity Activity { get; set; }

    public DateTime CreatedAt
    {
        get => _date; 
        set => _date = value.ToUniversalTime();
    }
}