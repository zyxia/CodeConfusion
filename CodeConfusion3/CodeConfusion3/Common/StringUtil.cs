namespace CodeConfusion3.Common
{
    public static class StringUtil
    {
        public static int rCharCount(ref string textTemp, int textTempLength, char c)
        {
            throw new System.NotImplementedException();
        }

        public static bool isInterger(in string tempText)
        {
            return int.TryParse(tempText, out var v);
        }
    }
}