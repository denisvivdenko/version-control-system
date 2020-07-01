using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

namespace vcs 
{
    public class Stage
    {
        public readonly List<Blob> Tracked;
        public readonly List<Blob> Untracked;

        public Stage(List<Blob> tracked, List<Blob> untracked)
        {
            Tracked = tracked;
            Untracked = untracked;
        }
        
        public Stage AddToTracked(Blob blob)
        {
            List<Blob> tracked = new List<Blob>();
            List<Blob> untracked = new List<Blob>();
            
            foreach (Blob compare in Untracked)
            {
                if (compare.IsEqual(blob))
                {
                    continue;
                }

                untracked.Add(compare);
            }

            tracked.AddRange(Tracked);
            tracked.Add(blob);

            return new Stage(tracked, untracked);
        }

        public Stage AddToUntracked(Blob blob)
        {
            List<Blob> tracked = new List<Blob>();
            List<Blob> untracked = new List<Blob>();
            
            foreach (Blob compare in Tracked)
            {
                if (compare.IsEqual(blob))
                {
                    continue;
                }

                tracked.Add(compare);
            }

            untracked.AddRange(Tracked);
            untracked.Add(blob);

            return new Stage(tracked, untracked);
        }

        public void PrintInformation()
        {
            Console.WriteLine("tracked: ");
            foreach (Blob file in Tracked)
            {
                Console.WriteLine($"\t {file.FilePath}");
            }

            Console.WriteLine("\nuntracked: ");
            foreach (Blob file in Untracked)
            {
                Console.WriteLine($"\t {file.FilePath}");
            }
        }

        public void WriteToIndex(CallDirectory callDirectory)
        {
            string indexPath = callDirectory.FindRepositoryFolder().ToString() + "\\index";

            using (StreamWriter streamWriter = new StreamWriter(indexPath)) 
            {
                foreach (Blob tracked in Tracked)
                {
                    streamWriter.WriteLine($"{tracked.Hash} {tracked.FilePath}");
                }
            }
        }
    }
}