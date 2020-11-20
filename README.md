# RestartGPU

A simple utility for restarting an Nvidia GPU using HardwareHelperLib to enable and disable the device.

Some have found that this can help with the odd power drain issues with their Asus Zephyrus G14.

### Instructions for a more comprehensive fix:

To have dGPU Reset app run on Sleep/Hibernate resume:

1. Win+R: taskschd.msc
2. Create Task...
3. Check Run with highest privileges
4. Select Configure for Windows 10
5. a Triggers tab > New...
   b Select On an Event
   c Select Custom > New Event Filter
   d Select By Source, check Power-Troubleshooter
   e Where it says <All Event IDs>, type the number 1, click "Ok"
6. a Actions tab > New...
   b Browse select GPUReset.exe
7. Conditions tab, make sure everything is unchecked
8. a Settings tab
   b Stop the task if it runs longer than > 1 hour

Click "Ok", test by putting the laptop to Sleep and/or Hibernate.

When it wakes, you should see the Command Prompt window notifying you of the dGPU being reset

_Be aware that this will kick off all apps using the nvidia gpu, including games._
