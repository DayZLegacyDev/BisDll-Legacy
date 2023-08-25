using BisDll.Model;
using BisDll.Model.MLOD;
using BisDll.Model.ODOL;

namespace odolout;

class Program
{
	public static bool extractModelCfg = true;

	static void Main(string[] args)
	{
		var path = args[0];
		var dst = path.Replace(".p3d", "mlod.p3d");
		if ( !File.Exists(path) ) throw new Exception();
		if ( Path.GetExtension(path) != ".p3d" ) throw new Exception();
		if ( File.Exists(dst) ) File.Delete(dst);

		ConvertP3D(path, dst);
	}

	public static bool ConvertP3D( string srcFile, string destFile )
	{
		Console.WriteLine( string.Format( "Reading the P3D ('{0}')...", srcFile ) );
		P3D instance = P3D.GetInstance( srcFile );
		if (instance is MLOD)
		{
			Console.WriteLine( string.Format( "'{0}' is already in editable MLOD format", srcFile ) );
		}
		else
		{
			ODOL odol = instance as ODOL;
			if (odol != null)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension( srcFile );
				Console.WriteLine( string.Format( "'{0}': ODOL was loaded successfully.", fileNameWithoutExtension ) );

				Console.WriteLine( string.Format( "'{0}': Start conversion...", fileNameWithoutExtension ) );
				MLOD mlod = Conversion.ODOL2MLOD( odol );
				Console.WriteLine( string.Format( "'{0}': Conversion successful.", fileNameWithoutExtension ) );

				Console.WriteLine( string.Format( "'{0}': Saving as: '{1}'", fileNameWithoutExtension, Path.GetFileName( destFile ) ) );
				mlod.WriteToFile( destFile, true );

				if (extractModelCfg)
				{
					Console.WriteLine( string.Format( "'{0}': Extracting model.cfg...", fileNameWithoutExtension ) );
					FileInfo fiDestP3d = new FileInfo( destFile );
					FileInfo fiDestModelCfg = new FileInfo( fiDestP3d.Directory.FullName + "\\" + "model.cfg" );
					string toWrite = "";

					if (fiDestModelCfg.Exists)
					{
						toWrite = odol.CombineModelCfg( File.ReadAllLines( fiDestModelCfg.FullName ) );
					}
					else
					{
						toWrite = odol.GetModelCfg();
					}

					File.WriteAllText( fiDestModelCfg.FullName, toWrite );
				}

				return true;
			}
			Console.WriteLine( string.Format( "'{0}' could not be loaded.", srcFile ) );
		}
		return false;
	}
}