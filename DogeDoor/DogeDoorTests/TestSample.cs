using System;
using NUnit.Framework;
using DogeDoor;
using System.Linq;
using System.Collections.Generic;
using Android.Bluetooth;
using Java.Lang;

namespace DogeDoorTests
{
   [TestFixture]
   public class TestsSample
   {

      [SetUp]
      public void Setup() { }


      [TearDown]
      public void Tear() { }

      [Test]
      public void CreateBluetoothWhenTurnedOn()
      {
         Bluetooth bt = new Bluetooth();
         Assert.IsNotNull(bt);
      }

      [Test]
      public void CreateBluetothWhenTurnedOff()
      {
         //To have this test pass, bluetooth must be turned off.
         Bluetooth bt = new Bluetooth();
         List<string> strings = bt.PairedDeviceNames;
         if (!bt.AdapterEnabled)
            Assert.IsTrue(strings.Count <= 0);
         else
            Assert.IsTrue(true, "Adapter is turned on");
      }

      [Test]
      public void PopulatePairedDeviceList()
      {
         Bluetooth bt = new Bluetooth();
         if (bt.AdapterEnabled)
         {
            List<string> pairedDevices = bt.PairedDeviceNames;
            Assert.IsNotNull(pairedDevices);
         }
         else
            Assert.IsTrue(true, "Adapter not turned on");
      }

      [Test]
      public void ConnectToDoorWhenPaired()
      {
         //Device must be turned on and already paired to do 
         //this test and device name must be "HC-05".
         Bluetooth bt = new Bluetooth();
         BluetoothDevice d = null;
         if (bt.AdapterEnabled)
         {
            List<BluetoothDevice> devices = bt.PairedDevices;
            foreach (BluetoothDevice device in devices)
            {
               if (device.Name.Equals("HC-05"))
               {
                  d = device;
                  break;
               }
            }
            Assert.IsNotNull(d);
         }
         else
            Assert.IsTrue(true, "Adapter not enabled");
      }

      [Test]
      public void ConnectToDoorWhenNotPaired()
      {
         //No device name can be "HC-06"
         Bluetooth bt = new Bluetooth();
         BluetoothDevice d = null;
         if (bt.AdapterEnabled)
         {
            List<BluetoothDevice> devices = bt.PairedDevices;
            foreach (BluetoothDevice device in devices)
            {
               if (device.Name.Equals("HC-06"))
               {
                  d = device;
                  break;
               }
            }
            Assert.IsNull(d);
         }
         else
            Assert.IsTrue(true, "Adapter not enabled");
      }

      [Test]
      public void SendCommandDevicePaired()
      {
         Bluetooth bt = new Bluetooth();
         if (bt.AdapterEnabled)
         {
            List<BluetoothDevice> devices = bt.PairedDevices;
            foreach (BluetoothDevice device in devices)
            {
               if (device.Name.Equals("HC-05"))
               {
                  bt.SelectedDevice = device;
                  break;
               }
            }
            bool open = bt.OpenSocket();
            bool sent = bt.SendCommand("O");
            Thread.Sleep(500);
            bt.CloseSocket();
            Assert.IsTrue(sent);
         }
         else
            Assert.IsTrue(true, "Adapter not enabled");
      }

      [Test]
      public void OpenSocketTestDevicePaired()
      {
         Bluetooth bt = new Bluetooth();
         if (bt.AdapterEnabled)
         {
            List<BluetoothDevice> devices = bt.PairedDevices;
            foreach (BluetoothDevice device in devices)
            {
               if (device.Name.Equals("HC-05"))
               {
                  bt.SelectedDevice = device;
                  break;
               }
            }
            bool open = bt.OpenSocket();
            Thread.Sleep(500);
            bt.CloseSocket();
            Assert.IsTrue(open);
         }
         else
            Assert.IsTrue(true, "Adapter not enabled");
      }
   }
}