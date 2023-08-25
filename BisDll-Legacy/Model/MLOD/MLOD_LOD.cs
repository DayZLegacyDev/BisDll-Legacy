using System;
using System.Collections.Generic;
using System.Linq;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.MLOD
{
	// Token: 0x02000037 RID: 55
	public class MLOD_LOD : P3D_LOD
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000793C File Offset: 0x00005B3C
		public override Vector3P[] Points
		{
			get
			{
				return this.points;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007958 File Offset: 0x00005B58
		public override Vector3P[] Normals
		{
			get
			{
				return this.normals;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007960 File Offset: 0x00005B60
		public override string[] Textures
		{
			get
			{
				return (from f in this.faces
				select f.Texture).Distinct<string>().ToArray<string>();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000799C File Offset: 0x00005B9C
		public override string[] MaterialNames
		{
			get
			{
				return (from f in this.faces
				select f.Material).Distinct<string>().ToArray<string>();
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000079D8 File Offset: 0x00005BD8
		public MLOD_LOD()
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000079E0 File Offset: 0x00005BE0
		public MLOD_LOD(float resolution)
		{
			this.resolution = resolution;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000079F0 File Offset: 0x00005BF0
		private Tagg ReadTagg(BinaryReaderEx input)
		{
			Tagg tagg = new MassTagg();
			if (!input.ReadBoolean())
			{
				throw new Exception( "Deactivated Tagg?" );
			}
			tagg.Name = input.ReadAsciiz();
			tagg.DataSize = input.ReadUInt32();
			switch (tagg.Name)
			{
				case "#SharpEdges#":
					{
						SharpEdgesTagg sharpEdgesTagg = new SharpEdgesTagg();
						sharpEdgesTagg.Name = "#SharpEdges#";
						sharpEdgesTagg.DataSize = tagg.DataSize;
						sharpEdgesTagg.Read( input );
						return sharpEdgesTagg;
					}
				case "#Property#":
					{
						PropertyTagg propertyTagg = new PropertyTagg();
						propertyTagg.Name = "#Property#";
						propertyTagg.DataSize = tagg.DataSize;
						propertyTagg.Read( input );
						return propertyTagg;
					}
				case "#Mass#":
					{
						MassTagg massTagg = new MassTagg();
						massTagg.Name = "#Mass#";
						massTagg.DataSize = tagg.DataSize;
						massTagg.Read( input );
						return massTagg;
					}
				case "#UVSet#":
					{
						UVSetTagg uVSetTagg = new UVSetTagg();
						uVSetTagg.Name = "#UVSet#";
						uVSetTagg.DataSize = tagg.DataSize;
						uVSetTagg.Read( input, faces );
						return uVSetTagg;
					}
				case "#Lock#":
					{
						LockTagg lockTagg = new LockTagg();
						lockTagg.Name = "#Lock#";
						lockTagg.DataSize = tagg.DataSize;
						lockTagg.Read( input, Points.Length, faces.Length );
						return lockTagg;
					}
				case "#Selected#":
					{
						SelectedTagg selectedTagg = new SelectedTagg();
						selectedTagg.Name = "#Selected#";
						selectedTagg.DataSize = tagg.DataSize;
						selectedTagg.Read( input, Points.Length, faces.Length );
						return selectedTagg;
					}
				case "#Animation#":
					{
						AnimationTagg animationTagg = new AnimationTagg();
						animationTagg.Name = "#Animation#";
						animationTagg.DataSize = tagg.DataSize;
						animationTagg.read( input );
						return animationTagg;
					}
				case "#EndOfFile#":
					return tagg;
				default:
					{
						NamedSelectionTagg namedSelectionTagg = new NamedSelectionTagg();
						namedSelectionTagg.Name = tagg.Name;
						namedSelectionTagg.DataSize = tagg.DataSize;
						namedSelectionTagg.Read( input, Points.Length, faces.Length );
						return namedSelectionTagg;
					}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007CD0 File Offset: 0x00005ED0
		private void WriteTagg(BinaryWriterEx output, Tagg tagg)
		{
			switch (tagg.Name)
			{
				case "#SharpEdges#":
					((SharpEdgesTagg)tagg).Write( output );
					break;
				case "#Property#":
					((PropertyTagg)tagg).Write( output );
					break;
				case "#Mass#":
					((MassTagg)tagg).Write( output );
					break;
				case "#UVSet#":
					((UVSetTagg)tagg).Write( output );
					break;
				case "#Lock#":
					((LockTagg)tagg).Write( output );
					break;
				case "#Selected#":
					((SelectedTagg)tagg).Write( output );
					break;
				case "#Animation#":
					((AnimationTagg)tagg).write( output );
					break;
				default:
					((NamedSelectionTagg)tagg).Write( output );
					break;
				case "#EndOfFile#":
					break;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007E8C File Offset: 0x0000608C
		public void Read(BinaryReaderEx input)
		{
			if (input.ReadAscii( 4 ) != "P3DM")
			{
				throw new Exception( "Only P3DM LODs are supported" );
			}
			if (input.ReadUInt32() != 28 || input.ReadUInt32() != 256)
			{
				throw new Exception( "Unknown P3DM version" );
			}
			uint num = input.ReadUInt32();
			uint num2 = input.ReadUInt32();
			uint num3 = input.ReadUInt32();
			unk1 = input.ReadUInt32();
			points = new Point[num];
			normals = new Vector3P[num2];
			faces = new Face[num3];
			for (int i = 0; i < num; i++)
			{
				points[i] = new Point( input );
			}
			for (int j = 0; j < num2; j++)
			{
				normals[j] = new Vector3P( input );
			}
			for (int k = 0; k < num3; k++)
			{
				faces[k] = new Face( input );
			}
			if (input.ReadAscii( 4 ) != "TAGG")
			{
				throw new Exception( "TAGG expected" );
			}
			taggs = new List<Tagg>();
			Tagg tagg;
			do
			{
				tagg = ReadTagg( input );
				if (tagg.Name != "#EndOfFile#")
				{
					taggs.Add( tagg );
				}
			}
			while (tagg.Name != "#EndOfFile#");
			resolution = input.ReadSingle();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008018 File Offset: 0x00006218
		public void Write(BinaryWriterEx output)
		{
			int num = points.Length;
			int num2 = normals.Length;
			int num3 = faces.Length;
			output.writeAscii( "P3DM", 4u );
			output.Write( 28 );
			output.Write( 256 );
			output.Write( num );
			output.Write( num2 );
			output.Write( num3 );
			output.Write( unk1 );
			for (int i = 0; i < num; i++)
			{
				points[i].Write( output );
			}
			for (int j = 0; j < num2; j++)
			{
				normals[j].Write( output );
			}
			for (int k = 0; k < num3; k++)
			{
				faces[k].Write( output );
			}
			output.writeAscii( "TAGG", 4u );
			foreach (Tagg tagg in taggs)
			{
				WriteTagg( output, tagg );
			}
			output.Write( value: true );
			output.writeAsciiz( "#EndOfFile#" );
			output.Write( 0 );
			output.Write( resolution );
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008170 File Offset: 0x00006370
		public float GetHeight()
		{
			int num = this.Points.Length;
			if ((long)num <= 1L)
			{
				return 0f;
			}
			float num2 = float.MaxValue;
			float num3 = float.MinValue;
			for (int i = 0; i < num; i++)
			{
				float y = this.points[i].Y;
				if (y > num3)
				{
					num3 = y;
				}
				if (y < num2)
				{
					num2 = y;
				}
			}
			return num3 - num2;
		}

		// Token: 0x040001AA RID: 426
		public uint unk1;

		// Token: 0x040001AB RID: 427
		public Point[] points;

		// Token: 0x040001AC RID: 428
		public Vector3P[] normals;

		// Token: 0x040001AD RID: 429
		public Face[] faces;

		// Token: 0x040001AE RID: 430
		public List<Tagg> taggs;
	}
}
