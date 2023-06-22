namespace ClassLibrary1
{
    public class User
    {
        public string? Name { get; set; }
        public int Color { get; set; }
        public string? IpAddress { get; set; }

        public User()
        {
            Console.Write("Input Username: ");
            Name = Console.ReadLine();
            for (int i = 1; i < Enum.GetNames(typeof(ConsoleColor)).Length; i++)
            {
                Console.ForegroundColor = (ConsoleColor)i;
                Console.WriteLine($"[{i}] {(ConsoleColor)i}");
            }
            int color;
            do Console.Write("Pick color: ");
            while (!int.TryParse(Console.ReadLine(), out color)
                || color < 1 || color >= Enum.GetNames(typeof(ConsoleColor)).Length);
            Color = color;
        }
    }
}
