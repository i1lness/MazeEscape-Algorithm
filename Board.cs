using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeEscape_Algorithm
{
    internal class Board
    {
        const char CIRCLE = '\u25cf';

        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }

        public int DestY { get; private set; }
        public int DestX { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall
        }

        enum HeadingType
        {
            Right,
            Down
        }

        public Board(int size, out Player player)
        {
            DestY = size - 2;
            DestX = size - 2;

            player = new Player(1, 1, this);
            _player = player;

            if (size % 2 == 0)
                size++;

            Tile = new TileType[size, size];
            Size = size;
        }

        public void GenerateMap()
        {
            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            if (Tile == null)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(CIRCLE);
                return;
            }

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = (GetTileColor(Tile[y, x]));

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Black;
                case TileType.Wall:
                    return ConsoleColor.White;
                default:
                    return ConsoleColor.Black;
            }
        }

        void GenerateByBinaryTree()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        Tile[y, x] = TileType.Wall;
                    }
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            Random random = new Random();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 || x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (random.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        void GenerateBySideWinder()
        {
            if (Tile == null)
                return;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        Tile[y, x] = TileType.Wall;
                    }
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            Random random = new Random();

            for (int y = 0; y < Size; y++)
            {

                int count = 1;

                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (random.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = random.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }
    }
}
