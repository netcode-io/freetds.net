using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DefGen
{
    class Program
    {
        static int Main(string[] args)
        {
            var r = ParseArgs(args, out var bitness, out var paths);
            if (r != 0)
                return r;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            var appSettings = configuration.GetSection("AppSettings");
            var vcBinPath = appSettings.GetValue<string>("VCBinPath");
            Environment.SetEnvironmentVariable("PATH", vcBinPath);
            Console.WriteLine($"VCBINPATH: {vcBinPath}");
            MakeDefFile(paths, bitness == "x86" ? 1 : 0);
            //foreach (var path in paths)
            //{
            //    Console.WriteLine($"BUILD: {path}");
            //    MakeDefFile(path, bitness == "x86" ? 1 : 0);
            //}
            return r;
        }

        static int ParseArgs(string[] args, out string bitness, out List<string> paths)
        {
            bitness = null;
            paths = null;
            if (args.Length < 2)
            {
                Console.WriteLine(@"HELP
DefGen x86 [path to lib]
DefGen x64 [path to lib]
");
                return 1;
            }
            bitness = args[0];
            if (bitness != "x86" && bitness != "x64")
            {
                Console.WriteLine("bitness must be x86 or x64");
                return 1;
            }
            paths = new List<string>();
            for (var i = 1; i < args.Length; i++)
            {
                var path = args[i];
                if (path.Contains("*"))
                {
                    paths.AddRange(Directory.GetFiles(Path.GetDirectoryName(path), Path.GetFileName(path)));
                    continue;
                }
                if (!File.Exists(path))
                {
                    Console.WriteLine($"NO FILE: {path}");
                    return 1;
                }
                paths.Add(path);
            }
            return 0;
        }

        static readonly string[] Excludes = new[] { "_vfprintf_l", "__local_stdio_printf_options", "fprintf" };
        static void MakeDefFile(IList<string> paths, int clip) => GenerateDefinitionFile(paths[0], paths.Aggregate(new List<string>(), (a, path) => { a.AddRange(GetExportableNames(path, clip).Except(Excludes)); return a; }));
        static void MakeDefFile(string path, int clip) => GenerateDefinitionFile(path, GetExportableNames(path, clip).Except(Excludes));

        static IEnumerable<string> GetExportableNames(string libFile, int clip)
        {
            var tmpFile = Path.GetTempFileName();
            var args = $@"/LINKERMEMBER:2 /OUT:""{tmpFile}"" ""{libFile}""";
            Process.Start("dumpbin.exe", args).WaitForExit();
            var lines = File.ReadAllLines(tmpFile);
            File.Delete(tmpFile);
            return lines
                .SkipWhile(x => !x.Contains("public symbols"))
                .Skip(2)
                .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim().Split(' '))
                .Select(x => new { Address = x[0], Name = x[1] })
                .Where(x => !x.Name.StartsWith("?") && !x.Name.StartsWith("__xmm@") && !x.Name.StartsWith("__real@"))
                .Select(x => x.Name.Substring(clip))
                .ToList();
        }

        static void GenerateDefinitionFile(string path, IEnumerable<string> exportedNames)
        {
            var library = Path.GetFileNameWithoutExtension(path);
            var defPath = Path.ChangeExtension(path, ".def");
            var defs = new List<string>
            {
                $"LIBRARY   {library}",
                "EXPORTS"
            };
            defs.AddRange(exportedNames.Select((x, i) => $"   {x} @{i + 1}"));
            File.WriteAllLines(defPath, defs);
        }
    }
}
