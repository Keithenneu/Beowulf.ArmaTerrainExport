using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using RGiesecke.DllExport;
using System.Runtime.InteropServices;
using System.Text;

namespace Beowulf.ArmaTerrainExport
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    internal class DllEntry
    {
        private static AddIn _addin;
        private static bool _initFailed = false;

        private static AddIn GetAddIn()
        {
            if (_initFailed) return null;
            try
            {
                return _addin ?? (_addin = new AddIn());
            }
            catch
            {
                _initFailed = true;
                return null;
            }
        }

        /// <summary>
        /// Gets called when arma starts up and loads all extension.
        /// It's perfect to load in static objects in a seperate thread so that the extension doesn't needs any seperate initalization
        /// </summary>
        /// <param name="output">The string builder object that contains the result of the function</param>
        /// <param name="outputSize">The maximum size of bytes that can be returned</param>
        [DllExport("RVExtensionVersion", CallingConvention = CallingConvention.Winapi)]
        public static void RvExtensionVersion(StringBuilder output, int outputSize)
        {
            try
            {
                var addin = GetAddIn();
                if (addin == null) return;
                output.Append(AddIn.GetVersion());
            }
            catch
            {
                // Don't kill the server
            }
        }

        /// <summary>
        /// The entry point for the default callExtension command.
        /// </summary>
        /// <param name="output">The string builder object that contains the result of the function</param>
        /// <param name="outputSize">The maximum size of bytes that can be returned</param>
        /// <param name="function">The string argument that is used along with callExtension</param>
        [DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]
        public static void RvExtension(StringBuilder output, int outputSize,
            [MarshalAs(UnmanagedType.LPStr)] string function)
        {

        }

        /// <summary>
        /// The entry point for the callExtensionArgs command.
        /// </summary>
        /// <param name="output">The string builder object that contains the result of the function</param>
        /// <param name="outputSize">The maximum size of bytes that can be returned</param>
        /// <param name="function">The string argument that is used along with callExtension</param>
        /// <param name="args">The args passed to callExtension as a string array</param>
        /// <param name="argCount">The size of the string array args</param>
        /// <returns>The result code</returns>
        [DllExport("RVExtensionArgs", CallingConvention = CallingConvention.Winapi)]
        public static int RvExtensionArgs(StringBuilder output, int outputSize,
            [MarshalAs(UnmanagedType.LPStr)] string function,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)]
            string[] args, int argCount)
        {
            try
            {
                outputSize--; // Ensure that we don't exceed the maximum output size - it's a bit paranoid but you should keep it there

                var addin = GetAddIn();
                if (addin == null) return 1;
                var error = _addin.Invoke(function, args);
                return error;

            }
            catch
            {
                // Don't kill the server
                return 2;
            }
        }
    }
}
