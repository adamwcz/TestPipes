using System.Globalization;
using System.IO;

namespace TestPipesDLL
{
   public static partial class Utilities
   {
      /// <summary>
      /// Returns a readable string of a file size based on a number of bytes
      /// </summary>
      /// <param name="i">Size in bytes</param>
      /// <returns>A string representation of the size. (i.e. "4.68 KB" or "1.25 MB")</returns>
      /// Source: https://www.somacon.com/p576.php
      public static string GetBytesReadable(long i)
      {
         // Get absolute value
         long absolute_i = (i < 0 ? -i : i);
         // Determine the suffix and readable value
         string suffix;
         double readable;
         if (absolute_i >= 0x1000000000000000) // Exabyte
         {
            suffix = "EB";
            readable = (i >> 50);
         }
         else if (absolute_i >= 0x4000000000000) // Petabyte
         {
            suffix = "PB";
            readable = (i >> 40);
         }
         else if (absolute_i >= 0x10000000000) // Terabyte
         {
            suffix = "TB";
            readable = (i >> 30);
         }
         else if (absolute_i >= 0x40000000) // Gigabyte
         {
            suffix = "GB";
            readable = (i >> 20);
         }
         else if (absolute_i >= 0x100000) // Megabyte
         {
            suffix = "MB";
            readable = (i >> 10);
         }
         else if (absolute_i >= 0x400) // Kilobyte
         {
            suffix = "KB";
            readable = i;
         }
         else
         {
            return i.ToString("0 B"); // Byte
         }
         // Divide by 1024 to get fractional value
         readable = (readable / 1024);
         // Return formatted number with suffix
         return readable.ToString("0.### ", CultureInfo.InvariantCulture) + suffix;
      }
      /// <summary>
      /// Returns a readable string of a file size based on the file at a given path
      /// </summary>
      /// <param name="theFilePath">Path to the file to measure the size of</param>
      /// <returns>A string representation of the size. (i.e. "4.68 KB" or "1.25 MB")</returns>
      public static string GetFileSizeBytesReadable(string theFilePath)
      {
         long aFileSize = new FileInfo(theFilePath).Length;
         return GetBytesReadable(aFileSize);
      }
   }
}