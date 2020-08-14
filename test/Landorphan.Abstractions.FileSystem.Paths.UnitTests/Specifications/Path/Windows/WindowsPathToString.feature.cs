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
    [NUnit.Framework.DescriptionAttribute("Windows Path ToString")]
    public partial class WindowsPathToStringFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "WindowsPathToString.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Windows Path ToString", "\tIn order to develop a reliable Windows Path parser \r\n\tAs a member of the Landorp" +
                    "han Team\r\n\tI want to to be able to convert incoming paths objects into a readabl" +
                    "e string", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Windows Paths can be converted back to the correct string")]
        [NUnit.Framework.TestCaseAttribute("(null)", "(empty)", "Null Paths turn into empty paths", null)]
        [NUnit.Framework.TestCaseAttribute("(empty)", "(empty)", "Empty", null)]
        [NUnit.Framework.TestCaseAttribute(".", ".", "Self Segment", null)]
        [NUnit.Framework.TestCaseAttribute("..", "..", "Parent Segment", null)]
        [NUnit.Framework.TestCaseAttribute("C:`", "C:`", "Volume Root Segment", null)]
        [NUnit.Framework.TestCaseAttribute("C:`foo", "C:`foo", "Volume Root Segment", null)]
        [NUnit.Framework.TestCaseAttribute("..`", "..`", "Parent + Empty", null)]
        [NUnit.Framework.TestCaseAttribute(".`", ".`", "Parent + Self", null)]
        [NUnit.Framework.TestCaseAttribute(".`file.txt", ".`file.txt", "Self + File", null)]
        [NUnit.Framework.TestCaseAttribute("C:foo.txt", "C:foo.txt", "Volume Relative + File", null)]
        [NUnit.Framework.TestCaseAttribute("c:bar`foo.txt", "c:bar`foo.txt", "Volume Relative + Dir + File", null)]
        [NUnit.Framework.TestCaseAttribute("``server`share`file.txt", "``server`share`file.txt", "UNC Server + Share + File", null)]
        [NUnit.Framework.TestCaseAttribute("`dir``dir``file.txt", "`dir``dir``file.txt", "Empty paths are kept unless normalized", null)]
        [NUnit.Framework.TestCaseAttribute("C:`dir`file.txt`", "C:`dir`file.txt`", "Trailing slashs are kept unless normalized", null)]
        [NUnit.Framework.TestCaseAttribute("`dir`dir`..`dir`..`..`file.txt", "`dir`dir`..`dir`..`..`file.txt", "Embedded Parent Segemnts", null)]
        [NUnit.Framework.TestCaseAttribute("`dir`dir`.`dir`.`dir`file.txt", "`dir`dir`.`dir`.`dir`file.txt", "Embedded Self Segments", null)]
        [NUnit.Framework.TestCaseAttribute("/", "`", "Root Path (note the change of slash types)", null)]
        public virtual void WindowsPathsCanBeConvertedBackToTheCorrectString(string path, string result, string notes, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Path", path);
            argumentsOfScenario.Add("Result", result);
            argumentsOfScenario.Add("Notes", notes);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Windows Paths can be converted back to the correct string", null, tagsOfScenario, argumentsOfScenario);
#line 6
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
#line 7
 testRunner.Given(string.Format("I have the following path: {0}", path), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 8
   testRunner.And("I\'m running on the following Operating System: Windows", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 9
      testRunner.And("I parse the path", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
  testRunner.When("I ask for the path to be represented as a string", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
  testRunner.Then("no exception should be thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 12
   testRunner.And(string.Format("I should receive the following string: {0}", result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
