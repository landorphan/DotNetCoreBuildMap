using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProjectOrder.Model;

namespace ProjectOrder.Parsers
{
   public enum State
   {
      Start,
      InProject,
      InProjectDependencies,
   }

   public enum LineType
   {
      Ignore,
      StartProject,
      EndProject,
      StartDependencyList,
      EndDependencyList,
      DependencyLine
   }

   public class ParseSolutionFile
   {
      private VisualStudioSolutionFile solutionFile;

      public ParseSolutionFile(string solutionFilePath)
      {
         this.solutionFile = new VisualStudioSolutionFile();
         this.solutionFile.Path = solutionFilePath;
         using (var stream = File.OpenRead(solutionFilePath))
         using (var reader = new StreamReader(stream))
         {
            while (!reader.EndOfStream)
            {
               var line = reader.ReadLine();
               this.solutionFile.RawFileText.Add(line);
            }
         }
      }

      private LineType DetermineLineTyeType(string line)
      {
         var retval = LineType.Ignore;
         return retval;
      }

      void ProcLine(string line)
      {

      }

      public VisualStudioSolutionFile Parse()
      {
         foreach(var line in solutionFile.RawFileText)
         {

         }
         return solutionFile;
      }
   }
}
