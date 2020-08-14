﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.3.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Landorphan.Abstractions.FileSystem.Paths.UnitTests.Specifications.Path.Windows
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Windows Path Normalization Level")]
    [NUnit.Framework.CategoryAttribute("Check-In")]
    public partial class WindowsPathNormalizationLevelFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "Check-In"};
        
#line 1 "WindowsPathNormalizationLevel.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Windows Path Normalization Level", "\tIn order to develop a reliable Windows Path parser \r\n\tAs a member of the Landorp" +
                    "han Team\r\n\tI want to to be able to convert incoming paths into a more managable " +
                    "form", ProgrammingLanguage.CSharp, new string[] {
                        "Check-In"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Windows Paths can be normalized to best available form.")]
        [NUnit.Framework.TestCaseAttribute("(null)", "NotNormalized", "Imposible for Null path to normalize", null)]
        [NUnit.Framework.TestCaseAttribute("(empty)", "NotNormalized", "Imposible for Empty path to normalize", null)]
        [NUnit.Framework.TestCaseAttribute("C:/", "NotNormalized", "The trailing slash adds an empty segment thus it\'s not normalized", null)]
        [NUnit.Framework.TestCaseAttribute("/", "Fully", "A root only segment should be created and this path is normalized", null)]
        [NUnit.Framework.TestCaseAttribute("/foo/bar", "Fully", "This path only contains root and generic segments", null)]
        [NUnit.Framework.TestCaseAttribute("foo/bar", "Fully", "This path is relative but fully normalized", null)]
        [NUnit.Framework.TestCaseAttribute("foo/../bar", "NotNormalized", "This path is not normal because of the parent segment but can be fully normalized" +
            "", null)]
        [NUnit.Framework.TestCaseAttribute("../foo/bar", "LeadingParentsOnly", "This path is as normalized as it can be ... leading parent segements can\'t be rem" +
            "oved", null)]
        [NUnit.Framework.TestCaseAttribute("C:/dir/file.txt", "Fully", "This path is fully normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:/dir/file.txt/", "NotNormalized", "The trailing slash adds an empty segement thus it\'s not normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:/dir", "Fully", "This path is fully normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:/dir/", "NotNormalized", "The trailing slash adds an empty segement thus it\'s not normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:./file.txt", "NotNormalized", "This is a Windows path so the \'.\' is a self reference ... thus resulting in NotNo" +
            "rmalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:./file.txt/", "NotNormalized", "The self reference and the trailing slash prevent this from being normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:file.txt", "Fully", "While not relative this path is still normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:file.txt/", "NotNormalized", "Trailing Slash prevents normalization", null)]
        [NUnit.Framework.TestCaseAttribute("C:dir", "Fully", "Fully normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:dir/", "NotNormalized", "Trailing slash prevents normalziaiton", null)]
        [NUnit.Framework.TestCaseAttribute("C:dir/file.txt", "Fully", "Fully normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:dir/file.txt/", "NotNormalized", "Trailing Slash", null)]
        [NUnit.Framework.TestCaseAttribute("//server/share", "Fully", "Fully normalized remote path", null)]
        [NUnit.Framework.TestCaseAttribute("//server/share/", "NotNormalized", "Trailing Slash", null)]
        [NUnit.Framework.TestCaseAttribute("//server/file.txt", "Fully", "Fully normalized remote path", null)]
        [NUnit.Framework.TestCaseAttribute("//server/file.txt/", "NotNormalized", "Trailing Slash", null)]
        [NUnit.Framework.TestCaseAttribute("//server/share/dir/file.txt", "Fully", "Fully normalized remote path", null)]
        [NUnit.Framework.TestCaseAttribute("//server/share/dir/file.txt/", "NotNormalized", "Trailing Slash", null)]
        [NUnit.Framework.TestCaseAttribute(".", "SelfReferenceOnly", "This is normalized as best as posilbe, its a special case where the path only has" +
            " a self reference", null)]
        [NUnit.Framework.TestCaseAttribute("./", "NotNormalized", "Self Reference ant Trailing Slash", null)]
        [NUnit.Framework.TestCaseAttribute("./file.txt", "NotNormalized", "The self reference could be removed in this case", null)]
        [NUnit.Framework.TestCaseAttribute("./file.txt/", "NotNormalized", "The Self reference and the trailing path", null)]
        [NUnit.Framework.TestCaseAttribute("./dir", "NotNormalized", "The self reference", null)]
        [NUnit.Framework.TestCaseAttribute("./dir/", "NotNormalized", "The self reference and the trailing slash", null)]
        [NUnit.Framework.TestCaseAttribute("./dir/file.txt", "NotNormalized", "The self reference", null)]
        [NUnit.Framework.TestCaseAttribute("./dir/file.txt/", "NotNormalized", "The self refrence and the trialing slash", null)]
        [NUnit.Framework.TestCaseAttribute("..", "LeadingParentsOnly", "Only has a leading parent ... best posible normalization", null)]
        [NUnit.Framework.TestCaseAttribute("../", "NotNormalized", "Trailing slash", null)]
        [NUnit.Framework.TestCaseAttribute("../dir/file.txt", "LeadingParentsOnly", "Only has leading parents ... best posible normalization", null)]
        [NUnit.Framework.TestCaseAttribute("../dir/file.txt/", "NotNormalized", "Trailing slash", null)]
        [NUnit.Framework.TestCaseAttribute("a/b/./c/../../d/../../../e", "NotNormalized", "To many resons to mention", null)]
        [NUnit.Framework.TestCaseAttribute("a/b/./c/../d/../../e", "NotNormalized", "To many resons to mention", null)]
        public virtual void WindowsPathsCanBeNormalizedToBestAvailableForm_(string path, string normalizationLevel, string notes, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Path", path);
            argumentsOfScenario.Add("Normalization Level", normalizationLevel);
            argumentsOfScenario.Add("Notes", notes);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Windows Paths can be normalized to best available form.", null, tagsOfScenario, argumentsOfScenario);
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
 testRunner.Given(string.Format("I have the following path: {0}", path), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
   testRunner.And("I\'m running on the following Operating System: Windows", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
     testRunner.When("I parse the path", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
  testRunner.Then(string.Format("the resulting path should have the following Normalization Level: {0}", normalizationLevel), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
