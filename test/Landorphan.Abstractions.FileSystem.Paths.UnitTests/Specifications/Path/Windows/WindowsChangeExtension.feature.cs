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
    [NUnit.Framework.DescriptionAttribute("Windows Change Extension feature")]
    public partial class WindowsChangeExtensionFeatureFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "WindowsChangeExtension.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Windows Change Extension feature", "\tIn order to reliably interact with the file systems of multiple platforms\r\n\tAs a" +
                    " developer\r\n\tI want to change the extension on a ptah.", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Change Extension Safe Execute (no Exceptions)")]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", ".json", "`foo.json", "Legal", "false", ".json", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", "json", "`foo.json", "Legal", "false", ".json", "simple change", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", "(null)", "`foo", "Legal", "false", "(empty)", "Null becomes empty string", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", "(empty)", "`foo", "Legal", "false", "(empty)", "Empty string means no extension", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", ".", "`foo", "Legal", "false", "(empty)", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", "json.txt", "`foo.json.txt", "Legal", "false", ".txt", "only the last part of the new extension is an extension", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", ".json.txt.", "`foo.json.txt.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", "..", "`foo..", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", ".json.", "`foo.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", "json.", "`foo.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.txt", "json%20", "`foo.json%20", "Illegal", "false", ".json%20", "Trailing \' \' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", ".json", "`foo.bar.json", "Legal", "false", ".json", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", "json", "`foo.bar.json", "Legal", "false", ".json", "simple change", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", "(null)", "`foo.bar", "Legal", "false", ".bar", "Null becomes empty string", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", "(empty)", "`foo.bar", "Legal", "false", ".bar", "Empty string means no extension", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", ".", "`foo.bar", "Legal", "false", ".bar", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", "json.txt", "`foo.bar.json.txt", "Legal", "false", ".txt", "only the last part of the new extension is an extension", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", ".json.txt.", "`foo.bar.json.txt.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", "..", "`foo.bar..", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", ".json.", "`foo.bar.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", "json.", "`foo.bar.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/foo.bar.txt", "json%20", "`foo.bar.json%20", "Illegal", "false", ".json%20", "Trailing \' \' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", "json", "`.gitignore.json", "Legal", "false", ".json", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", ".json", "`.gitignore.json", "Legal", "false", ".json", "simple change", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", "(null)", "`.gitignore", "Legal", "false", "(empty)", "Null becomes empty string", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", "(empty)", "`.gitignore", "Legal", "false", "(empty)", "Empty string means no extension", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", ".", "`.gitignore", "Legal", "false", "(empty)", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", "json.txt", "`.gitignore.json.txt", "Legal", "false", ".txt", "only the last part of the new extension is an extension", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", ".json.txt.", "`.gitignore.json.txt.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", "..", "`.gitignore..", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", ".json.", "`.gitignore.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", "json.", "`.gitignore.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore", "json%20", "`.gitignore.json%20", "Illegal", "false", ".json%20", "Trailing \' \' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", "json", "`.gitignore.json", "Legal", "false", ".json", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", ".json", "`.gitignore.json", "Legal", "false", ".json", "simple change", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", "(null)", "`.gitignore", "Legal", "false", "(empty)", "Null becomes empty string", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", "(empty)", "`.gitignore", "Legal", "false", "(empty)", "Empty string means no extension", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", ".", "`.gitignore", "Legal", "false", "(empty)", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", "json.txt", "`.gitignore.json.txt", "Legal", "false", ".txt", "only the last part of the new extension is an extension", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", ".json.txt.", "`.gitignore.json.txt.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", "..", "`.gitignore..", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", ".json.", "`.gitignore.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", "json.", "`.gitignore.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/.gitignore.txt", "json%20", "`.gitignore.json%20", "Illegal", "false", ".json%20", "Trailing \' \' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/file", "json", "`file.json", "Legal", "false", ".json", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/file", ".json", "`file.json", "Legal", "false", ".json", "simple change", null)]
        [NUnit.Framework.TestCaseAttribute("/file", "(null)", "`file", "Legal", "false", "(empty)", "Null becomes empty string", null)]
        [NUnit.Framework.TestCaseAttribute("/file", "(empty)", "`file", "Legal", "false", "(empty)", "Empty string means no extension", null)]
        [NUnit.Framework.TestCaseAttribute("/file", ".", "`file", "Legal", "false", "(empty)", "The leading period in the extension is ignored", null)]
        [NUnit.Framework.TestCaseAttribute("/file", "json.txt", "`file.json.txt", "Legal", "false", ".txt", "only the last part of the new extension is an extension", null)]
        [NUnit.Framework.TestCaseAttribute("/file", ".json.txt.", "`file.json.txt.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/file", "..", "`file..", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/file", ".json.", "`file.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/file", "json.", "`file.json.", "Illegal", "false", "(empty)", "Trailing \'.\' are illegal on Windows", null)]
        [NUnit.Framework.TestCaseAttribute("/file", "json%20", "`file.json%20", "Illegal", "false", ".json%20", "Trailing \' \' are illegal on Windows", null)]
        public virtual void ChangeExtensionSafeExecuteNoExceptions(string path, string newExtension, string result, string newPathStatus, string isDiscouraged, string resultingExtension, string notes, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Path", path);
            argumentsOfScenario.Add("New Extension", newExtension);
            argumentsOfScenario.Add("Result", result);
            argumentsOfScenario.Add("New Path Status", newPathStatus);
            argumentsOfScenario.Add("Is Discouraged", isDiscouraged);
            argumentsOfScenario.Add("Resulting Extension", resultingExtension);
            argumentsOfScenario.Add("Notes", notes);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Change Extension Safe Execute (no Exceptions)", null, tagsOfScenario, argumentsOfScenario);
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
  testRunner.When("I parse the path", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
   testRunner.And(string.Format("I change the path\'s extension to: {0}", newExtension), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 11
     testRunner.Then(string.Format("the resulting status should be {0}", newPathStatus), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 12
   testRunner.And(string.Format("the resulting path\'s IsDiscouraged property should be {0}", isDiscouraged), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 13
   testRunner.And(string.Format("the result should be: {0}", result), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 14
     testRunner.And(string.Format("the resulting path\'s Extension should be: {0}", resultingExtension), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
