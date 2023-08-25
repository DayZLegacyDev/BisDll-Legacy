using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BisDll.Common;
using BisDll.Stream;

namespace BisDll.Model.ODOL
{
	public class EmbeddedMaterial : IDeserializable
	{
		public void WriteToFile(string fileName)
		{
			List<string> list = new List<string>();
			string item = "Emissive[] = " + this.emissive + ";";
			string item2 = "Ambient[] = " + this.ambient + ";";
			string item3 = "Diffuse[] = " + this.diffuse + ";";
			string item4 = "forcedDiffuse[] = " + this.forcedDiffuse + ";";
			string item5 = "Specular[] = " + this.specular + ";";
			string item6 = "specularPower = " + this.specularPower.ToString(new CultureInfo("en-GB").NumberFormat) + ";";
			string text = Enum.GetName(this.pixelShader.GetType(), this.pixelShader);
			string text2 = Enum.GetName(this.vertexShader.GetType(), this.vertexShader);
			if (text == "")
			{
				text = "Unknown PixelShaderID (" + this.pixelShader + ")";
			}
			if (text2 == "")
			{
				text2 = "Unknown VertexShaderID (" + this.vertexShader + ")";
			}
			string item7 = "PixelShader = " + text + ";";
			string item8 = "VertexShader = " + text2 + ";";
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			list.Add(item4);
			list.Add(item5);
			list.Add(item6);
			list.Add(item7);
			list.Add(item8);
			if (this.surfaceFile != "")
			{
				list.Add("surfaceInfo = " + this.surfaceFile + ";");
			}
			if (this.stageTextures != null)
			{
				for (int i = 0; i < this.stageTextures.Length; i++)
				{
					list.Add("class Stage" + i.ToString() + 1.ToString());
					list.Add("{");
					list.Add("\tfilter = " + Enum.GetName(this.stageTextures[i].textureFilter.GetType(), this.stageTextures[i].textureFilter) + ";");
					list.Add("\ttexture = " + this.stageTextures[i].texture + ";");
					list.Add("\tuvSource = " + this.stageTransforms[i].uvSource + ";");
					list.Add("\tclass uvTransform");
					list.Add("\t{");
					list.Add("\t\taside[] = " + this.stageTransforms[i].transformation.Orientation.Aside + ";");
					list.Add("\t\tup[] = " + this.stageTransforms[i].transformation.Orientation.Up + ";");
					list.Add("\t\tdir[] = " + this.stageTransforms[i].transformation.Orientation.Dir + ";");
					list.Add("\t\tpos[] = " + this.stageTransforms[i].transformation.Position + ";");
					list.Add("\t};");
					list.Add("};");
				}
			}
			File.WriteAllLines(fileName, list.ToArray());
		}
		
