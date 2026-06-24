namespace NumberGuessGameApi.Models;

public class Player
{
    public int PlayerId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Game> Games { get; set; } = new List<Game>();
}