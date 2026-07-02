namespace NumberGuessGameApi.DataTransferObjects;

public class RegisterPlayerRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

public class RegisterPlayerResponse
{
    public int PlayerId { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public int PlayerId { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class StartGameRequest
{
    public int PlayerId { get; set; }
}

public class StartGameResponse
{
    public int GameId { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class GuessRequest
{
    public int GameId { get; set; }
    public string Number { get; set; } = string.Empty;
}