		public void ReadObject(BinaryReaderEx input)
		{
			this.materialName = input.ReadAsciiz();
			Logging_Functions.Echo<string>(input, this.materialName, "materialName");
			this.version = input.ReadUInt32();
			Logging_Functions.Echo<uint>( input, this.version, "version" );
			Console.WriteLine( "[Material ver. " + version + "] " + materialName );
			this.emissive.read(input);
			Logging_Functions.Echo<ColorP>(input, this.emissive, "emissive");
			this.ambient.read(input);
			Logging_Functions.Echo<ColorP>(input, this.ambient, "ambient");
			this.diffuse.read(input);
			Logging_Functions.Echo<ColorP>(input, this.diffuse, "diffuse");
			this.forcedDiffuse.read(input);
			Logging_Functions.Echo<ColorP>(input, this.forcedDiffuse, "forcedDiffuse");
			this.specular.read(input);
			Logging_Functions.Echo<ColorP>(input, this.specular, "specular");
			this.specularCopy.read(input);
			Logging_Functions.Echo<ColorP>(input, this.specularCopy, "specularCopy");
			if (this.version >= 16U)
			{
				input.Position += 32;
			}
			this.specularPower = input.ReadSingle();
			Logging_Functions.Echo<float>(input, this.specularPower, "specularPower");
			this.pixelShader = (EmbeddedMaterial.PixelShaderID)input.ReadUInt32();
			Logging_Functions.Echo<EmbeddedMaterial.PixelShaderID>(input, this.pixelShader, "pixelShader");
			if (this.version == 15U)
			{
				input.Position += 40;
			}
			this.vertexShader = (EmbeddedMaterial.VertexShaderID)input.ReadUInt32();
			Logging_Functions.Echo<EmbeddedMaterial.VertexShaderID>(input, this.vertexShader, "vertexShader");
			if (this.version >= 16U)
			{
				input.Position += 24;
			}
			uint variable = input.ReadUInt32();
			this.mainLight = (EmbeddedMaterial.EMainLight)variable;
			Logging_Functions.Echo(input, variable, "mainLight", true);
			this.fogMode = (EmbeddedMaterial.EFogMode)input.ReadUInt32();
			Logging_Functions.Echo<EmbeddedMaterial.EFogMode>(input, this.fogMode, "fogMode");
			if (this.version == 3U)
			{
				input.Position += 1;
				Logging_Functions.Echo("RVMat version == 3");
			}
			if (this.version >= 6U)
			{
				Logging_Functions.Echo("RVMat version >= 6");
				this.surfaceFile = input.ReadAsciiz();
				Logging_Functions.Echo<string>(input, this.surfaceFile, "surfaceFile");
			}
			if (this.version >= 4U)
			{
				Logging_Functions.Echo("RVMat version >= 4");
				this.nRenderFlags = input.ReadUInt32();
				Logging_Functions.Echo<uint>(input, this.nRenderFlags, "nRenderFlags");
				this.renderFlags = input.ReadUInt32();
				Logging_Functions.Echo<uint>(input, this.renderFlags, "renderFlags");
			}
			if (this.version > 6U)
			{
				Logging_Functions.Echo("RVMat version > 6");
				this.nStages = input.ReadUInt32();
				Logging_Functions.Echo(input, this.nStages, "nStages", true);
			}
			if (this.version > 8U)
			{
				Logging_Functions.Echo("RVMat version > 8");
				this.nTexGens = input.ReadUInt32();
				Logging_Functions.Echo<uint>(input, this.nTexGens, "nTexGens");
			}
			this.stageTextures = new StageTexture[this.nStages];
			Logging_Functions.Echo<StageTexture[]>(input, this.stageTextures, "stageTextures");
			this.stageTransforms = new StageTransform[this.nTexGens];
			Logging_Functions.Echo<StageTransform[]>(input, this.stageTransforms, "stageTransforms");
			if (this.version < 8U)
			{
				Logging_Functions.Echo("RVMat version < 8");
				int num = 0;
				while ((long)num < (long)((ulong)this.nStages))
				{
					this.stageTransforms[num] = new StageTransform(input);
					this.stageTextures[num].read(input, this.version);
					Logging_Functions.Echo<StageTexture>(input, this.stageTextures[num], "stageTextures");
					num++;
				}
			}
			else
			{
				Logging_Functions.Echo("RVMat version 8+");
				int num2 = 0;
				while ((long)num2 < (long)((ulong)this.nStages))
				{
					this.stageTextures[num2] = new StageTexture();
					this.stageTextures[num2].read(input, this.version);
					Logging_Functions.Echo<StageTexture>(input, this.stageTextures[num2], "stageTextures");
					num2++;
				}
				int num3 = 0;
				while ((long)num3 < (long)((ulong)this.nTexGens))
				{
					this.stageTransforms[num3] = new StageTransform(input);
					Logging_Functions.Echo<StageTransform>(input, this.stageTransforms[num3], "stageTransforms");
					num3++;
				}
			}
			if (this.version >= 10U)
			{
				Logging_Functions.Echo("RVMat version >= 10");
				this.stageTI.read(input, this.version);
			}
		}

		public string materialName;

		private uint version;

		private ColorP emissive;

		private ColorP ambient;

		private ColorP diffuse;

		private ColorP forcedDiffuse;

		private ColorP specular;

		private ColorP specularCopy;

		public float specularPower;

		public EmbeddedMaterial.PixelShaderID pixelShader;

		public EmbeddedMaterial.VertexShaderID vertexShader;

		private EmbeddedMaterial.EMainLight mainLight;

		private EmbeddedMaterial.EFogMode fogMode;

		public string surfaceFile;

