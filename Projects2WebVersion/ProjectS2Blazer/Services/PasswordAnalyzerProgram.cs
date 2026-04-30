using EntropyNamespace;
using BruteForceAttack;
using DicAttack;
using HybAttack;
using RuleBasedAttack;
using PassGenerator;
using StrengthAnalysis;
using ProjectS2Blazer.Services;
using System.Runtime.CompilerServices;

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

        var entropy = Entropy.CalculateEntropy(password);
        var brute = BruteForce.BruteForceAttackTime(password);
        var dictTime = new DictionaryAttack().DictAttack(password, dictionary);
        var hybrid = new HybridAttack().HybrAttack(password, dictionary);
        var rule = new RuleBased().RuleBasedAttackTime(password, dictionary);
        var ruleProb = RuleBased.AnalyzePasswordProblems(password);

        return new AnalysisResult
        {
            Entropy = entropy,
            BruteForceTime = brute,
            DictionaryTime = dictTime,
            HybridTime = hybrid,
            RuleBasedTime = rule,
            RuleBasedProb = ruleProb,
            Evaluation = PasswordStrengthEvaluator.Evaluate(entropy, brute, dictTime, hybrid, rule) 
        };
    }
    
    public string GenPassword()
    {
        string GenPass;

        GenPass = Generator.GenPass();

        return GenPass; 
    }
}

public class AnalysisResult
{
    public double Entropy { get; set; }
    public double BruteForceTime { get; set; }
    public double DictionaryTime { get; set; }
    public double HybridTime { get; set; }
    public double RuleBasedTime { get; set; }
    public string Evaluation {get; set; }
    public List<string> RuleBasedProb{get; set; }
}
