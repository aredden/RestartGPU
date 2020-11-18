namespace RestartGPU {

    using HardwareHelperLib;
    using System;
    using System.Threading;

    internal class Program {

        private static void Main(string[] args) {
            HH_Lib lib = new HH_Lib();
            var devices = lib.GetAll();
            DEVICE_INFO graphics = new DEVICE_INFO();
            var found = false;
            foreach (var info in devices) {
                if (info.name.ToLower().Contains("nvidia geforce")) {
                    found = true;
                    graphics = info;
                }
            }
            if (found) {
                Console.WriteLine($"Found GPU: {graphics.name}\nResetting device...");
                ResetGPU(graphics);
            }
            else {
                Console.WriteLine("Could not find graphics.");
                Thread.Sleep(3000);
            }
        }
        private static void ResetGPU(DEVICE_INFO device) {
            HH_Lib libo = new HH_Lib();
            libo.SetDeviceState(device, false);
            Thread.Sleep(1000);
            libo.SetDeviceState(device, true);
            Console.WriteLine($"{device.name} Successfully reset.");
            Thread.Sleep(3000);
        }
    }
}