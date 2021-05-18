using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace PathFinder.DataAccess1.Entities
{
    public class Grid
    {
        [Key] public string Name { get; set; }
        public int[,] Maze { get; set; }
    }

    public class IntTwoDimensionsArrayToStringValueConverter : ValueConverter<int[,], string>
    {
        public IntTwoDimensionsArrayToStringValueConverter() : base(le => ArrayToString(le), s => StringToIntArray(s))
        {
        }

        public static string ArrayToString(int[,] value)
        {
            if (value == null || value.Length == 0)
            {
                return null;
            }

            return JsonConvert.SerializeObject(value);
        }

        public static int[,] StringToIntArray(string value)
        {
            return string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<int[,]>(value);
        }
    }
}