using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SageCS.Audio;

namespace SageCS.Core.Loaders
{
    static class WavLoader
    {
        public static SoundBuffer Load(Stream s)
        {
            BinaryReader br = new BinaryReader(s);
            var magic = new String(br.ReadChars(4));
            if (magic != "RIFF")
                throw new FormatException("Invalid WAV file");

            UInt32 size = br.ReadUInt32();
            var id = new String(br.ReadChars(4));
            if (id != "WAVE")
                throw new FormatException("Invalid WAV file");

            var fmtSig = new String(br.ReadChars(4));
            if (fmtSig != "fmt ")
                throw new FormatException("Format Chunk in WAV file not found");

            var FmtChunckSize = br.ReadInt32();
            var AudioFormat = br.ReadInt16();
            var Channels = br.ReadInt16();
            var SampleRate = br.ReadInt32();
            var ByteRate = br.ReadInt32();
            var BlockAlign = br.ReadInt16();
            var BitsPerSample = br.ReadInt16(); ;

            var dataSig = new String(br.ReadChars(4));
            if (dataSig != "data")
            {
                throw new FormatException("Data chunk in WAV file not found");
            }
            
            var DataChunckSize = br.ReadInt32();

            var data = br.ReadBytes(DataChunckSize);
            OpenTK.Audio.OpenAL.ALFormat format;

            if (Channels == 1 && BitsPerSample == 8)
                format = OpenTK.Audio.OpenAL.ALFormat.Mono8;
            else if (Channels == 1 && BitsPerSample == 16)
                format = OpenTK.Audio.OpenAL.ALFormat.Mono16;
            else if (Channels == 2 && BitsPerSample == 8)
                format = OpenTK.Audio.OpenAL.ALFormat.Stereo8;
            else if (Channels == 2 && BitsPerSample == 16)
                format = OpenTK.Audio.OpenAL.ALFormat.Stereo16;
            else
                throw new Exception("Unsupported audio format");


            SoundBuffer sb = new SoundBuffer();
            sb.BufferData(data, format, SampleRate);
            return sb;
        }
    }
}
