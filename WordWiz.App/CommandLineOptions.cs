using CommandLine;

public class Options {
    public const string SourceDictionaryName = "sourcedictionary";
    public const string TargetDictionaryName = "targetdictionary";

    [Option('s', SourceDictionaryName, Required = false, HelpText = "Sets the relative source dir.")]
    public string? SourceDictionary { get; set; } = "Files";

    [Option('t', TargetDictionaryName, Required = false, HelpText = "Sets the relative dir for all results.")]
    public string? TargetDictionary { get; set; } = "Results";
}