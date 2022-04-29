﻿mergeInto(LibraryManager.library, {
    EnableEthereum: async function (gameObjectName, callback, fallback) {
        const parsedObjectName = UTF8ToString(gameObjectName);
        const parsedCallback = UTF8ToString(callback);
        const parsedFallback = UTF8ToString(fallback);

        try {

            const accounts = await ethereum.request({ method: 'eth_requestAccounts' });
            ethereum.autoRefreshOnNetworkChange = false;
            console.log("account[0]", accounts[0]);
            var bufferSize = lengthBytesUTF8(accounts[0]) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(accounts[0], buffer, bufferSize);
            SendMessage(parsedObjectName, parsedCallback, accounts[0]);

            return buffer;
        } catch (error) {
            SendMessage(parsedObjectName, parsedFallback, error.message);
            return null;
        }
    },
    EthereumInit: function (gameObjectName, callBackAccountChange, callBackChainChange) {
        const parsedObjectName = UTF8ToString(gameObjectName);
        const parsedCallbackAccountChange = UTF8ToString(callBackAccountChange);
        const parsedCallbackChainChange = UTF8ToString(callBackChainChange);
        console.log("EthereumInit");

        ethereum.on("accountsChanged",
            function (accounts) {
                console.log(accounts[0]);
                SendMessage(parsedObjectName, parsedCallbackAccountChange, accounts[0]);
            });
        ethereum.on("chainChanged",
            function (chainId) {
                console.log(chainId);
                SendMessage(parsedObjectName, parsedCallbackChainChange, chainId.toString());
            });
    },
    GetChainId: async function (gameObjectName, callback, fallback) {
        const parsedObjectName = UTF8ToString(gameObjectName);
        const parsedCallback = UTF8ToString(callback);
        const parsedFallback = UTF8ToString(fallback);
        try {

            const chainId = await ethereum.request({ method: 'eth_chainId' });
            SendMessage(parsedObjectName, parsedCallback, chainId.toString());

        } catch (error) {
            SendMessage(parsedObjectName, parsedFallback, error.message);
            return null;
        }
    },
    IsMetamaskAvailable: function () {
        if (window.ethereum) return true;
        return false;
    },
    GetSelectedAddress: function () {
        var returnValue = ethereum.selectedAddress;
        if (returnValue !== null) {
            var bufferSize = lengthBytesUTF8(returnValue) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(returnValue, buffer, bufferSize);
            return buffer;
        }
    },
    Request: async function (message, gameObjectName, callback, fallback) {
        const parsedMessageStr = UTF8ToString(message);
        const parsedObjectName = UTF8ToString(gameObjectName);
        const parsedCallback = UTF8ToString(callback);
        const parsedFallback = UTF8ToString(fallback);

        try {

            let parsedMessage = JSON.parse(parsedMessageStr);
            console.log(parsedMessage);
            const response = await ethereum.request(parsedMessage);
            let rpcResponse = {
                jsonrpc: "2.0",
                result: response,
                id: parsedMessage.id,
                error: null
            }
            console.log(rpcResponse);

            var json = JSON.stringify(rpcResponse);
            console.log(json);
            SendMessage(parsedObjectName, parsedCallback, json);
            return json;
        } catch (e) {
            let rpcResonseError = {
                jsonrpc: "2.0",
                id: parsedMessage.id,
                error: {
                    message: e,
                }
            }
            return JSON.stringify(rpcResonseError);
        }
    },

    Send: async function (message) {
        return new Promise(function (resolve, reject) {
            console.log(JSON.parse(message));
            ethereum.send(JSON.parse(message), function (error, result) {
                console.log(result);
                console.log(error);
                resolve(JSON.stringify(result));
            });
        });
    },

    Sign: async function (utf8HexMsg) {
        return new Promise(function (resolve, reject) {
            const from = ethereum.selectedAddress;
            const params = [utf8HexMsg, from];
            const method = 'personal_sign';
            ethereum.send({
                method,
                params,
                from,
            }, function (error, result) {
                if (error) {
                    reject(error);
                } else {
                    console.log(result.result);
                    resolve(JSON.stringify(result.result));
                }

            });
        });
    },
    GetBalance: async function (address, id, gameObjectName, callback, fallback) {
        address = UTF8ToString(address);
        const parsedObjectName = UTF8ToString(gameObjectName);

        const parsedCallback = UTF8ToString(callback);
        const parsedFallback = UTF8ToString(fallback);

        try {
            let balance = await ethereum.request({ "jsonrpc": "2.0", "method": "eth_getBalance", "params": [address], "id": id });

            balance = parseInt(balance, 16);
            console.log("balance", balance);
            balance = balance.toString();
            var bufferSize = lengthBytesUTF8(balance) + 1;
            var buffer = _malloc(bufferSize);
            stringToUTF8(balance, buffer, bufferSize);
            SendMessage(parsedObjectName, parsedCallback, balance);
            return buffer;
        } catch (error) {
            SendMessage(parsedObjectName, parsedFallback, error.message);
            return null;
        }



    }



});