		private uint nRenderFlags;

		private uint renderFlags;

		private uint nStages;

		private uint nTexGens;

		public StageTexture[] stageTextures;

		public StageTransform[] stageTransforms;

		private StageTexture stageTI = new StageTexture();

		private enum EFogMode
		{
			FM_None,                   /*no fog*/
			FM_Fog,                    /*fog used by usual opaque objects; the more the object is covered by fog, the closer its color is to fog color*/
			FM_Alpha,                  /*fog used by objects with alpha; the more the object is covered by fog, the more transparent it is*/
			FM_FogAlpha,               /*combination of both above approaches; used for roads (alpha-out could be quicker than fogging); can be used to fade away objects when object is not just normally fogging*/
			FM_FogSky                  /*fog for sky objects (moon, stars)*/
		}

		private enum EMainLight /*Various kinds of light calculation*/
		{
			ML_None,
			ML_Sun,
			ML_Sky,
			ML_Horizon,
			ML_Stars,
			ML_SunObject,
			ML_SunHaloObject,
			ML_MoonObject,
			ML_MoonHaloObject
		}

		public enum PixelShaderID : uint
		{
			PSNormal,                                   /*diffuse color modulate, alpha replicate*/
			PSNormalDXTA,                               /*diffuse color modulate, alpha replicate, DXT alpha correction*/
			PSNormalMap,                                /*normal map shader*/
			PSNormalMapThrough,                         /*normal map shader - through lighting*/
			PSNormalMapGrass,                           /*normal map shader - through lighting*/
			PSNormalMapDiffuse,                         /**/
			PSDetail,                                   /*detail texturing*/
			PSInterpolation,                            /**/
			PSWater,                                    /*sea water*/
			PSWaterSimple,                              /*small water*/
			PSWhite,                                    /**/
			PSWhiteAlpha,                               /**/
			PSAlphaShadow,                              /*shadow alpha write*/
			PSAlphaNoShadow,                            /*shadow alpha (no shadow write*/
			PSDummy0,                                   /**/
			PSDetailMacroAS,                            /*detail with ambient shadow texture*/
			PSNormalMapMacroAS,                         /*normal map with ambient shadow texture*/
			PSNormalMapDiffuseMacroAS,                  /*diffuse normal map with ambient shadow texture*/
			PSNormalMapSpecularMap,                     /*normal map with specular map*/
			PSNormalMapDetailSpecularMap,               /*normal map with detail and specular map*/
			PSNormalMapMacroASSpecularMap,              /*normal map with ambient shadow and specular map*/
			PSNormalMapDetailMacroASSpecularMap,        /*normal map with detail and ambient shadow and specular map*/
			PSNormalMapSpecularDIMap,                   /*normal map with specular map, diffuse is inverse of specular*/
			PSNormalMapDetailSpecularDIMap,             /*normal map with detail and specular map, diffuse is inverse of specular*/
			PSNormalMapMacroASSpecularDIMap,            /*normal map with ambient shadow and specular map, diffuse is inverse ofspecular*/
			PSNormalMapDetailMacroASSpecularDIMap,      /*normal map with detail and ambient shadow and specular map, diffuse is inverseof specular*/
			PSTerrain1,                                 /*terrain - X layers*/
			PSTerrain2,                                 /*terrain - X layers*/
			PSTerrain3,                                 /*terrain - X layers*/
			PSTerrain4,                                 /*terrain - X layers*/
			PSTerrain5,                                 /*terrain - X layers*/
			PSTerrain6,                                 /*terrain - X layers*/
			PSTerrain7,                                 /*terrain - X layers*/
			PSTerrain8,                                 /*terrain - X layers*/
			PSTerrain9,                                 /*terrain - X layers*/
			PSTerrain10,                                /*terrain - X layers*/
			PSTerrain11,                                /*terrain - X layers*/
			PSTerrain12,                                /*terrain - X layers*/
			PSTerrain13,                                /*terrain - X layers*/
			PSTerrain14,                                /*terrain - X layers*/
			PSTerrain15,                                /*terrain - X layers*/
			PSTerrainSimple1,                           /*terrainSimple - X layers*/
			PSTerrainSimple2,                           /*terrainSimple - X layers*/
			PSTerrainSimple3,                           /*terrainSimple - X layers*/
			PSTerrainSimple4,                           /*terrainSimple - X layers*/
			PSTerrainSimple5,                           /*terrainSimple - X layers*/
			PSTerrainSimple6,                           /*terrainSimple - X layers*/
			PSTerrainSimple7,                           /*terrainSimple - X layers*/
			PSTerrainSimple8,                           /*terrainSimple - X layers*/
			PSTerrainSimple9,                           /*terrainSimple - X layers*/
			PSTerrainSimple10,                          /*terrainSimple - X layers*/
			PSTerrainSimple11,                          /*terrainSimple - X layers*/
			PSTerrainSimple12,                          /*terrainSimple - X layers*/
			PSTerrainSimple13,                          /*terrainSimple - X layers*/
			PSTerrainSimple14,                          /*terrainSimple - X layers*/
			PSTerrainSimple15,                          /*terrainSimple - X layers*/
			PSGlass,                                    /*glass shader with environmental map*/
			PSNonTL,                                    /*very simple 2D pixel shader*/
			PSNormalMapSpecularThrough,                 /*normal map shader - through with specular lighting*/
			PSGrass,                                    /*grass shader - alpha discretized*/
			PSNormalMapThroughSimple,                   /*simple version of NormalMapThrough shader*/
			PSNormalMapSpecularThroughSimple,           /*simple version of NormalMapSpecularThrough shader*/
			PSRoad,                                     /*road shader*/
			PSShore,                                    /*shore shader*/
			PSShoreWet,                                 /*shore shader for the wet part*/
			PSRoad2Pass,                                /*road shader - second pass*/
			PSShoreFoam,                                /*shore shader for the foam on the top of the shore*/
			PSNonTLFlare,                               /*shader to be used for flares*/
			PSNormalMapThroughLowEnd,                   /*substitute shader for NormalMapThrough shaders for low-end settings*/
			PSTerrainGrass1,                            /*terrain grass - X layers*/
			PSTerrainGrass2,                            /*terrain grass - X layers*/
			PSTerrainGrass3,                            /*terrain grass - X layers*/
			PSTerrainGrass4,                            /*terrain grass - X layers*/
			PSTerrainGrass5,                            /*terrain grass - X layers*/
			PSTerrainGrass6,                            /*terrain grass - X layers*/
			PSTerrainGrass7,                            /*terrain grass - X layers*/
			PSTerrainGrass8,                            /*terrain grass - X layers*/
			PSTerrainGrass9,                            /*terrain grass - X layers*/
			PSTerrainGrass10,                           /*terrain grass - X layers*/
			PSTerrainGrass11,                           /*terrain grass - X layers*/
			PSTerrainGrass12,                           /*terrain grass - X layers*/
			PSTerrainGrass13,                           /*terrain grass - X layers*/
			PSTerrainGrass14,                           /*terrain grass - X layers*/
			PSTerrainGrass15,                           /*terrain grass - X layers*/
			PSCrater1,                                  /*Crater rendering - X craters*/
			PSCrater2,                                  /*Crater rendering - X craters*/
			PSCrater3,                                  /*Crater rendering - X craters*/
			PSCrater4,                                  /*Crater rendering - X craters*/
			PSCrater5,                                  /*Crater rendering - X craters*/
			PSCrater6,                                  /*Crater rendering - X craters*/
			PSCrater7,                                  /*Crater rendering - X craters*/
			PSCrater8,                                  /*Crater rendering - X craters*/
			PSCrater9,                                  /*Crater rendering - X craters*/
			PSCrater10,                                 /*Crater rendering - X craters*/
			PSCrater11,                                 /*Crater rendering - X craters*/
			PSCrater12,                                 /*Crater rendering - X craters*/
			PSCrater13,                                 /*Crater rendering - X craters*/
			PSCrater14,                                 /*Crater rendering - X craters*/
			PSSprite,                                   /*Shader used for sprite rendering - it uses SoftParticle approach*/
			PSSpriteSimple,                             /*Shader used for sprite rendering - no SoftParticle approach*/
			PSCloud,                                    /*Shader used for clouds*/
			PSHorizon,                                  /*Shader used for the horizon*/
			PSSuper,                                    /*Super shader*/
			PSMulti,                                    /*Multi shader*/
			PSTerrainX,                                 /*terrain - general number of layers*/
			PSTerrainSimpleX,                           /*terrainSimple - general number of layers*/
			PSTerrainGrassX,                            /*terrain grass - general number of layers*/
			PSTree,                                     /*Tree shader*/
			PSTreePRT,                                  /*Tree shader - very cheap shader with PRT*/
			PSTreeSimple,                               /*Tree shader - simpler version of Tree*/
			PSSkin,                                     /*Human skin - derived from Super shader*/
			PSCalmWater,                                /*calm water surface*/
			PSTreeAToC,                                 /*tree with alpha to coverage*/
			PSGrassAToC,                                /*grass with alpha to coverage*/
			PSTreeAdv,                                  /*advanced tree crown shader*/
			PSTreeAdvSimple,                            /*advanced tree crown shader*/
			PSTreeAdvTrunk,                             /*advanced tree shader*/
			PSTreeAdvTrunkSimple,                       /*advanced tree shader*/
			PSTreeAdvAToC,                              /*advanced tree crown shader*/
			PSTreeAdvSimpleAToC,                        /*advanced tree crown shader*/
			PSTreeSN,                                   /*Tree shader width simple noise*/
			PSSpriteExtTi,                              /*Sprite used for vehicles covering*/
			PSTerrainSNX,                               /*terrain - general number of layers + satellite normal map*/
			PSInterpolationAlpha,                       /**/
			PSVolCloud,                                 /*Shader used for volumetric cloud - it uses SoftParticle approach*/
			PSVolCloudSimple,                           /*Shader used for volumetric cloud - no SoftParticle approach*/
			PSUnderwaterOcclusion,                      /*Shader used for underwater occlusion object*/
			PSSimulWeatherClouds,                       /** SimulWeather clouds */
			PSSimulWeatherCloudsWithLightning,          /** SimulWeather clouds with lightning */
			PSSimulWeatherCloudsCPU,                    /** SimulWeather clouds with CPU distance fading */
			PSSimulWeatherCloudsWithLightningCPU,       /** SimulWeather clouds with lightning and CPU distance fading */
			PSSuperExt,                                 /* skyscraper & building, intended as super shader light version */
			PSSuperHair,                                /* super shader for hair rendering*/
			PSSuperHairAtoC,                            /* super shader for hair rendering, atoc version*/
			PSCaustics,                                 /* shader for caustics effect */
			PSRefract,                                  /* shader for refractions _ARMA3_REFRACTION */
			PSSpriteRefract,                            /* _ARMA3_REFRACTION_SPRITES - Shader used for sprite rendering withrefraction - it uses SoftParticle approach*/
			PSSpriteRefractSimple,                      /* _ARMA3_REFRACTION_SPRITES - Shader used for sprite rendering withrefraction- no SoftParticle approach*/
			PSSuperAToC,                                /* Super shader AToC variant */
			PSNonTLFlareNew,                            /*shader to be used for flares, new HDR version*/
			PSNonTLFlareLight,                          /*shader to be used for flares from dynamic lights (not sun)*/
			PSTerrainNoDetailX,                         /*terrainX without detail map*/
			PSTerrainNoDetailSNX,                       /*terrainSNX without detail map*/
			PSTerrainSimpleSNX,                         /*terrainSNX without parallax mapping*/
			PSNormalPiP,                                /*shader for PiP screens*/
			PSNonTLFlareNewNoOcclusion,                 /*same as NonTLFlareNew, but without occlusion test*/
			PSEmpty,                                    /*empty shader, does not output anything (used only for depth output)*/
			PSPoint,                                    /*Shader used for point lights*/
			PSTreeAdvTrans,                             /*same as TreeAdv, but there is translucency map in alpha channel of MCA texture ( instead of AO)*/
			PSTreeAdvTransAToC,                         /*same as TreeAdv, but there is translucency map in alpha channel of MCA texture ( instead of AO)*/
			PSCollimator,                               /*special shader for collimator*/
			PSLODDiag,                                  /*shader for lod diagnostics*/
			PSDepthOnly,                                /*Special replacement for AlphaOnly for priming non- alpha objects */
			NPixelShaderID,
			PSUninitialized = 4294967295U
		}
		
