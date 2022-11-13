using System;
using System.Collections.Generic;

namespace Maze
{
    [Flags]
    public enum WallState
    {
        LEFT    = 1,
        RIGHT   = 2,
        UP      = 4,
        DOWN    = 8,

        VISITED = 128,
    }

    public struct Position
    {
        public int m_X;
        public int m_Y;
    }

    public struct Neighbour
    {
        public Position m_position;
        public WallState m_sharedWall;
    }

    public class MazeGenerator
    {
        private static WallState GetOppositeWal(WallState wall)
        {
            switch (wall)
            {
                case WallState.RIGHT: return WallState.LEFT;
                case WallState.LEFT: return WallState.RIGHT;
                case WallState.UP: return WallState.DOWN;
                case WallState.DOWN: return WallState.UP;
                default: return WallState.LEFT;
            }
        }

        private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, uint width, uint height)
        {
            var random = new System.Random();
            var positionStack = new Stack<Position>();
            var position = new Position { m_X = random.Next(0, (int)width), m_Y = random.Next(0, (int)height) };

            maze[position.m_X, position.m_Y] |= WallState.VISITED;
            positionStack.Push(position);

            while (positionStack.Count > 0)
            {
                var current = positionStack.Pop();
                var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

                if (neighbours.Count > 0)
                {
                    positionStack.Push(current);

                    var randomIndex = random.Next(0, neighbours.Count);
                    var randomNeighbour = neighbours[randomIndex];

                    var newPosition = randomNeighbour.m_position;
                    maze[current.m_X, current.m_Y] &= ~randomNeighbour.m_sharedWall;
                    maze[newPosition.m_X, newPosition.m_Y] &= ~GetOppositeWal(randomNeighbour.m_sharedWall);

                    maze[newPosition.m_X, newPosition.m_Y] |= WallState.VISITED;

                    positionStack.Push(newPosition);
                }
            }

            return maze;
        }

        private static List<Neighbour> GetUnvisitedNeighbours(Position position, WallState[,] maze, uint width, uint height)
        {
            var list = new List<Neighbour>();

//          LEFT
            if (position.m_X > 0)
            {
                if (!maze[position.m_X - 1, position.m_Y].HasFlag(WallState.VISITED))
                {
                    list.Add(new Neighbour
                    {
                        m_position = new Position
                        {
                            m_X = position.m_X - 1,
                            m_Y = position.m_Y
                        },
                        m_sharedWall = WallState.LEFT
                    });
                }
            }

//          RIGHT
            if (position.m_X < width - 1)
            {
                if (!maze[position.m_X + 1, position.m_Y].HasFlag(WallState.VISITED))
                {
                    list.Add(new Neighbour
                    {
                        m_position = new Position
                        {
                            m_X = position.m_X + 1,
                            m_Y = position.m_Y
                        },
                        m_sharedWall = WallState.RIGHT
                    });
                }
            }

//          UP
            if (position.m_Y < height - 1)
            {
                if (!maze[position.m_X, position.m_Y + 1].HasFlag(WallState.VISITED))
                {
                    list.Add(new Neighbour
                    {
                        m_position = new Position
                        {
                            m_X = position.m_X,
                            m_Y = position.m_Y + 1
                        },
                        m_sharedWall = WallState.UP
                    });
                }
            }

//          BOTTOM
            if (position.m_Y > 0)
            {
                if (!maze[position.m_X, position.m_Y - 1].HasFlag(WallState.VISITED))
                {
                    list.Add(new Neighbour
                    {
                        m_position = new Position
                        {
                            m_X = position.m_X,
                            m_Y = position.m_Y - 1
                        },
                        m_sharedWall = WallState.DOWN
                    });
                }
            }

            return list;
        }

        public static WallState[,] Generate(uint width, uint height)
        {
            WallState[,] maze = new WallState[width, height];
            WallState initial = WallState.LEFT | WallState.RIGHT | WallState.UP | WallState.DOWN;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    maze[i, j] = initial;
                }
            }

            return ApplyRecursiveBacktracker(maze, width, height);
        }
    }
}
