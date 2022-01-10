using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;

namespace ZTStudio
{
    static class MdlTests
    {

        /// <summary>
    /// Recursively processes all files in a folder and writes the hash files out to a .INI file
    /// </summary>
    /// <param name="StrPath">Source Folder</param>
    /// <param name="StrDestinationFileName">Destination file name</param>
        public static void GetHashesOfFilesInFolder(string StrPath, string StrDestinationFileName)
        {

            // First create a recursive list.

            // This list stores the results.
            var result = new List<string>();

            // This stack stores the directories to process.
            var StackDirectories = new Stack<string>();

            // Add the initial directory
            StackDirectories.Push(StrPath);
        10:
            ;


            // Continue processing for each stacked directory
            while (StackDirectories.Count > 0)
            {
            // Get top directory string

            15:
                ;
                string StrCurrentDirectory = StackDirectories.Pop();
            20:
                ;
                foreach (string StrCurrentFile in Directory.GetFiles(StrCurrentDirectory, "*"))
                {
                    string ObjHash = Conversions.ToString(GenerateHash("sha256", StrCurrentFile));
                    MdlSettings.IniWrite(StrDestinationFileName, "Hashes", StrCurrentFile.Replace(StrPath + @"\", ""), ObjHash);
                    // ObjHash.dispose()

                }

            29:
                ;


            // Loop through all subdirectories and add them to the stack.
            30:
                ;
                foreach (var StrSubDirectoryName in Directory.GetDirectories(StrCurrentDirectory))
                    StackDirectories.Push(StrSubDirectoryName);
            }
        }

        /// <summary>
    /// Function to obtain the desired hash of a file
    /// </summary>
    /// <param name="StrHashType">Hash type</param>
    /// <param name="StrFileName">Source file name</param>
    /// <returns></returns>
        public static object GenerateHash(string StrHashType, string StrFileName)
        {

            // Declaring the variable : hash
            object HashGenerator;
            switch (StrHashType ?? "")
            {
                case "md5":
                    {
                        HashGenerator = MD5.Create();
                        break;
                    }

                case "sha1":
                    {
                        HashGenerator = SHA1.Create();
                        break;
                    }

                case "sha256":
                    {
                        HashGenerator = SHA256.Create();
                        break;
                    }

                default:
                    {
                        MdlZTStudio.HandledError("MdlTests", "GenerateHash", "Unknown type of hash: " + StrHashType, false, null);
                        return null;
                    }
            }

            // Declaring a variable to be an array of bytes
            byte[] HashValue;

            // Creating e a FileStream for the file passed as a parameter
            var FileStream = File.OpenRead(StrFileName);

            // Positioning the cursor at the beginning of stream
            FileStream.Position = 0L;
            // Calculating the hash of the file
            HashValue = (byte[])HashGenerator.ComputeHash(FileStream);
            // The array of bytes is converted into hexadecimal before it can be read easily
            var ObjHash = PrintByteArray(HashValue);

            // Closing the open file
            FileStream.Close();

            // The hash is returned
            return ObjHash;
        }

        /// <summary>
    /// Traverse the array of bytes and converting each byte in hexadecimal
    /// </summary>
    /// <param name="array">Byte array</param>
    /// <returns></returns>
        public static object PrintByteArray(byte[] Array)
        {
            string hex_value = "";

            // Traverse the array of bytes
            int I;
            var loopTo = Array.Length - 1;
            for (I = 0; I <= loopTo; I++)

                // Convert each byte in hexadecimal
                hex_value += Array[I].ToString("X2");

            // Return the string in lowercase
            return hex_value.ToLower();
        }
    }
}