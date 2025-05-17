using CommandLine;

public class Options {
    public const string SourceDirectoryName = "sourcedictionary";
    public const string TargetDirectoryName = "targetdictionary";

    [Option('s', SourceDirectoryName, Required = false, HelpText = "Sets the relative source dir.")]
    public string? SourceDirectory { get; set; } = "Files";

    [Option('t', TargetDirectoryName, Required = false, HelpText = "Sets the relative dir for all results.")]
    public string? TargetDirectory { get; set; } = "Results";
}