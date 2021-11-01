using Stark.NumberGenerator.Library.Banks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stark.NumberGenerator.Library;

namespace Stark.NumberGenerator.Tests
{
  [TestClass]
  public class NumberGenerationTest
  {
    [TestMethod]
    public void TestFnbNumber()
    {
      FNB fnb = new FNB();
      var randomAccountNumber = fnb.GenerateAccountNumber();
      Assert.IsNotNull(randomAccountNumber);
    }

    [TestMethod]
    public void TestAbsaNumber()
    {
      ABSA absa = new ABSA();
      var randomAccountNumber = absa.GenerateAccountNumber();
      Assert.IsNotNull(randomAccountNumber);
    }

    [TestMethod]
    public void TestCapitecNumber()
    {
      Capitec capitec = new Capitec();
      var randomAccountNumber = capitec.GenerateAccountNumber();
      Assert.IsNotNull(randomAccountNumber);
    }

    [TestMethod]
    public void TestStandardBankNumber()
    {
      StandardBank standardBank = new StandardBank();
      var randomAccountNumber = standardBank.GenerateAccountNumber();
      Assert.IsNotNull(randomAccountNumber);
    }

    [TestMethod]
    public void TestNedbankNumber()
    {
      Nedbank nedbank = new Nedbank();
      var randomAccountNumber = nedbank.GenerateAccountNumber();
      Assert.IsNotNull(randomAccountNumber);
    }

    [TestMethod]
    public void TestIDNumberGeneration()
    {
      IDNumberModel idNumber = IDNumberModel.GenerateIDNumber();
      Assert.IsNotNull(idNumber);
    }
  }
}
