using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using System.Threading;


namespace HelloWorld
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ClickMe_Click(object sender, RoutedEventArgs e)
        {
            this.HelloMessage.Text = "Hello, Windows IoT Core!";
        }



        private void OnOff_Click(object sender, RoutedEventArgs e)
        {
            // Get the default GPIO controller on the system
            GpioController gpio = GpioController.GetDefault();
            if (gpio == null)
                return; // GPIO not available on this system

            // Open GPIO 5
            using (GpioPin pin = gpio.OpenPin(5))
            {
                // Latch HIGH value first. This ensures a default value when the pin is set as output
                pin.Write(GpioPinValue.High);

                // Set the IO direction as output
                pin.SetDriveMode(GpioPinDriveMode.Output);


                for (int i = 0; i < 10; i++)
                {
                    pin.Write(GpioPinValue.High);
                    System.Threading.Tasks.Task.Delay(500).Wait();
                    pin.Write(GpioPinValue.Low);
                    System.Threading.Tasks.Task.Delay(500).Wait(); 
                }

            } // Close pin - will revert to its power-on state

        }
    }
}
