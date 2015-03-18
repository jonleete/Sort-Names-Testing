using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.IO;
using Sort;

namespace UnitTest
{
    /// <summary>
    /// Summary description for UnitTest
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        public List<Person> personCollection = new List<Person>();
        public List<SortTest> sortTestCollection = new List<SortTest>();
        
        public class SortTest {
            List<Person> inputNames;
            List<Person> outputNamesOrdered;

            public SortTest(List<Person> inputNames, List<Person> outputNames) {
                this.inputNames = inputNames;
                this.outputNamesOrdered = outputNames;
            }

            public List<Person> getOutputNamesOrdered()
            {
                return outputNamesOrdered;
            }

            public List<Person> getInputNamesOrdered()
            {
                return inputNames;
            }
        }

        public UnitTest()
        {
            personCollection.Add(new Person("LEETE", "JONATHAN"));
            personCollection.Add(new Person("LEETE", "CHRISTOPHER"));
            personCollection.Add(new Person("LEETE", "MATTHEW"));
            personCollection.Add(new Person("SMITH", "WILLIAM"));
            personCollection.Add(new Person("BOND", "JAMES"));
            personCollection.Add(new Person("WILLIAMSON", "TAMIKA"));
            personCollection.Add(new Person("BUTLER", "JOE"));
            personCollection.Add(new Person("WALKIN", "CHRIS"));
            personCollection.Add(new Person("ROSE", "KATIE"));

            List<Person> inputNamesA = new List<Person>();
            inputNamesA.Add(personCollection[0]);
            inputNamesA.Add(personCollection[1]);
            inputNamesA.Add(personCollection[2]);
            inputNamesA.Add(personCollection[3]);

            List<Person> outputNamesA = new List<Person>();
            outputNamesA.Add(personCollection[1]);
            outputNamesA.Add(personCollection[0]);
            outputNamesA.Add(personCollection[2]);
            outputNamesA.Add(personCollection[3]);

            sortTestCollection.Add(new SortTest(inputNamesA, outputNamesA));

            List<Person> inputNamesB = new List<Person>();
            inputNamesB.Add(personCollection[4]);
            inputNamesB.Add(personCollection[5]);
            inputNamesB.Add(personCollection[6]);
            inputNamesB.Add(personCollection[7]);

            List<Person> outputNamesB = new List<Person>();
            outputNamesB.Add(personCollection[4]);
            outputNamesB.Add(personCollection[6]);
            outputNamesB.Add(personCollection[7]);
            outputNamesB.Add(personCollection[5]);

            sortTestCollection.Add(new SortTest(inputNamesB, outputNamesB));

            List<Person> inputNamesC = new List<Person>();
            inputNamesC.Add(personCollection[8]);
            inputNamesC.Add(personCollection[0]);
            inputNamesC.Add(personCollection[1]);
            inputNamesC.Add(personCollection[2]);

            List<Person> outputNamesC = new List<Person>();
            outputNamesC.Add(personCollection[1]);
            outputNamesC.Add(personCollection[0]);
            outputNamesC.Add(personCollection[2]);
            outputNamesC.Add(personCollection[8]);

            sortTestCollection.Add(new SortTest(inputNamesC, outputNamesC));
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        public void createTestFiles()
        {
            TextWriter writerA = new StreamWriter(Application.StartupPath + "\\writerFileA.txt");
            writerA.WriteLine(personCollection[0].ToString());
            writerA.WriteLine(personCollection[1].ToString());
            writerA.WriteLine(personCollection[2].ToString());
            writerA.WriteLine(personCollection[3].ToString());
            writerA.Close();

            TextWriter writerB = new StreamWriter(Application.StartupPath + "\\writerFileB.txt");
            writerB.WriteLine(personCollection[4].ToString());
            writerB.WriteLine(personCollection[5].ToString());
            writerB.WriteLine(personCollection[6].ToString());
            writerB.WriteLine(personCollection[7].ToString());
            writerB.Close();

            TextWriter writerC = new StreamWriter(Application.StartupPath + "\\writerFileC.txt");
            writerC.WriteLine(personCollection[8].ToString());
            writerC.WriteLine(personCollection[0].ToString());
            writerC.WriteLine(personCollection[1].ToString());
            writerC.WriteLine(personCollection[2].ToString());
            writerC.Close();
        }

        [TestMethod]
        public void TestReadFile()
        {
            createTestFiles();
            string[] filenameA = {Application.StartupPath + "\\writerFileA.txt"};
            string[] filenameB = {Application.StartupPath + "\\writerFileB.txt"};
            string[] filenameC = {Application.StartupPath + "\\writerFileC.txt"};

            string[] fakeFile = { Application.StartupPath + "\thisIsAFakeFile.txt" };

            if (Program.Main(filenameA) != (int)Program.ERROR_CODE.NoErrors) Assert.Fail("Cannot open existing file");
            if (Program.Main(filenameB) != (int)Program.ERROR_CODE.NoErrors) Assert.Fail("Cannot open existing file");
            if (Program.Main(filenameC) != (int)Program.ERROR_CODE.NoErrors) Assert.Fail("Cannot open existing file");

            if (Program.Main(fakeFile) != (int)Program.ERROR_CODE.CannotOpenFile) Assert.Fail("Fake file should return CannotOpenFile");
        }

        [TestMethod]
        public void TestNameSortA()
        {
            SortTest Test = sortTestCollection[0];
            List<Person> personListA = Test.getInputNamesOrdered();
            personListA.Sort(new PersonComparer());
            List<Person> expectedListA = Test.getOutputNamesOrdered();
            for (int testIndex = 0; testIndex < 4; testIndex++)
            {
                if (!personListA[testIndex].Equals(expectedListA[testIndex])) Assert.Fail("Name's don't match");
            }
        }

        [TestMethod]
        public void TestNameSortB()
        {
            SortTest Test = sortTestCollection[1];
            List<Person> personListB = Test.getInputNamesOrdered();
            personListB.Sort(new PersonComparer());
            List<Person> expectedListB = Test.getOutputNamesOrdered();
            for (int testIndex = 0; testIndex < 4; testIndex++)
            {
                if (!personListB[testIndex].Equals(expectedListB[testIndex])) Assert.Fail("Name's don't match");
            }
        }

        [TestMethod]
        public void TestNameSortC()
        {
            SortTest Test = sortTestCollection[2];
            List<Person> personListC = Test.getInputNamesOrdered();
            personListC.Sort(new PersonComparer());
            List<Person> expectedListC = Test.getOutputNamesOrdered();
            for (int testIndex = 0; testIndex < 4; testIndex++)
            {
                if (!personListC[testIndex].Equals(expectedListC[testIndex])) Assert.Fail("Name's don't match");
            }
        }


    }
}
