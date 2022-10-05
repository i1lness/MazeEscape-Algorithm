using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeEscape_Algorithm
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }

    internal class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }

        Random _random = new Random();

        Board _board;

        enum Dir
        {
            Up,
            Left,
            Down,
            Right
        }

        int _dir = (int)Dir.Up;

        List<Pos> _points = new List<Pos>();

        public Player(int posY, int posX, Board board)
        {
            PosY = posY;
            PosX = posX;
            _board = board;
        }

        public void Initialize()
        {
            int[] frontY = new int[4] { -1, 0, 1, 0 };
            int[] frontX = new int[4] { 0, -1, 0, 1 };
            int[] rightY = new int[4] { 0, -1, 0, 1 };
            int[] rightX = new int[4] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    _dir = (_dir - 1 + 4) % 4;

                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }

        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;
            _sumTick = deltaTick;
            if (_sumTick > MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}
