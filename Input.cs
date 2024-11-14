namespace BlackJackGame;

public class Input
{
    //PressedKeys
    public static HashSet<Keys> pressedKeys = new HashSet<Keys>();
    public static Thread thread = new Thread(IsPressed);
    static bool state;

    public Input(bool s)
    {
        state = s;
    }
    static void IsPressed()
    {
        while (gameloop == true)
        {
            var KeyInfo = Console.ReadKey(intercept: true);

            Keys key = KeyInfo.Key switch
            {
                ConsoleKey.Enter => Keys.Enter,
                ConsoleKey.Spacebar => Keys.Space,
                ConsoleKey.Escape => Keys.Escape,
                ConsoleKey.C when (KeyInfo.Modifiers == ConsoleModifiers.Control) => Keys.CTRLC,
                ConsoleKey.H => Keys.Hit,
                ConsoleKey.X => Keys.Stop,
                ConsoleKey.S => Keys.Split,
                ConsoleKey.B => Keys.Bid,
                ConsoleKey.LeftArrow => Keys.Left,
                ConsoleKey.RightArrow => Keys.Right,
                ConsoleKey.UpArrow => Keys.Up,
                ConsoleKey.DownArrow => Keys.Down,
                _ => Keys.None
            };

            if (key != Keys.None)
            {
                pressedKeys.Add(key);
            }
        }
    }
    // Method to check if a specific key is pressed
    public static bool OnKey(Keys key)
    {
        return pressedKeys.Contains(key);
    }
    public enum Keys
    {
        Enter,
        Space,
        Escape,
        CTRLC,
        Hit,
        Stop,
        Split,
        Bid,
        Left,
        Right,
        Up,
        Down,
        None
    }
}
