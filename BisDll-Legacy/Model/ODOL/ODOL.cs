using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BisDll.Common;
using BisDll.Stream;
using BisDll.Common.Math;

namespace BisDll.Model.ODOL
{
	// Token: 0x0200001F RID: 31
	public class ODOL : P3D
	{
		public Skeleton Skeleton
		{
			get
			{
				return this.modelInfo.skeleton;
			}
		}
		
		public override float Mass
		{
			get
			{
				return this.modelInfo.mass;
			}
		}

		public override P3D_LOD[] LODs
		{
			get
			{
				return this.lods;
			}
		}
		public string fileName;
		public ODOL(string fileName )
		{
			this.fileName = fileName;
			System.IO.Stream stream = File.OpenRead( fileName );
			Read( new BinaryReaderEx( stream ) );
		}
		
		public bool IsSnappable()
		{
			LOD lod = this.lods.FirstOrDefault((LOD l) => l.Resolution.getLODType() == LodName.Memory);
			if (lod != null)
			{
				if ((from ns in lod.NamedSelections
				where ns.Name.Equals("lb", StringComparison.InvariantCultureIgnoreCase) || ns.Name.Equals("le", StringComparison.InvariantCultureIgnoreCase) || ns.Name.Equals("pb", StringComparison.InvariantCultureIgnoreCase) || ns.Name.Equals("pe", StringComparison.InvariantCultureIgnoreCase)
				select ns).Count<NamedSelection>() >= 4)
				{
					return true;
				}
			}
			return false;
		}
		
		private void Read(BinaryReaderEx input)
		{
			string b = input.ReadAscii(4);
			if ("ODOL" != b)
			{
				throw new FormatException("ODOL signature is missing");
			}
			base.Version = input.ReadUInt32();
			Console.WriteLine("Version is " + base.Version.ToString() + ", Hex: " + base.Version.ToString("X4"));
			if (base.Version > 73U)
			{
				throw new FormatException("Unknown ODOL version");
			}
			if (base.Version < 28U)
			{
				throw new FormatException("Old ODOL version is currently not supported");
			}
			input.Version = (int)base.Version;
			if (base.Version >= 44U)
			{
				input.UseLZOCompression = true;
				Logging_Functions.Echo("LZO Compression Enabled");
			}
			if (base.Version >= 64U)
			{
				input.UseCompressionFlag = true;
				Logging_Functions.Echo("Compression Flag Used");
			}
			if (base.Version >= 59U)
			{
				this.appID = input.ReadUInt32();
				Logging_Functions.Echo<uint>(input, this.appID, "appID");
			}
			if (base.Version >= 58U)
			{
				this.muzzleFlash = input.ReadAsciiz();
				Logging_Functions.Echo<string>(input, this.muzzleFlash, "muzzleFlash");
			}
			this.nLods = input.ReadInt32();
			Logging_Functions.Echo<int>(input, this.nLods, "nLods");
			this.resolutions = new float[this.nLods];
			for (int i = 0; i < this.nLods; i++)
			{
				this.resolutions[i] = input.ReadSingle();
				Logging_Functions.Echo("Found resolution with index " + i.ToString() + " and data " + this.resolutions[i].ToString());
			}
			this.modelInfo = new ODOL_ModelInfo(input, this.nLods);
			if (base.Version >= 30U)
			{
				this.hasAnims = input.ReadBoolean();
				if (this.hasAnims)
				{
					this.animations.Read(input);
					Logging_Functions.Echo("Animations present and read.");
				}
			}
			this.lodStartAdresses = new uint[this.nLods];
			this.lodEndAdresses = new uint[this.nLods];
			this.permanent = new bool[this.nLods];
			for (int j = 0; j < this.nLods; j++)
			{
				this.lodStartAdresses[j] = input.ReadUInt32();
				Logging_Functions.Echo(string.Concat(new string[]
				{
					"LOD start address of LOD ",
					j.ToString(),
					" found at: ",
					this.lodStartAdresses[j].ToString(),
					", Hex: ",
					this.lodStartAdresses[j].ToString("X4")
				}));
			}
			for (int k = 0; k < this.nLods; k++)
			{
				this.lodEndAdresses[k] = input.ReadUInt32();
				Logging_Functions.Echo(string.Concat(new string[]
				{
					"LOD end address of LOD ",
					k.ToString(),
					" found at: ",
					this.lodEndAdresses[k].ToString(),
					", Hex: ",
					this.lodEndAdresses[k].ToString("X4")
				}));
			}
			for (int l = 0; l < this.nLods; l++)
			{
				this.permanent[l] = input.ReadBoolean();
				Logging_Functions.Echo("LOD " + l.ToString() + " is permanent: " + this.permanent[l].ToString());
			}
			this.LoadableLodInfos = new List<LoadableLodInfo>(this.nLods);
			this.lods = new LOD[this.nLods];
			long position = input.Position;
			for (int m = 0; m < this.nLods; m++)
			{
				if (!this.permanent[m])
				{
					LoadableLodInfo loadableLodInfo = new LoadableLodInfo();
					loadableLodInfo.ReadObject(input);
					this.LoadableLodInfos.Add(loadableLodInfo);
					position = input.Position;
				}
				input.Position = (long)((ulong)this.lodStartAdresses[m]);
				this.lods[m] = new LOD();
				this.lods[m].Read(input, this.resolutions[m]);
				input.Position = position;
			}
			if (base.Version >= 54U)
			{
				input.Position = (long)((ulong)this.lodEndAdresses.Max<uint>());
				this.buoyancyType = input.ReadInt32();
			}
			input.Close();
		}

