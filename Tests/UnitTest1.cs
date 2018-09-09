using System;
using libTask1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TextFileAppendGetContentTest()
        {
            TextFile file = new TextFile("testFile");
            var content = "teststring";
            file.Append(content);

            Assert.AreEqual("teststring", file.GetContent());
            file.Delete();
        }

        [TestMethod]
        public void TextFileCreateDirectoryCountTest()
        {
            TextFile file = new TextFile("testFile");
            var content = "teststring";
            file.Append(content);

            Assert.AreEqual(1, FileSystem.Directories.Count);
            file.Delete();
        }

        [TestMethod]
        public void TextFileCreateFilesCountTest()
        {
            TextFile file = new TextFile("testFile");
            var content = "teststring";
            file.Append(content);

            Assert.AreEqual(1, FileSystem.Directories[0].Files.Count);
            file.Delete();
        }

        [TestMethod]
        public void TextFileRenameTest()
        {
            TextFile file = new TextFile("testFile");
            var content = "teststring";
            file.Append(content);
            file.Rename("newFile");

            Assert.AreEqual("newFile", file.Name);
            file.Delete();
        }
    }
}
