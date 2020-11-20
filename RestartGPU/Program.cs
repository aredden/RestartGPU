namespace RestartGPU {

    using HardwareHelperLib;
    using System;
    using System.Threading;

    internal class Program {

        private static bool cliOnly = false;
        private static HH_Lib hhlib = new HH_Lib();

        private static void Main(string[] args) {
            if (args.Length > 0) {
                if(args[0] == "cli") {
                    cliOnly = true;
                }
            }
            var devices = hhlib.GetAll();
            DEVICE_INFO graphics = new DEVICE_INFO();
            var found = false;
            foreach (var info in devices) {
                if (info.name.ToLower().Contains("nvidia geforce")) {
                    found = true;
                    graphics = info;
                }
            }
            if (found) {
                if (!cliOnly) {
                    Console.WriteLine($"Found GPU: {graphics.name}\nResetting device...");
                }
                ResetGPU(graphics);
            }
            else {
                if (!cliOnly) {
                    Console.WriteLine("Could not find graphics.");
                    Thread.Sleep(3000);
                } else {
                    Console.WriteLine(0);
                }
            }
        }
        private static void ResetGPU(DEVICE_INFO device) {
            DeviceStatus current = GetStatus(device);
            if (current==DeviceStatus.Enabled) {
                hhlib.SetDeviceState(device, false);
                if (!cliOnly) {
                    PrintStatus(device);
                }
            } else if (current == DeviceStatus.Disabled) {
                if (!cliOnly) {
                    Console.WriteLine("Device currently disabled.\nWill attempt to enable...");
                }
            } else if (current == DeviceStatus.Unknown) {
                if (!cliOnly) {
                    Console.WriteLine("Device currently in unknown state.\nWill attempt to enable...");
                }
            }
            Thread.Sleep(1000);
            hhlib.SetDeviceState(device, true);
            PrintStatus(device);
            if (!cliOnly && GetStatus(device)==DeviceStatus.Enabled) {
                Console.WriteLine($"{device.name} Successfully reset.");
                Thread.Sleep(3000);
            } else if (!cliOnly && GetStatus(device) == DeviceStatus.Disabled) {
                Console.WriteLine($"{device.name} not successfully reenabled / reset (look above for more info).");
                Thread.Sleep(3000);
            } else if (!cliOnly && GetStatus(device) == DeviceStatus.Unknown) {
                Console.WriteLine($"{device.name} is in an unknown state. There seems to be an issue.");
                Thread.Sleep(3000);
            }
            else {
                Console.WriteLine(1);
            }
        }

        private static DeviceStatus GetStatus(DEVICE_INFO device) {
            DeviceStatus status = new DeviceStatus();
            var devices = hhlib.GetAll();
            foreach (var ice in devices) {
                if(device.name == ice.name) {
                    status = ice.status;
                }
            }
            return status;
        }

        private static void PrintStatus(DEVICE_INFO device) {
            if (!cliOnly) {
                Console.WriteLine($"Current state {GetStatus(device)}");
            }
        }
    }
}