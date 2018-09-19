using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using System.Linq;
using System.Collections.Generic;
using Android.Content;

namespace DogeDoor
{
   [Activity(Label = "DogeDoor", MainLauncher = true)]
   public class MainActivity : Activity
   {
      private RadioGroup _rbDevices;
      private Bluetooth _bluetooth; 
      private List<RadioButton> _rButtons = new List<RadioButton>();
      private string _deviceAddress;

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.Main);
         _bluetooth = new Bluetooth();
         //Set up view components
         _rbDevices = FindViewById<RadioGroup>(Resource.Id.listDevices);
         Button btnConnectDevice = FindViewById<Button>(Resource.Id.btnConnectDevice);

         _DisplayPairedDevices();

         //Button click events
         btnConnectDevice.Click += delegate
         {
            try
            {
               _SetChosenDevice();
               btnConnectDevice.Text = "Connecting to Device";
               var doorControl = new Intent(this, typeof(DoorControl));
               doorControl.PutExtra("Bluetooth", _deviceAddress);
               StartActivity(doorControl);
            }
            catch
            {

            }
         };
      }

      private void _DisplayPairedDevices()
      {
         RadioButton rb;
         foreach (string name in _bluetooth.PairedDeviceNames)
         {
            rb = new RadioButton(this);
            rb.Text = name;
            _rButtons.Add(rb);
            _rbDevices.AddView(rb);
         }
      }

      private void _SetChosenDevice()
      {
         int id = _rbDevices.CheckedRadioButtonId;
         foreach(BluetoothDevice device in _bluetooth.PairedDevices)
         {
            if(device.Name.Equals(_rButtons.ElementAt(id-1).Text))
            {
               _deviceAddress = device.Address;
            }
         }
      }
   }
}