		public enum VertexShaderID
		{
			Basic,                           /*no extra info*/
			NormalMap,                       /*normal map*/
			NormalMapDiffuse,                /*normal map + detail map*/
			Grass,                           /**/
			Dummy1,                          /**/
			Dummy2,                          /**/
			ShadowVolume,                    /*shadow volumes*/
			Water,                           /*per-vertex water animation*/
			WaterSimple,                     /*per-vertex water animation (without foam)*/
			Sprite,                          /*particle effects*/
			Point,                           /*anti-aliased points*/
			NormalMapThrough,                /*normal map - tree shader*/
			Dummy3,                          /**/
			Terrain,                         /*one pass terrain, no alpha mask - based on VSNormalMapDiffuse*/
			BasicAS,                         /*ambient shadow*/
			NormalMapAS,                     /*normal map with ambient shadow*/
			NormalMapDiffuseAS,              /*diffuse normal map with ambient shadow*/
			Glass,                           /*glass shader*/
			NormalMapSpecularThrough,        /*normal map with specular - tree shader*/
			NormalMapThroughNoFade,          /*normal map - tree shader - without face fading*/
			NormalMapSpecularThroughNoFade,  /*normal map with specular - tree shader - without face fading*/
			Shore,                           /*sea shore - similar to Terrain*/
			TerrainGrass,                    /*grass layer - similar to Terrain*/
			Super,                           /*Super shader - expensive shader containing all common features*/
			Multi,                           /*Multi shader - shader with multiple layers suitable for huge surfaces like houses*/
			Tree,                            /*Tree shader - cheap shader designed for trees and bushes*/
			TreeNoFade,                      /*Tree shader - cheap shader designed for trees and bushes - without face fading*/
			TreePRT,                         /*Tree shader - very cheap shader designed for trees and bushes*/
			TreePRTNoFade,                   /*Tree shader - very cheap shader designed for trees and bushes - without face fading*/
			Skin,                            /*Human skin - derived from Super shader*/
			CalmWater,                       /*calm water surface - special shader*/
			TreeAdv,                         /*advanced tree crown shader  VSTreeAdv*/
			TreeAdvTrunk,                    /*advanced tree crown shader  VSTreeAdvTrunk*/
			VolCloud,                        /*volumetric clouds*/
			Road,                            /*roads*/
			UnderwaterOcclusion,             /*underwater occlusion object vertex shader*/
			SimulWeatherClouds,              /*simul weather clouds*/
			SimulWeatherCloudsCPU,           /*simul weather clouds with CPU distance fading*/
			SpriteOnSurface,                 /*sprite on surface*/
			TreeAdvModNormals,               /*advanced tree crown shader  with modified vertex normals*/
			Refract,                         /*vertex shader for refractions - _ARMA3_REFRACTION*/
			SimulWeatherCloudsGS,            /*simul weather clouds with geom shader*/
			BasicFade,                       /*basic with face fading (based on the angle with camera direction */
			Star,                            /*Similar to Point but only for drawing stars */
			TreeAdvNoFade,                   /*advanced tree crown shader - no face fading*/
			NVertexShaderID
		}

