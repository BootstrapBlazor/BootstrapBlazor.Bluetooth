let device, myCharacteristic;

export function notification(wrapper, element, callfunction = null, serviceUuid = 'heart_rate', characteristicUuid = 'heart_rate_measurement', autoConnect=false) {
    if (!element.id) element = document;
    const log = element.querySelector("[data-action=log]");
    const notificationValue = document.querySelector("[data-action=notificationValue]");

    if (callfunction && callfunction == "stopNotification") {
        console.log('stopNotification');
        stopNotification();
        return;
    }

    getNotification();

    async function getNotification() {
        if (notificationValue) notificationValue.innerHTML = '--';
        if (characteristicUuid.startsWith('0x')) {
            characteristicUuid = parseInt(characteristicUuid);
        }
        try {
            let devices = await navigator.bluetooth.getDevices();
            if (devices.length > 0) {
                if (autoConnect) {
                    device = devices[0];
                    console.log("自动连接设备", device);
                    logII(`自动连接设备 ${device.name}`);
                }
                logII(`获取到授权设备: ${devices.length}`);
            }
        } catch (error) {
            logErr('Argh! ' + error);
        }

        try {
            if (!device) {
                logII(`Requesting Bluetooth Device... ${serviceUuid}`);
                try {

                    device = await navigator.bluetooth.requestDevice({
                        filters: [{ services: [serviceUuid] }]
                    });
                } catch (error) {
                    device = null;
                    logErr('Argh! ' + error);
                    return;
                }
            }

            //device.addEventListener('advertisementreceived', (event) => {
            //    logII('Advertisement received.');
            //    logII('  Device Name: ' + event.device.name);
            //    logII('  Device ID: ' + event.device.id);
            //    logII('  RSSI: ' + event.rssi);
            //    logII('  TX Power: ' + event.txPower);
            //    logII('  UUIDs: ' + event.uuids);
            //    event.manufacturerData.forEach((valueDataView, key) => {
            //        logDataView('Manufacturer', key, valueDataView);
            //    });
            //    event.serviceData.forEach((valueDataView, key) => {
            //        logDataView('Service', key, valueDataView);
            //    });
            //});

            //logII('Watching advertisements from "' + device.name + '"...');
            //await device.watchAdvertisements(); 

            wrapper.invokeMethodAsync('UpdateDevicename', device.name);

            logII(`${device.name}`);
            logII('Connecting to GATT Server...');
            const server = await device.gatt.connect();

            wrapper.invokeMethodAsync('UpdateStatus', 'Getting Service...');
            logII('Getting Service...');
            const service = await server.getPrimaryService(serviceUuid);

            logII('Getting Characteristic...');
            myCharacteristic = await service.getCharacteristic(characteristicUuid);

            await myCharacteristic.startNotifications();

            logII('> Notifications started');
            myCharacteristic.addEventListener('characteristicvaluechanged',
                handleNotifications);
        } catch (error) {
            logErr('Argh! ' + error);
        }
    }

    async function stopNotification() {
        device = null;
        if (myCharacteristic) {
            try {
                await myCharacteristic.stopNotifications();
                logII('> Notifications stopped');
                myCharacteristic.removeEventListener('characteristicvaluechanged',
                    handleNotifications);
            } catch (error) {
                logErr('Argh! ' + error);
            }
        }
    }

    function handleNotifications(event) {
        let value = event.target.value;
        let a = [];
        // Convert raw data bytes to hex values just for the sake of showing something.
        // In the "real" world, you'd use data.getUint8, data.getUint16 or even
        // TextDecoder to process raw data bytes.
        for (let i = 0; i < value.byteLength; i++) {
            a.push('0x' + ('00' + value.getUint8(i).toString(16)).slice(-2));
        }
        let result = a.join(' ');
        wrapper.invokeMethodAsync('UpdateValue', result);
        logII('> ' + result);
        if (result) notificationValue.innerHTML = result;
    }

    function logII(info) {
        if (log) log.textContent += info + '\n';
        console.log(info);
        wrapper.invokeMethodAsync('UpdateStatus', info);
    }

    function logErr(info) {
        if (log) log.textContent += info + '\n';
        console.log(info);
        wrapper.invokeMethodAsync('UpdateError', info);
    }

}

/* Utils */

const logDataView = (labelOfDataSource, key, valueDataView) => {
    const hexString = [...new Uint8Array(valueDataView.buffer)].map(b => {
        return b.toString(16).padStart(2, '0');
    }).join(' ');
    const textDecoder = new TextDecoder('ascii');
    const asciiString = textDecoder.decode(valueDataView.buffer);
    console.log(`  ${labelOfDataSource} Data: ` + key +
        '\n    (Hex) ' + hexString +
        '\n    (ASCII) ' + asciiString);
};