using System;

namespace Application.Comments;

public class CommentDto
{
    private DateTime _date;
    public int Id { get; set; }

    public DateTime CreatedAt
    {
        get => _date; 
        set => _date = value.ToUniversalTime();
    }
    public string Body { get; set; }
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Image { get; set; }
}