using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain.Models.MazeGenarators
{
    public class Kruskal : IMazeGenerator
    {
        public string Name => "Kruskal";
        
        private readonly Grid grid;
        private Grid uniqueNumbersGrid;
        private readonly int height;
        private readonly int width;
        private readonly int[,] mazeGrid;
        private const int WallValue = -1;
        public Kruskal(Grid grid, int width, int height)
        {
            this.grid = grid;
            this.width = width;
            this.height = height;
            mazeGrid = GetGridWithWallsEverywhere();
        }

        public int[,] Create()
        {
            uniqueNumbersGrid = CreateGridWithUniqueNumbers();
            var candidates = CreateListOfWalls();
            while (candidates.Count > 0)
            {
                var wall = ExtractWallFromCandidateSetRandomly(candidates);
                var neighbor= GetRandomNeighbour(wall);
                if (grid.InBounds(neighbor) && ValuesNotEqual(wall, neighbor))
                {
                    ChangeNeighborValue(wall, neighbor);
                    mazeGrid[wall.X, wall.Y] = grid[wall.X, wall.Y];
                }
            }

            return mazeGrid;
        }
        
        private int[,] GetGridWithWallsEverywhere()
        {
            var temp = new int[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    temp[x, y] = WallValue;
                }
            }

            return temp;
        }
        
        private Grid CreateGridWithUniqueNumbers()
        {
            var board = new Grid(new int[width, height]);
            var counter = 1;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    board[x, y] = counter;
                    counter++;
                }
            }

            return board;
        }
        
        private List<Point> CreateListOfWalls()
        {
            var walls = new List<Point>();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var wall = new Point(x, y);
                    if (!grid.InBounds(wall)) 
                        continue;
                    for (var i = 0; i < 4; i++)
                        walls.Add(wall);
                }
            }

            return walls;
        }
        
        private static Point ExtractWallFromCandidateSetRandomly(IList<Point> candidates)
        {
            var index = GetRandomIndexLessUpperBound(candidates.Count);
            var wall = candidates[index];
            candidates.RemoveAt(index);
            return wall;
        }
        
        private Point GetRandomNeighbour(Point point)
        {
            var neighbors = grid.GetNeighbors(point, false).ToList();
            var index = GetRandomIndexLessUpperBound(neighbors.Count);
            return neighbors[index];
        }

        private bool ValuesNotEqual(Point first, Point second) => 
            uniqueNumbersGrid[first.X, first.Y] != uniqueNumbersGrid[second.X, second.Y];
        
        private void ChangeNeighborValue(Point current, Point neighbor)
        {
            var currentValue = uniqueNumbersGrid[current.X, current.Y];
            var neighborValue = uniqueNumbersGrid[neighbor.X, neighbor.Y];
            for (var x = 0; x < width; x++) 
            {
                for (var y = 0; y < height; y++) 
                {
                    if (uniqueNumbersGrid[x, y] == neighborValue) 
                    {
                        uniqueNumbersGrid[x, y] = currentValue;
                    }
                }
            }
        }
        
        private static int GetRandomIndexLessUpperBound(int rightBoarder)
        {
            var rnd = new Random();
            return rnd.Next(rightBoarder);
        }
    }
}
