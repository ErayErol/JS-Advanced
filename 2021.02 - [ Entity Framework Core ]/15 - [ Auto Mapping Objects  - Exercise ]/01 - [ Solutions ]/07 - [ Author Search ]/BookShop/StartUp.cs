﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using BookShop.Models;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using Data;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            string input = Console.ReadLine();
            string result = GetAuthorNamesEndingIn(db, input);
            Console.WriteLine(result);
            //File.WriteAllText("../../../r.txt", result);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context
                .Authors
                .Where(b => b.FirstName.EndsWith(input))
                .Select(b => new { FullName = b.FirstName + " " + b.LastName,})
                .OrderBy(b => b.FullName)
                .ToList();

            var sb = new StringBuilder();
            authors.ForEach(author => sb.AppendLine(author.FullName));
            var result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
