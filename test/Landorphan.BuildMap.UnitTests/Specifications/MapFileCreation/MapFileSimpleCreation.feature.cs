﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.4.0.0
//      SpecFlow Generator Version:3.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Landorphan.BuildMap.UnitTests.Specifications.MapFileCreation
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Map File Creation")]
    [NUnit.Framework.CategoryAttribute("Check-In")]
    public partial class MapFileCreationFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "Check-In"};
        
#line 1 "MapFileSimpleCreation.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Specifications/MapFileCreation", "Map File Creation", "\tIn order to build complicated project structures\r\n\tAs a developer\r\n\tI want to to" +
                    " be able to create a mapping of all projects, their dependencies and their build" +
                    " order.", ProgrammingLanguage.CSharp, new string[] {
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
        [NUnit.Framework.DescriptionAttribute("Build a Simple Project")]
        public virtual void BuildASimpleProject()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Build a Simple Project", null, tagsOfScenario, argumentsOfScenario);
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
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "Language"});
                table1.AddRow(new string[] {
                            "Project1",
                            "CSharp"});
                table1.AddRow(new string[] {
                            "Project2",
                            "CSharp"});
#line 8
 testRunner.Given("I locate the following project files:", ((string)(null)), table1, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table2.AddRow(new string[] {
                            "Solution1"});
#line 12
      testRunner.And("I locate the following solution files:", ((string)(null)), table2, "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Solution",
                            "Project"});
                table3.AddRow(new string[] {
                            "Solution1",
                            "Project1"});
                table3.AddRow(new string[] {
                            "Solution1",
                            "Project2"});
#line 15
   testRunner.And("the following solutions contain the following located projects:", ((string)(null)), table3, "And ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "Project",
                            "Reference"});
                table4.AddRow(new string[] {
                            "Project2",
                            "Project1"});
#line 19
   testRunner.And("the following projects contain the following references:", ((string)(null)), table4, "And ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Solution",
                            "Base Project",
                            "Dependent On"});
                table5.AddRow(new string[] {
                            "Solution1",
                            "Project2",
                            "Project1"});
#line 22
   testRunner.And("the following solutions define the following additional dependencies:", ((string)(null)), table5, "And ");
#line hidden
#line 25
      testRunner.And("the projects and solutions are saved on disk", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
  testRunner.When("I create the map file with the following search patterns: **/*.sln;**/*.csproj", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Group",
                            "Item",
                            "Types",
                            "Language",
                            "Name",
                            "Status",
                            "Solutions",
                            "Id",
                            "Relative Path",
                            "Dependent On"});
                table6.AddRow(new string[] {
                            "0",
                            "0",
                            "Library",
                            "CSharp",
                            "Project1",
                            "Valid",
                            "Solution1",
                            "1",
                            "Project1`Project1.csproj",
                            ""});
                table6.AddRow(new string[] {
                            "0",
                            "0",
                            "Library",
                            "CSharp",
                            "Project2",
                            "Valid",
                            "Solution1",
                            "2",
                            "Project2`Project2.csproj",
                            "1"});
#line 27
  testRunner.Then("the map file should contain the following projects:", ((string)(null)), table6, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Build a Project with multiple languages")]
        public virtual void BuildAProjectWithMultipleLanguages()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Build a Project with multiple languages", null, tagsOfScenario, argumentsOfScenario);
#line 32
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
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "Language"});
                table7.AddRow(new string[] {
                            "Project1",
                            "CSharp"});
                table7.AddRow(new string[] {
                            "Project2",
                            "FSharp"});
                table7.AddRow(new string[] {
                            "Project3",
                            "VisualBasic"});
#line 33
 testRunner.Given("I locate the following project files:", ((string)(null)), table7, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name"});
                table8.AddRow(new string[] {
                            "Solution1"});
#line 38
      testRunner.And("I locate the following solution files:", ((string)(null)), table8, "And ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "Solution",
                            "Project"});
                table9.AddRow(new string[] {
                            "Solution1",
                            "Project1"});
                table9.AddRow(new string[] {
                            "Solution1",
                            "Project2"});
                table9.AddRow(new string[] {
                            "Solution1",
                            "Project3"});
#line 41
   testRunner.And("the following solutions contain the following located projects:", ((string)(null)), table9, "And ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "Project",
                            "Reference"});
                table10.AddRow(new string[] {
                            "Project2",
                            "Project1"});
                table10.AddRow(new string[] {
                            "Project3",
                            "Project1"});
#line 46
   testRunner.And("the following projects contain the following references:", ((string)(null)), table10, "And ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "Solution",
                            "Base Project",
                            "Dependent On"});
                table11.AddRow(new string[] {
                            "Solution1",
                            "Project3",
                            "Project2"});
#line 50
   testRunner.And("the following solutions define the following additional dependencies:", ((string)(null)), table11, "And ");
#line hidden
#line 53
      testRunner.And("the projects and solutions are saved on disk", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
  testRunner.When("I create the map file with the following search patterns: **/*.sln;**/*.csproj;**" +
                        "/*.vbproj;**/*.fsproj", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "Group",
                            "Item",
                            "Types",
                            "Language",
                            "Name",
                            "Status",
                            "Solutions",
                            "Id",
                            "Relative Path",
                            "Dependent On"});
                table12.AddRow(new string[] {
                            "0",
                            "0",
                            "Library",
                            "CSharp",
                            "Project1",
                            "Valid",
                            "Solution1",
                            "1",
                            "Project1`Project1.csproj",
                            ""});
                table12.AddRow(new string[] {
                            "0",
                            "0",
                            "Library",
                            "FSharp",
                            "Project2",
                            "Valid",
                            "Solution1",
                            "2",
                            "Project2`Project2.fsproj",
                            "1"});
                table12.AddRow(new string[] {
                            "0",
                            "0",
                            "Library",
                            "VisualBasic",
                            "Project3",
                            "Valid",
                            "Solution1",
                            "3",
                            "Project3`Project3.vbproj",
                            "1,2"});
#line 55
  testRunner.Then("the map file should contain the following projects:", ((string)(null)), table12, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
