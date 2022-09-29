mergeInto(LibraryManager.library, {
    Clear: function (nameDB, pathDB) {
        nameDB = UTF8ToString(nameDB);
        pathDB = UTF8ToString(pathDB);
        console.log("Clear: " + nameDB + " " + pathDB);
        var db = window.indexedDB.open(nameDB);
        db.onsuccess = function (event) {
            db = event.target.result;
            console.log('db clear', db);
            var transaction = db.transaction(['FILE_DATA'], 'readwrite');
            var objectStore = transaction.objectStore('FILE_DATA');
            //get all keys from object store
            var getAllKeysRequest = objectStore.getAllKeys();
            getAllKeysRequest.onsuccess = function (event) {
                var keys = event.target.result;
                console.log('keys', keys);
                keys.forEach(function (key) {
                    if (key.includes(pathDB)) {
                        console.log('key', key);
                        objectStore.delete(key);
                    }

                })

            }

        }

    },
    GetKeyIndexedDB: function (gameObjectName, callback, fallback) {
        console.log("[JavaScript] GetKeyIndexedDB");
        const parsedObjectName = UTF8ToString(gameObjectName);
        const parsedCallback = UTF8ToString(callback);
        const parsedFallback = UTF8ToString(fallback);
        var GetKeys = new Promise(function (resolve, reject) {
                var db = window.indexedDB.open("/idbfs");
                db.onsuccess = function (event) {
                    db = event.target.result;
                    console.log('db clear', db);
                    var transaction = db.transaction(['FILE_DATA'], 'readwrite');
                    var objectStore = transaction.objectStore('FILE_DATA');
                    //get all keys from object store
                    var getAllKeysRequest = objectStore.getAllKeys();
                    getAllKeysRequest.onsuccess = function (event) {
                        var keys = event.target.result;
                        console.log('keys', keys);
                        resolve(keys);
                    }
                }

            });
        try {
            GetKeys.then(function (keys) {
                var keyString = keys.toString();
                var bufferSize = lengthBytesUTF8(keyString) + 1;
                var buffer = _malloc(bufferSize);
                stringToUTF8(keyString, buffer, bufferSize);
                SendMessage(parsedObjectName, parsedCallback, keyString);
                return buffer;
            })
        } catch (error) {
            SendMessage(parsedObjectName, parsedFallback, error.message);
            return null;
        }   
    },


});