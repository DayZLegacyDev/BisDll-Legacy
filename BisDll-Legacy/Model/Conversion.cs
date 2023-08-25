using System;
using System.Collections.Generic;
using System.Linq;
using BisDll.Common.Math;
using BisDll.Model.MLOD;
using BisDll.Model.ODOL;

namespace BisDll.Model
{
	// Token: 0x02000009 RID: 9
	public static class Conversion
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002FA4 File Offset: 0x000011A4
		private static PointFlags ClipFlagsToPointFlags(ClipFlags clipFlags)
		{
			var pointFlags = PointFlags.NONE;
			if ((clipFlags & ClipFlags.ClipLandStep) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.ONLAND;
			}
			else if ((clipFlags & ClipFlags.ClipLandUnder) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.UNDERLAND;
			}
			else if ((clipFlags & ClipFlags.ClipLandAbove) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.ABOVELAND;
			}
			else if ((clipFlags & ClipFlags.ClipLandKeep) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.KEEPLAND;
			}
			if ((clipFlags & ClipFlags.ClipDecalStep) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.DECAL;
			}
			else if ((clipFlags & ClipFlags.ClipDecalVertical) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.VDECAL;
			}
			if ((clipFlags & (ClipFlags)209715200) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.NOLIGHT;
			}
			else if ((clipFlags & (ClipFlags)212860928) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.FULLLIGHT;
			}
			else if ((clipFlags & (ClipFlags)211812352) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.HALFLIGHT;
			}
			else if ((clipFlags & (ClipFlags)210763776) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.AMBIENT;
			}
			if ((clipFlags & ClipFlags.ClipFogStep) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.NOFOG;
			}
			else if ((clipFlags & ClipFlags.ClipFogSky) != ClipFlags.ClipNone)
			{
				pointFlags |= PointFlags.SKYFOG;
			}
			int num = (int)(clipFlags & ClipFlags.ClipUserMask) / (int)ClipFlags.ClipUserStep;
			return pointFlags | (PointFlags)(65536 * num);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000030D0 File Offset: 0x000012D0
		public static MLOD.MLOD ODOL2MLOD(ODOL.ODOL odol)
		{
			P3D_LOD[] lods = odol.LODs;
			int num = lods.Length;
			MLOD_LOD[] array = new MLOD_LOD[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = Conversion.OdolLod2MLOD(odol, (LOD)lods[i]);
			}
			return new MLOD.MLOD( array);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003124 File Offset: 0x00001324
		private static MLOD_LOD OdolLod2MLOD( ODOL.ODOL odol, LOD src)
		{
			MLOD_LOD mlod_LOD = new MLOD_LOD(src.Resolution);
			int vertexCount = src.VertexCount;
			Conversion.ConvertPoints(odol, mlod_LOD, src);
			mlod_LOD.normals = src.Normals;
			Conversion.ConvertFaces(odol, mlod_LOD, src);
			float mass = odol.modelInfo.mass;
			Skeleton skeleton = odol.Skeleton;
			mlod_LOD.taggs = new List<Tagg>();
			if (src.Resolution == 1E+13f)
			{
				MassTagg item = Conversion.CreateMassTagg(vertexCount, mass);
				mlod_LOD.taggs.Add(item);
			}
			IEnumerable<UVSetTagg> collection = Conversion.CreateUVSetTaggs(src);
			mlod_LOD.taggs.AddRange(collection);
			IEnumerable<PropertyTagg> collection2 = Conversion.CreatePropertyTaggs(src);
			mlod_LOD.taggs.AddRange(collection2);
			IEnumerable<NamedSelectionTagg> collection3 = Conversion.CreateNamedSelectionTaggs(src);
			mlod_LOD.taggs.AddRange(collection3);
			IEnumerable<AnimationTagg> collection4 = Conversion.CreateAnimTaggs(src);
			mlod_LOD.taggs.AddRange(collection4);
			if (Resolution.KeepsNamedSelections(src.Resolution))
			{
				return mlod_LOD;
			}
			Dictionary<string, List<Conversion.PointWeight>> nsPoints = new Dictionary<string, List<Conversion.PointWeight>>();
			Dictionary<string, List<int>> nsFaces = new Dictionary<string, List<int>>();
			Conversion.ReconstructNamedSelectionBySections(src, out nsPoints, out nsFaces);
			Dictionary<string, List<Conversion.PointWeight>> nsPoints2 = new Dictionary<string, List<Conversion.PointWeight>>();
			Dictionary<string, List<int>> nsFaces2 = new Dictionary<string, List<int>>();
			Conversion.ReconstructProxies(src, out nsPoints2, out nsFaces2);
			Dictionary<string, List<Conversion.PointWeight>> nsPoints3 = new Dictionary<string, List<Conversion.PointWeight>>();
			Conversion.ReconstructNamedSelectionsByBones(src, odol.Skeleton, out nsPoints3);
			Conversion.ApplySelectedPointsAndFaces(mlod_LOD, nsPoints, nsFaces);
			Conversion.ApplySelectedPointsAndFaces(mlod_LOD, nsPoints2, nsFaces2);
			Conversion.ApplySelectedPointsAndFaces(mlod_LOD, nsPoints3, null);
			return mlod_LOD;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003270 File Offset: 0x00001470
		private static void ApplySelectedPointsAndFaces(MLOD_LOD dstLod, Dictionary<string, List<Conversion.PointWeight>> nsPoints, Dictionary<string, List<int>> nsFaces)
		{
			foreach (Tagg tagg in dstLod.taggs)
			{
				if (tagg is NamedSelectionTagg)
				{
					NamedSelectionTagg namedSelectionTagg = tagg as NamedSelectionTagg;
					List<Conversion.PointWeight> list;
					if (nsPoints != null && nsPoints.TryGetValue(namedSelectionTagg.Name, out list))
					{
						foreach (Conversion.PointWeight pointWeight in list)
						{
							byte b = (byte)-pointWeight.weight;
							if (b != 0)
							{
								namedSelectionTagg.points[pointWeight.pointIndex] = b;
							}
						}
					}
					List<int> list2;
					if (nsFaces != null && nsFaces.TryGetValue(namedSelectionTagg.Name, out list2))
					{
						foreach (int num in list2)
						{
							namedSelectionTagg.faces[num] = 1;
						}
					}
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000033B4 File Offset: 0x000015B4
		private static void ConvertPoints( ODOL.ODOL odol, MLOD_LOD dstLod, LOD srcLod)
		{
			Vector3P boundingCenter = odol.modelInfo.boundingCenter;
			Vector3P bboxMinVisual = odol.modelInfo.bboxMinVisual;
			Vector3P bboxMaxVisual = odol.modelInfo.bboxMaxVisual;
			int num = srcLod.Vertices.Length;
			dstLod.points = new Point[num];
			for (int i = 0; i < num; i++)
			{
				Vector3P pos = srcLod.Vertices[i] + boundingCenter;
				dstLod.points[i] = new Point(pos, Conversion.ClipFlagsToPointFlags(srcLod.ClipFlags[i]));
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003440 File Offset: 0x00001640
		private static void ConvertFaces( ODOL.ODOL odol, MLOD_LOD dstLod, LOD srcLOD)
		{
			List<Face> list = new List<Face>(srcLOD.VertexCount * 2);
			foreach (Section section in srcLOD.Sections)
			{
				float[] uvdata = srcLOD.UVSets[0].UVData;
				foreach (uint num in section.getFaceIndexes(srcLOD.Faces))
				{
					int num2 = srcLOD.Faces[(int)num].VertexIndices.Length;
					Vertex[] array = new Vertex[num2];
					for (int k = 0; k < num2; k++)
					{
						int num3 = srcLOD.Faces[(int)num].VertexIndices[num2 - 1 - k];
						array[k] = new Vertex(num3, num3, uvdata[num3 * 2], uvdata[num3 * 2 + 1]);
					}
					string texture = (section.textureIndex == -1) ? "" : srcLOD.Textures[(int)section.textureIndex];
					string material = (section.materialIndex == -1) ? "" : srcLOD.Materials[section.materialIndex].materialName;
					Face item = new Face(num2, array, FaceFlags.DEFAULT, texture, material);
					list.Add(item);
				}
			}
			dstLod.faces = list.ToArray();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000035B8 File Offset: 0x000017B8
		private static void ReconstructNamedSelectionBySections(LOD src, out Dictionary<string, List<Conversion.PointWeight>> points, out Dictionary<string, List<int>> faces)
		{
			points = new Dictionary<string, List<PointWeight>>( src.NamedSelections.Length * 2 );
			faces = new Dictionary<string, List<int>>( src.NamedSelections.Length * 2 );
			NamedSelection[] namedSelections = src.NamedSelections;
			foreach (NamedSelection namedSelection in namedSelections)
			{
				if (namedSelection.IsSectional)
				{
					IEnumerable<uint> enumerable = Enumerable.SelectMany<int, uint>( (IEnumerable<int>)namedSelection.Sections, (Func<int, IEnumerable<uint>>)(( int si ) => src.Sections[si].getFaceIndexes( src.Faces )) );
					IEnumerable<PointWeight> enumerable2 = Enumerable.Select<VertexIndex, PointWeight>( Enumerable.SelectMany<uint, VertexIndex>( enumerable, (Func<uint, IEnumerable<VertexIndex>>)(( uint fi ) => src.Faces[fi].VertexIndices) ), (Func<VertexIndex, PointWeight>)(( VertexIndex vi ) => new PointWeight( vi, byte.MaxValue )) );
					faces[namedSelection.Name] = Enumerable.ToList<int>( Enumerable.Select<uint, int>( enumerable, (Func<uint, int>)(( uint fi ) => (int)fi) ) );
					points[namedSelection.Name] = Enumerable.ToList<PointWeight>( enumerable2 );
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000370C File Offset: 0x0000190C
		private static void ReconstructProxies(LOD src, out Dictionary<string, List<Conversion.PointWeight>> points, out Dictionary<string, List<int>> faces)
		{
			points = new Dictionary<string, List<Conversion.PointWeight>>(src.NamedSelections.Length * 2);
			faces = new Dictionary<string, List<int>>(src.NamedSelections.Length * 2);
			for (int i = 0; i < src.Faces.Length; i++)
			{
				Polygon polygon = src.Faces[i];
				if (polygon.VertexIndices.Length == 3)
				{
					VertexIndex vi = polygon.VertexIndices[0];
					VertexIndex vi2 = polygon.VertexIndices[1];
					VertexIndex vi3 = polygon.VertexIndices[2];
					Vector3P vector3P = src.Vertices[vi];
					Vector3P vector3P2 = src.Vertices[vi2];
					Vector3P vector3P3 = src.Vertices[vi3];
					float num = vector3P.Distance(vector3P2);
					float num2 = vector3P.Distance(vector3P3);
					float num3 = vector3P2.Distance(vector3P3);
					if (num > num2)
					{
						Methods.Swap<Vector3P>(ref vector3P2, ref vector3P3);
						Methods.Swap<float>(ref num, ref num2);
					}
					if (num > num3)
					{
						Methods.Swap<Vector3P>(ref vector3P, ref vector3P3);
						Methods.Swap<float>(ref num, ref num3);
					}
					if (num2 > num3)
					{
						Methods.Swap<Vector3P>(ref vector3P, ref vector3P2);
						Methods.Swap<float>(ref num2, ref num3);
					}
					Vector3P vector3P4 = vector3P;
					Vector3P vector3P5 = vector3P2 - vector3P;
					Vector3P vector3P6 = vector3P3 - vector3P;
					vector3P5.Normalize();
					vector3P6.Normalize();
					if (Methods.EqualsFloat(vector3P6 * vector3P5, 0f, 0.05f))
					{
						for (int j = 0; j < src.Proxies.Length; j++)
						{
							Vector3P position = src.Proxies[j].transformation.Position;
							Vector3P up = src.Proxies[j].transformation.Orientation.Up;
							Vector3P dir = src.Proxies[j].transformation.Orientation.Dir;
							if (vector3P4.Equals(position) && vector3P5.Equals(dir) && vector3P6.Equals(up))
							{
								Proxy proxy = src.Proxies[j];
								string name = src.NamedSelections[proxy.namedSelectionIndex].Name;
								if (!faces.ContainsKey(name))
								{
									faces[name] = i.Yield<int>().ToList<int>();
									points[name] = Methods.Yield<Conversion.PointWeight>(new Conversion.PointWeight[]
									{
										new Conversion.PointWeight(vi, byte.MaxValue),
										new Conversion.PointWeight(vi2, byte.MaxValue),
										new Conversion.PointWeight(vi3, byte.MaxValue)
									}).ToList<Conversion.PointWeight>();
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000039D8 File Offset: 0x00001BD8
		private static void ReconstructNamedSelectionsByBones(LOD src, Skeleton skeleton, out Dictionary<string, List<Conversion.PointWeight>> points)
		{
			points = new Dictionary<string, List<Conversion.PointWeight>>(src.NamedSelections.Length * 2);
			if (src.VertexBoneRef.Length == 0)
			{
				return;
			}
			ushort num = 0;
			AnimationRTWeight[] vertexBoneRef = src.VertexBoneRef;
			for (int i = 0; i < vertexBoneRef.Length; i++)
			{
				foreach (AnimationRTPair animationRTPair in vertexBoneRef[i].AnimationRTPairs)
				{
					byte selectionIndex = animationRTPair.SelectionIndex;
					byte weight = animationRTPair.Weight;
					int num2 = src.SubSkeletonsToSkeleton[(int)selectionIndex];
					string key = skeleton.bones[num2 * 2];
					Conversion.PointWeight item = new Conversion.PointWeight((int)num, weight);
					List<Conversion.PointWeight> list;
					if (!points.TryGetValue(key, out list))
					{
						list = new List<Conversion.PointWeight>(10000);
						list.Add(item);
						points[key] = list;
					}
					else
					{
						list.Add(item);
					}
				}
				num += 1;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003ACC File Offset: 0x00001CCC
		private static IEnumerable<NamedSelectionTagg> CreateNamedSelectionTaggs(LOD src)
		{
			int nPoints = src.VertexCount;
			int nFaces = src.Faces.Length;
			NamedSelection[] namedSelections = src.NamedSelections;
			NamedSelection[] array = namedSelections;
			foreach (NamedSelection namedSelection in array)
			{
				NamedSelectionTagg namedSelectionTagg = new NamedSelectionTagg();
				namedSelectionTagg.Name = namedSelection.Name;
				namedSelectionTagg.DataSize = (uint)(nPoints + nFaces);
				namedSelectionTagg.points = new byte[nPoints];
				namedSelectionTagg.faces = new byte[nFaces];
				bool flag = namedSelection.SelectedVerticesWeights.Length != 0;
				int num = 0;
				VertexIndex[] selectedVertices = namedSelection.SelectedVertices;
				foreach (int num2 in selectedVertices)
				{
					byte b = (byte)((!flag) ? 1 : ((byte)(-namedSelection.SelectedVerticesWeights[num++])));
					namedSelectionTagg.points[num2] = b;
				}
				selectedVertices = namedSelection.SelectedFaces;
				foreach (int num3 in selectedVertices)
				{
					try
					{
						if ( num3 < 0 ) continue;
						namedSelectionTagg.faces[num3] = 1;
					}
					catch (Exception e)
					{
						break;
					}
				}
				yield return namedSelectionTagg;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003ADC File Offset: 0x00001CDC
		private static IEnumerable<AnimationTagg> CreateAnimTaggs(LOD src)
		{
			Keyframe[] frames = src.Frames;
			foreach (Keyframe keyframe in frames)
			{
				int num = keyframe.points.Length;
				AnimationTagg animationTagg = new AnimationTagg();
				animationTagg.Name = "#Animation#";
				animationTagg.DataSize = (uint)(num * 12 + 4);
				animationTagg.frameTime = keyframe.time;
				animationTagg.framePoints = new Vector3P[num];
				Array.Copy( keyframe.points, animationTagg.framePoints, num );
				yield return animationTagg;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003AEC File Offset: 0x00001CEC
		private static MassTagg CreateMassTagg(int nPoints, float totalMass)
		{
			MassTagg massTagg = new MassTagg();
			massTagg.Name = "#Mass#";
			massTagg.DataSize = (uint)(nPoints * 4);
			massTagg.mass = new float[nPoints];
			for (int i = 0; i < nPoints; i++)
			{
				massTagg.mass[i] = totalMass / (float)nPoints;
			}
			return massTagg;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003B40 File Offset: 0x00001D40
		private static IEnumerable<UVSetTagg> CreateUVSetTaggs(LOD src)
		{
			int nFaces = src.Faces.Length;
			int num3;
			for (int i = 0; i < src.UVSets.Length; i = num3)
			{
				UVSetTagg uvsetTagg = new UVSetTagg();
				uvsetTagg.Name = "#UVSet#";
				uvsetTagg.uvSetNr = (uint)i;
				uvsetTagg.faceUVs = new float[nFaces][,];
				float[] uvdata = src.UVSets[i].UVData;
				uint num = 4U;
				for (int j = 0; j < nFaces; j++)
				{
					Polygon polygon = src.Faces[j];
					int num2 = polygon.VertexIndices.Length;
					uvsetTagg.faceUVs[j] = new float[num2, 2];
					for (int k = 0; k < num2; k++)
					{
						VertexIndex vi = polygon.VertexIndices[num2 - 1 - k];
						uvsetTagg.faceUVs[j][k, 0] = uvdata[vi * 2];
						uvsetTagg.faceUVs[j][k, 1] = uvdata[vi * 2 + 1];
						num += 8U;
					}
				}
				uvsetTagg.DataSize = num;
				yield return uvsetTagg;
				num3 = i + 1;
			}
			yield break;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003B50 File Offset: 0x00001D50
		private static IEnumerable<PropertyTagg> CreatePropertyTaggs(LOD src)
		{
			int num;
			for (int i = 0; i < src.NamedProperties.Length / 2; i = num)
			{
				yield return new PropertyTagg
				{
					Name = "#Property#",
					DataSize = 128U,
					name = src.NamedProperties[i, 0],
					value = src.NamedProperties[i, 1]
				};
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x02000052 RID: 82
		private struct PointWeight
		{
			// Token: 0x0600024C RID: 588 RVA: 0x0000ACA0 File Offset: 0x00008EA0
			public PointWeight(int index, byte weight)
			{
				this.pointIndex = index;
				this.weight = weight;
			}

			// Token: 0x040001EB RID: 491
			public int pointIndex;

			// Token: 0x040001EC RID: 492
			public byte weight;
		}
	}
}
