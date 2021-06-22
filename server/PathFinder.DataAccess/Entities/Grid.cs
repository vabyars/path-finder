using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace PathFinder.DataAccess1.Entities
{
    public class Grid
    {
        [Key] public string Name { get; set; }
        public int[,] Maze { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class IntTwoDimensionsArrayToStringValueConverter : ValueConverter<int[,], string>
    {
        public IntTwoDimensionsArrayToStringValueConverter() : base(le => ArrayToString(le), s => StringToIntArray(s))
        {
        }

        private static string ArrayToString(int[,] value)
        {
            return value == null || value.Length == 0 ? null : JsonConvert.SerializeObject(value);
        }

        private static int[,] StringToIntArray(string value)
        {
            return string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<int[,]>(value);
        }
    }

    public class PointToStringConverter : ValueConverter<Point, string>
    {
        public PointToStringConverter() : base(le => PointToString(le), s => StringToPoint(s))
        {
        }

        private static string PointToString(Point point)
        {
            return JsonConvert.SerializeObject(point);
        }

        private static Point StringToPoint(string value)
        {
            return string.IsNullOrEmpty(value) ? Point.Empty : JsonConvert.DeserializeObject<Point>(value);
        }
    }
}