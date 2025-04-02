namespace JsonConstantsGenerator.Attributes;

/// <summary>
/// 
/// </summary>
public class GenerateConstantsFromList : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public string? FileName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? ClassName { get; set; }
}