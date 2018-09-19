using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace DogeDoor
{
   public class Bluetooth
   {
      private BluetoothAdapter _bluetoothAdapter;
      private BluetoothDevice _bluetoothDevice;
      private BluetoothSocket _bluetoothSocket;
      private const string _DEVICE_UUID = "00001101-0000-1000-8000-00805F9B34FB";
      private List<string> _pairedDeviceNames = new List<string>();
      private List<BluetoothDevice> _pairedDevices = new List<BluetoothDevice>();

      
      public Bluetooth()
      {
         _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
         _pairedDevices = _bluetoothAdapter.BondedDevices.ToList();
         _GetDeviceNames();
      }

      public bool AdapterEnabled
      {
         get
         {
            return _bluetoothAdapter.IsEnabled;
         }
      }

      public List<string> PairedDeviceNames
      {
         get
         {
            return _pairedDeviceNames;
         }
      }

      public List<BluetoothDevice> PairedDevices
      {
         get
         {
            return _pairedDevices;
         }
      }

      public BluetoothDevice SelectedDevice
      {
         set
         {
            _bluetoothDevice = value;
         }
         get
         {
            return _bluetoothDevice;
         }
      }

      public bool OpenSocket()
      {
         try
         {
            _bluetoothSocket = _bluetoothDevice.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString(_DEVICE_UUID));
            _bluetoothSocket.Connect();
            return true;
         }
         catch
         {
            return false;
         }
      }

      public void CloseSocket()
      {
         try
         {
            _bluetoothSocket.Close();
            _bluetoothSocket = null;
            _bluetoothDevice = null;
         }
         catch
         {

         }
      }

      public bool SendCommand(string command)
      {
         byte[] buffer = Encoding.UTF8.GetBytes(command);
         try
         {
            _bluetoothSocket.OutputStream.Write(buffer, 0, buffer.Length);
            return true;
         }
         catch
         {
            return false;
         }
      }


      private void _GetDeviceNames()
      {
         foreach(BluetoothDevice device in _pairedDevices)
            _pairedDeviceNames.Add(device.Name);
      }
   }
}