using System;
using System.Collections.Generic;
using System.IO;

namespace Beowulf.ArmaTerrainExport
{
    public class AddIn
    {

        private static StreamWriter _current;

        public int Invoke(string function, IEnumerable<string> args)
        {
            switch (function)
            {
                case "new":
                    _current = new StreamWriter("C:/arma3/terrain/" + DateTime.Now.ToFileTime() + ".txt");
                    return 0;
                case "data":
                    _current.WriteLine(string.Join(" ", args));
                    return 0;
                case "end":
                    _current.Close();
                    _current = null;
                    return 0;
                default:
                    return 1;
            }
        }

        public static string GetVersion() => "0.1";
    }
}