		public string[] GetHiddenSelectionNames()
		{
			List<string> ret = new List<string>();
			for (int idxLod = 0; idxLod < nLods; idxLod++)
			{
				if (lods[idxLod].Name.StartsWith("."))
				{
					foreach ( var section in lods[idxLod].Sections )
					{
						ret.Add("");
					}
					for ( int idxNamedSelection = 0; idxNamedSelection < lods[idxLod].NamedSelections.Count(); idxNamedSelection++ )
					{
						if (lods[idxLod].NamedSelections[idxNamedSelection].Sections.Count() == 1)
						{
							ret[lods[idxLod].NamedSelections[idxNamedSelection].Sections[0]] = lods[idxLod].NamedSelections[idxNamedSelection].Name;
						}
					}
				}
			}

			List<string> ret2 = new List<string>();
			foreach ( var selection in ret)
			{
				if (selection != "") ret2.Add( selection );
			}

			return ret2.ToArray();
		}
			
		public string[] GetAxisSelectionNames()
		{
			List<string> ret = new List<string>();

			for (int idxLod = 0; idxLod < nLods; idxLod++ )
			{
				if ( lods[idxLod].Name == "Memory" )
				{
					for ( int idxAnimClass = 0; idxAnimClass < animations.axisData[idxLod].Count(); idxAnimClass++ )
					{
						if (animations.axisData[idxLod][idxAnimClass] == null)
						{
							ret.Add( "" );
							continue;
						}

						foreach (var selection in lods[idxLod].NamedSelections)
						{
							if (selection.SelectedVertices.Count() == 2)
							{
								switch (animations.animationClasses[idxAnimClass].animType)
								{
									case Animations.AnimationClass.AnimType.Rotation:
									case Animations.AnimationClass.AnimType.RotationX:
									case Animations.AnimationClass.AnimType.RotationY:
									case Animations.AnimationClass.AnimType.RotationZ:
									{
										Vector3P r = lods[idxLod].Points[selection.SelectedVertices[0].value];
										if (AreVectorsAboutEqual( r, animations.axisData[idxLod][idxAnimClass][0] ))
										{
											ret.Add( selection.Name );
											break;
										}
										continue;
									}
									case Animations.AnimationClass.AnimType.Translation:
									case Animations.AnimationClass.AnimType.TranslationX:
									case Animations.AnimationClass.AnimType.TranslationY:
									case Animations.AnimationClass.AnimType.TranslationZ:
									{
										Vector3P a = lods[idxLod].Points[selection.SelectedVertices[0].value];
										Vector3P b = lods[idxLod].Points[selection.SelectedVertices[1].value];
										Vector3P diff = b - a;
										if (AreVectorsAboutEqual( diff, animations.axisData[idxLod][idxAnimClass][1] ))
										{
											ret.Add( selection.Name );
											break;
										}
										continue;
									}
								}
							}
						}
					}

					break;
				}
			}

			return ret.ToArray();
		}

