using uk.org.hs2.genericutils;
using logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections;
using System.Text.RegularExpressions;

namespace actionengine.actions
{
    /// <summary>
    /// AUTHOR      : Des McCarter @ BJSS
    /// DATE        : 26/04/2017
    /// DESCRIPTION : 
    /// 
    /// A factory of actions that can be executed from any BDD statement (or code). 
    /// 
    /// All actions currently exist within actions.xml 
    /// (located in "this namespace"/xml). All objects that represent the actions file (actions.xml) exist in this namespace,
    /// namely Actions, Action, Dependencies, DependsOn and Test.
    /// 
    /// Test class - defines an actual test to be triggered.
    /// Action class - defines a collection of Test(s) to be triggered corresponding to a given "match" (text found in BDD statement)
    /// Actions clas - defines a collection of Action
    /// 
    /// 
    /// </summary>
    public class ActionFactory
    {
        private static void ExecuteTests(Action actionObj)
        {
            ExecuteTests(actionObj, new Hashtable());
        }

        private static void ExecuteTests(Action actionObj, Hashtable args)
        {
            // *** point to the tests ...
            Test[] tests = actionObj.Test;

            // *** check for dependencies. Execute these
            // *** tests first ...

            if (actionObj.Dependencies != null)
            {
                foreach (string dependentAction in actionObj.Dependencies.DependsOn)
                {
                    Action da = GetActionByName(dependentAction);

                    if (da != null)
                    {
                        // *** execute this test (and its dependencies too) before
                        // *** we execute the main test ...

                        ExecuteTests(da);
                    }
                }
            }

            if (tests != null)
            {
                // *** execute all tests for this
                // *** action ...

                Log.Debug("[ACTION-FACTORY] ACTION: '" + actionObj.Name + "'");
                //Log.Debug("[ACTION-FACTORY]         '" + actionObj.Description!=null? actionObj.Description:"Unknown Action (no description)" + "'");

                int tIndex = 0;

                foreach (Test test in tests)
                {
                    string testName = "Test" + test.Name;

                    // *** attempt to locate the class
                    // *** for the test ...

                    Type[] typeArr =
                    GetTestExecutionAssembly().GetTypes().
                        Where(t => t.Name.Split(new char[] { '.' })[t.Name.Split(new char[] { '.' }).Length-1].
                        Equals(testName)).ToArray();

                    if(typeArr.Length==0)
                    {
                        Log.ErrAndFail("[ERR] Failed to locate test class " + testName);
                    }

                    Type testType = typeArr[0];

                    TestActionStep testStep = null;

                    try
                    {
                        testStep =
                            Activator.CreateInstance(testType) as TestActionStep;
                    }
                    catch(Exception e)
                    {
                        Log.ErrAndFail("[ERR] Failed to create instance of test class '" + testName + 
                            "'. The class either does not extend TestActionStep or does not have a default constructor. Exception '"+e.Message+"'");
                    }

                    if (testStep == null)
                    {
                        Log.ErrAndFail("[ERR] 'Created instance' of test class '" + testName+"' but value is null");
                    }

                    MethodInfo[] testExecutionMethods = 
                        testType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    if (testExecutionMethods == null)
                    {
                        Log.ErrAndFail("[ERR] Failed to find method 'RunTest' in test class " + testName);
                    }

                    // *** get method to execute ...

                    MethodInfo testExecutionMethod =
                        testExecutionMethods.Where(
                            item => item.Name.Equals(actionObj.Method)).ToArray().Length > 0 ?
                        testExecutionMethods.Where(
                            item => item.Name.Equals(actionObj.Method)).ToArray()[0] :
                            null;

                    if (testExecutionMethod == null)
                    {
                        Log.ErrAndFail("[ERR] Method '" + actionObj.Method + "' not found in test class '" +
                            testName + "'");
                    }

                    int maxtries = 3;

                    Log.Debug("[ACTION-FACTORY] EXECUTING TEST '" + test.Name + "'");

                    // *** attempt to run this test maxtries times. 
                    // *** Fail if exceeded ...

                    for (int i = 0; i < maxtries; i++)
                    {
                        try
                        {
                            foreach(DictionaryEntry entry in args)
                            {
                                Match testNameMatch = 
                                    Regex.Match(entry.Key.ToString().Trim(), 
                                    @"^Test" + test.Name + @"\.([^\.]*)");

                                Match indexOfTestMatch =
                                    Regex.Match(entry.Key.ToString().Trim(),
                                    @"^([0-9]*)\.([0-9]*)");

                                // *** if the test name is specified then
                                // *** set/override the parameter of this test ...

                                if ( testNameMatch.Success )
                                {
                                    int paramIndex = int.Parse(testNameMatch.Groups[1].Value);

                                    if( (test.Parameter==null) || (paramIndex>=test.Parameter.Length) )
                                    {
                                        throw new Exception("[ERR] Class '" + test.Name + 
                                            "' does not have a parameter index of " + paramIndex + ". Max parameters = " + 
                                            test.Parameter.Length);
                                    }

                                    // *** replace the existing parameter value in XML
                                    // *** with the one specified in code ...

                                    test.Parameter[paramIndex] = entry.Value.ToString();
                                }
                                else
                                if( indexOfTestMatch.Success )
                                {
                                    int testIndex = int.Parse(indexOfTestMatch.Groups[1].Value);
                                    int paramIndex = int.Parse(indexOfTestMatch.Groups[2].Value);

                                    if(testIndex==tIndex)
                                    {
                                        if ((test.Parameter == null) || (paramIndex >= test.Parameter.Length))
                                        {
                                            throw new Exception("[ERR] Class '" + test.Name +
                                                "' does not have a parameter index of " + paramIndex + ". Max parameters = " +
                                                test.Parameter.Length);
                                        }

                                        // *** replace the existing parameter value in XML
                                        // *** with the one specified in code ...

                                        test.Parameter[paramIndex] = entry.Value.ToString();
                                    }
                                }
                            }

                            // *** execute test method ...
                            testExecutionMethod.Invoke(testStep, test.Parameter);

                            break;
                        }
                        catch(Exception e)
                        {
                            Log.Debug("[ACTION-FACTORY] ERROR  '" + test.Name + "'. (Exception=["+e.Message+"])");

                            if ( (i+1) == maxtries )
                            {
                                throw e;
                            }
                            else
                            {
                                Log.Debug("[ACTION-FACTORY] RETRYING TEST '" + test.Name + "' ...");
                            }
                        }
                    }

                    Log.Debug("[ACTION-FACTORY] EXECUTED TEST  '" + test.Name + "' (successfully)");

                    ++tIndex;
                }

                Log.Debug("[ACTION-FACTORY] ACTION: '" + actionObj.Name + "' EXECUTED SUCCESSFULLY");
            }
        }

