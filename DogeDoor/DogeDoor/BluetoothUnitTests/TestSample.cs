using System;
using NUnit.Framework;
using DogeDoor;


namespace BluetoothUnitTests
{
   [TestFixture]
   public class TestsSample
   {

      [SetUp]
      public void Setup()
      {
         Bluetooth test = new Bluetooth();
         Assert.IsNotNull(test);
      }


      [TearDown]
      public void Tear() { }

      [Test]
      public void Pass()
      {
         Console.WriteLine("test1");
         Assert.True(true);
      }

      [Test]
      public void Fail()
      {
         Assert.False(true);
      }

      [Test]
      [Ignore("another time")]
      public void Ignore()
      {
         Assert.True(false);
      }

      [Test]
      public void Inconclusive()
      {
         Assert.Inconclusive("Inconclusive");
      }
   }
}