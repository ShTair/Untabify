using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Untabify
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                var target = new Target
                {
                    Path = AppDomain.CurrentDomain.BaseDirectory,
                    SpaceCount = 4,
                    Exts = new List<string> { ".htm", ".html", ".css", ".js", ".json" },
                };

                var json = JsonConvert.SerializeObject(target, Formatting.Indented);
                var name = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.json");
                File.WriteAllText(name, json);

                return;
            }
            else
            {
                var json = File.ReadAllText(args[0]);
                var target = JsonConvert.DeserializeObject<Target>(json);

                var spaces = string.Join("", Enumerable.Range(0, target.SpaceCount).Select(_ => " "));

                foreach (var name in Directory.EnumerateFiles(target.Path, "*", SearchOption.AllDirectories))
                {
                    var ext = Path.GetExtension(name).ToLower();
                    if (!target.Exts.Contains(ext)) continue;

                    Console.WriteLine(name);

                    var text = File.ReadAllText(name);
                    text = text.Replace("\t", spaces);
                    File.WriteAllText(name, text);
                }
            }
        }
    }
}
