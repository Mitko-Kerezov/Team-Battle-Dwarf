﻿namespace SummerOlympiads.ConsoleClient
{
    using System;

    using SummerOlympiads.Data.Excel;
    using SummerOlympiads.Model;
    using SummerOlympiads.Utils;

    internal class EntryPoint
    {
        private static void Main()
        {
            using (var db = new OlympiadsEntities())
            {
                var files = ZipHandler.ExtractDefaultFile();
                foreach (var file in files)
                {
                    var importer = new ExcelReader(file);
                    foreach (var record in importer)
                    {
                        Console.WriteLine(record.Year  + " " + record.PersonId);
                    }
                }
            }
            ZipHandler.CleanUp();
        }
    }
}