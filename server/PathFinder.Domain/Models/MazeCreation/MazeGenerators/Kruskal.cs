using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PathFinder.Domain.Models.GridFolder;

namespace PathFinder.Domain.Models.MazeCreation.MazeGenerators
{
    public class Kruskal : IMazeGenerator
    {
        public string Name => "Kruskal";

        private Grid grid;
        private Grid uniqueNumbersGrid;
        private int height;
        private int width;
        
        private const int WallValue = -1;
        
        public int[,] Create(int resultWidth, int resultHeight)
        {
            width = resultWidth;
            height = resultHeight;
            grid = new Grid(new int[resultHeight, resultWidth]);
            var mazeGrid = GetGridWithWallsEverywhere();
            uniqueNumbersGrid = CreateGridWithUniqueNumbers();
            var candidates = CreateListOfWalls();
            while (candidates.Count > 0)
            {
                var wall = ExtractWallFromCandidateSetRandomly(candidates);
                var neighbor= GetRandomNeighbour(wall);
                if (grid.InBounds(neighbor) && ValuesNotEqual(wall, neighbor))
                {
                    ChangeNeighborValue(wall, neighbor);
                    mazeGrid[wall.X, wall.Y] = 1;
                }
            }
            return mazeGrid;
        }
        
        private int[,] GetGridWithWallsEverywhere()
        {
            var temp = new int[height, width];
            ApplyFunctionForeachPointInGrid((x, y) => temp[x, y] = WallValue);
            
            return temp;
        }
        
        private Grid CreateGridWithUniqueNumbers()
        {
            var board = new Grid(new int[height, width]);
            var counter = 1;
            ApplyFunctionForeachPointInGrid((x, y) => board[x, y] = counter++);
            
            return board;
        }
        
        private List<Point> CreateListOfWalls()
        {
            var walls = new List<Point>();
            ApplyFunctionForeachPointInGrid((x, y) =>
            {
                var wall = new Point(x, y);
                if (!grid.InBounds(wall))
                    return;
                for (var i = 0; i < 4; i++)
                    walls.Add(wall);
            });

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
            
            ApplyFunctionForeachPointInGrid((x, y) =>
            {
                if (uniqueNumbersGrid[x, y] == neighborValue) 
                {
                    uniqueNumbersGrid[x, y] = currentValue;
                }
            });
        }
        
        private static int GetRandomIndexLessUpperBound(int rightBoarder)
        {
            var rnd = new Random();
            return rnd.Next(rightBoarder);
        }

        private void ApplyFunctionForeachPointInGrid(Action<int, int> updateFunc)
        {
            for (var x = 0; x < height; x++)
            {
                for (var y = 0; y < width; y++)
                {
                    updateFunc(x, y);
                }
            }
        }
    }
}
