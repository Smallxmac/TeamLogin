// This is a slightly changed version of Jacobs data reader.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Network.Packets;

namespace LoginServer.Network
{
    public class PacketWriter
    {
        private byte[] _buffer;

        /// <summary>
        /// Constructor for loading the buffer so that it can be read from locally.
        /// </summary>
        /// <param name="buffer">Byte array that will be used to read from.</param>
        public PacketWriter(byte[] buffer)
        {
            _buffer = buffer;
        }

        /// <summary>
        /// Constructor for making a new packet based on information given.
        /// </summary>
        /// <param name="packetLength">How big the packet is so the buffer can be resized.</param>
        /// <param name="packetType">What Type of packet is it.</param>
        public PacketWriter(short packetLength, PacketTypes packetType)
        {
            _buffer = new byte[packetLength];
            WriteInt16((short)Enum.ToObject(typeof(PacketTypes), packetType), 0);
            WriteInt16(packetLength, 2);
        }

        /// <summary>
        /// Writes the header to the buffer.
        /// </summary>
        /// <param name="packetLength">How large the packet is.</param>
        /// <param name="packetType">The packet Type.</param>
        public void WriteHeader(short packetLength, PacketTypes packetType)
        {
            WriteInt16((short)Enum.ToObject(typeof(PacketTypes), packetType), 0);
            WriteInt16(packetLength, 2);
        }

        /// <summary>
        /// Get the buffer length
        /// </summary>
        /// <returns>length of the buffer.</returns>
        public int BufferLength()
        {
            return _buffer.Length;
        }

        /// <summary>
        /// Writes a signed 8bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteSByte(sbyte value, int offset)
        {
            (*(sbyte*)(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Writes a signed 16bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteInt16(short value, int offset)
        {
            (*(short*)(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Writes a signed 32bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteInt32(int value, int offset)
        {
            (*(int*)(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Writes a signed 64bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteInt64(long value, int offset)
        {
            (*(long*)(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Writes an unsigned 8bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteByte(byte value, int offset)
        {
            (*(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Writes an unsigned 16bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteUInt16(ushort value, int offset)
        {
            (*(ushort*)(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Writes an unsigned 32bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteUInt32(uint value, int offset)
        {
            (*(uint*)(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Writes an unsigned 64bit value.
        /// </summary>
        /// <param name="value">The value-</param>
        /// <param name="offset">The offset.</param>
        public unsafe void WriteUInt64(ulong value, int offset)
        {
            (*(ulong*)(DataPointer + offset)) = value;
        }

        /// <summary>
        /// Reads a signed 8bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe sbyte ReadSByte(int offset)
        {
            return (*(sbyte*)(DataPointer + offset));
        }

        /// <summary>
        /// Reads a signed 16bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe short ReadInt16(int offset)
        {
            return (*(short*)(DataPointer + offset));
        }

        /// <summary>
        /// Reads a signed 32bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe int ReadInt32(int offset)
        {
            return (*(int*)(DataPointer + offset));
        }

        /// <summary>
        /// Reads a signed 64bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe long ReadInt64(int offset)
        {
            return (*(long*)(DataPointer + offset));
        }

        /// <summary>
        /// Reads an unsigned 8bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe byte ReadByte(int offset)
        {
            return (*(DataPointer + offset));
        }

        /// <summary>
        /// Reads an array of bytes.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="len">The length.</param>
        /// <returns>Returns the array of bytes.</returns>
        public byte[] ReadBytes(int offset, int len)
        {
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
                ret[i] = ReadByte(offset + i);
            return ret;
        }

        /// <summary>
        /// Writes an array of bytes
        /// </summary>
        /// <param name="bytes">The Byte Array</param>
        /// <param name="offset">The offset</param>
        public void WriteBytes(byte[] bytes, int offset)
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
            return (*(ushort*)(DataPointer + offset));
        }

        /// <summary>
        /// Reads an unsigned 32bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe uint ReadUInt32(int offset)
        {
            return (*(uint*)(DataPointer + offset));
        }

        /// <summary>
        /// Reads an unsigned 64bit value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>Returns the value.</returns>
        public unsafe ulong ReadUInt64(int offset)
        {
            return (*(ulong*)(DataPointer + offset));
        }

        /// <summary>
        /// Writes a string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        public void WriteString(string value, int offset)
        {
            foreach (byte b in GetBytes(value))
                WriteByte(b, offset++);
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
            var stringBuilder = new StringBuilder();

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
            byte length = ReadByte(offset);
            offset++;
            return ReadString(offset, length);
        }

        /// <summary>
        /// Reads a string value.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="nextoffset">The next offset.</param>
        /// <returns>Returns the value.</returns>
        public string ReadStringFromLength(int offset, out int nextoffset)
        {
            byte length = ReadByte(offset);
            offset++;
            nextoffset = (length + offset);
            return ReadString(offset, length);
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

        /// <summary>
        /// Builds the current information into a byte Array.
        /// </summary>
        /// <returns>The Byte array</returns>
        public byte[] Build()
        {
            return _buffer;
        }

        /// <summary>
        /// Converts the current information into a byte array that is in string format.
        /// </summary>
        /// <returns>The string of the array.</returns>
        public override string ToString()
        {
            return BitConverter.ToString(_buffer);
        }

        /// <summary>
        /// Gets the pointer associated with the packet.
        /// </summary>
        public unsafe byte* DataPointer
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
