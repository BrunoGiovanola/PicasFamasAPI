namespace NumberGuessGameApi.Models;

public class Game
{
    public int GameId { get; set; }

    public int PlayerId { get; set; }

    public Player Player { get; set; } = null!;

    public string SecretNumber { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public bool Finished { get; set; } = false;

    public ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
}