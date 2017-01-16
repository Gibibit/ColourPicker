using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ColourPicker
{
    /// <summary>
    ///     This program examines an image and prints the most common colours as hex values.
    /// </summary>
    public static class Program
    {
        private static string[] EXCLUDE = {"#000000", "#111111"};
        private static int MIN = 10;

        public static void Main(string[] args)
        {
            var imgLoc = "test.png";

            if (args.Length == 0)
            {
                Console.WriteLine("No image given, using test image.");
            }
            else if (!File.Exists(args[0]))
            {
                Console.WriteLine("Invalid file path, using test image.");

                try
                {
                    imgLoc = args[0];
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid file, using test image.");
                }
            }

            var bitmap = new Bitmap(imgLoc);

            var dict = new Dictionary<string, int>();

            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var colour = bitmap.GetPixel(x, y);

                    //var hexColour = ColorTranslator.ToHtml(colour);
                    var hexColour = $"#{colour.R.ToString("X").PadLeft(2, '0')}{colour.G.ToString("X").PadLeft(2, '0')}{colour.B.ToString("X").PadLeft(2, '0')}{colour.A.ToString("X").PadLeft(2, '0')}";

                    // if (EXCLUDE.Contains(hexColour)) continue;
                    if (EXCLUDE.Any(excl => hexColour.Contains(excl))) continue;

                    if (!dict.ContainsKey(hexColour))
                    {
                        dict.Add(hexColour, 1);
                    }

                    else
                    {
                        dict[hexColour]++;
                    }
                }
            }

            var colours = string.Join(", \n", dict.Where(x => x.Value > MIN).Select(x => x.Key));

            Console.WriteLine($"The most common colours in the image, excluding black or white are: \n{colours}");

            Console.ReadLine();
        }
    }
}