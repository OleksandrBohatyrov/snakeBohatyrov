using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snakeBohatyrov
{
    internal class Params
    {
        private string resourceFolder;

        public Params()
        {
            var ind = Directory.GetCurrentDirectory().ToString().IndexOf("bin", StringComparison.Ordinal);

            string binFolder = Directory.GetCurrentDirectory().ToString().Substring(0, ind).ToString();

            resourceFolder = binFolder + "resources\\";
        }

        public string GetResourceFolder()
        {
            return resourceFolder;
        }
    }
}