        public static Action ExecuteActionUsingMatch(string match)
        {
            string actionsLocation = new StackTrace().GetFrame(1).
                GetMethod().DeclaringType.Namespace;

            actionsLocation = Regex.Match(actionsLocation,
                @"^([^\.]*\.[^\.]*).*$").Groups[1].Value + @".actions.actions.xml";

            return ExecuteActionUsingMatch(match, new Hashtable(), actionsLocation);
        }

        /// <summary>
        /// Executes an action specified by the given
        /// human reable action (given as a parameter) ...
        /// </summary>
        /// <param name="match"></param>
        public static Action ExecuteActionUsingMatch(string match, Hashtable args, string actionsFile)
        {
            Action actionObj = null;

            // *** get the object for this action ...
            if( (actionObj=GetActionByMatch(match, actionsFile)) != null )
            {
                // *** if it exists then
                // *** execute the action ...

                ExecuteTests(actionObj, args);
            }
            else
            {
                Log.ErrAndFail("Failed to locate action containing match '" + match + "'");
            }

            return actionObj;
        }

        /// <summary>
        /// Get test steps for human readable action.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Test[] GetTestStepsForAction(string action, string actionsFile)
        {
            Action actionObj = GetActionByMatch(action, actionsFile);

            Test[] test = null;

            if(actionObj!=null)
            {
                test = actionObj.Test;
            }

            return test;
        }

        private static Assembly testExecutionAssembly = null;

        private static Assembly GetTestExecutionAssembly()
        {
            if (testExecutionAssembly != null)
            {
                return testExecutionAssembly;
            }
            else
            {
                Assembly fa = typeof(ActionFactory).Assembly;
                
                for (int i = 0; i < 10; i++)
                {
                    StackFrame sf = new StackTrace().GetFrame(i);

                    MethodBase mb = sf.GetMethod();

                    if ((mb != null) && !mb.DeclaringType.Assembly.FullName.Equals(fa.FullName))
                    {
                        testExecutionAssembly = mb.DeclaringType.Assembly;
                        break;
                    }
                }
            }

            return testExecutionAssembly;
        }

        /// <summary>
        /// Get action for given 'human readable' match.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Action GetActionByMatch(string match, string actionsFile)
        {
            using (Stream stream = GenericUtils.GetResourceStream(actionsFile))
            {
                using (StreamReader r = new StreamReader(stream))
                {
                    Action[] aarr = 
                        (new XmlSerializer(typeof(Actions)).Deserialize(r) as Actions)
                        .Action.Where(
                            item =>
                                item.Match.Where(
                                    m => m.Equals(match)
                                    ).ToArray().Length > 0
                                ).ToArray();

                    if(aarr.Length==0)
                    {
                        throw new Exception("[ERR] Action matching '" + match + "' not found in resource " + actionsFile);
                    }

                    return aarr[0];
                }
            }
        }

        /// <summary>
        /// Get action for given 'human readable' match.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Action GetActionByName(string action)
        {
            string actionsFile = AppSettings.Get("action.factory");

            using (Stream stream =
                GetTestExecutionAssembly().GetManifestResourceStream(
                    actionsFile))
            {
                using (StreamReader r = new StreamReader(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Actions));

                    return (serializer.Deserialize(r) as Actions)
                        .Action.Where(
                            item =>
                                item.Name.Equals(action)).ToArray()[0];
                }
            }
        }
    }
}
