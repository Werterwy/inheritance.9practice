using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inheritance._9practice
{

    abstract class Storage
    {
        protected string name;
        protected string model;

        public Storage(string name, string model)
        {
            this.name = name;
            this.model = model;
        }

        public abstract double GetMemory();
        public abstract void CopyData(double dataSize);
        public abstract double GetFreeSpace();
        public abstract void PrintDeviceInfo();
    }

    class Flash : Storage
    {
        private double usbSpeed;
        private double memorySize;

        public Flash(string name, string model, double usbSpeed, double memorySize)
            : base(name, model)
        {
            this.usbSpeed = usbSpeed;
            this.memorySize = memorySize;
        }

        public override double GetMemory()
        {
            return memorySize;
        }

        public override void CopyData(double dataSize)
        {
            double time = dataSize / usbSpeed;
            Console.WriteLine($"Копирование данных во Flash. Скорость: {usbSpeed} MB/s, Время: {time} seconds");
            dataSize /= 1024;
            memorySize -= dataSize;
        }

        public override double GetFreeSpace()
        {
            return memorySize;
        }

        public override void PrintDeviceInfo()
        {
            Console.WriteLine($"Flash накопитель: {name}, Модель: {model}, USB скорость: {usbSpeed} MB/s, Память: {memorySize} GB");
        }
    }

    class DVD : Storage
    {
        private double readWriteSpeed;
        private bool isDoubleLayer;

        public DVD(string name, string model, double readWriteSpeed, bool isDoubleLayer)
            : base(name, model)
        {
            this.readWriteSpeed = readWriteSpeed;
            this.isDoubleLayer = isDoubleLayer;
        }

        public override double GetMemory()
        {
            return isDoubleLayer ? 9 : 4.7;
        }

        public override void CopyData(double dataSize)
        {
            double time = dataSize / readWriteSpeed;
            Console.WriteLine($"Копирование данных на DVD. Скорость: {readWriteSpeed} MB/s, Время: {time} seconds");
        }

        public override double GetFreeSpace()
        {
            return isDoubleLayer ? 9 : 4.7;
        }

        public override void PrintDeviceInfo()
        {
            Console.WriteLine($"DVD: {name}, Модель: {model}, скорость чтения / записи: {readWriteSpeed} MB/s, Тип: {(isDoubleLayer ? "двусторонний" : "односторонний")}");
        }
    }

    class HDD : Storage
    {
        private double usbSpeed;
        private int partitions;
        private double partitionSize;

        public HDD(string name, string model, double usbSpeed, int partitions, double partitionSize)
            : base(name, model)
        {
            this.usbSpeed = usbSpeed;
            this.partitions = partitions;
            this.partitionSize = partitionSize;
        }

        public override double GetMemory()
        {
            return partitions * partitionSize;
        }

        public override void CopyData(double dataSize)
        {
            double time = dataSize / usbSpeed;
            Console.WriteLine($"Копирование данных на HDD. Скорость: {usbSpeed} MB/s, Время: {time} seconds");
            dataSize /= 1024;
            partitionSize -= dataSize / partitions;
        }

        public override double GetFreeSpace()
        {
            return partitions * partitionSize;
        }

        public override void PrintDeviceInfo()
        {
            Console.WriteLine($"HDD: {name}, Модель: {model}, USB Скорость: {usbSpeed} MB/s, Количество разделов: {partitions}, объем разделов: {partitionSize} GB");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage[] devices = new Storage[]
            {
                new Flash("FlashDrive1", "Model1", 100, 64),
                new DVD("DVD1", "Model2", 20, false),
                new HDD("ExternalHDD1", "Model3", 50, 2, 500),
            };

            double totalMemory = 0;

            foreach (var device in devices)
            {
                totalMemory += device.GetMemory();
                device.PrintDeviceInfo();
            }

            Console.WriteLine("");
            devices[0].CopyData(780);
            devices[1].CopyData(780);
            devices[2].CopyData(780);

            Console.WriteLine("");

            Console.WriteLine("Свободная память FlashDrive1, ExternalHDD1, DVD1: ");
            Console.WriteLine(devices[0].GetFreeSpace());
            Console.WriteLine(devices[2].GetFreeSpace());
            Console.WriteLine(devices[1].GetMemory());
            
            Console.WriteLine($"\nОбщая память всех устройств: {totalMemory} GB");


        }
    }
}
