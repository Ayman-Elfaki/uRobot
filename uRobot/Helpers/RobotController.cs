using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using System.Threading;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.UI.Popups;

namespace uRobot.Helpers
{
    public class RobotController
    {
        private StreamSocket _socket;

        private RfcommDeviceService _service;

        private DataWriter _dataWriter;

        public async Task<uint> SendAsync(string msg)
        {
            try
            {

                _dataWriter.WriteString(msg);
                
                // Launch an async task to 
                //complete the write operation
                var store = _dataWriter.StoreAsync().AsTask();

                return await store;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
                return 0;
            }
        }

        public async Task ConnectAsync(string deviceName)
        {
            try
            {
                var devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));

                var device = devices.Single(x => x.Name == deviceName);

                _service = await RfcommDeviceService.FromIdAsync(device.Id);

                _socket = new StreamSocket();

                await _socket.ConnectAsync(_service.ConnectionHostName, _service.ConnectionServiceName, SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication);

                _dataWriter = new DataWriter(_socket.OutputStream);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }


        public async Task DisconnectAsync()
        {
            try
            {
                await _socket.CancelIOAsync();
                _socket.Dispose();
                _socket = null;
                _service.Dispose();
                _service = null;
                _dataWriter.Dispose();
                _dataWriter = null;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

    }
}