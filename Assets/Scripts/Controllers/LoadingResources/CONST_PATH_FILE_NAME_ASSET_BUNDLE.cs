
public class CONST_PATH_FILE_NAME_ASSET_BUNDLE
{
    public const string ab = "https://raw.githubusercontent.com/HoDienCong12c5/serverBundle/main/ab";
    public const string ac = "https://raw.githubusercontent.com/HoDienCong12c5/serverBundle/main/ac";

    private string name;

    public static string GetUrl(string name)
    {

        switch (name)
        {
            case "ab":
                return CONST_PATH_FILE_NAME_ASSET_BUNDLE.ab;
            case "ac":
                return CONST_PATH_FILE_NAME_ASSET_BUNDLE.ac;
            default:
                return "";
        }

    }


}
