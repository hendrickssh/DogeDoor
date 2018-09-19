using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Bluetooth;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DogeDoor
{
   [Activity(Label = "DoorControl")]
   public class DoorControl : Activity
   {
      private Bluetooth _bluetooth = new Bluetooth();

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.DoorControl);
         TextView deviceAvailable = FindViewById<TextView>(Resource.Id.txtDeviceAvailable);
         Button openDoor = FindViewById<Button>(Resource.Id.btnOpenDoor);
         Button closeDoor = FindViewById<Button>(Resource.Id.btnCloseDoor);
         Button lockDoor = FindViewById<Button>(Resource.Id.btnLockDoor);

         string deviceAddress = Intent.GetStringExtra("Bluetooth") ?? "0";
         deviceAvailable.Text = "Device Status: Connecting";

         if (ConnectToDevice(deviceAddress))
         {
            if (_bluetooth.OpenSocket())
               deviceAvailable.Text = "Device Status: Connected";
            else
               deviceAvailable.Text = "Device Status: Unavailable";
         }

         openDoor.Click += delegate
         {
            _bluetooth.SendCommand("O");
         };

         closeDoor.Click += delegate
         {
            _bluetooth.SendCommand("C");
         };

         lockDoor.Click += delegate
         {
            _bluetooth.SendCommand("L");
         };
      }

      public override void OnBackPressed()
      {
         _bluetooth.CloseSocket();
         base.OnBackPressed();
      }

      private bool ConnectToDevice(string deviceAddress)
      {
         foreach(BluetoothDevice device in _bluetooth.PairedDevices)
         {
            if(device.Address.Equals(deviceAddress))
            {
               _bluetooth.SelectedDevice = device;
               return true;
            }
         }
         return false;
      }
   }
}