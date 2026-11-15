namespace JsonGeneratorApp.Core.Models;

public sealed class Record
{
    public int UserId { get; set; }
    
    public string MaskedPan { get; set; }

    public DateTime ExpDate { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Phone { get; set; }

    /// <summary>
    /// Helper method to mask card number
    /// </summary>
    public static string MaskPan(long pan)
    {
        string panStr = pan.ToString();
        if (panStr.Length < 10)
            return new string('*', panStr.Length);

        string firstSix = panStr.Substring(0, 6);
        string lastFour = panStr.Substring(panStr.Length - 4);
        string middleMask = new('*', panStr.Length - 10);

        return $"{firstSix}{middleMask}{lastFour}";
    }
}