using EntropyNamespace;
using BruteForceAttack;
using DicAttack;
using HybAttack;
using RuleBasedAttack;
using ProjectS2Blazer.Services;

namespace ProjectS2Blazer.Services;

public class PasswordAnalyzer
{
    private readonly PasswordDictionaryService _dict;
    public PasswordAnalyzer(PasswordDictionaryService dict)
    {
        _dict = dict;
    }
    public AnalysisResult Analyze(string password)
    {
        var dictionary = _dict.GetAll();

        return new AnalysisResult
        {
            Entropy = Entropy.CalculateEntropy(password),
            BruteForceTime = BruteForce.BruteForceAttackTime(password),
            DictionaryTime = new DictionaryAttack().DictAttack(password, dictionary),
            HybridTime = new HybridAttack().HybrAttack(password, dictionary),
            RuleBasedTime = new RuleBased().RuleBasedAttackTime(password, dictionary)
        };
    }
}

public class AnalysisResult
{
    public double Entropy { get; set; }
    public double BruteForceTime { get; set; }
    public double DictionaryTime { get; set; }
    public double HybridTime { get; set; }
    public double RuleBasedTime { get; set; }
}