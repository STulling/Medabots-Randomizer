using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medabots_Patcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        byte[] file;

        private void WriteInt(byte[] bytes, uint offset, uint opcode)
        {
            byte[] bytez = BitConverter.GetBytes(opcode);
            bytes[offset] = bytez[0];
            bytes[offset + 1] = bytez[1];
            bytes[offset + 2] = bytez[2];
            bytes[offset + 3] = bytez[3];
        }

        private void WritePayload(byte[] bytes, uint offset, uint[] payload)
        {
            for (uint i = 0; i < payload.Length; i++)
            {
                WriteInt(bytes, offset + 4 * i, payload[i]);
            }
        }

        private void WriteShort(byte[] bytes, uint offset, ushort opcode)
        {
            byte[] bytez = BitConverter.GetBytes(opcode);
            bytes[offset] = bytez[0];
            bytes[offset + 1] = bytez[1];
        }

        private void WritePatches(byte[] bytes, Dictionary<uint, ushort> codePatches)
        {
            foreach (KeyValuePair<uint, ushort> entry in codePatches)
            {
                WriteShort(bytes, entry.Key, entry.Value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string chosenFile = openFileDialog.FileName;
                file = File.ReadAllBytes(chosenFile);
                byte[] id_bytes = new byte[0x12];
                Array.Copy(file, 0xa0, id_bytes, 0, 0x12);
                string id_string = Encoding.Default.GetString(id_bytes);
                if (id_string.Contains("MEDACORE"))
                {
                    file = null;
                    MessageBox.Show("PLEASE SELECT AN ENGLISH MEDABOTS ROM");
                    return;
                }
                if (!id_string.Contains("MEDABOTS"))
                {
                    file = null;
                    MessageBox.Show("PLEASE SELECT A MEDABOTS ROM");
                    return;
                }
                RomLabel.Content = "ROM Loaded";
            }
        }

        public static int GetIntAtPosition(byte[] bytes, int address)
        {
            int read_int = 0;
            read_int += (bytes[address + 3] << 24);
            read_int += (bytes[address + 2] << 16);
            read_int += (bytes[address + 1] << 8);
            read_int += bytes[address];
            return read_int;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (file == null)
            {
                MessageBox.Show("PLEASE SELECT A ROM");
                return;
            }

            uint jumpOffset = 0x104;
            uint hookOffset = 0x7f3530;
            uint trainerOffset = 0x7f3600;
            uint instr1 = (uint)GetIntAtPosition(file, (int)jumpOffset);
            uint instr2 = (uint)GetIntAtPosition(file, (int)jumpOffset + 4);
            uint instr3 = (uint)GetIntAtPosition(file, (int)jumpOffset + 8);
            uint[] jumpPayload = new uint[]
            {
                0xE92D8000,                         // push r15
                0xE51FF004,                         // ldr r15, traineraddr
                0x08000000 + hookOffset             // hookOffset
            };
            uint[] hookPayload = new uint[]
            {
                0xE92D4000,                         // push r14
                0xE3A0E402,                         // mov r14, #0x2000000
                0xE28EE701,                         // add r14, #40000
                0xE24EE004,                         // sub r14, #28
                0xE90E08FF,                         // stmdb [r14], r0-r7, r11
                0xEB00002D,                         // bl trainerfunc
                0xE3A0E402,                         // mov r14, #0x2000000
                0xE28EE701,                         // add r14, #40000
                0xE24EE028,                         // sub r14, #28
                0xE89E08FF,                         // ldmia [r14], r0-r7, r11
                0xE8BD4000,                         // pop r14
                instr1,                             // --- original instruction #1 ---
                instr2,                             // --- original instruction #2 ---
                instr3,                             // --- original instruction #3 ---
                0xE8BD8000                          // pop r15
            };
            uint[] trainerPayload = new uint[]
            {
                // Set text_speed to instant
                0xE3A01403,                         // mov r1, #0x3000000
                0xE3A000FF,                         // mov r0, #0xFF
                0xE5C1045A,                         // strb r0, [r1, #0x45A]
                // Return
                0xE12FFF1E                          // bx r15
            };
            Dictionary<uint, ushort> codePatches = new Dictionary<uint, ushort>
            {
                // Instant Character Popup
                { 0x3F5F6, 0x3008 },
                { 0x3F600, 0xDC08 }
            };
            WritePayload(file, jumpOffset, jumpPayload);
            WritePayload(file, hookOffset, hookPayload);
            WritePayload(file, trainerOffset, trainerPayload);
            WritePatches(file, codePatches);
            File.WriteAllBytes("patched.gba", file);
            MessageBox.Show("ALL DONE!");
        }
    }
}