		public bool AreVectorsAboutEqual( Vector3P a, Vector3P b)
		{
			if (Math.Abs( a.X - b.X ) > 0.000001) return false;
			if (Math.Abs( a.Y - b.Y ) > 0.000001) return false;
			if (Math.Abs( a.Z - b.Z ) > 0.000001) return false;
			return true;
		}
		
		public string CombineModelCfg( string[] existing )
		{
			var ret = new System.Text.StringBuilder();

			bool inSkeletons = false;
			bool inModels = false;
			bool hasDefault = false;

			foreach ( string line in existing )
			{
				if (line.ToLower().Contains( "class cfgskeletons" ))
				{
					if (modelInfo.skeleton.Name != "") inSkeletons = true;
					inModels = false;
				}
				else if (line.ToLower().Contains( "class cfgmodels" ))
				{
					inSkeletons = false;
					inModels = true;
				}
				else if (inSkeletons)
				{
					if (line.Contains( "\tclass " + modelInfo.skeleton.Name ))
					{
						inSkeletons = false;
					}
					else if (line.StartsWith( "};" ))
					{
						ret.AppendLine( "\tclass " + modelInfo.skeleton.Name );
						ret.AppendLine( "\t{" );
						ret.AppendLine( "\t\tIsDiscrete = " + modelInfo.skeleton.isDiscrete.GetHashCode() + ";" );
						ret.AppendLine( "\t\tSkeletonInherit = \"" + modelInfo.skeleton.pivotsNameObsolete + "\";" );
						ret.AppendLine( "\t\tSkeletonBones[] =" );
						ret.AppendLine( "\t\t{" );
						for (int idxBone = 0; idxBone < modelInfo.skeleton.bones.Count(); idxBone += 2)
						{
							ret.Append( "\t\t\t\"" + modelInfo.skeleton.bones[idxBone] + "\", \"" + modelInfo.skeleton.bones[idxBone + 1] + "\"" );
							if (idxBone + 1 != modelInfo.skeleton.bones.Count() - 1) ret.Append( "," );
							ret.AppendLine();
						}
						ret.AppendLine( "\t\t};" );
						ret.AppendLine( "\t};" );
					}
				}
				else if (inModels)
				{
					if (line.Contains( "\tclass " + Path.GetFileNameWithoutExtension( fileName ) ))
					{
						inModels = false;
					}
					else if (line.Contains( "class Default" ))
					{
						hasDefault = true;
					}
					else if (line.StartsWith( "};" ))
					{
						inModels = false;
						if (!hasDefault)
						{
							ret.AppendLine( "\tclass Default" );
							ret.AppendLine( "\t{" );
							ret.AppendLine( "\t\tSections[] = {};" );
							ret.AppendLine( "\t\tSectionsInherit = \"\";" );
							ret.AppendLine( "\t\tSkeletonName = \"\";" );
							ret.AppendLine( "\t};" );
						}

						ret.AppendLine( "\tclass " + Path.GetFileNameWithoutExtension( fileName ) + ": Default" );
						ret.AppendLine( "\t{" );

						string[] selections = GetHiddenSelectionNames();
						if (selections.Count() != 0)
						{
							ret.AppendLine( "\t\tSections[] =" );
							ret.AppendLine( "\t\t{" );

							for (int idxSelection = 0; idxSelection < selections.Count(); idxSelection++)
							{
								ret.Append( "\t\t\t\"" + selections[idxSelection] + "\"" );
								if (idxSelection + 1 != selections.Count()) ret.Append( "," );
								ret.AppendLine();
							}

							ret.AppendLine( "\t\t};" );
						}

						if (modelInfo.skeleton.Name != "") ret.AppendLine( "\t\tSkeletonName = \"" + modelInfo.skeleton.Name + "\";" );
						if (animations.animationClasses != null)
						{
							string[] axes = GetAxisSelectionNames();

							ret.AppendLine( "\t\tclass Animations" );
							ret.AppendLine( "\t\t{" );
							for (int idxAnimClass = 0; idxAnimClass < animations.animationClasses.Count(); idxAnimClass++)
							{
								ret.AppendLine( "\t\t\tclass " + animations.animationClasses[idxAnimClass].animName );
								ret.AppendLine( "\t\t\t{" );

								ret.AppendLine( "\t\t\t\ttype = \"" + animations.animationClasses[idxAnimClass].animType + "\";" );
								ret.AppendLine( "\t\t\t\tsource = \"" + animations.animationClasses[idxAnimClass].animSource + "\";" );
								ret.AppendLine( "\t\t\t\tselection = \"" + modelInfo.skeleton.bones[animations.Anims2Bones[0][idxAnimClass] * 2] + "\";" );
								if (axes[idxAnimClass] != "")
									ret.AppendLine( "\t\t\t\taxis = \"" + axes[idxAnimClass] + "\";" );

								if (animations.animationClasses[idxAnimClass].sourceAddress != 0)
									ret.AppendLine( "\t\t\t\tsourceAddress = \"" + animations.animationClasses[idxAnimClass].sourceAddress + "\";" );
								if (animations.animationClasses[idxAnimClass].minPhase != 0 || animations.animationClasses[idxAnimClass].maxPhase != 1)
									ret.AppendLine( "\t\t\t\tminPhase = \"" + animations.animationClasses[idxAnimClass].minPhase + "\";" );
								if (animations.animationClasses[idxAnimClass].maxPhase != 1)
									ret.AppendLine( "\t\t\t\tmaxPhase = \"" + animations.animationClasses[idxAnimClass].maxPhase + "\";" );
								ret.AppendLine( "\t\t\t\tminValue = \"" + animations.animationClasses[idxAnimClass].minValue + "\";" );
								ret.AppendLine( "\t\t\t\tmaxValue = \"" + animations.animationClasses[idxAnimClass].maxValue + "\";" );
								switch (animations.animationClasses[idxAnimClass].animType)
								{
									case Animations.AnimationClass.AnimType.Rotation:
									case Animations.AnimationClass.AnimType.RotationX:
									case Animations.AnimationClass.AnimType.RotationY:
									case Animations.AnimationClass.AnimType.RotationZ:
										ret.AppendLine( "\t\t\t\tangle0 = \"" + animations.animationClasses[idxAnimClass].angle0 + "\";" );
										ret.AppendLine( "\t\t\t\tangle1 = \"" + animations.animationClasses[idxAnimClass].angle1 + "\";" );
										break;
									case Animations.AnimationClass.AnimType.Translation:
									case Animations.AnimationClass.AnimType.TranslationX:
									case Animations.AnimationClass.AnimType.TranslationY:
									case Animations.AnimationClass.AnimType.TranslationZ:
										ret.AppendLine( "\t\t\t\toffset0 = \"" + animations.animationClasses[idxAnimClass].offset0 + "\";" );
										ret.AppendLine( "\t\t\t\toffset1 = \"" + animations.animationClasses[idxAnimClass].offset1 + "\";" );
										break;
									case Animations.AnimationClass.AnimType.Direct:
										ret.AppendLine( "\t\t\t\taxisPos = \"" + animations.animationClasses[idxAnimClass].axisPos + "\";" );
										ret.AppendLine( "\t\t\t\taxisDir = \"" + animations.animationClasses[idxAnimClass].axisDir + "\";" );
										ret.AppendLine( "\t\t\t\tangle = \"" + animations.animationClasses[idxAnimClass].angle + "\";" );
										ret.AppendLine( "\t\t\t\taxisOffset = \"" + animations.animationClasses[idxAnimClass].axisOffset + "\";" );
										break;
									case Animations.AnimationClass.AnimType.Hide:
										ret.AppendLine( "\t\t\t\thideValue = \"" + animations.animationClasses[idxAnimClass].hideValue + "\";" );
										break;
								}

								ret.AppendLine( "\t\t\t};" );
							}

							ret.AppendLine( "\t\t};" );
						}

						ret.AppendLine( "\t};" );
					}
				}

				ret.AppendLine( line );
			}

			return ret.ToString();
		}