		/*
		public enum PixelShaderID : uint
		{
			PSNormal,
			PSNormalDXTA,
			PSNormalMap,
			PSNormalMapThrough,
			PSNormalMapGrass,
			PSNormalMapDiffuse,
			PSDetail,
			PSInterpolation,
			PSWater,
			PSWaterSimple,
			PSWhite,
			PSWhiteAlpha,
			PSAlphaShadow,
			PSAlphaNoShadow,
			PSDummy0,
			PSDetailMacroAS,
			PSNormalMapMacroAS,
			PSNormalMapDiffuseMacroAS,
			PSNormalMapSpecularMap,
			PSNormalMapDetailSpecularMap,
			PSNormalMapMacroASSpecularMap,
			PSNormalMapDetailMacroASSpecularMap,
			PSNormalMapSpecularDIMap,
			PSNormalMapDetailSpecularDIMap,
			PSNormalMapMacroASSpecularDIMap,
			PSNormalMapDetailMacroASSpecularDIMap,
			PSTerrain1,
			PSTerrain2,
			PSTerrain3,
			PSTerrain4,
			PSTerrain5,
			PSTerrain6,
			PSTerrain7,
			PSTerrain8,
			PSTerrain9,
			PSTerrain10,
			PSTerrain11,
			PSTerrain12,
			PSTerrain13,
			PSTerrain14,
			PSTerrain15,
			PSTerrainSimple1,
			PSTerrainSimple2,
			PSTerrainSimple3,
			PSTerrainSimple4,
			PSTerrainSimple5,
			PSTerrainSimple6,
			PSTerrainSimple7,
			PSTerrainSimple8,
			PSTerrainSimple9,
			PSTerrainSimple10,
			PSTerrainSimple11,
			PSTerrainSimple12,
			PSTerrainSimple13,
			PSTerrainSimple14,
			PSTerrainSimple15,
			PSGlass,
			PSNonTL,
			PSNormalMapSpecularThrough,
			PSGrass,
			PSNormalMapThroughSimple,
			PSNormalMapSpecularThroughSimple,
			PSRoad,
			PSShore,
			PSShoreWet,
			PSRoad2Pass,
			PSShoreFoam,
			PSNonTLFlare,
			PSNormalMapThroughLowEnd,
			PSTerrainGrass1,
			PSTerrainGrass2,
			PSTerrainGrass3,
			PSTerrainGrass4,
			PSTerrainGrass5,
			PSTerrainGrass6,
			PSTerrainGrass7,
			PSTerrainGrass8,
			PSTerrainGrass9,
			PSTerrainGrass10,
			PSTerrainGrass11,
			PSTerrainGrass12,
			PSTerrainGrass13,
			PSTerrainGrass14,
			PSTerrainGrass15,
			PSCrater1,
			PSCrater2,
			PSCrater3,
			PSCrater4,
			PSCrater5,
			PSCrater6,
			PSCrater7,
			PSCrater8,
			PSCrater9,
			PSCrater10,
			PSCrater11,
			PSCrater12,
			PSCrater13,
			PSCrater14,
			PSSprite,
			PSSpriteSimple,
			PSCloud,
			PSHorizon,
			PSSuper,
			PSMulti,
			PSTerrainX,
			PSTerrainSimpleX,
			PSTerrainGrassX,
			PSTree,
			PSTreePRT,
			PSTreeSimple,
			PSSkin,
			PSCalmWater,
			PSTreeAToC,
			PSGrassAToC,
			PSTreeAdv,
			PSTreeAdvSimple,
			PSTreeAdvTrunk,
			PSTreeAdvTrunkSimple,
			PSTreeAdvAToC,
			PSTreeAdvSimpleAToC,
			PSTreeSN,
			PSSpriteExtTi,
			PSTerrainSNX,
			PSSimulWeatherClouds,
			PSSimulWeatherCloudsWithLightning,
			PSSimulWeatherCloudsCPU,
			PSSimulWeatherCloudsWithLightningCPU,
			PSSuperExt,
			PSSuperAToC,
			NPixelShaderID,
			PSNone = 129U,
			PSUninitialized = 4294967295U
		}

		public enum VertexShaderID
		{
			VSBasic,
			VSNormalMap,
			VSNormalMapDiffuse,
			VSGrass,
			VSDummy1,
			VSDummy2,
			VSShadowVolume,
			VSWater,
			VSWaterSimple,
			VSSprite,
			VSPoint,
			VSNormalMapThrough,
			VSDummy3,
			VSTerrain,
			VSBasicAS,
			VSNormalMapAS,
			VSNormalMapDiffuseAS,
			VSGlass,
			VSNormalMapSpecularThrough,
			VSNormalMapThroughNoFade,
			VSNormalMapSpecularThroughNoFade,
			VSShore,
			VSTerrainGrass,
			VSSuper,
			VSMulti,
			VSTree,
			VSTreeNoFade,
			VSTreePRT,
			VSTreePRTNoFade,
			VSSkin,
			VSCalmWater,
			VSTreeAdv,
			VSTreeAdvTrunk,
			VSSimulWeatherClouds,
			VSSimulWeatherCloudsCPU,
			NVertexShaderID
		}
		*/
	}
}
