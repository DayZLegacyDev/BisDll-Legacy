using System;
using System.IO;
using System.Runtime.InteropServices;
using BisDll.Compression;

namespace BisDll.Stream
{
	public class BinaryReaderEx : BinaryReader
	{
		public bool UseCompressionFlag { get; set; }
		public bool UseLZOCompression { get; set; }
		public int Version { get; set; }
		
		public long Position
		{
			get
			{
				return this.BaseStream.Position;
			}
			set
			{
				this.BaseStream.Position = value;
			}
		}
		
		public BinaryReaderEx( System.IO.Stream stream ) : base(stream)
		{
			this.UseCompressionFlag = false;
		}
		
		public uint ReadUInt24()
		{
			return (uint)((int)this.ReadByte() + ((int)this.ReadByte() << 8) + ((int)this.ReadByte() << 16));
		}
		
		public string ReadAscii(int count)
		{
			string text = "";
			for (int i = 0; i < count; i++)
			{
				text += ((char)this.ReadByte()).ToString();
			}
			return text;
		}
		
		public string ReadAsciiz()
		{
			string text = "";
			char c;
			while ((c = (char)this.ReadByte()) != '\0')
			{
				text += c.ToString();
			}
			return text;
		}

		private T ReadObject<T>() where T : IDeserializable, new()
		{
			T result = Activator.CreateInstance<T>();
			result.ReadObject(this);
			return result;
		}
		
		private T[] ReadArrayBase<T>(Func<BinaryReaderEx, T> readElement, int size)
		{
			T[] result;
			try
			{
				T[] array = new T[size];
				for (int i = 0; i < size; i++)
				{
					try
					{
						array[i] = readElement(this);
					}
					catch
					{
					}
				}
				result = array;
			}
			catch
			{
				Console.WriteLine("Error!");
				result = null;
			}
			return result;
		}
		
		public T[] ReadArray<T>(Func<BinaryReaderEx, T> readElement)
		{
			return this.ReadArrayBase<T>(readElement, this.ReadInt32());
		}
		
		public T[] ReadArray<T>() where T : IDeserializable, new()
		{
			return this.ReadArray<T>((BinaryReaderEx i) => i.ReadObject<T>());
		}
		
		public float[] ReadFloatArray()
		{
			return this.ReadArray<float>((BinaryReaderEx i) => i.ReadSingle());
		}
		
		public int[] ReadIntArray()
		{
			return this.ReadArray<int>((BinaryReaderEx i) => i.ReadInt32());
		}
		
		public string[] ReadStringArray()
		{
			return this.ReadArray<string>((BinaryReaderEx i) => i.ReadAsciiz());
		}
		
		public T[] ReadCompressedArray<T>(Func<BinaryReaderEx, T> readElement, int elemSize)
		{
			int num = this.ReadInt32();
			uint expectedSize = (uint)(num * elemSize);
			return new BinaryReaderEx(new MemoryStream(this.ReadCompressed(expectedSize))).ReadArrayBase<T>(readElement, num);
		}
		
		public T[] ReadCompressedArray<T>(Func<BinaryReaderEx, T> readElement)
		{
			return this.ReadCompressedArray<T>(readElement, Marshal.SizeOf(typeof(T)));
		}
		
		public T[] ReadCompressedObjectArray<T>(int sizeOfT) where T : IDeserializable, new()
		{
			return this.ReadCompressedArray<T>((BinaryReaderEx i) => i.ReadObject<T>(), sizeOfT);
		}
		
		public short[] ReadCompressedShortArray()
		{
			return this.ReadCompressedArray<short>((BinaryReaderEx i) => i.ReadInt16());
		}
		
		public int[] ReadCompressedIntArray()
		{
			return this.ReadCompressedArray<int>((BinaryReaderEx i) => i.ReadInt32());
		}
		
		public float[] ReadCompressedFloatArray()
		{
			return this.ReadCompressedArray<float>((BinaryReaderEx i) => i.ReadSingle());
		}
		
		public T[] ReadCondensedArray<T>(Func<BinaryReaderEx, T> readElement, int sizeOfT)
		{
			int num = this.ReadInt32();
			T[] array = new T[num];
			if (this.ReadBoolean())
			{
				T t = readElement(this);
				for (int i = 0; i < num; i++)
				{
					array[i] = t;
				}
				return array;
			}
			uint expectedSize = (uint)(num * sizeOfT);
			BinaryReaderEx binaryReaderEx = new BinaryReaderEx(new MemoryStream(this.ReadCompressed(expectedSize)));
			array = binaryReaderEx.ReadArrayBase<T>(readElement, num);
			binaryReaderEx.Close();
			return array;
		}
		
		public T[] ReadCondensedObjectArray<T>(int sizeOfT) where T : IDeserializable, new()
		{
			return this.ReadCondensedArray<T>((BinaryReaderEx i) => i.ReadObject<T>(), sizeOfT);
		}
		
		public int[] ReadCondensedIntArray()
		{
			return this.ReadCondensedArray<int>((BinaryReaderEx i) => i.ReadInt32(), 4);
		}
		
		public int ReadCompactInteger()
		{
			int num = (int)this.ReadByte();
			if ((num & 128) != 0)
			{
				int num2 = (int)this.ReadByte();
				num += (num2 - 1) * 128;
			}
			return num;
		}
		
		public byte[] ReadCompressed(uint expectedSize)
		{
			if (expectedSize == 0U)
			{
				return new byte[0];
			}
			if (this.UseLZOCompression)
			{
				return this.ReadLZO(expectedSize);
			}
			return this.ReadLZSS(expectedSize, false);
		}
		
		public byte[] ReadLZO(uint expectedSize)
		{
			bool flag = expectedSize >= 1024U;
			if (this.UseCompressionFlag)
			{
				flag = this.ReadBoolean();
			}
			if (!flag)
			{
				return this.ReadBytes((int)expectedSize);
			}
			return LZO.readLZO(this.BaseStream, expectedSize);
		}
		
		public byte[] ReadLZSS(uint expectedSize, bool inPAA = false)
		{
			if (expectedSize < 1024U && !inPAA)
			{
				return this.ReadBytes((int)expectedSize);
			}
			byte[] result = new byte[expectedSize];
			LZSS.readLZSS(this.BaseStream, out result, expectedSize, inPAA);
			return result;
		}
		
		public byte[] ReadCompressedIndices(int bytesToRead, uint expectedSize)
		{
			byte[] array = new byte[expectedSize];
			int num = 0;
			for (int i = 0; i < bytesToRead; i++)
			{
				byte b = this.ReadByte();
				if ((b & 128) != 0)
				{
					byte b2 = (byte)((int)b - 127);
					byte b3 = this.ReadByte();
					for (int j = 0; j < (int)b2; j++)
					{
						array[num++] = b3;
					}
				}
				else
				{
					for (int k = 0; k < (int)(b + 1); k++)
					{
						array[num++] = this.ReadByte();
					}
				}
			}
			return array;
		}
		
		public uint skipGridCompressed()
		{
			long position = this.Position;
			ushort num = this.ReadUInt16();
			for (int i = 0; i < 16; i++)
			{
				if ((num & 1) == 1)
				{
					this.skipGridCompressed();
				}
				else
				{
					this.Position += 4L;
				}
				num = (ushort)(num >> 1);
			}
			return (uint)(this.Position - position);
		}
	}
}
