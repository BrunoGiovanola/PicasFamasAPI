namespace NumberGuessGameApi.Models;

public class Game
{
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public string SecretNumber { get; set; } = string.Empty;
    public bool IsFinished { get; set; } = false;
    public DateTime CreatedAt { get; set; }

    public Player Player { get; set; } = null!;
    public ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
}