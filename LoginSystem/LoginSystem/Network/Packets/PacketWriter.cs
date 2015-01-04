using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Network.Packets
{
    public class PacketWriter
    {
        private byte[] _buffer;
        public PacketWriter(byte[] buffer)
        {
            _buffer = buffer;
        }

        public PacketWriter(short PacketLength, PacketTypes PacketType)
        {
            _buffer = new byte[PacketLength];
            WriteInt16((short)Enum.ToObject(typeof(PacketTypes), PacketType), 0);
            WriteInt16(PacketLength, 2);
        }

        public void WriteHeader(short PacketLength, PacketTypes PacketType)
        {
            WriteInt16((short)Enum.ToObject(typeof(PacketTypes), PacketType), 0);
            WriteInt16(PacketLength, 2);
        }

        /// <summary>
        /// Writes a signed 8bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteSByte(sbyte value, int offset)
        {
            (*(sbyte*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Writes a signed 16bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteInt16(short value, int offset)
        {
            (*(short*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Writes a signed 32bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteInt32(int value, int offset)
        {
            (*(int*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Writes a signed 64bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteInt64(long value, int offset)
        {
            (*(long*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Writes an unsigned 8bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteByte(byte value, int offset)
        {
            (*(byte*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Writes an unsigned 16bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteUInt16(ushort value, int offset)
        {
            (*(ushort*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Writes an unsigned 32bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteUInt32(uint value, int offset)
        {
            (*(uint*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Writes an unsigned 64bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteUInt64(ulong value, int offset)
        {
            (*(ulong*)(dataPointer + offset)) = value;
        }
        /// <summary>
        /// Reads a signed 8bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe sbyte ReadSByte(int offset)
        {
            return (*(sbyte*)(dataPointer + offset));
        }
        /// <summary>
        /// Reads a signed 16bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe short ReadInt16(int offset)
        {
            return (*(short*)(dataPointer + offset));
        }
        /// <summary>
        /// Reads a signed 32bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe int ReadInt32(int offset)
        {
            return (*(int*)(dataPointer + offset));
        }
        /// <summary>
        /// Reads a signed 64bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe long ReadInt64(int offset)
        {
            return (*(long*)(dataPointer + offset));
        }
        /// <summary>
        /// Reads an unsigned 8bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe byte ReadByte(int offset)
        {
            return (*(byte*)(dataPointer + offset));
        }

        /// <summary>
        /// Reads an array of bytes.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="len">The length.</param>
        /// <returns>Returns the array of bytes.</returns>
        public unsafe byte[] ReadBytes(int offset, int len)
        {
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
                ret[i] = ReadByte(offset + i);
            return ret;
        }

        public unsafe void WriteBytes(byte[] bytes, int offset)
        {
            for (int i = 0; i < bytes.Length; i++)
                WriteByte(bytes[i], (offset + i));
        }

        /// <summary>
        /// Reads an unsigned 16bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe ushort ReadUInt16(int offset)
        {
            return (*(ushort*)(dataPointer + offset));
        }
        /// <summary>
        /// Reads an unsigned 32bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe uint ReadUInt32(int offset)
        {
            return (*(uint*)(dataPointer + offset));
        }
        /// <summary>
        /// Reads an unsigned 64bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe ulong ReadUInt64(int offset)
        {
            return (*(ulong*)(dataPointer + offset));
        }

        /// <summary>
        /// Writes a string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        public void WriteString(string value, int offset)
        {
            foreach (byte b in GetBytes(value))
                WriteByte((byte)b, offset++);
        }

        /// <summary>
        /// Writes a string with length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        public void WriteStringWithLength(string value, int offset)
        {
            WriteByte((byte)value.Length, offset);
            offset++;
            WriteString(value, offset);
        }

        /// <summary>
        /// Writes a string with length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="nextoffset">The next offset.</param>
        public void WriteStringWithLength(string value, int offset, out int nextoffset)
        {
            WriteByte((byte)value.Length, offset);
            offset++;
            WriteString(value, offset);
            nextoffset = (offset + value.Length);
        }

        /// <summary>
        /// Reads a string value from length.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>Returns the value.</returns>
        public string ReadString(int offset, int length)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                byte _byte = ReadByte(offset + i);
                if (_byte > 0)
                    stringBuilder.Append((char)_byte);
            }

            return stringBuilder.ToString();
        }
        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public string ReadStringFromLength(int offset)
        {
            byte Length = ReadByte(offset);
            offset++;
            return ReadString(offset, Length);
        }

        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="nextoffset">The next offset.</param>
        /// <returns>Returns the value.</returns>
        public string ReadStringFromLength(int offset, out int nextoffset)
        {
            byte Length = ReadByte(offset);
            offset++;
            nextoffset = (Length + offset);
            return ReadString(offset, Length);
        }

        /// <summary>
        /// Writes a boolean value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public void WriteBool(bool value, int offset)
        {
            WriteByte((byte)(value ? 1 : 0), offset);
        }

        /// <summary>
        /// Reads a boolean value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public bool ReadBool(int offset)
        {
            return (ReadByte(offset) > 0);
        }

        public byte[] Build()
        {
            return _buffer;
        }

        public override string ToString()
        {
            return BitConverter.ToString(_buffer);
        }
        /// <summary>
        /// Gets the pointer associated with the packet.
        /// </summary>
        public unsafe byte* dataPointer
        {
            get
            {
                fixed (byte* pointer = _buffer)
                    return pointer;
            }
        }
        /// <summary>
        /// Gets the bytes whom the string represents.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Returns the bytes.</returns>
        public static byte[] GetBytes(string str)
        {
            // I've experienced problems with Encoding.ASCII.GetBytes in the past, so I want to avoid that.
            byte[] b = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
                b[i] = (byte)str[i];
            return b;
        }
    }
}
