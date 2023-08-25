using System;
using System.IO;
using System.Linq;
using BisDll.Model.MLOD;
using BisDll.Model.ODOL;
using BisDll.Stream;

namespace BisDll.Model
{
	public abstract class P3D
	{
		public uint Version { get; protected set; }
		
		public abstract P3D_LOD[] LODs { get; }
		
		public abstract float Mass { get; }
		
		public static P3D GetInstance(string fileName)
		{
			System.IO.Stream stream = File.OpenRead(fileName);
			string type = new BinaryReaderEx( stream ).ReadAscii( 4 );
			stream.Close();
			if (type == "ODOL")
			{
				return new ODOL.ODOL( fileName );
			}
			if (type == "MLOD")
			{
				return new MLOD.MLOD( fileName );
			}
			throw new FormatException();
		}
		
		public virtual P3D_LOD GetLOD(float resolution)
		{
			return this.LODs.FirstOrDefault((P3D_LOD lod) => lod.Resolution == resolution);
		}
	}
}
