
public class CONST_PATH_FILE_NAME_ASSET_BUNDLE
{
    public const string ab = "https://raw.githubusercontent.com/HoDienCong12c5/serverBundle/main/ab";
    public const string objectbundle = "https://raw.githubusercontent.com/Hungduc123/AssetBundle/master/objectbundle";
    public static string GetUrl(string name)
    {

        switch (name)
        {
            case "ab":
                return ab;
            case "objectbundle":
                return objectbundle;
            default:
                return "";
        }

    }


}
