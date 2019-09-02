using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Windows.Forms;

namespace PCPOS
{
    internal class Zip
    {
        public static void Go(string pathFile, string pathZip)
        {
            try
            {
                ZipOutputStream zip = new ZipOutputStream(File.Create(pathZip));
                zip.SetLevel(9);
                string folder = pathFile;
                ZipFolder(pathFile, pathZip, pathFile, zip);
                zip.Finish();
                zip.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void ZipFolder(string RootFolder, string pathZip, string CurrentFolder, ZipOutputStream zStream)
        {
            try
            {
                string[] SubFolders = Directory.GetDirectories(CurrentFolder);
                foreach (string Folder in SubFolders)
                    ZipFolder(RootFolder, pathZip, Folder, zStream);

                string relativePath = CurrentFolder.Substring(RootFolder.Length) + "/";

                if (relativePath.Length > 1)
                {
                    ZipEntry dirEntry;
                    dirEntry = new ZipEntry(relativePath);
                    dirEntry.DateTime = DateTime.Now;
                }
                foreach (string file in Directory.GetFiles(CurrentFolder))
                {
                    if (file != pathZip)
                        AddFileToZip(zStream, relativePath, file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void AddFileToZip(ZipOutputStream zStream, string relativePath, string file)
        {
            try
            {
                byte[] buffer = new byte[4096];
                string fileRelativePath = (relativePath.Length > 1 ? relativePath : string.Empty) + Path.GetFileName(file);
                ZipEntry entry = new ZipEntry(fileRelativePath);
                entry.DateTime = DateTime.Now;
                zStream.PutNextEntry(entry);
                using (FileStream fs = File.OpenRead(file))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        zStream.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}