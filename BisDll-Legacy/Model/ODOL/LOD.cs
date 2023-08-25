using System;
using System.Linq;
using BisDll.Common;
using BisDll.Common.Math;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200001C RID: 28
	public class LOD : P3D_LOD, IComparable<LOD>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004D80 File Offset: 0x00002F80
		public NamedSelection[] NamedSelections
		{
			get
			{
				return this.namedSelections;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004D88 File Offset: 0x00002F88
		public override string[] MaterialNames
		{
			get
			{
				return (from m in this.materials
				select m.materialName).ToArray<string>();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004DBC File Offset: 0x00002FBC
		public EmbeddedMaterial[] Materials
		{
			get
			{
				return this.materials;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public int VertexCount
		{
			get
			{
				return this.vertices.Length;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public int SectionCount
		{
			get
			{
				return this.sections.Length;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004DDC File Offset: 0x00002FDC
		public int TextureCount
		{
			get
			{
				return this.textures.Length;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public int PolygonCount
		{
			get
			{
				return this.polygons.Faces.Length;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public int MaterialCount
		{
			get
			{
				return this.materials.Length;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004E04 File Offset: 0x00003004
		public AnimationRTWeight[] VertexBoneRef
		{
			get
			{
				return this.vertexBoneRef;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004E0C File Offset: 0x0000300C
		public VertexNeighborInfo[] NeighborBoneRef
		{
			get
			{
				return this.neighborBoneRef;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004E14 File Offset: 0x00003014
		public ClipFlags[] ClipFlags
		{
			get
			{
				if (this.odolVersion < 50U)
				{
					return this.clipOldFormat;
				}
				return this.clip;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004E30 File Offset: 0x00003030
		public Vector3P[] Vertices
		{
			get
			{
				return this.vertices;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004E38 File Offset: 0x00003038
		public override Vector3P[] Normals
		{
			get
			{
				return this.normals;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004E40 File Offset: 0x00003040
		public Section[] Sections
		{
			get
			{
				return this.sections;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004E48 File Offset: 0x00003048
		public UVSet[] UVSets
		{
			get
			{
				return this.uvSets;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004E50 File Offset: 0x00003050
		public Polygon[] Faces
		{
			get
			{
				return this.polygons.Faces;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004E60 File Offset: 0x00003060
		public string[,] NamedProperties
		{
			get
			{
				return this.namedProperties;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004E68 File Offset: 0x00003068
		public Keyframe[] Frames
		{
			get
			{
				return this.frames;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004E70 File Offset: 0x00003070
		public int[] SubSkeletonsToSkeleton
		{
			get
			{
				return this.subSkeletonsToSkeleton;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004E78 File Offset: 0x00003078
		public Proxy[] Proxies
		{
			get
			{
				return this.proxies;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004E80 File Offset: 0x00003080
		public override Vector3P[] Points
		{
			get
			{
				return this.Vertices;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004E88 File Offset: 0x00003088
		public override string[] Textures
		{
			get
			{
				return this.textures;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004E90 File Offset: 0x00003090
		public void Read( BinaryReaderEx input, float resolution )
		{
			odolVersion = (uint)input.Version;
			Logging_Functions.Echo( input, odolVersion, "odolVersion" );
			base.resolution = resolution;
			Logging_Functions.Echo( input, base.resolution, "resolution" );
			proxies = input.ReadArray<Proxy>();
			Logging_Functions.Echo( input, proxies, "proxies" );
			subSkeletonsToSkeleton = input.ReadIntArray();
			Logging_Functions.Echo( input, subSkeletonsToSkeleton, "subSkeletonsToSkeleton" );
			skeletonToSubSkeleton = input.ReadArray<SubSkeletonIndexSet>();
			Logging_Functions.Echo( input, skeletonToSubSkeleton, "skeletonToSubSkeleton" );
			if (odolVersion >= 50)
			{
				vertexCount = input.ReadUInt32();
				Logging_Functions.Echo( input, vertexCount, "vertexCount" );
			}
			else
			{
				int[] array = input.ReadCondensedIntArray();
				clipOldFormat = Array.ConvertAll( array, ( int item ) => (ClipFlags)item );
			}
			if (odolVersion >= 51)
			{
				faceArea = input.ReadSingle();
				Logging_Functions.Echo( input, faceArea, "faceArea" );
			}
			orHints = (ClipFlags)input.ReadInt32();
			Logging_Functions.Echo( input, orHints, "orHints" );
			andHints = (ClipFlags)input.ReadInt32();
			Logging_Functions.Echo( input, andHints, "andHints" );
			bMin = new Vector3P( input );
			Logging_Functions.Echo( input, bMin, "bMin" );
			bMax = new Vector3P( input );
			Logging_Functions.Echo( input, bMax, "bMax" );
			bCenter = new Vector3P( input );
			Logging_Functions.Echo( input, bCenter, "bCenter" );
			bRadius = input.ReadSingle();
			Logging_Functions.Echo( input, bRadius, "bRadius" );
			textures = input.ReadStringArray();
			Logging_Functions.Echo( input, textures, "textures" );
			materials = input.ReadArray<EmbeddedMaterial>();
			Logging_Functions.Echo( input, materials, "materials" );
			pointToVertex = input.ReadCompressedVertexIndexArray();
			Logging_Functions.Echo( input, pointToVertex, "pointToVertex" );
			vertexToPoint = input.ReadCompressedVertexIndexArray();
			Logging_Functions.Echo( input, vertexToPoint, "vertexToPoint" );
			polygons = new Polygons( input );
			Logging_Functions.Echo( input, polygons, "polygons" );
			sections = input.ReadArray<Section>();
			Logging_Functions.Echo( input, sections, "sections" );
			namedSelections = input.ReadArray<NamedSelection>();
			Logging_Functions.Echo( input, namedSelections, "namedSelections" );
			nNamedProperties = input.ReadUInt32();
			Logging_Functions.Echo( input, nNamedProperties, "nNamedProperties" );
			namedProperties = new string[nNamedProperties, 2];
			Logging_Functions.Echo( input, namedProperties, "namedProperties" );
			for (int i = 0; i < nNamedProperties; i++)
			{
				namedProperties[i, 0] = input.ReadAsciiz();
				namedProperties[i, 1] = input.ReadAsciiz();
			}
			frames = input.ReadArray<Keyframe>();
			Logging_Functions.Echo( input, frames, "frames" );
			colorTop = input.ReadInt32();
			Logging_Functions.Echo( input, colorTop, "colorTop" );
			color = input.ReadInt32();
			Logging_Functions.Echo( input, color, "color" );
			special = input.ReadInt32();
			Logging_Functions.Echo( input, special, "special" );
			vertexBoneRefIsSimple = input.ReadBoolean();
			sizeOfRestData = input.ReadUInt32();
			Logging_Functions.Echo( input, sizeOfRestData, "sizeOfRestData", hex: true );
			if (odolVersion >= 50)
			{
				int[] array2 = input.ReadCondensedIntArray();
				Logging_Functions.Echo( input, array2, "array2" );
				clip = Array.ConvertAll( array2, ( int item ) => (ClipFlags)item );
			}
			UVSet uVSet = new UVSet();
			uVSet.Read( input, odolVersion );
			nUVSets = input.ReadUInt32();
			uvSets = new UVSet[nUVSets];
			uvSets[0] = uVSet;
			for (int j = 1; j < nUVSets; j++)
			{
				uvSets[j] = new UVSet();
				uvSets[j].Read( input, odolVersion );
			}
			vertices = input.ReadCompressedObjectArray<Vector3P>( 12 );
			Logging_Functions.Echo( input, vertices, "vertices" );
			if (odolVersion >= 45)
			{
				Vector3PCompressed[] array3 = input.ReadCondensedObjectArray<Vector3PCompressed>( 4 );
				normals = Array.ConvertAll( array3, (Converter<Vector3PCompressed, Vector3P>)(( Vector3PCompressed item ) => item) );
				Logging_Functions.Echo( input, normals, "normals" );
			}
			else
			{
				normals = input.ReadCondensedObjectArray<Vector3P>( 12 );
				Logging_Functions.Echo( input, normals, "normals" );
			}
			STCoords = (STPair[])((odolVersion >= 45) ? ((Array)input.ReadCompressedObjectArray<STPairCompressed>( 8 )) : ((Array)input.ReadCompressedObjectArray<STPairUncompressed>( 24 )));
			Logging_Functions.Echo( input, STCoords, "STCoords" );
			vertexBoneRef = input.ReadCompressedObjectArray<AnimationRTWeight>( 12 );
			Logging_Functions.Echo( input, vertexBoneRef, "vertexBoneRef" );
			neighborBoneRef = input.ReadCompressedObjectArray<VertexNeighborInfo>( 32 );
			Logging_Functions.Echo( input, neighborBoneRef, "neighborBoneRef" );
			if (odolVersion >= 67)
			{
				input.ReadUInt32();
			}
			if (odolVersion >= 68)
			{
				input.ReadByte();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000545C File Offset: 0x0000365C
		public void Write(BinaryWriterEx output)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005464 File Offset: 0x00003664
		public int CompareTo(LOD other)
		{
			return this.resolution.CompareTo(other.resolution);
		}

		// Token: 0x040000C9 RID: 201
		private uint odolVersion;

		// Token: 0x040000CA RID: 202
		private Proxy[] proxies;

		// Token: 0x040000CB RID: 203
		private int[] subSkeletonsToSkeleton;

		// Token: 0x040000CC RID: 204
		private SubSkeletonIndexSet[] skeletonToSubSkeleton;

		// Token: 0x040000CD RID: 205
		private uint vertexCount;

		// Token: 0x040000CE RID: 206
		private float faceArea;

		// Token: 0x040000CF RID: 207
		private ClipFlags[] clipOldFormat;

		// Token: 0x040000D0 RID: 208
		private ClipFlags[] clip;

		// Token: 0x040000D1 RID: 209
		private ClipFlags orHints;

		// Token: 0x040000D2 RID: 210
		private ClipFlags andHints;

		// Token: 0x040000D3 RID: 211
		private Vector3P bMin;

		// Token: 0x040000D4 RID: 212
		private Vector3P bMax;

		// Token: 0x040000D5 RID: 213
		private Vector3P bCenter;

		// Token: 0x040000D6 RID: 214
		private float bRadius;

		// Token: 0x040000D7 RID: 215
		private string[] textures;

		// Token: 0x040000D8 RID: 216
		private EmbeddedMaterial[] materials;

		// Token: 0x040000D9 RID: 217
		private VertexIndex[] pointToVertex;

		// Token: 0x040000DA RID: 218
		private VertexIndex[] vertexToPoint;

		// Token: 0x040000DB RID: 219
		private Polygons polygons;

		// Token: 0x040000DC RID: 220
		private Section[] sections;

		// Token: 0x040000DD RID: 221
		private NamedSelection[] namedSelections;

		// Token: 0x040000DE RID: 222
		private uint nNamedProperties;

		// Token: 0x040000DF RID: 223
		private string[,] namedProperties;

		// Token: 0x040000E0 RID: 224
		private Keyframe[] frames;

		// Token: 0x040000E1 RID: 225
		private int colorTop;

		// Token: 0x040000E2 RID: 226
		private int color;

		// Token: 0x040000E3 RID: 227
		private int special;

		// Token: 0x040000E4 RID: 228
		private bool vertexBoneRefIsSimple;

		// Token: 0x040000E5 RID: 229
		private uint sizeOfRestData;

		// Token: 0x040000E6 RID: 230
		private uint nUVSets;

		// Token: 0x040000E7 RID: 231
		private UVSet[] uvSets;

		// Token: 0x040000E8 RID: 232
		private Vector3P[] vertices;

		// Token: 0x040000E9 RID: 233
		private Vector3P[] normals;

		// Token: 0x040000EA RID: 234
		private STPair[] STCoords;

		// Token: 0x040000EB RID: 235
		private AnimationRTWeight[] vertexBoneRef;

		// Token: 0x040000EC RID: 236
		private VertexNeighborInfo[] neighborBoneRef;

		// Token: 0x0200005F RID: 95
		private struct PointWeight
		{
			// Token: 0x06000278 RID: 632 RVA: 0x0000B60C File Offset: 0x0000980C
			public PointWeight(int index, byte weight)
			{
				this.pointIndex = index;
				this.weight = weight;
			}

			// Token: 0x040002DD RID: 733
			public int pointIndex;

			// Token: 0x040002DE RID: 734
			public byte weight;
		}
	}
}
