namespace MazeEscape_Algorithm
{
    public class Program
    {
        static void Main(String[] args)
        {
            Player player;
            Board board = new Board(25, out player);
            board.GenerateMap();
            player.Start();

            Console.CursorVisible = false;

            const int WAIT_TICK = (1 / 30) * 1000;

            int lastTick = 0;

            while (true)
            {
                #region FrameLock
                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                player.Update(deltaTick);

                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}
