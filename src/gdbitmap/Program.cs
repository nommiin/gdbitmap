using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace gdbitmap {
    class Program {
        static void Main(string[] args) {
            foreach (String gdRead in args) {
                using (BinaryReader gdBinary = new BinaryReader(File.Open(gdRead, FileMode.Open))) {
                    // Read header
                    int w, h;
                    gdBinary.BaseStream.Seek(3, SeekOrigin.Begin);
                    w = gdBinary.ReadByte();
                    gdBinary.BaseStream.Seek(1, SeekOrigin.Current);
                    h = gdBinary.ReadByte();
                    gdBinary.BaseStream.Seek(6, SeekOrigin.Current);

                    // Create bitmap
                    Bitmap gdBitmap = new Bitmap(w, h);
                    Graphics gdGraphics = Graphics.FromImage(gdBitmap);

                    // Read pixels
                    for (int y = 0; y < gdBitmap.Height; y++) {
                        for (int x = 0; x < gdBitmap.Width; x++) {
                            gdBinary.BaseStream.Seek(1, SeekOrigin.Current);
                            gdGraphics.DrawRectangle(new Pen(Color.FromArgb(255, gdBinary.ReadByte(), gdBinary.ReadByte(), gdBinary.ReadByte())), x, y, 1, 1);
                        }
                    }

                    // Save bitmap
                    gdBitmap.Save(Path.GetDirectoryName(gdRead) + @"\" + Path.GetFileNameWithoutExtension(gdRead) + ".bmp");
                }
            }
        }
    }
}
