using System.Collections.Generic;

namespace Untabify
{
    class Target
    {
        public string Path { get; set; }

        public int SpaceCount { get; set; }

        public List<string> Exts { get; set; }
    }
}