		public string GetModelCfg()
		{
			var ret = new System.Text.StringBuilder();

			if (modelInfo.skeleton.bones != null)
			{
				ret.AppendLine( "class CfgSkeletons" );
				ret.AppendLine( "{" );
				ret.AppendLine( "\tclass " + modelInfo.skeleton.Name );
				ret.AppendLine( "\t{" );
				ret.AppendLine( "\t\tIsDiscrete = " + modelInfo.skeleton.isDiscrete.GetHashCode() + ";" );
				ret.AppendLine( "\t\tSkeletonInherit = \"" + modelInfo.skeleton.pivotsNameObsolete + "\";" );
				ret.AppendLine( "\t\tSkeletonBones[] =" );
				ret.AppendLine( "\t\t{" );
				for (int idxBone = 0; idxBone < modelInfo.skeleton.bones.Count(); idxBone += 2)
				{
					ret.Append( "\t\t\t\"" + modelInfo.skeleton.bones[idxBone] + "\", \"" + modelInfo.skeleton.bones[idxBone + 1] + "\"" );
					if (idxBone + 1 != modelInfo.skeleton.bones.Count() - 1) ret.Append( "," );
					ret.AppendLine();
				}
				ret.AppendLine( "\t\t};" );
				ret.AppendLine( "\t};" );
				ret.AppendLine( "};\n" );
			}

			ret.AppendLine( "class CfgModels" );
			ret.AppendLine( "{" );
			ret.AppendLine( "\tclass Default" );
			ret.AppendLine( "\t{" );
			ret.AppendLine( "\t\tSections[] = {};" );
			ret.AppendLine( "\t\tSectionsInherit = \"\";" );
			ret.AppendLine( "\t\tSkeletonName = \"\";" );
			ret.AppendLine( "\t};" );
			ret.AppendLine( "\tclass " + Path.GetFileNameWithoutExtension( fileName ) + ": Default" );
			ret.AppendLine( "\t{" );

			string[] selections = GetHiddenSelectionNames();
			if ( selections.Count() != 0)
			{
				ret.AppendLine( "\t\tSections[] =" );
				ret.AppendLine( "\t\t{" );

				for (int idxSelection = 0; idxSelection < selections.Count(); idxSelection++)
				{
					ret.Append( "\t\t\t\"" + selections[idxSelection] + "\"" );
					if (idxSelection + 1 != selections.Count()) ret.Append( "," );
					ret.AppendLine();
				}

				ret.AppendLine( "\t\t};" );
			}

			if (modelInfo.skeleton.Name != "") ret.AppendLine( "\t\tSkeletonName = \"" + modelInfo.skeleton.Name + "\";" );
			if (animations.animationClasses != null)
			{
				string[] axes = GetAxisSelectionNames();

				ret.AppendLine( "\t\tclass Animations" );
				ret.AppendLine( "\t\t{" );
				for (int idxAnimClass = 0; idxAnimClass < animations.animationClasses.Count(); idxAnimClass++)
				{
					ret.AppendLine( "\t\t\tclass " + animations.animationClasses[idxAnimClass].animName );
					ret.AppendLine( "\t\t\t{" );

					ret.AppendLine( "\t\t\t\ttype = \"" + animations.animationClasses[idxAnimClass].animType + "\";" );
					ret.AppendLine( "\t\t\t\tsource = \"" + animations.animationClasses[idxAnimClass].animSource + "\";" );
					ret.AppendLine( "\t\t\t\tselection = \"" + modelInfo.skeleton.bones[animations.Anims2Bones[0][idxAnimClass]*2] + "\";" );
					if (axes[idxAnimClass] != "")
						ret.AppendLine( "\t\t\t\taxis = \"" + axes[idxAnimClass] + "\";" );
					
					if (animations.animationClasses[idxAnimClass].sourceAddress != 0)
						ret.AppendLine( "\t\t\t\tsourceAddress = \"" + animations.animationClasses[idxAnimClass].sourceAddress + "\";" );
					if (animations.animationClasses[idxAnimClass].minPhase != 0 || animations.animationClasses[idxAnimClass].maxPhase != 1)
						ret.AppendLine( "\t\t\t\tminPhase = \"" + animations.animationClasses[idxAnimClass].minPhase + "\";" );
					if (animations.animationClasses[idxAnimClass].maxPhase != 1)
						ret.AppendLine( "\t\t\t\tmaxPhase = \"" + animations.animationClasses[idxAnimClass].maxPhase + "\";" );
					ret.AppendLine( "\t\t\t\tminValue = \"" + animations.animationClasses[idxAnimClass].minValue + "\";" );
					ret.AppendLine( "\t\t\t\tmaxValue = \"" + animations.animationClasses[idxAnimClass].maxValue + "\";" );
					switch (animations.animationClasses[idxAnimClass].animType)
					{
						case Animations.AnimationClass.AnimType.Rotation:
						case Animations.AnimationClass.AnimType.RotationX:
						case Animations.AnimationClass.AnimType.RotationY:
						case Animations.AnimationClass.AnimType.RotationZ:
							ret.AppendLine( "\t\t\t\tangle0 = \"" + animations.animationClasses[idxAnimClass].angle0 + "\";" );
							ret.AppendLine( "\t\t\t\tangle1 = \"" + animations.animationClasses[idxAnimClass].angle1 + "\";" );
							break;
						case Animations.AnimationClass.AnimType.Translation:
						case Animations.AnimationClass.AnimType.TranslationX:
						case Animations.AnimationClass.AnimType.TranslationY:
						case Animations.AnimationClass.AnimType.TranslationZ:
							ret.AppendLine( "\t\t\t\toffset0 = \"" + animations.animationClasses[idxAnimClass].offset0 + "\";" );
							ret.AppendLine( "\t\t\t\toffset1 = \"" + animations.animationClasses[idxAnimClass].offset1 + "\";" );
							break;
						case Animations.AnimationClass.AnimType.Direct:
							ret.AppendLine( "\t\t\t\taxisPos = \"" + animations.animationClasses[idxAnimClass].axisPos + "\";" );
							ret.AppendLine( "\t\t\t\taxisDir = \"" + animations.animationClasses[idxAnimClass].axisDir + "\";" );
							ret.AppendLine( "\t\t\t\tangle = \"" + animations.animationClasses[idxAnimClass].angle + "\";" );
							ret.AppendLine( "\t\t\t\taxisOffset = \"" + animations.animationClasses[idxAnimClass].axisOffset + "\";" );
							break;
						case Animations.AnimationClass.AnimType.Hide:
							ret.AppendLine( "\t\t\t\thideValue = \"" + animations.animationClasses[idxAnimClass].hideValue + "\";" );
							break;
					}

					ret.AppendLine( "\t\t\t};" );
				}

				ret.AppendLine( "\t\t};" );
			}
			
			ret.AppendLine( "\t};" );
			ret.AppendLine( "};" );

			return ret.ToString();
		}

		// Token: 0x04000119 RID: 281
		public const int LATEST_VERSION = 73;

		// Token: 0x0400011A RID: 282
		public const int MINIMAL_VERSION = 28;

		// Token: 0x0400011B RID: 283
		private string muzzleFlash;

		// Token: 0x0400011C RID: 284
		private uint appID;

		// Token: 0x0400011D RID: 285
		private int nLods;

		// Token: 0x0400011E RID: 286
		private float[] resolutions;

		// Token: 0x0400011F RID: 287
		public ODOL_ModelInfo modelInfo;

		// Token: 0x04000120 RID: 288
		private bool hasAnims;

		// Token: 0x04000121 RID: 289
		private Animations animations = new Animations();

		// Token: 0x04000122 RID: 290
		private uint[] lodStartAdresses;

		// Token: 0x04000123 RID: 291
		private uint[] lodEndAdresses;

		// Token: 0x04000124 RID: 292
		private bool[] permanent;

		// Token: 0x04000125 RID: 293
		private List<LoadableLodInfo> LoadableLodInfos;

		// Token: 0x04000126 RID: 294
		private LOD[] lods;

		// Token: 0x04000127 RID: 295
		private int buoyancyType;
	}
}
